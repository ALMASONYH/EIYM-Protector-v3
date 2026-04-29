
using System;
using System.Collections.Generic;
using System.Linq;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

using DnFieldAttributes = dnlib.DotNet.FieldAttributes;
using DnMethodAttributes = dnlib.DotNet.MethodAttributes;
using DnMethodImplAttributes = dnlib.DotNet.MethodImplAttributes;
using DnTypeAttributes = dnlib.DotNet.TypeAttributes;
using DnOpCodes = dnlib.DotNet.Emit.OpCodes;

namespace MasonProtector.Core
{
    internal class AntiHookProtection
    {
        private readonly Obfuscation engine;
        private readonly Random rng;

        private struct ApiCheck
        {
            public string Module;
            public string Function;
            public byte[][] AcceptedX86;
            public byte[][] AcceptedX64;
        }

        private static readonly ApiCheck[] ApiTable = new ApiCheck[]
        {
            new ApiCheck {
                Module = "kernel32.dll",
                Function = "IsDebuggerPresent",

                AcceptedX86 = new byte[][] { new byte[] { 0x64, 0xA1 }, new byte[] { 0xFF, 0x25 }, new byte[] { 0x55, 0x8B } },

                AcceptedX64 = new byte[][] { new byte[] { 0x65, 0x48 }, new byte[] { 0x48, 0xFF }, new byte[] { 0x48, 0x83 } },
            },
            new ApiCheck {
                Module = "kernel32.dll",
                Function = "CheckRemoteDebuggerPresent",
                AcceptedX86 = new byte[][] { new byte[] { 0xFF, 0x25 }, new byte[] { 0x8B, 0xFF }, new byte[] { 0x55, 0x8B } },
                AcceptedX64 = new byte[][] { new byte[] { 0x48, 0x83 }, new byte[] { 0x48, 0xFF }, new byte[] { 0x40, 0x53 }, new byte[] { 0x48, 0x89 } },
            },
            new ApiCheck {
                Module = "kernel32.dll",
                Function = "OutputDebugStringA",
                AcceptedX86 = new byte[][] { new byte[] { 0xFF, 0x25 }, new byte[] { 0x8B, 0xFF }, new byte[] { 0x55, 0x8B } },
                AcceptedX64 = new byte[][] { new byte[] { 0x48, 0x83 }, new byte[] { 0x48, 0x89 }, new byte[] { 0x48, 0xFF }, new byte[] { 0x40, 0x53 } },
            },
            new ApiCheck {
                Module = "ntdll.dll",
                Function = "NtQueryInformationProcess",
                AcceptedX86 = new byte[][] { new byte[] { 0xB8, 0x19 }, new byte[] { 0xB8, 0x16 }, new byte[] { 0xB8, 0x9A }, new byte[] { 0xB8, 0xCF } },
                AcceptedX64 = new byte[][] { new byte[] { 0x4C, 0x8B }, new byte[] { 0x48, 0x8B }, new byte[] { 0xB8, 0x19 } },
            },
        };

        internal AntiHookProtection(Obfuscation eng)
        {
            engine = eng;
            rng = eng.rng;
        }

