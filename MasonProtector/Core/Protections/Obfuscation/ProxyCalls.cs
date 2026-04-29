
using System;
using System.Collections.Generic;
using System.Linq;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

using DnMethodAttributes = dnlib.DotNet.MethodAttributes;
using DnMethodImplAttributes = dnlib.DotNet.MethodImplAttributes;
using DnTypeAttributes = dnlib.DotNet.TypeAttributes;
using DnOpCodes = dnlib.DotNet.Emit.OpCodes;

namespace MasonProtector.Core
{
    internal class ProxyCallsProtection
    {
        private Obfuscation engine;
        private Random rng;

        internal ProxyCallsProtection(Obfuscation eng)
        {
            engine = eng;
            rng = eng.rng;
        }

        internal void ApplyProxyCalls(ModuleDef module)
        {
            var proxyType = new TypeDefUser("", engine.MakeName(),
                module.CorLibTypes.Object.TypeDefOrRef);
            proxyType.Attributes = DnTypeAttributes.NotPublic | DnTypeAttributes.Abstract |
                DnTypeAttributes.Sealed | DnTypeAttributes.BeforeFieldInit;
            module.Types.Add(proxyType);
            engine.injectedTypes.Add(proxyType);

            var delegateCache = new Dictionary<string, MethodDef>();

            foreach (TypeDef type in module.GetTypes())
            {
                if (engine.IsCompilerGenerated(type)) continue;
                foreach (MethodDef method in type.Methods)
                {
                    if (!engine.CanProcessMethod(method)) continue;
                    try { ProxyMethodCalls(module, method, proxyType, delegateCache); } catch { }
                }
            }
        }

        private void ProxyMethodCalls(ModuleDef module, MethodDef method, TypeDef proxyType,
            Dictionary<string, MethodDef> cache)
        {
            var il = method.Body.Instructions;
            for (int i = 0; i < il.Count; i++)
            {
                if (il[i].OpCode != DnOpCodes.Call) continue;

                var target = il[i].Operand as IMethod;
                if (target == null) continue;
                MethodDef mdCheck = target as MethodDef;
                if (mdCheck != null && engine.injectedMethods.Contains(mdCheck)) continue;

                if (mdCheck != null)
                {
                    bool isAccessible = mdCheck.IsPublic ||
                        (mdCheck.IsAssembly || mdCheck.IsFamilyOrAssembly);
                    if (!isAccessible) continue;
                }

                var targetSig = target.MethodSig;
                if (targetSig == null || targetSig.HasThis) continue;
                if (targetSig.Params.Count > 4) continue;
                if (targetSig.GenParamCount > 0) continue;

                string cacheKey = target.FullName;
                MethodDef proxy;
                if (!cache.TryGetValue(cacheKey, out proxy))
                {
                    proxy = BuildProxyMethod(module, target, proxyType);
                    if (proxy == null) continue;
                    cache[cacheKey] = proxy;
                    proxyType.Methods.Add(proxy);
                    engine.injectedMethods.Add(proxy);
                }

                il[i].Operand = proxy;
            }
        }

        private MethodDef BuildProxyMethod(ModuleDef module, IMethod target, TypeDef proxyType)
        {
            var targetSig = target.MethodSig;
            if (targetSig == null) return null;

            var proxySig = MethodSig.CreateStatic(
                targetSig.RetType,
                targetSig.Params.ToArray());

            var proxy = new MethodDefUser(engine.MakeName(),
                proxySig,
                DnMethodImplAttributes.IL | DnMethodImplAttributes.Managed,
                DnMethodAttributes.Assembly | DnMethodAttributes.Static | DnMethodAttributes.HideBySig);

            proxy.Body = new CilBody();
            var il = proxy.Body.Instructions;

            for (int i = 0; i < targetSig.Params.Count; i++)
            {
                switch (i)
                {
                    case 0: il.Add(Instruction.Create(DnOpCodes.Ldarg_0)); break;
                    case 1: il.Add(Instruction.Create(DnOpCodes.Ldarg_1)); break;
                    case 2: il.Add(Instruction.Create(DnOpCodes.Ldarg_2)); break;
                    case 3: il.Add(Instruction.Create(DnOpCodes.Ldarg_3)); break;
                }
            }

            il.Add(Instruction.Create(DnOpCodes.Call, module.Import(target)));
            il.Add(Instruction.Create(DnOpCodes.Ret));

            return proxy;
        }
    }
}
