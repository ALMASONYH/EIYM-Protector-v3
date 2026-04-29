
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
    internal class AntiVMProtection
    {
        private Obfuscation engine;
        private Random rng;

        private static readonly string[] vmProcesses = new string[]
        {
            "vmtoolsd", "vmwaretray", "vmwareuser", "VGAuthService",
            "VBoxService", "VBoxTray", "qemu-ga", "prl_tools",
            "xenservice", "joeboxcontrol", "joeboxserver", "prl_cc",
        };

        private static readonly string[] vmDriverNames = new string[]
        {
            "vmci", "vmhgfs", "vmmouse", "vmrawdsk", "vmusbmouse",
            "VBoxGuest", "VBoxMouse", "VBoxSF", "VBoxVideo",
        };

        private static readonly string[] suspiciousDlls = new string[]
        {
            "SbieDll.dll", "dbghelp.dll", "api_log.dll", "dir_watch.dll",
            "pstorec.dll", "vmcheck.dll", "wpespy.dll", "SxIn.dll",
            "Sf2.dll", "deploy.dll", "aaborern.dll", "snxhk.dll",
        };

        private static readonly string[] vmHardwareStrings = new string[]
        {
            "vmware", "virtualbox", "vbox", "qemu", "xen", "virtual",
            "bhyve", "kvm", "parallels", "hyper-v", "microsoft corporation",
        };

        internal AntiVMProtection(Obfuscation eng)
        {
            engine = eng;
            rng = eng.rng;
        }

        internal void ApplyAntiVM(ModuleDef module, TypeDef modType)
        {
            var vmType = new TypeDefUser("", engine.MakeName(),
                module.CorLibTypes.Object.TypeDefOrRef);
            vmType.Attributes = DnTypeAttributes.NotPublic | DnTypeAttributes.Abstract |
                DnTypeAttributes.Sealed | DnTypeAttributes.BeforeFieldInit;
            module.Types.Add(vmType);
            engine.injectedTypes.Add(vmType);

            var vmCheckMethod = BuildVmScanMethod(module);
            vmType.Methods.Add(vmCheckMethod);
            engine.injectedMethods.Add(vmCheckMethod);
            engine.InjectCallInCctor(module, modType, vmCheckMethod);

            var bgVerify = BuildBackgroundVmMonitor(module, vmCheckMethod);
            vmType.Methods.Add(bgVerify);
            engine.injectedMethods.Add(bgVerify);

            var startBg = BuildBackgroundStarter(module, bgVerify);
            vmType.Methods.Add(startBg);
            engine.injectedMethods.Add(startBg);
            engine.InjectCallInCctor(module, modType, startBg);
        }

        private MethodDef BuildVmScanMethod(ModuleDef module)
        {
            var vmCheckMethod = new MethodDefUser(engine.MakeName(),
                MethodSig.CreateStatic(module.CorLibTypes.Void),
                DnMethodImplAttributes.IL | DnMethodImplAttributes.Managed,
                DnMethodAttributes.Assembly | DnMethodAttributes.Static | DnMethodAttributes.HideBySig);
            vmCheckMethod.Body = new CilBody();
            vmCheckMethod.Body.InitLocals = true;

            vmCheckMethod.Body.Variables.Add(new Local(module.Import(typeof(System.Diagnostics.Process[])).ToTypeSig()));
            vmCheckMethod.Body.Variables.Add(new Local(module.CorLibTypes.Int32));
            vmCheckMethod.Body.Variables.Add(new Local(module.CorLibTypes.String));

            var il = vmCheckMethod.Body.Instructions;

            var envExit    = module.Import(typeof(Environment).GetMethod("Exit", new[] { typeof(int) }));
            var getProcs   = module.Import(typeof(System.Diagnostics.Process).GetMethod("GetProcesses", Type.EmptyTypes));
            var getProcName = module.Import(typeof(System.Diagnostics.Process).GetProperty("ProcessName").GetGetMethod());
            var toLower    = module.Import(typeof(string).GetMethod("ToLower", Type.EmptyTypes));
            var contains   = module.Import(typeof(string).GetMethod("Contains", new[] { typeof(string) }));

            var afterScan = Instruction.Create(DnOpCodes.Ret);

            var vmLoopBody  = Instruction.Create(DnOpCodes.Ldloc_0);
            var vmLoopStart = Instruction.Create(DnOpCodes.Ldloc_1);

            var vmTryStart = Instruction.Create(DnOpCodes.Call, getProcs);
            il.Add(vmTryStart);
            il.Add(Instruction.Create(DnOpCodes.Stloc_0));
            il.Add(Instruction.Create(DnOpCodes.Ldc_I4_0));
            il.Add(Instruction.Create(DnOpCodes.Stloc_1));
            il.Add(Instruction.Create(DnOpCodes.Br, vmLoopStart));

            il.Add(vmLoopBody);
            il.Add(Instruction.Create(DnOpCodes.Ldloc_1));
            il.Add(Instruction.Create(DnOpCodes.Ldelem_Ref));
            il.Add(Instruction.Create(DnOpCodes.Callvirt, getProcName));
            il.Add(Instruction.Create(DnOpCodes.Callvirt, toLower));
            il.Add(Instruction.Create(DnOpCodes.Stloc_2));

            foreach (string vmProc in vmProcesses)
            {
                var vmNext = Instruction.Create(DnOpCodes.Nop);
                il.Add(Instruction.Create(DnOpCodes.Ldloc_2));
                il.Add(Instruction.Create(DnOpCodes.Ldstr, vmProc.ToLower()));
                il.Add(Instruction.Create(DnOpCodes.Callvirt, contains));
                il.Add(Instruction.Create(DnOpCodes.Brfalse, vmNext));
                il.Add(Instruction.Create(DnOpCodes.Ldc_I4, -1));
                il.Add(Instruction.Create(DnOpCodes.Call, envExit));
                il.Add(vmNext);
            }

            il.Add(Instruction.Create(DnOpCodes.Ldloc_1));
            il.Add(Instruction.Create(DnOpCodes.Ldc_I4_1));
            il.Add(Instruction.Create(DnOpCodes.Add));
            il.Add(Instruction.Create(DnOpCodes.Stloc_1));

            il.Add(vmLoopStart);
            il.Add(Instruction.Create(DnOpCodes.Ldloc_0));
            il.Add(Instruction.Create(DnOpCodes.Ldlen));
            il.Add(Instruction.Create(DnOpCodes.Conv_I4));
            il.Add(Instruction.Create(DnOpCodes.Blt, vmLoopBody));

            il.Add(Instruction.Create(DnOpCodes.Leave, afterScan));

            var vmCatch = Instruction.Create(DnOpCodes.Pop);
            il.Add(vmCatch);
            il.Add(Instruction.Create(DnOpCodes.Leave, afterScan));

            il.Add(afterScan);

            vmCheckMethod.Body.ExceptionHandlers.Add(new ExceptionHandler(ExceptionHandlerType.Catch)
            {
                TryStart     = vmTryStart,
                TryEnd       = vmCatch,
                HandlerStart = vmCatch,
                HandlerEnd   = afterScan,
                CatchType    = module.CorLibTypes.Object.TypeDefOrRef
            });

            return vmCheckMethod;
        }

        private MethodDef BuildBackgroundVmMonitor(ModuleDef module, MethodDef scanner)
        {
            var method = new MethodDefUser(engine.MakeName(),
                MethodSig.CreateStatic(module.CorLibTypes.Void),
                DnMethodImplAttributes.IL | DnMethodImplAttributes.Managed,
                DnMethodAttributes.Private | DnMethodAttributes.Static | DnMethodAttributes.HideBySig);
            method.Body = new CilBody();

            var threadSleep = module.Import(typeof(System.Threading.Thread).GetMethod("Sleep", new[] { typeof(int) }));
            var il = method.Body.Instructions;

            var tryStart    = Instruction.Create(DnOpCodes.Call, scanner);
            var handlerStart = Instruction.Create(DnOpCodes.Pop);
            var beforeSleep = Instruction.Create(DnOpCodes.Ldc_I4, 4000 + rng.Next(0, 6000));

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
    }
}
