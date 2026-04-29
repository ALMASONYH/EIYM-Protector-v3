
using System;
using System.Collections.Generic;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

using DnOpCodes = dnlib.DotNet.Emit.OpCodes;

namespace MasonProtector.Core
{
    internal class IntEncodingProtection
    {
        private Obfuscation engine;
        private Random rng;

        internal IntEncodingProtection(Obfuscation eng)
        {
            engine = eng;
            rng = eng.rng;
        }

        internal void ApplyIntEncoding(ModuleDef module)
        {
            foreach (TypeDef type in module.GetTypes())
            {
                if (engine.IsCompilerGenerated(type)) continue;
                foreach (MethodDef method in type.Methods)
                {
                    if (!engine.CanProcessMethod(method)) continue;
                    try
                    {
                        EncodeMethodInts(module, method);
                        method.Body.SimplifyBranches();
                        method.Body.OptimizeBranches();
                    }
                    catch { }
                }
            }

            foreach (TypeDef type in module.GetTypes())
            {
                if (engine.injectedTypes.Contains(type)) continue;
                if (engine.IsCompilerGenerated(type)) continue;
                if (!engine.IsWinFormsType(type)) continue;
                foreach (MethodDef method in type.Methods)
                {
                    if (!method.HasBody || !method.Body.HasInstructions) continue;
                    if (engine.injectedMethods.Contains(method)) continue;
                    if (method.HasGenericParameters) continue;
                    try
                    {
                        EncodeMethodInts(module, method);
                        method.Body.SimplifyBranches();
                        method.Body.OptimizeBranches();
                    }
                    catch { }
                }
            }
        }

        private void EncodeMethodInts(ModuleDef module, MethodDef method)
        {
            var il = method.Body.Instructions;
            for (int i = 0; i < il.Count; i++)
            {
                if (!engine.IsIntLoad(il[i])) continue;
                int val = engine.ExtractInt(il[i]);
                if (val == int.MinValue) continue;
                if (val >= -1 && val <= 8 && rng.Next(0, 3) != 0) continue;

                int variant = rng.Next(0, 8);
                var replacement = new List<Instruction>();

                switch (variant)
                {
                    case 0:
                        int xk = rng.Next(int.MinValue, int.MaxValue);
                        replacement.Add(Instruction.Create(DnOpCodes.Ldc_I4, xk));
                        replacement.Add(Instruction.Create(DnOpCodes.Ldc_I4, xk ^ val));
                        replacement.Add(Instruction.Create(DnOpCodes.Xor));
                        break;
                    case 1:
                        int a1 = rng.Next(100000, 9999999);
                        replacement.Add(Instruction.Create(DnOpCodes.Ldc_I4, a1));
                        replacement.Add(Instruction.Create(DnOpCodes.Ldc_I4, a1 - val));
                        replacement.Add(Instruction.Create(DnOpCodes.Sub));
                        break;
                    case 2:
                        replacement.Add(Instruction.Create(DnOpCodes.Ldc_I4, ~val));
                        replacement.Add(Instruction.Create(DnOpCodes.Not));
                        break;
                    case 3:
                        int s = rng.Next(1, 8);
                        int shifted = val << s;
                        if ((shifted >> s) == val)
                        {
                            replacement.Add(Instruction.Create(DnOpCodes.Ldc_I4, shifted));
                            replacement.Add(Instruction.Create(DnOpCodes.Ldc_I4, s));
                            replacement.Add(Instruction.Create(DnOpCodes.Shr));
                        }
                        else
                        {
                            int sfk = rng.Next(int.MinValue, int.MaxValue);
                            replacement.Add(Instruction.Create(DnOpCodes.Ldc_I4, sfk));
                            replacement.Add(Instruction.Create(DnOpCodes.Ldc_I4, sfk ^ val));
                            replacement.Add(Instruction.Create(DnOpCodes.Xor));
                        }
                        break;
                    case 4:
                        int x1 = rng.Next(int.MinValue, int.MaxValue);
                        int x2 = rng.Next(int.MinValue, int.MaxValue);
                        int x3 = val ^ x1 ^ x2;
                        replacement.Add(Instruction.Create(DnOpCodes.Ldc_I4, x1));
                        replacement.Add(Instruction.Create(DnOpCodes.Ldc_I4, x2));
                        replacement.Add(Instruction.Create(DnOpCodes.Xor));
                        replacement.Add(Instruction.Create(DnOpCodes.Ldc_I4, x3));
                        replacement.Add(Instruction.Create(DnOpCodes.Xor));
                        break;
                    case 5:
                        int b1 = rng.Next(1000, 999999);
                        int b2 = rng.Next(1000, 999999);
                        int bres = val - b1 + b2;
                        replacement.Add(Instruction.Create(DnOpCodes.Ldc_I4, b1));
                        replacement.Add(Instruction.Create(DnOpCodes.Ldc_I4, b2));
                        replacement.Add(Instruction.Create(DnOpCodes.Sub));
                        replacement.Add(Instruction.Create(DnOpCodes.Ldc_I4, bres));
                        replacement.Add(Instruction.Create(DnOpCodes.Add));
                        break;
                    case 6:
                        int mk = rng.Next(int.MinValue, int.MaxValue);
                        int p1 = val & mk;
                        int p2 = val & ~mk;
                        replacement.Add(Instruction.Create(DnOpCodes.Ldc_I4, p1));
                        replacement.Add(Instruction.Create(DnOpCodes.Ldc_I4, p2));
                        replacement.Add(Instruction.Create(DnOpCodes.Or));
                        break;
                    default:
                        int nk = rng.Next(int.MinValue, int.MaxValue);
                        replacement.Add(Instruction.Create(DnOpCodes.Ldc_I4, nk));
                        replacement.Add(Instruction.Create(DnOpCodes.Not));
                        replacement.Add(Instruction.Create(DnOpCodes.Ldc_I4, val - (~nk)));
                        replacement.Add(Instruction.Create(DnOpCodes.Add));
                        break;
                }

                il[i].OpCode = replacement[0].OpCode;
                il[i].Operand = replacement[0].Operand;
                for (int j = 1; j < replacement.Count; j++)
                    il.Insert(i + j, replacement[j]);
                i += replacement.Count - 1;
            }
        }
    }
}