        internal void ApplyAntiHook(ModuleDef module, TypeDef modType)
        {

            int wantCount = rng.Next(2, ApiTable.Length + 1);
            var picked = ApiTable.OrderBy(_ => rng.Next()).Take(wantCount).ToArray();

            var hookType = new TypeDefUser("", engine.MakeName(),
                module.CorLibTypes.Object.TypeDefOrRef);
            hookType.Attributes = DnTypeAttributes.NotPublic | DnTypeAttributes.Abstract |
                DnTypeAttributes.Sealed | DnTypeAttributes.BeforeFieldInit;
            module.Types.Add(hookType);
            engine.injectedTypes.Add(hookType);

            var loadLib = ImportPInvoke(module, hookType, "LoadLibraryA",   "kernel32.dll", typeof(IntPtr), new[] { typeof(string) });
            var getProc = ImportPInvoke(module, hookType, "GetProcAddress", "kernel32.dll", typeof(IntPtr), new[] { typeof(IntPtr), typeof(string) });

            engine.injectedMethods.Add(loadLib);
            engine.injectedMethods.Add(getProc);

            var marshalCopy   = module.Import(typeof(System.Runtime.InteropServices.Marshal).GetMethod("Copy",
                                    new[] { typeof(IntPtr), typeof(byte[]), typeof(int), typeof(int) }));
            var envExit       = module.Import(typeof(Environment).GetMethod("Exit", new[] { typeof(int) }));
            var is64Process   = module.Import(typeof(Environment).GetProperty("Is64BitProcess").GetGetMethod());

            var checkMethod = new MethodDefUser(engine.MakeName(),
                MethodSig.CreateStatic(module.CorLibTypes.Void),
                DnMethodImplAttributes.IL | DnMethodImplAttributes.Managed,
                DnMethodAttributes.Assembly | DnMethodAttributes.Static | DnMethodAttributes.HideBySig);
            checkMethod.Body = new CilBody();
            checkMethod.Body.InitLocals = true;

            var ipSig = module.Import(typeof(IntPtr)).ToTypeSig();
            checkMethod.Body.Variables.Add(new Local(ipSig));
            checkMethod.Body.Variables.Add(new Local(ipSig));
            checkMethod.Body.Variables.Add(new Local(new SZArraySig(module.CorLibTypes.Byte)));
            checkMethod.Body.Variables.Add(new Local(module.CorLibTypes.Boolean));

            var il = checkMethod.Body.Instructions;

            var afterTry = Instruction.Create(DnOpCodes.Ret);
            var tryStart = Instruction.Create(DnOpCodes.Call, is64Process);
            il.Add(tryStart);
            il.Add(Instruction.Create(DnOpCodes.Stloc_3));

            foreach (var api in picked)
            {
                EmitApiCheck(module, il, api, loadLib, getProc, marshalCopy, envExit);
            }

            il.Add(Instruction.Create(DnOpCodes.Leave, afterTry));

            var handlerStart = Instruction.Create(DnOpCodes.Pop);
            il.Add(handlerStart);
            il.Add(Instruction.Create(DnOpCodes.Leave, afterTry));
            il.Add(afterTry);

            checkMethod.Body.ExceptionHandlers.Add(new ExceptionHandler(ExceptionHandlerType.Catch)
            {
                TryStart = tryStart,
                TryEnd = handlerStart,
                HandlerStart = handlerStart,
                HandlerEnd = afterTry,
                CatchType = module.CorLibTypes.Object.TypeDefOrRef,
            });

            hookType.Methods.Add(checkMethod);
            engine.injectedMethods.Add(checkMethod);
            engine.InjectCallAtTop(module, modType, checkMethod);

            var bgMonitor = BuildBackgroundHookMonitor(module, checkMethod);
            hookType.Methods.Add(bgMonitor);
            engine.injectedMethods.Add(bgMonitor);

            var startBg = BuildBackgroundStarter(module, bgMonitor);
            hookType.Methods.Add(startBg);
            engine.injectedMethods.Add(startBg);
            engine.InjectCallInCctor(module, modType, startBg);
        }

