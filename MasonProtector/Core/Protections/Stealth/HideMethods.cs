
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

using DnFieldAttributes = dnlib.DotNet.FieldAttributes;
using DnMethodAttributes = dnlib.DotNet.MethodAttributes;
using DnMethodImplAttributes = dnlib.DotNet.MethodImplAttributes;
using DnTypeAttributes = dnlib.DotNet.TypeAttributes;
using DnOpCodes = dnlib.DotNet.Emit.OpCodes;

namespace MasonProtector.Core
{
    internal class HideMethodsProtection
    {
        private Obfuscation engine;
        private Random rng;

        internal HideMethodsProtection(Obfuscation eng)
        {
            engine = eng;
            rng = eng.rng;
        }

        internal void ApplyHideMethods(ModuleDef module)
        {
            var compGenTypeRef = module.Import(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute)) as TypeRef;
            var compGenRef = compGenTypeRef != null ? compGenTypeRef.Resolve() : null;
            var compGenCtor = compGenRef != null ? compGenRef.FindDefaultConstructor() : null;
            ICustomAttributeType compGenCtorRef = compGenCtor != null ? module.Import(compGenCtor) : null;

            foreach (TypeDef type in module.GetTypes())
            {
                if (engine.IsCompilerGenerated(type)) continue;
                foreach (MethodDef method in type.Methods)
                {
                    if (engine.injectedMethods.Contains(method)) continue;
                    if (method.IsConstructor) continue;

                    if (compGenCtorRef != null)
                    {
                        bool hasAttr = method.CustomAttributes.Any(
                            ca => ca.AttributeType.FullName == "System.Runtime.CompilerServices.CompilerGeneratedAttribute");
                        if (!hasAttr)
                        {
                            method.CustomAttributes.Add(new CustomAttribute(compGenCtorRef));
                        }
                    }

                    method.IsAggressiveInlining = false;

                    TryInjectBranchTrap(method);
                }
            }
        }

        private void TryInjectBranchTrap(MethodDef method)
        {
            if (method == null || !method.HasBody) return;
            if (method.IsStaticConstructor) return;
            var body = method.Body;
            if (body.Instructions == null || body.Instructions.Count < 4) return;

            var first = body.Instructions[0];
            var second = body.Instructions[1];
            if (first == null || second == null) return;

            if (IsBranchTarget(body, first) || IsBranchTarget(body, second)) return;
            if (TouchesHandler(body, first) || TouchesHandler(body, second)) return;

            var trap = new Instruction(DnOpCodes.Br, second);
            body.Instructions.Insert(1, trap);

            byte alignment = (byte)(rng.Next(0, 2) == 0 ? 1 : 2);
            body.Instructions.Insert(2, new Instruction(DnOpCodes.Unaligned, alignment));
        }

        private bool IsBranchTarget(CilBody body, Instruction probe)
        {
            foreach (var ins in body.Instructions)
            {
                var op = ins.OpCode.OperandType;
                if (op == OperandType.InlineBrTarget || op == OperandType.ShortInlineBrTarget)
                {
                    if (ins.Operand == probe) return true;
                }
                else if (op == OperandType.InlineSwitch)
                {
                    var arr = ins.Operand as Instruction[];
                    if (arr != null)
                    {
                        for (int k = 0; k < arr.Length; k++)
                            if (arr[k] == probe) return true;
                    }
                }
            }
            return false;
        }

        private bool TouchesHandler(CilBody body, Instruction probe)
        {
            if (!body.HasExceptionHandlers) return false;
            foreach (var eh in body.ExceptionHandlers)
            {
                if (eh.TryStart == probe || eh.TryEnd == probe) return true;
                if (eh.HandlerStart == probe || eh.HandlerEnd == probe) return true;
                if (eh.FilterStart == probe) return true;
            }
            return false;
        }
    }
}
