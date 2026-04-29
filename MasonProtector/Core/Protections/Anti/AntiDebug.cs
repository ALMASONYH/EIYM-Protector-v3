
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

using DnFieldAttributes = dnlib.DotNet.FieldAttributes;
using DnMethodAttributes = dnlib.DotNet.MethodAttributes;
using DnMethodImplAttributes = dnlib.DotNet.MethodImplAttributes;
using DnTypeAttributes = dnlib.DotNet.TypeAttributes;
using DnOpCodes = dnlib.DotNet.Emit.OpCodes;

namespace MasonProtector.Core
{
    internal class AntiDebugProtection
    {
        private Obfuscation engine;
        private Random rng;

        private static readonly string[] debuggerProcesses = new string[]
        {
            "dnspy", "x64dbg", "x32dbg", "ollydbg", "ida", "ida64",
            "idag", "idaq", "idaw", "windbg", "dotpeek", "ilspy",
            "de4dot", "megadumper", "extremedumper",
            "cheatengine", "ghidra", "pestudio", "exeinfope",
            "protection_id", "detect_it_easy", "cff explorer", "hiew",
            "resourcehacker", "apimonitor", "immunitydebugger", "scylla",
            "lordpe", "importrec", "petools", "dumppe", "systemexplorer",
        };

        private static readonly string[] fingerprintNames = new string[]
        {
            "dnspy", "ilspy", "de4dot", "x64dbg", "x32dbg", "x96dbg",
            "ollydbg", "ida pro", "ida disassembler", "hex-rays",
            "windbg", "dotpeek", "megadumper", "extreme dumper",
            "extremedumper", "cheat engine", "ghidra", "pestudio",
            "pe-bear", "exeinfo", "protection_id", "protection id",
            "detect it easy", "cff explorer", "hiew", "resource hacker",
            "api monitor", "immunity debugger", "scylla", "lordpe",
            "lord pe", "importrec", "petools", "pe tools",
            "process hacker", "process explorer", "system explorer",
            "reflexil", "simple assembly explorer", "fiddler",
            "wireshark", "httpdebugger", "frida", "vmunpack",
            "the wireshark", "ilspycmd", "dot net spy",
        };

        private static readonly string[] fingerprintCompanies = new string[]
        {
            "0xd4d", "ollydbg.de", "hex-rays", "jetbrains",
            "sysinternals", "immunity inc", "ghidra developers",
            "ilspy team", "de4dot", "the wireshark foundation",
            "telerik", "icsharpcode",
        };

        private static readonly string[] fingerprintWindowTitles = new string[]
        {
            "dnspy", "ilspy", "x64dbg", "x32dbg", "ollydbg", "ida pro",
            "windbg", "cheat engine", "process hacker", "process explorer",
            "ghidra", "scylla", "cff explorer", "wireshark", "fiddler",
            "pe-bear", "lord pe", "lordpe", "pe tools", "petools",
            "immunity debugger", "reflexil", "exeinfope", "pestudio",
            "detect it easy", "die ", "api monitor", "megadumper",
            "extreme dumper", "extremedumper",
        };

        internal AntiDebugProtection(Obfuscation eng)
        {
            engine = eng;
            rng = eng.rng;
        }

