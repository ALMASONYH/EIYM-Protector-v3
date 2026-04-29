
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
    internal class RenamerProtection
    {
        private Obfuscation engine;
        private Random rng;

        private static readonly string[] confuserMarkers = new string[]
        {
            "ConfusedByAttribute", "Confuser.Core", "ConfuserEx",
            "Dotfuscator.Attributes", "SmartAssembly.Attributes",
            "BabelObfuscatorAttribute", "EazObfuscator", "Xenocode.Client",
            "ReactorAttribute", "CryptoObfuscator", "Agile.NET",
            "MaxtoCode", "Goliath.NET", "Spices.NET", "Skater.NET"
        };

        internal RenamerProtection(Obfuscation eng)
        {
            engine = eng;
            rng = eng.rng;
        }

        internal void ApplyRenamer(ModuleDef module, PreAnalysis.AnalysisResult analysis)
        {
            var resourceRenames = new Dictionary<string, string>();

            foreach (TypeDef type in module.GetTypes())
            {
                if (type.Name == "<Module>" || type.IsGlobalModuleType) continue;
                if (type.IsRuntimeSpecialName || type.IsSpecialName) continue;
                if (engine.IsCompilerGenerated(type)) continue;

                bool safeToRenameType = !(analysis != null && analysis.SerializableTypes.Contains(type));

                string oldFullName = type.FullName;

                if (engine.cfg.RenameNamespaces)
                    AssignUniqueNamespace(type);

                if (engine.cfg.RenameTypes && safeToRenameType && CanRenameType(type))
                    type.Name = GenerateObfuscatedName();

                string newFullName = type.FullName;
                if (oldFullName != newFullName)
                {
                    string oldRes = oldFullName.Replace('/', '.') + ".resources";
                    string newRes = newFullName.Replace('/', '.') + ".resources";
                    resourceRenames[oldRes] = newRes;
                }

                if (engine.cfg.RenameMethods)
                {
                    foreach (MethodDef m in type.Methods)
                    {
                        if (CanRenameMethod(module, m))
                        {
                            m.Name = GenerateObfuscatedName();
                            foreach (var p in m.Parameters)
                            {
                                if (p.ParamDef != null && !string.IsNullOrEmpty(p.Name))
                                    p.ParamDef.Name = GenerateObfuscatedName();
                            }
                        }
                    }
                }

                if (engine.cfg.RenameFields)
                {
                    foreach (FieldDef f in type.Fields)
                    {
                        if (CanRenameField(f))
                            f.Name = GenerateObfuscatedName();
                    }
                }

                if (engine.cfg.RenameProperties)
                {
                    foreach (PropertyDef p in type.Properties)
                    {
                        if (CanRenameProperty(p))
                        {
                            string oldPropName = p.Name;
                            p.Name = GenerateObfuscatedName();

                            if (p.GetMethod != null && !p.GetMethod.IsPublic && !p.GetMethod.IsAbstract)
                                p.GetMethod.Name = GenerateObfuscatedName();
                            if (p.SetMethod != null && !p.SetMethod.IsPublic && !p.SetMethod.IsAbstract)
                                p.SetMethod.Name = GenerateObfuscatedName();

                            StripAccessedThroughProperty(type, oldPropName);
                        }
                    }
                }

                if (engine.cfg.RenameEvents)
                {
                    foreach (EventDef ev in type.Events)
                    {
                        if (CanRenameEvent(ev))
                        {
                            ev.Name = GenerateObfuscatedName();
                            if (ev.AddMethod != null && !ev.AddMethod.IsPublic && !ev.AddMethod.IsAbstract)
                                ev.AddMethod.Name = GenerateObfuscatedName();
                            if (ev.RemoveMethod != null && !ev.RemoveMethod.IsPublic && !ev.RemoveMethod.IsAbstract)
                                ev.RemoveMethod.Name = GenerateObfuscatedName();
                        }
                    }
                }
            }

            foreach (TypeDef type in module.GetTypes())
            {
                if (!engine.IsCompilerGenerated(type)) continue;
                if (engine.injectedTypes.Contains(type)) continue;
                if (type.IsGlobalModuleType || type.Name == "<Module>") continue;
                if (type.IsRuntimeSpecialName || type.IsSpecialName) continue;
                if (type.Name.StartsWith("<PrivateImplementationDetails>")) continue;
                if (type.Name.StartsWith("__StaticArrayInit")) continue;

                string oldFullName = type.FullName;

                if (engine.cfg.RenameNamespaces)
                    AssignUniqueNamespace(type);

                if (engine.cfg.RenameTypes && CanRenameType(type))
                    type.Name = GenerateObfuscatedName();

                string newFullName = type.FullName;
                if (oldFullName != newFullName)
                {
                    string oldRes = oldFullName.Replace('/', '.') + ".resources";
                    string newRes = newFullName.Replace('/', '.') + ".resources";
                    resourceRenames[oldRes] = newRes;
                }

                if (engine.cfg.RenameFields)
                {
                    foreach (FieldDef f in type.Fields)
                    {
                        if (CanRenameField(f))
                            f.Name = GenerateObfuscatedName();
                    }
                }

                if (engine.cfg.RenameMethods)
                {
                    foreach (MethodDef m in type.Methods)
                    {
                        if (CanRenameMethod(module, m))
                            m.Name = GenerateObfuscatedName();
                    }
                }

                if (engine.cfg.RenameProperties)
                {
                    foreach (PropertyDef p in type.Properties)
                    {
                        if (!CanRenameProperty(p)) continue;
                        string oldPropName = p.Name;
                        p.Name = GenerateObfuscatedName();
                        if (p.GetMethod != null && !p.GetMethod.IsPublic && !p.GetMethod.IsAbstract)
                            p.GetMethod.Name = GenerateObfuscatedName();
                        if (p.SetMethod != null && !p.SetMethod.IsPublic && !p.SetMethod.IsAbstract)
                            p.SetMethod.Name = GenerateObfuscatedName();
                        StripAccessedThroughProperty(type, oldPropName);
                    }
                }

                if (engine.cfg.RenameEvents)
                {
                    foreach (EventDef ev in type.Events)
                    {
                        if (!CanRenameEvent(ev)) continue;
                        ev.Name = GenerateObfuscatedName();
                        if (ev.AddMethod != null && !ev.AddMethod.IsPublic && !ev.AddMethod.IsAbstract)
                            ev.AddMethod.Name = GenerateObfuscatedName();
                        if (ev.RemoveMethod != null && !ev.RemoveMethod.IsPublic && !ev.RemoveMethod.IsAbstract)
                            ev.RemoveMethod.Name = GenerateObfuscatedName();
                    }
                }
            }

            foreach (var res in module.Resources)
            {
                EmbeddedResource emb = res as EmbeddedResource;
                if (emb != null && resourceRenames.ContainsKey(emb.Name))
                    emb.Name = resourceRenames[emb.Name];
            }

            if (resourceRenames.Count > 0)
                RewriteResourceLiterals(module, resourceRenames);
        }

        private static void StripAccessedThroughProperty(TypeDef type, string oldPropName)
        {
            foreach (FieldDef field in type.Fields)
            {
                for (int ci = field.CustomAttributes.Count - 1; ci >= 0; ci--)
                {
                    var ca = field.CustomAttributes[ci];
                    if (ca.AttributeType == null) continue;
                    if (ca.AttributeType.FullName !=
                        "System.Runtime.CompilerServices.AccessedThroughPropertyAttribute") continue;
                    if (ca.ConstructorArguments.Count == 0) continue;
                    object v = ca.ConstructorArguments[0].Value;
                    if (v != null && v.ToString() == oldPropName)
                    {
                        field.CustomAttributes.RemoveAt(ci);
                        break;
                    }
                }
            }
        }

        private static void RewriteResourceLiterals(ModuleDef module, Dictionary<string, string> resourceRenames)
        {
            foreach (TypeDef type in module.GetTypes())
            {
                foreach (MethodDef m in type.Methods)
                {
                    if (!m.HasBody || !m.Body.HasInstructions) continue;
                    var il = m.Body.Instructions;
                    for (int i = 0; i < il.Count; i++)
                    {
                        if (il[i].OpCode != DnOpCodes.Ldstr) continue;
                        string s = il[i].Operand as string;
                        if (string.IsNullOrEmpty(s)) continue;

                        string mapped;
                        if (s.EndsWith(".resources", StringComparison.Ordinal) &&
                            resourceRenames.TryGetValue(s, out mapped))
                        {
                            il[i].Operand = mapped;
                            continue;
                        }

                        string lookupKey = s + ".resources";
                        if (resourceRenames.TryGetValue(lookupKey, out mapped))
                        {
                            il[i].Operand = mapped.Substring(0, mapped.Length - ".resources".Length);
                        }
                    }
                }
            }
        }

        private string GenerateObfuscatedName()
        {
            int length = rng.Next(8, 32);
            return engine.GenerateStyledName(length, true);
        }

        private void AssignUniqueNamespace(TypeDef type)
        {
            if (engine.preserveNamespaceTypes.Contains(type)) return;
            if (string.IsNullOrEmpty(type.Namespace)) return;
            type.Namespace = GenerateObfuscatedName();
        }

        internal void ApplyLateInjectedRemap(ModuleDef module)
        {
            if (!engine.cfg.EnableRenamer) return;

            foreach (TypeDef type in module.GetTypes())
            {
                if (engine.preserveNamespaceTypes.Contains(type)) continue;
                if (!engine.injectedTypes.Contains(type)) continue;
                if (type.IsGlobalModuleType || type.Name == "<Module>") continue;
                if (type.IsRuntimeSpecialName || type.IsSpecialName) continue;

                if (engine.cfg.RenameNamespaces && !string.IsNullOrEmpty(type.Namespace))
                    type.Namespace = GenerateObfuscatedName();

                if (engine.cfg.RenameTypes && CanRenameType(type))
                    type.Name = GenerateObfuscatedName();

                if (engine.cfg.RenameFields)
                {
                    foreach (FieldDef f in type.Fields)
                    {
                        if (CanRenameField(f))
                            f.Name = GenerateObfuscatedName();
                    }
                }

                if (engine.cfg.RenameMethods)
                {
                    foreach (MethodDef m in type.Methods)
                    {
                        if (CanRenameMethod(module, m))
                        {
                            m.Name = GenerateObfuscatedName();
                            foreach (var p in m.Parameters)
                            {
                                if (p.ParamDef != null && !string.IsNullOrEmpty(p.Name))
                                    p.ParamDef.Name = GenerateObfuscatedName();
                            }
                        }
                    }
                }

                if (engine.cfg.RenameProperties)
                {
                    foreach (PropertyDef p in type.Properties)
                    {
                        if (!CanRenameProperty(p)) continue;
                        string oldPropName = p.Name;
                        p.Name = GenerateObfuscatedName();
                        if (p.GetMethod != null && !p.GetMethod.IsPublic && !p.GetMethod.IsAbstract)
                            p.GetMethod.Name = GenerateObfuscatedName();
                        if (p.SetMethod != null && !p.SetMethod.IsPublic && !p.SetMethod.IsAbstract)
                            p.SetMethod.Name = GenerateObfuscatedName();
                        StripAccessedThroughProperty(type, oldPropName);
                    }
                }

                if (engine.cfg.RenameEvents)
                {
                    foreach (EventDef ev in type.Events)
                    {
                        if (!CanRenameEvent(ev)) continue;
                        ev.Name = GenerateObfuscatedName();
                        if (ev.AddMethod != null && !ev.AddMethod.IsPublic && !ev.AddMethod.IsAbstract)
                            ev.AddMethod.Name = GenerateObfuscatedName();
                        if (ev.RemoveMethod != null && !ev.RemoveMethod.IsPublic && !ev.RemoveMethod.IsAbstract)
                            ev.RemoveMethod.Name = GenerateObfuscatedName();
                    }
                }
            }
        }

        private bool CanRenameType(TypeDef t)
        {
            if (t.IsRuntimeSpecialName || t.IsSpecialName) return false;
            if (t.Name.StartsWith("<")) return false;
            if (t.IsForwarder) return false;
            return true;
        }

        private bool CanRenameMethod(ModuleDef module, MethodDef m)
        {
            if (m.IsRuntimeSpecialName || m.IsSpecialName) return false;
            if (m.IsConstructor) return false;
            if (m.Name == "Main" || m.Name == "InitializeComponent") return false;
            if (m.Name.StartsWith("<")) return false;
            if (module.EntryPoint == m) return false;
            if (m.IsPinvokeImpl) return false;
            if (m.DeclaringType != null && m.DeclaringType.IsDelegate) return false;
            if (m.IsVirtual || m.IsAbstract) return false;
            if (m.DeclaringType != null && m.DeclaringType.HasGenericParameters) return false;
            return true;
        }

        private bool CanRenameField(FieldDef f)
        {
            if (f.IsRuntimeSpecialName || f.IsSpecialName) return false;
            if (f.Name.StartsWith("<")) return false;
            if (f.DeclaringType != null && f.DeclaringType.HasGenericParameters) return false;

            foreach (var ca in f.CustomAttributes)
            {
                if (ca.AttributeType != null &&
                    ca.AttributeType.FullName == "System.ThreadStaticAttribute")
                    return false;
            }

            if (f.IsLiteral && f.DeclaringType != null && f.DeclaringType.IsEnum) return false;

            if (f.IsLiteral)
            {
                foreach (var ca in f.CustomAttributes)
                {
                    if (ca.AttributeType == null) continue;
                    string an = ca.AttributeType.FullName;
                    if (an == "System.Runtime.Serialization.EnumMemberAttribute") return false;
                    if (an == "System.Xml.Serialization.XmlEnumAttribute") return false;
                    if (an == "Newtonsoft.Json.JsonPropertyAttribute") return false;
                }
            }
            return true;
        }

        private static readonly HashSet<string> propertyDangerousAttrs = new HashSet<string>(StringComparer.Ordinal)
        {
            "System.Xml.Serialization.XmlElementAttribute",
            "System.Xml.Serialization.XmlAttributeAttribute",
            "System.Xml.Serialization.XmlArrayAttribute",
            "System.Xml.Serialization.XmlArrayItemAttribute",
            "System.Runtime.Serialization.DataMemberAttribute",
            "Newtonsoft.Json.JsonPropertyAttribute",
            "System.Text.Json.Serialization.JsonPropertyNameAttribute",
            "System.ComponentModel.DataAnnotations.Schema.ColumnAttribute",
            "System.ComponentModel.BindableAttribute",
        };

        private bool CanRenameProperty(PropertyDef p)
        {
            if (p.IsRuntimeSpecialName || p.IsSpecialName) return false;
            if (p.Name.StartsWith("<")) return false;
            if (p.DeclaringType != null && p.DeclaringType.HasGenericParameters) return false;

            if (p.GetMethod != null && p.GetMethod.IsPublic &&
                (p.GetMethod.IsVirtual || p.GetMethod.IsAbstract)) return false;
            if (p.SetMethod != null && p.SetMethod.IsPublic &&
                (p.SetMethod.IsVirtual || p.SetMethod.IsAbstract)) return false;

            if (p.CustomAttributes.Count > 0)
            {
                bool isPublicAccessor = (p.GetMethod != null && p.GetMethod.IsPublic) ||
                                        (p.SetMethod != null && p.SetMethod.IsPublic);
                if (isPublicAccessor)
                {
                    foreach (var ca in p.CustomAttributes)
                    {
                        if (ca.AttributeType != null &&
                            propertyDangerousAttrs.Contains(ca.AttributeType.FullName))
                            return false;
                    }
                }
            }
            return true;
        }

        private bool CanRenameEvent(EventDef e)
        {
            if (e.IsRuntimeSpecialName || e.IsSpecialName) return false;
            if (e.Name.StartsWith("<")) return false;
            if (e.AddMethod != null && e.AddMethod.IsPublic &&
                (e.AddMethod.IsVirtual || e.AddMethod.IsAbstract)) return false;
            if (e.RemoveMethod != null && e.RemoveMethod.IsPublic &&
                (e.RemoveMethod.IsVirtual || e.RemoveMethod.IsAbstract)) return false;
            return true;
        }
    }
}