        private MethodDef BuildBackgroundHookMonitor(ModuleDef module, MethodDef scanner)
        {
            var method = new MethodDefUser(engine.MakeName(),
                MethodSig.CreateStatic(module.CorLibTypes.Void),
                DnMethodImplAttributes.IL | DnMethodImplAttributes.Managed,
                DnMethodAttributes.Private | DnMethodAttributes.Static | DnMethodAttributes.HideBySig);
            method.Body = new CilBody();

            var threadSleep = module.Import(typeof(System.Threading.Thread).GetMethod("Sleep", new[] { typeof(int) }));
            var il = method.Body.Instructions;

            var tryStart = Instruction.Create(DnOpCodes.Call, scanner);
            var handlerStart = Instruction.Create(DnOpCodes.Pop);
            var beforeSleep = Instruction.Create(DnOpCodes.Ldc_I4, 3500 + rng.Next(0, 5500));

            il.Add(tryStart);
            il.Add(Instruction.Create(DnOpCodes.Leave, beforeSleep));
            il.Add(handlerStart);
            il.Add(Instruction.Create(DnOpCodes.Leave, beforeSleep));
            il.Add(beforeSleep);
            il.Add(Instruction.Create(DnOpCodes.Call, threadSleep));
            il.Add(Instruction.Create(DnOpCodes.Br, tryStart));

            method.Body.ExceptionHandlers.Add(new ExceptionHandler(ExceptionHandlerType.Catch)
            {
                TryStart = tryStart,
                TryEnd = handlerStart,
                HandlerStart = handlerStart,
                HandlerEnd = beforeSleep,
                CatchType = module.CorLibTypes.Object.TypeDefOrRef
            });

            return method;
        }

        private MethodDef BuildBackgroundStarter(ModuleDef module, MethodDef bgEntry)
        {
            var startBg = new MethodDefUser(engine.MakeName(),
                MethodSig.CreateStatic(module.CorLibTypes.Void),
                DnMethodImplAttributes.IL | DnMethodImplAttributes.Managed,
                DnMethodAttributes.Assembly | DnMethodAttributes.Static | DnMethodAttributes.HideBySig);
            startBg.Body = new CilBody();
            startBg.Body.InitLocals = true;
            startBg.Body.Variables.Add(new Local(module.Import(typeof(System.Threading.Thread)).ToTypeSig()));

            var sbIl = startBg.Body.Instructions;

            var threadStartCtor = module.Import(typeof(System.Threading.ThreadStart).GetConstructor(
                new[] { typeof(object), typeof(IntPtr) }));
            var threadCtor = module.Import(typeof(System.Threading.Thread).GetConstructor(
                new[] { typeof(System.Threading.ThreadStart) }));
            var threadSetBg = module.Import(typeof(System.Threading.Thread).GetProperty("IsBackground").GetSetMethod());
            var threadStart = module.Import(typeof(System.Threading.Thread).GetMethod("Start", Type.EmptyTypes));

            var tryStart = Instruction.Create(DnOpCodes.Ldnull);
            sbIl.Add(tryStart);
            sbIl.Add(Instruction.Create(DnOpCodes.Ldftn, bgEntry));
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

            return startBg;
        }

        private void EmitApiCheck(ModuleDef module, IList<Instruction> il, ApiCheck api,
            MethodDef loadLib, MethodDef getProc, IMethod marshalCopy, IMethod envExit)
        {

            il.Add(Instruction.Create(DnOpCodes.Ldstr, api.Module));
            il.Add(Instruction.Create(DnOpCodes.Call, loadLib));
            il.Add(Instruction.Create(DnOpCodes.Stloc_0));

            var skipApi = Instruction.Create(DnOpCodes.Nop);
            var ipZero  = module.Import(typeof(IntPtr).GetField("Zero"));
            il.Add(Instruction.Create(DnOpCodes.Ldloc_0));
            il.Add(Instruction.Create(DnOpCodes.Ldsfld, ipZero));

            var ipEq = module.Import(typeof(IntPtr).GetMethod("op_Equality", new[] { typeof(IntPtr), typeof(IntPtr) }));
            il.Add(Instruction.Create(DnOpCodes.Call, ipEq));
            il.Add(Instruction.Create(DnOpCodes.Brtrue, skipApi));

            il.Add(Instruction.Create(DnOpCodes.Ldloc_0));
            il.Add(Instruction.Create(DnOpCodes.Ldstr, api.Function));
            il.Add(Instruction.Create(DnOpCodes.Call, getProc));
            il.Add(Instruction.Create(DnOpCodes.Stloc_1));

            il.Add(Instruction.Create(DnOpCodes.Ldloc_1));
            il.Add(Instruction.Create(DnOpCodes.Ldsfld, ipZero));
            il.Add(Instruction.Create(DnOpCodes.Call, ipEq));
            il.Add(Instruction.Create(DnOpCodes.Brtrue, skipApi));

            il.Add(Instruction.Create(DnOpCodes.Ldc_I4_2));
            il.Add(Instruction.Create(DnOpCodes.Newarr, module.CorLibTypes.Byte.TypeDefOrRef));
            il.Add(Instruction.Create(DnOpCodes.Stloc_2));

            il.Add(Instruction.Create(DnOpCodes.Ldloc_1));
            il.Add(Instruction.Create(DnOpCodes.Ldloc_2));
            il.Add(Instruction.Create(DnOpCodes.Ldc_I4_0));
            il.Add(Instruction.Create(DnOpCodes.Ldc_I4_2));
            il.Add(Instruction.Create(DnOpCodes.Call, marshalCopy));

            var x64Branch = Instruction.Create(DnOpCodes.Nop);
            var afterChecks = Instruction.Create(DnOpCodes.Nop);

            il.Add(Instruction.Create(DnOpCodes.Ldloc_3));
            il.Add(Instruction.Create(DnOpCodes.Brtrue, x64Branch));

            EmitPatternMatch(il, api.AcceptedX86, envExit, afterChecks);
            il.Add(Instruction.Create(DnOpCodes.Br, afterChecks));

            il.Add(x64Branch);
            EmitPatternMatch(il, api.AcceptedX64, envExit, afterChecks);

            il.Add(afterChecks);
            il.Add(skipApi);
        }

