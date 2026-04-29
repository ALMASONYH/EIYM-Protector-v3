
using System;
using System.Collections.Generic;
using System.Linq;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

using DnOpCodes = dnlib.DotNet.Emit.OpCodes;

namespace MasonProtector.Core
{
    internal class ControlFlowProtection
    {
        private Obfuscation engine;
        private Random rng;

        internal ControlFlowProtection(Obfuscation eng)
        {
            engine = eng;
            rng = eng.rng;
        }

        internal void ApplyControlFlow(ModuleDef module)
        {
            foreach (TypeDef type in module.GetTypes())
            {
                if (engine.IsCompilerGenerated(type)) continue;
                foreach (MethodDef method in type.Methods)
                {
                    if (!engine.CanProcessMethod(method)) continue;
                    if (method.IsConstructor || method.IsStaticConstructor) continue;
                    if (method.Body.HasExceptionHandlers) continue;
                    if (method.Body.Instructions.Count < 4) continue;
                    if (HasAnyBranch(method.Body.Instructions)) continue;
                    if (method == module.EntryPoint) continue;
                    try
                    {
                        FlattenControlFlow(module, method);
                        method.Body.SimplifyBranches();
                        method.Body.OptimizeBranches();
                    }
                    catch { }
                }
            }
        }

        private void FlattenControlFlow(ModuleDef module, MethodDef method)
        {
            method.Body.SimplifyBranches();
            method.Body.SimplifyMacros(method.Parameters);

            var il = method.Body.Instructions;
            if (il.Count < 4) return;

            var blocks = SplitIntoBlocks(il);
            if (blocks.Count < 2) return;
            if (blocks.Count > 60) return;

            var stateVar = new Local(module.CorLibTypes.Int32);
            method.Body.Variables.Add(stateVar);

            int[] stateMap = new int[blocks.Count];
            var usedStates = new HashSet<int>();
            for (int i = 0; i < blocks.Count; i++)
            {
                int state;
                do { state = rng.Next(100, 999999); } while (usedStates.Contains(state));
                stateMap[i] = state;
                usedStates.Add(state);
            }

            var newIl = new List<Instruction>();

            newIl.Add(Instruction.Create(DnOpCodes.Ldc_I4, stateMap[0]));
            newIl.Add(Instruction.Create(DnOpCodes.Stloc, stateVar));

            var loopHead = Instruction.Create(DnOpCodes.Nop);
            var exitPoint = Instruction.Create(DnOpCodes.Ret);
            newIl.Add(loopHead);

            int[] shuffled = Enumerable.Range(0, blocks.Count).OrderBy(x => rng.Next()).ToArray();

            var blockEntries = new Instruction[blocks.Count];
            for (int si = 0; si < shuffled.Length; si++)
                blockEntries[shuffled[si]] = Instruction.Create(DnOpCodes.Nop);

            for (int si = 0; si < shuffled.Length; si++)
            {
                int blockIdx = shuffled[si];
                var block = blocks[blockIdx];

                newIl.Add(Instruction.Create(DnOpCodes.Ldloc, stateVar));
                newIl.Add(Instruction.Create(DnOpCodes.Ldc_I4, stateMap[blockIdx]));
                newIl.Add(Instruction.Create(DnOpCodes.Beq, blockEntries[blockIdx]));
            }
            newIl.Add(Instruction.Create(DnOpCodes.Br, exitPoint));

            for (int si = 0; si < shuffled.Length; si++)
            {
                int blockIdx = shuffled[si];
                var block = blocks[blockIdx];

                newIl.Add(blockEntries[blockIdx]);

                foreach (var inst in block)
                {
                    if (inst.OpCode == DnOpCodes.Ret)
                    {
                        newIl.Add(Instruction.Create(DnOpCodes.Br, exitPoint));
                    }
                    else
                    {
                        newIl.Add(inst);
                    }
                }

                int nextIdx = blockIdx + 1;
                if (nextIdx < blocks.Count)
                {
                    newIl.Add(Instruction.Create(DnOpCodes.Ldc_I4, stateMap[nextIdx]));
                    newIl.Add(Instruction.Create(DnOpCodes.Stloc, stateVar));
                }
                else
                {
                    newIl.Add(Instruction.Create(DnOpCodes.Ldc_I4, -1));
                    newIl.Add(Instruction.Create(DnOpCodes.Stloc, stateVar));
                }
                newIl.Add(Instruction.Create(DnOpCodes.Br, loopHead));
            }

            newIl.Add(exitPoint);

            il.Clear();
            foreach (var inst in newIl)
                il.Add(inst);

            method.Body.OptimizeBranches();
        }