        internal void ApplyAntiDebug(ModuleDef module, TypeDef modType)
        {
            var antiType = new TypeDefUser("", engine.MakeName(),
                module.CorLibTypes.Object.TypeDefOrRef);
            antiType.Attributes = DnTypeAttributes.NotPublic | DnTypeAttributes.Abstract |
                DnTypeAttributes.Sealed | DnTypeAttributes.BeforeFieldInit;
            module.Types.Add(antiType);
            engine.injectedTypes.Add(antiType);

            var initMethod = new MethodDefUser(engine.MakeName(),
                MethodSig.CreateStatic(module.CorLibTypes.Void),
                DnMethodImplAttributes.IL | DnMethodImplAttributes.Managed,
                DnMethodAttributes.Assembly | DnMethodAttributes.Static | DnMethodAttributes.HideBySig);
            initMethod.Body = new CilBody();
            initMethod.Body.InitLocals = true;

            var il = initMethod.Body.Instructions;

            var debuggerType = module.Import(typeof(System.Diagnostics.Debugger));
            var isAttachedProp = module.Import(typeof(System.Diagnostics.Debugger).GetProperty("IsAttached").GetGetMethod());
            var envExit = module.Import(typeof(Environment).GetMethod("Exit", new[] { typeof(int) }));
            var envFailFast = module.Import(typeof(Environment).GetMethod("FailFast", new[] { typeof(string) }));
            var threadSleep = module.Import(typeof(System.Threading.Thread).GetMethod("Sleep", new[] { typeof(int) }));
            var envTickCount = module.Import(typeof(Environment).GetProperty("TickCount").GetGetMethod());
            var processGetCurrent = module.Import(typeof(System.Diagnostics.Process).GetMethod("GetCurrentProcess"));
            var processGetProcesses = module.Import(typeof(System.Diagnostics.Process).GetMethod("GetProcesses", Type.EmptyTypes));
            var processGetName = module.Import(typeof(System.Diagnostics.Process).GetProperty("ProcessName").GetGetMethod());
            var stringToLower = module.Import(typeof(string).GetMethod("ToLower", Type.EmptyTypes));
            var stringContains = module.Import(typeof(string).GetMethod("Contains", new[] { typeof(string) }));

            var scanOne = BuildScanOneProcess(module, envExit);
            antiType.Methods.Add(scanOne);
            engine.injectedMethods.Add(scanOne);

            var checkMethod = BuildProcessScanner(module, antiType, envExit, scanOne);
            antiType.Methods.Add(checkMethod);
            engine.injectedMethods.Add(checkMethod);

            var timerCheck = BuildTimingCheck(module, antiType, envExit, envTickCount);
            antiType.Methods.Add(timerCheck);
            engine.injectedMethods.Add(timerCheck);

            var safeExit      = Instruction.Create(DnOpCodes.Ret);
            var callCheckInst = Instruction.Create(DnOpCodes.Call, checkMethod);

            il.Add(Instruction.Create(DnOpCodes.Call, isAttachedProp));
            il.Add(Instruction.Create(DnOpCodes.Brfalse, callCheckInst));
            il.Add(Instruction.Create(DnOpCodes.Ldstr, "Runtime error"));
            il.Add(Instruction.Create(DnOpCodes.Call, envFailFast));

            il.Add(callCheckInst);
            il.Add(Instruction.Create(DnOpCodes.Call, timerCheck));
            il.Add(safeExit);

            antiType.Methods.Add(initMethod);
            engine.injectedMethods.Add(initMethod);
            engine.InjectCallAtTop(module, modType, initMethod);

            var bgThread = BuildBackgroundMonitor(module, antiType, isAttachedProp, envExit, checkMethod, timerCheck);
            antiType.Methods.Add(bgThread);
            engine.injectedMethods.Add(bgThread);

            var startBg = new MethodDefUser(engine.MakeName(),
                MethodSig.CreateStatic(module.CorLibTypes.Void),
                DnMethodImplAttributes.IL | DnMethodImplAttributes.Managed,
                DnMethodAttributes.Assembly | DnMethodAttributes.Static | DnMethodAttributes.HideBySig);
            startBg.Body = new CilBody();
            var sbIl = startBg.Body.Instructions;

            var threadStartCtor = module.Import(typeof(System.Threading.ThreadStart).GetConstructor(
                new[] { typeof(object), typeof(IntPtr) }));
            var threadCtor = module.Import(typeof(System.Threading.Thread).GetConstructor(
                new[] { typeof(System.Threading.ThreadStart) }));
            var threadSetBg = module.Import(typeof(System.Threading.Thread).GetProperty("IsBackground").GetSetMethod());
            var threadStart = module.Import(typeof(System.Threading.Thread).GetMethod("Start", Type.EmptyTypes));

            startBg.Body.InitLocals = true;
            startBg.Body.Variables.Add(new Local(module.Import(typeof(System.Threading.Thread)).ToTypeSig()));

            var tryStart = Instruction.Create(DnOpCodes.Ldnull);
            sbIl.Add(tryStart);
            sbIl.Add(Instruction.Create(DnOpCodes.Ldftn, bgThread));
            sbIl.Add(Instruction.Create(DnOpCodes.Newobj, threadStartCtor));
            sbIl.Add(Instruction.Create(DnOpCodes.Newobj, threadCtor));
            sbIl.Add(Instruction.Create(DnOpCodes.Stloc_0));
            sbIl.Add(Instruction.Create(DnOpCodes.Ldloc_0));
            sbIl.Add(Instruction.Create(DnOpCodes.Ldc_I4_1));
            sbIl.Add(Instruction.Create(DnOpCodes.Callvirt, threadSetBg));
            sbIl.Add(Instruction.Create(DnOpCodes.Ldloc_0));
            sbIl.Add(Instruction.Create(DnOpCodes.Callvirt, threadStart));
            var retInst = Instruction.Create(DnOpCodes.Ret);
            sbIl.Add(Instruction.Create(DnOpCodes.Leave, retInst));
            var catchInst = Instruction.Create(DnOpCodes.Pop);
            sbIl.Add(catchInst);
            sbIl.Add(Instruction.Create(DnOpCodes.Leave, retInst));
            sbIl.Add(retInst);

            startBg.Body.ExceptionHandlers.Add(new ExceptionHandler(ExceptionHandlerType.Catch)
            {
                TryStart = tryStart,
                TryEnd = catchInst,
                HandlerStart = catchInst,
                HandlerEnd = retInst,
                CatchType = module.CorLibTypes.Object.TypeDefOrRef
            });

            antiType.Methods.Add(startBg);
            engine.injectedMethods.Add(startBg);
            engine.InjectCallInCctor(module, modType, startBg);
        }