        private void EmitPatternMatch(IList<Instruction> il, byte[][] patterns, IMethod envExit, Instruction afterChecks)
        {

            var passed = Instruction.Create(DnOpCodes.Br, afterChecks);

            foreach (var pat in patterns)
            {
                var nextPat = Instruction.Create(DnOpCodes.Nop);
                il.Add(Instruction.Create(DnOpCodes.Ldloc_2));
                il.Add(Instruction.Create(DnOpCodes.Ldc_I4_0));
                il.Add(Instruction.Create(DnOpCodes.Ldelem_U1));
                il.Add(Instruction.Create(DnOpCodes.Ldc_I4, (int)pat[0]));
                il.Add(Instruction.Create(DnOpCodes.Bne_Un, nextPat));
                il.Add(Instruction.Create(DnOpCodes.Ldloc_2));
                il.Add(Instruction.Create(DnOpCodes.Ldc_I4_1));
                il.Add(Instruction.Create(DnOpCodes.Ldelem_U1));
                il.Add(Instruction.Create(DnOpCodes.Ldc_I4, (int)pat[1]));
                il.Add(Instruction.Create(DnOpCodes.Bne_Un, nextPat));
                il.Add(Instruction.Create(DnOpCodes.Br, passed));
                il.Add(nextPat);
            }

            il.Add(Instruction.Create(DnOpCodes.Ldc_I4_M1));
            il.Add(Instruction.Create(DnOpCodes.Call, envExit));
            il.Add(passed);
        }

        private MethodDef ImportPInvoke(ModuleDef module, TypeDef owner,
            string entryPoint, string dllName, Type retType, Type[] paramTypes)
        {
            var sigParams = new TypeSig[paramTypes.Length];
            for (int i = 0; i < paramTypes.Length; i++)
                sigParams[i] = module.Import(paramTypes[i]).ToTypeSig();
            var retSig = module.Import(retType).ToTypeSig();

            var m = new MethodDefUser(engine.MakeName(),
                MethodSig.CreateStatic(retSig, sigParams),
                DnMethodImplAttributes.PreserveSig,
                DnMethodAttributes.Assembly | DnMethodAttributes.Static |
                DnMethodAttributes.HideBySig | DnMethodAttributes.PinvokeImpl);

            var mod = new ModuleRefUser(module, dllName);
            m.ImplMap = new ImplMapUser(mod, entryPoint,
                PInvokeAttributes.CallConvWinapi | PInvokeAttributes.CharSetAnsi);

            owner.Methods.Add(m);
            return m;
        }
    }
}