        private List<List<Instruction>> SplitIntoBlocks(IList<Instruction> il)
        {
            int[] depths = new int[il.Count + 1];
            int d = 0;
            for (int i = 0; i < il.Count; i++)
            {
                depths[i] = d;
                d += GetStackDelta(il[i]);
                if (d < 0) d = 0;
            }
            depths[il.Count] = d;

            var blocks = new List<List<Instruction>>();
            var current = new List<Instruction>();
            int blockSize = rng.Next(2, 5);
            int count = 0;

            for (int i = 0; i < il.Count; i++)
            {
                current.Add(il[i]);
                count++;

                bool isTerminator = il[i].OpCode == DnOpCodes.Ret ||
                    il[i].OpCode == DnOpCodes.Br || il[i].OpCode == DnOpCodes.Br_S ||
                    il[i].OpCode == DnOpCodes.Throw || il[i].OpCode == DnOpCodes.Rethrow;

                bool atStackZero = depths[i + 1] == 0;

                if ((count >= blockSize && atStackZero) || isTerminator || i == il.Count - 1)
                {
                    blocks.Add(current);
                    current = new List<Instruction>();
                    count = 0;
                    blockSize = rng.Next(2, 5);
                }
            }

            if (current.Count > 0) blocks.Add(current);
            return blocks;
        }

        private int GetStackDelta(Instruction inst)
        {
            int push = 0, pop = 0;
            switch (inst.OpCode.StackBehaviourPush)
            {
                case StackBehaviour.Push0: push = 0; break;
                case StackBehaviour.Push1:
                case StackBehaviour.Pushi:
                case StackBehaviour.Pushi8:
                case StackBehaviour.Pushr4:
                case StackBehaviour.Pushr8:
                case StackBehaviour.Pushref: push = 1; break;
                case StackBehaviour.Push1_push1: push = 2; break;
                case StackBehaviour.Varpush:
                    if (inst.OpCode == DnOpCodes.Call || inst.OpCode == DnOpCodes.Callvirt || inst.OpCode == DnOpCodes.Newobj)
                    {
                        var mr = inst.Operand as IMethod;
                        if (mr != null && mr.MethodSig != null && mr.MethodSig.RetType != null &&
                            mr.MethodSig.RetType.FullName != "System.Void")
                            push = 1;
                    }
                    break;
                default: push = 0; break;
            }
            switch (inst.OpCode.StackBehaviourPop)
            {
                case StackBehaviour.Pop0: pop = 0; break;
                case StackBehaviour.Pop1:
                case StackBehaviour.Popi:
                case StackBehaviour.Popref: pop = 1; break;
                case StackBehaviour.Pop1_pop1:
                case StackBehaviour.Popi_pop1:
                case StackBehaviour.Popi_popi:
                case StackBehaviour.Popi_popi8:
                case StackBehaviour.Popi_popr4:
                case StackBehaviour.Popi_popr8:
                case StackBehaviour.Popref_pop1:
                case StackBehaviour.Popref_popi: pop = 2; break;
                case StackBehaviour.Popi_popi_popi:
                case StackBehaviour.Popref_popi_popi:
                case StackBehaviour.Popref_popi_popi8:
                case StackBehaviour.Popref_popi_popr4:
                case StackBehaviour.Popref_popi_popr8:
                case StackBehaviour.Popref_popi_popref:
                case StackBehaviour.Popref_popi_pop1: pop = 3; break;
                case StackBehaviour.Varpop:
                    if (inst.OpCode == DnOpCodes.Call || inst.OpCode == DnOpCodes.Callvirt || inst.OpCode == DnOpCodes.Newobj)
                    {
                        var m = inst.Operand as IMethod;
                        if (m != null && m.MethodSig != null)
                        {
                            pop = m.MethodSig.Params.Count;
                            if (m.MethodSig.HasThis && inst.OpCode != DnOpCodes.Newobj) pop++;
                        }
                    }
                    break;
                default: pop = 0; break;
            }
            return push - pop;
        }

        private void InjectOpaqueBranches(ModuleDef module, MethodDef method, Local stateVar)
        {
            var il = method.Body.Instructions;

            var safe = engine.FindSafeInsertPositions(il, method.Body.ExceptionHandlers);
            if (safe.Count == 0) return;

            int injectCount = Math.Min(Math.Min(il.Count / 8, 10), safe.Count);
            for (int n = 0; n < injectCount; n++)
            {
                int pick = rng.Next(0, safe.Count);
                int pos = safe[pick];
                safe.RemoveAt(pick);
                if (pos >= il.Count) continue;

                var target = il[pos];
                int opaqueVal = rng.Next(10000, 9999999);

                il.Insert(pos,     Instruction.Create(DnOpCodes.Ldc_I4, opaqueVal));
                il.Insert(pos + 1, Instruction.Create(DnOpCodes.Ldc_I4, opaqueVal));
                il.Insert(pos + 2, Instruction.Create(DnOpCodes.Xor));
                il.Insert(pos + 3, Instruction.Create(DnOpCodes.Brtrue, target));

                for (int i = 0; i < safe.Count; i++)
                    if (safe[i] >= pos) safe[i] += 4;
            }
        }

        private bool HasAnyBranch(IList<Instruction> il)
        {
            for (int i = 0; i < il.Count; i++)
            {
                var fc = il[i].OpCode.FlowControl;
                if (fc == FlowControl.Cond_Branch) return true;
                if (fc == FlowControl.Branch) return true;
                if (il[i].OpCode == DnOpCodes.Switch) return true;
            }
            return false;
        }
    }
}