        private MethodDef BuildProcessScanner(ModuleDef module, TypeDef owner, IMethod envExit, MethodDef scanOne)
        {
            var method = new MethodDefUser(engine.MakeName(),
                MethodSig.CreateStatic(module.CorLibTypes.Void),
                DnMethodImplAttributes.IL | DnMethodImplAttributes.Managed,
                DnMethodAttributes.Assembly | DnMethodAttributes.Static | DnMethodAttributes.HideBySig);
            method.Body = new CilBody();
            method.Body.InitLocals = true;

            var processArrayType = module.Import(typeof(System.Diagnostics.Process[])).ToTypeSig();
            method.Body.Variables.Add(new Local(processArrayType));
            method.Body.Variables.Add(new Local(module.CorLibTypes.Int32));

            var il = method.Body.Instructions;

            var getProcesses = module.Import(typeof(System.Diagnostics.Process).GetMethod("GetProcesses", Type.EmptyTypes));

            var afterHandler = Instruction.Create(DnOpCodes.Ret);

            var tryStart = Instruction.Create(DnOpCodes.Call, getProcesses);
            il.Add(tryStart);
            il.Add(Instruction.Create(DnOpCodes.Stloc_0));

            il.Add(Instruction.Create(DnOpCodes.Ldc_I4_0));
            il.Add(Instruction.Create(DnOpCodes.Stloc_1));

            var loopStart = Instruction.Create(DnOpCodes.Ldloc_1);
            var loopBody = Instruction.Create(DnOpCodes.Ldloc_0);
            il.Add(Instruction.Create(DnOpCodes.Br, loopStart));

            var afterInner = Instruction.Create(DnOpCodes.Nop);
            il.Add(loopBody);
            il.Add(Instruction.Create(DnOpCodes.Ldloc_1));
            il.Add(Instruction.Create(DnOpCodes.Ldelem_Ref));
            il.Add(Instruction.Create(DnOpCodes.Call, scanOne));
            il.Add(Instruction.Create(DnOpCodes.Leave, afterInner));
            var innerCatch = Instruction.Create(DnOpCodes.Pop);
            il.Add(innerCatch);
            il.Add(Instruction.Create(DnOpCodes.Leave, afterInner));
            il.Add(afterInner);

            method.Body.ExceptionHandlers.Add(new ExceptionHandler(ExceptionHandlerType.Catch)
            {
                TryStart = loopBody,
                TryEnd = innerCatch,
                HandlerStart = innerCatch,
                HandlerEnd = afterInner,
                CatchType = module.CorLibTypes.Object.TypeDefOrRef
            });

            il.Add(Instruction.Create(DnOpCodes.Ldloc_1));
            il.Add(Instruction.Create(DnOpCodes.Ldc_I4_1));
            il.Add(Instruction.Create(DnOpCodes.Add));
            il.Add(Instruction.Create(DnOpCodes.Stloc_1));

            il.Add(loopStart);
            il.Add(Instruction.Create(DnOpCodes.Ldloc_0));
            il.Add(Instruction.Create(DnOpCodes.Ldlen));
            il.Add(Instruction.Create(DnOpCodes.Conv_I4));
            il.Add(Instruction.Create(DnOpCodes.Blt, loopBody));

            il.Add(Instruction.Create(DnOpCodes.Leave, afterHandler));

            var handlerStart = Instruction.Create(DnOpCodes.Pop);
            il.Add(handlerStart);
            il.Add(Instruction.Create(DnOpCodes.Leave, afterHandler));

            il.Add(afterHandler);

            method.Body.ExceptionHandlers.Add(new ExceptionHandler(ExceptionHandlerType.Catch)
            {
                TryStart = tryStart,
                TryEnd = handlerStart,
                HandlerStart = handlerStart,
                HandlerEnd = afterHandler,
                CatchType = module.CorLibTypes.Object.TypeDefOrRef
            });

            return method;
        }

