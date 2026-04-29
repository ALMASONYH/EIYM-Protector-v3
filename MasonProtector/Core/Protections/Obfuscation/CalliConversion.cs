
using System;
using System.Collections.Generic;
using System.Linq;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

using DnOpCodes = dnlib.DotNet.Emit.OpCodes;

namespace MasonProtector.Core
{
    internal class CalliConversionProtection
    {
        private Obfuscation engine;
        private Random rng;

        internal CalliConversionProtection(Obfuscation eng)
        {
            engine = eng;
            rng = eng.rng;
        }

        internal void ApplyCalliConversion(ModuleDef module)
        {
            foreach (TypeDef type in module.GetTypes())
            {
                if (engine.IsCompilerGenerated(type)) continue;
                foreach (MethodDef method in type.Methods)
                {
                    if (!engine.CanProcessMethod(method)) continue;
                    try
                    {
                        ConvertCallsToCalli(module, method);
                        method.Body.SimplifyBranches();
                        method.Body.OptimizeBranches();
                    }
                    catch { }
                }
            }
        }

        private void ConvertCallsToCalli(ModuleDef module, MethodDef method)
        {
            if (method.Body.HasExceptionHandlers) return;
            var il = method.Body.Instructions;
            for (int i = 0; i < il.Count; i++)
            {
                if (il[i].OpCode != DnOpCodes.Call) continue;

                var target = il[i].Operand as IMethod;
                if (target == null) continue;
                if (target is MethodDef) continue;

                var targetSig = target.MethodSig;
                if (targetSig == null) continue;
                if (targetSig.HasThis) continue;
                if (targetSig.GenParamCount > 0) continue;
                if (targetSig.Params.Count > 4) continue;

                bool hasComplexParam = false;
                foreach (var p in targetSig.Params)
                {
                    if (p.IsByRef || p.IsPointer) { hasComplexParam = true; break; }
                }
                if (hasComplexParam) continue;

                if (rng.Next(0, 4) != 0) continue;

                var calliSig = MethodSig.CreateStatic(
                    targetSig.RetType,
                    targetSig.Params.ToArray());

                il[i].OpCode = DnOpCodes.Ldftn;
                il[i].Operand = target;
                il.Insert(i + 1, Instruction.Create(DnOpCodes.Calli, calliSig));
                i++;
            }
        }
    }
}
