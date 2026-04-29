
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
    internal class ScatterAntiDebugProtection
    {
        private Obfuscation engine;
        private Random rng;

        internal ScatterAntiDebugProtection(Obfuscation eng)
        {
            engine = eng;
            rng = eng.rng;
        }

        internal void ScatterAntiDebugChecks(ModuleDef module, TypeDef modType)
        {
            var isAttached  = module.Import(typeof(System.Diagnostics.Debugger).GetProperty("IsAttached").GetGetMethod());
            var envExit     = module.Import(typeof(Environment).GetMethod("Exit", new[] { typeof(int) }));
            var envFailFast = module.Import(typeof(Environment).GetMethod("FailFast", new[] { typeof(string) }));

            int injected = 0;
            foreach (TypeDef type in module.GetTypes())
            {
                if (engine.IsCompilerGenerated(type)) continue;
                foreach (MethodDef method in type.Methods)
                {
                    if (!engine.CanProcessMethod(method)) continue;
                    if (engine.injectedMethods.Contains(method)) continue;
                    if (rng.Next(0, 4) != 0) continue;
                    if (injected >= 60) return;

                    var il = method.Body.Instructions;
                    int pos = rng.Next(0, Math.Max(1, il.Count - 2));

                    if (il[pos].OpCode == DnOpCodes.Ret) continue;

                    if (il[pos].OpCode == DnOpCodes.Leave) continue;
                    if (il[pos].OpCode == DnOpCodes.Leave_S) continue;

                    if (il[pos].OpCode == DnOpCodes.Rethrow) continue;

                    if (method.Body.HasExceptionHandlers)
                    {
                        bool isBoundary = false;
                        foreach (var eh in method.Body.ExceptionHandlers)
                        {
                            if (il[pos] == eh.HandlerStart || il[pos] == eh.HandlerEnd ||
                                il[pos] == eh.TryStart    || il[pos] == eh.TryEnd     ||
                                il[pos] == eh.FilterStart)
                            {
                                isBoundary = true;
                                break;
                            }
                        }
                        if (isBoundary) continue;
                    }

                    var skip = il[pos];

                    if (rng.Next(0, 2) == 0)
                    {
                        il.Insert(pos,     Instruction.Create(DnOpCodes.Call, isAttached));
                        il.Insert(pos + 1, Instruction.Create(DnOpCodes.Brfalse, skip));
                        il.Insert(pos + 2, Instruction.Create(DnOpCodes.Ldc_I4, -1));
                        il.Insert(pos + 3, Instruction.Create(DnOpCodes.Call, envExit));
                    }
                    else
                    {
                        il.Insert(pos,     Instruction.Create(DnOpCodes.Call, isAttached));
                        il.Insert(pos + 1, Instruction.Create(DnOpCodes.Brfalse, skip));
                        il.Insert(pos + 2, Instruction.Create(DnOpCodes.Ldstr, ""));
                        il.Insert(pos + 3, Instruction.Create(DnOpCodes.Call, envFailFast));
                    }
                    injected++;
                }
            }
        }
    }
}