        private MethodDef BuildScanOneProcess(ModuleDef module, IMethod envExit)
        {
            var processSig = module.Import(typeof(System.Diagnostics.Process)).ToTypeSig();
            var method = new MethodDefUser(engine.MakeName(),
                MethodSig.CreateStatic(module.CorLibTypes.Void, processSig),
                DnMethodImplAttributes.IL | DnMethodImplAttributes.Managed,
                DnMethodAttributes.Assembly | DnMethodAttributes.Static | DnMethodAttributes.HideBySig);
            method.Body = new CilBody();
            method.Body.InitLocals = true;
            method.Body.Variables.Add(new Local(module.CorLibTypes.String));
            method.Body.Variables.Add(new Local(module.Import(typeof(System.Diagnostics.FileVersionInfo)).ToTypeSig()));

            var il = method.Body.Instructions;

            var getProcName = module.Import(typeof(System.Diagnostics.Process).GetProperty("ProcessName").GetGetMethod());
            var getMainModule = module.Import(typeof(System.Diagnostics.Process).GetProperty("MainModule").GetGetMethod());
            var getFvi = module.Import(typeof(System.Diagnostics.ProcessModule).GetProperty("FileVersionInfo").GetGetMethod());
            var getOrigName = module.Import(typeof(System.Diagnostics.FileVersionInfo).GetProperty("OriginalFilename").GetGetMethod());
            var getFileDesc = module.Import(typeof(System.Diagnostics.FileVersionInfo).GetProperty("FileDescription").GetGetMethod());
            var getProdName = module.Import(typeof(System.Diagnostics.FileVersionInfo).GetProperty("ProductName").GetGetMethod());
            var getInternalName = module.Import(typeof(System.Diagnostics.FileVersionInfo).GetProperty("InternalName").GetGetMethod());
            var getCompName = module.Import(typeof(System.Diagnostics.FileVersionInfo).GetProperty("CompanyName").GetGetMethod());
            var getMainWindowTitle = module.Import(typeof(System.Diagnostics.Process).GetProperty("MainWindowTitle").GetGetMethod());
            var toLower = module.Import(typeof(string).GetMethod("ToLower", Type.EmptyTypes));
            var contains = module.Import(typeof(string).GetMethod("Contains", new[] { typeof(string) }));

            var retInst = Instruction.Create(DnOpCodes.Ret);
            var stage2Start = Instruction.Create(DnOpCodes.Ldarg_0);
            var stage3Start = Instruction.Create(DnOpCodes.Ldarg_0);

            var stage1Try = Instruction.Create(DnOpCodes.Ldarg_0);
            il.Add(stage1Try);
            il.Add(Instruction.Create(DnOpCodes.Callvirt, getProcName));
            il.Add(Instruction.Create(DnOpCodes.Callvirt, toLower));
            il.Add(Instruction.Create(DnOpCodes.Stloc_0));

            string[] nameSubset = fingerprintNames.OrderBy(x => rng.Next()).Take(rng.Next(20, fingerprintNames.Length + 1)).ToArray();
            foreach (string key in nameSubset)
            {
                var skipKey = Instruction.Create(DnOpCodes.Nop);
                il.Add(Instruction.Create(DnOpCodes.Ldloc_0));
                il.Add(Instruction.Create(DnOpCodes.Ldstr, key));
                il.Add(Instruction.Create(DnOpCodes.Callvirt, contains));
                il.Add(Instruction.Create(DnOpCodes.Brfalse, skipKey));
                il.Add(Instruction.Create(DnOpCodes.Ldc_I4, -1));
                il.Add(Instruction.Create(DnOpCodes.Call, envExit));
                il.Add(skipKey);
            }

            il.Add(Instruction.Create(DnOpCodes.Leave, stage2Start));
            var stage1Catch = Instruction.Create(DnOpCodes.Pop);
            il.Add(stage1Catch);
            il.Add(Instruction.Create(DnOpCodes.Leave, stage2Start));

            method.Body.ExceptionHandlers.Add(new ExceptionHandler(ExceptionHandlerType.Catch)
            {
                TryStart = stage1Try,
                TryEnd = stage1Catch,
                HandlerStart = stage1Catch,
                HandlerEnd = stage2Start,
                CatchType = module.CorLibTypes.Object.TypeDefOrRef
            });

            il.Add(stage2Start);
            il.Add(Instruction.Create(DnOpCodes.Callvirt, getMainModule));
            il.Add(Instruction.Create(DnOpCodes.Callvirt, getFvi));
            il.Add(Instruction.Create(DnOpCodes.Stloc_1));

            IMethod[] strFields = new IMethod[] { getOrigName, getFileDesc, getProdName, getInternalName };
            foreach (IMethod fieldGetter in strFields)
            {
                var skipField = Instruction.Create(DnOpCodes.Nop);
                il.Add(Instruction.Create(DnOpCodes.Ldloc_1));
                il.Add(Instruction.Create(DnOpCodes.Callvirt, fieldGetter));
                il.Add(Instruction.Create(DnOpCodes.Stloc_0));
                il.Add(Instruction.Create(DnOpCodes.Ldloc_0));
                il.Add(Instruction.Create(DnOpCodes.Brfalse, skipField));
                il.Add(Instruction.Create(DnOpCodes.Ldloc_0));
                il.Add(Instruction.Create(DnOpCodes.Callvirt, toLower));
                il.Add(Instruction.Create(DnOpCodes.Stloc_0));
                foreach (string key in fingerprintNames)
                {
                    var skipKey = Instruction.Create(DnOpCodes.Nop);
                    il.Add(Instruction.Create(DnOpCodes.Ldloc_0));
                    il.Add(Instruction.Create(DnOpCodes.Ldstr, key));
                    il.Add(Instruction.Create(DnOpCodes.Callvirt, contains));
                    il.Add(Instruction.Create(DnOpCodes.Brfalse, skipKey));
                    il.Add(Instruction.Create(DnOpCodes.Ldc_I4, -1));
                    il.Add(Instruction.Create(DnOpCodes.Call, envExit));
                    il.Add(skipKey);
                }
                il.Add(skipField);
            }

            {
                var skipCompany = Instruction.Create(DnOpCodes.Nop);
                il.Add(Instruction.Create(DnOpCodes.Ldloc_1));
                il.Add(Instruction.Create(DnOpCodes.Callvirt, getCompName));
                il.Add(Instruction.Create(DnOpCodes.Stloc_0));
                il.Add(Instruction.Create(DnOpCodes.Ldloc_0));
                il.Add(Instruction.Create(DnOpCodes.Brfalse, skipCompany));
                il.Add(Instruction.Create(DnOpCodes.Ldloc_0));
                il.Add(Instruction.Create(DnOpCodes.Callvirt, toLower));
                il.Add(Instruction.Create(DnOpCodes.Stloc_0));
                foreach (string key in fingerprintCompanies)
                {
                    var skipKey = Instruction.Create(DnOpCodes.Nop);
                    il.Add(Instruction.Create(DnOpCodes.Ldloc_0));
                    il.Add(Instruction.Create(DnOpCodes.Ldstr, key));
                    il.Add(Instruction.Create(DnOpCodes.Callvirt, contains));
                    il.Add(Instruction.Create(DnOpCodes.Brfalse, skipKey));
                    il.Add(Instruction.Create(DnOpCodes.Ldc_I4, -1));
                    il.Add(Instruction.Create(DnOpCodes.Call, envExit));
                    il.Add(skipKey);
                }
                il.Add(skipCompany);
            }

            il.Add(Instruction.Create(DnOpCodes.Leave, stage3Start));
            var stage2Catch = Instruction.Create(DnOpCodes.Pop);
            il.Add(stage2Catch);
            il.Add(Instruction.Create(DnOpCodes.Leave, stage3Start));

            method.Body.ExceptionHandlers.Add(new ExceptionHandler(ExceptionHandlerType.Catch)
            {
                TryStart = stage2Start,
                TryEnd = stage2Catch,
                HandlerStart = stage2Catch,
                HandlerEnd = stage3Start,
                CatchType = module.CorLibTypes.Object.TypeDefOrRef
            });

            il.Add(stage3Start);
            il.Add(Instruction.Create(DnOpCodes.Callvirt, getMainWindowTitle));
            il.Add(Instruction.Create(DnOpCodes.Stloc_0));

            var skipTitle = Instruction.Create(DnOpCodes.Nop);
            il.Add(Instruction.Create(DnOpCodes.Ldloc_0));
            il.Add(Instruction.Create(DnOpCodes.Brfalse, skipTitle));
            il.Add(Instruction.Create(DnOpCodes.Ldloc_0));
            il.Add(Instruction.Create(DnOpCodes.Callvirt, toLower));
            il.Add(Instruction.Create(DnOpCodes.Stloc_0));

            foreach (string key in fingerprintWindowTitles)
            {
                var skipKey = Instruction.Create(DnOpCodes.Nop);
                il.Add(Instruction.Create(DnOpCodes.Ldloc_0));
                il.Add(Instruction.Create(DnOpCodes.Ldstr, key));
                il.Add(Instruction.Create(DnOpCodes.Callvirt, contains));
                il.Add(Instruction.Create(DnOpCodes.Brfalse, skipKey));
                il.Add(Instruction.Create(DnOpCodes.Ldc_I4, -1));
                il.Add(Instruction.Create(DnOpCodes.Call, envExit));
                il.Add(skipKey);
            }
            il.Add(skipTitle);

            il.Add(Instruction.Create(DnOpCodes.Leave, retInst));
            var stage3Catch = Instruction.Create(DnOpCodes.Pop);
            il.Add(stage3Catch);
            il.Add(Instruction.Create(DnOpCodes.Leave, retInst));

            method.Body.ExceptionHandlers.Add(new ExceptionHandler(ExceptionHandlerType.Catch)
            {
                TryStart = stage3Start,
                TryEnd = stage3Catch,
                HandlerStart = stage3Catch,
                HandlerEnd = retInst,
                CatchType = module.CorLibTypes.Object.TypeDefOrRef
            });

            il.Add(retInst);
            return method;
        }

        private MethodDef BuildTimingCheck(ModuleDef module, TypeDef owner,
            IMethod envExit, IMethod envTickCount)
        {
            var method = new MethodDefUser(engine.MakeName(),
                MethodSig.CreateStatic(module.CorLibTypes.Void),
                DnMethodImplAttributes.IL | DnMethodImplAttributes.Managed,
                DnMethodAttributes.Assembly | DnMethodAttributes.Static | DnMethodAttributes.HideBySig);
            method.Body = new CilBody();
            method.Body.Instructions.Add(Instruction.Create(DnOpCodes.Ret));
            return method;
        }

        private MethodDef BuildBackgroundMonitor(ModuleDef module, TypeDef owner,
            IMethod isAttached, IMethod envExit, MethodDef processScanner, MethodDef timerCheck)
        {
            var method = new MethodDefUser(engine.MakeName(),
                MethodSig.CreateStatic(module.CorLibTypes.Void),
                DnMethodImplAttributes.IL | DnMethodImplAttributes.Managed,
                DnMethodAttributes.Assembly | DnMethodAttributes.Static | DnMethodAttributes.HideBySig);
            method.Body = new CilBody();

            var threadSleep = module.Import(typeof(System.Threading.Thread).GetMethod("Sleep", new[] { typeof(int) }));
            var il = method.Body.Instructions;

            var tryStart       = Instruction.Create(DnOpCodes.Call, isAttached);
            var afterAttach    = Instruction.Create(DnOpCodes.Call, processScanner);
            var handlerStart   = Instruction.Create(DnOpCodes.Pop);
            var beforeSleep    = Instruction.Create(DnOpCodes.Ldc_I4, 1500 + rng.Next(0, 2500));
            var loopBack       = Instruction.Create(DnOpCodes.Br, tryStart);

            il.Add(tryStart);
            il.Add(Instruction.Create(DnOpCodes.Brfalse, afterAttach));
            il.Add(Instruction.Create(DnOpCodes.Ldc_I4, -1));
            il.Add(Instruction.Create(DnOpCodes.Call, envExit));
            il.Add(afterAttach);
            il.Add(Instruction.Create(DnOpCodes.Call, timerCheck));
            il.Add(Instruction.Create(DnOpCodes.Leave, beforeSleep));
            il.Add(handlerStart);
            il.Add(Instruction.Create(DnOpCodes.Leave, beforeSleep));
            il.Add(beforeSleep);
            il.Add(Instruction.Create(DnOpCodes.Call, threadSleep));
            il.Add(loopBack);

            method.Body.ExceptionHandlers.Add(new ExceptionHandler(ExceptionHandlerType.Catch)
            {
                TryStart     = tryStart,
                TryEnd       = handlerStart,
                HandlerStart = handlerStart,
                HandlerEnd   = beforeSleep,
                CatchType    = module.CorLibTypes.Object.TypeDefOrRef
            });

            return method;
        }
    }
}
