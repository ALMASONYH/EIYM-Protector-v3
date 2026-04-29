# EIYM Protector v2

**.NET Assembly Protector & Obfuscator**

Developed by **MasonGroup** (Freemasonry)
**Team:** Battal Alqhtani & Turki Alotibi

![Mason Protector](https://i.ibb.co/FqKNKDns/image.png)

---

## About

EIYM Protector is an advanced .NET assembly protector built on top of **dnlib**.
It applies **48+ independent protection layers** to .NET executables and libraries,
making static analysis, dynamic analysis, dumping, tampering, and reverse engineering
extremely difficult.

Select your `.exe` or `.dll`, choose the protections you want, and click **Protect**.
You can also **drag and drop** a file directly onto the application.

For the full technical reference (every flag, file map, what each layer does, and
the order they run in), open **[`docs/MasonBook.pdf`](docs/MasonBook.pdf)**.

---

## Feature Catalogue (48 toggles + Renamer)

### Encryption (13)
| # | Layer | What it does |
|---|---|---|
| 1 | **String Encryption** | Per-string XOR with random keys, decrypted at runtime |
| 2 | **Int Encoding** | Replaces `int` constants with XOR / ADD / SUB / double-XOR expressions |
| 3 | **Mutation Encoding** | Wraps integer math in nested mutation layers |
| 4 | **Constants Encoding** | Encodes other numeric constants (long, float, double) |
| 5 | **String Composition** | Splits strings into runtime-composed fragments |
| 6 | **Field Encryption** | Encrypts static field values; decrypted in cctor |
| 7 | **Array Encryption** | Encrypts initialized array data (`InitializeArray` blobs) |
| 8 | **Numeric Obfuscation** | Higher-order numeric obfuscation passes |
| 9 | **Method Body Encryption** | Encrypts selected method bodies with per-method keys |
| 10 | **Cross-Reference Encryption** | Encrypts method/field tokens used between methods |
| 11 | **Polymorphic Encryption** | Re-encrypts repeatedly with rotating algorithms |
| 12 | **Delegate Encryption** | Wraps direct calls behind encrypted delegate fields |
| 13 | **Runtime Encryption (RE)** | AES-256-CBC method body encryption + DynamicMethod rebuild at runtime *(see below)* |

### Obfuscation (13)
| # | Layer | What it does |
|---|---|---|
| 14 | **Control Flow** | Inserts fake conditional branches at method entry |
| 15 | **Control Flow Flattening v2** | Reroutes blocks through a dispatcher state machine |
| 16 | **Opaque Predicates** | Inserts always-true / always-false branches that confuse decompilers |
| 17 | **Branch Confusion** | Replaces `br` / `brtrue` with semantically equivalent obscured forms |
| 18 | **VM Obfuscation v1** | Converts methods to int-stack VM bytecode |
| 19 | **VM Obfuscation v2** | Converts methods to object-stack VM bytecode (handles `newobj`, refs, boxing) |
| 20 | **Code Virtualization** | Additional in-engine virtualization layer |
| 21 | **Calli Conversion** | Replaces `call` with `calli` + function pointer |
| 22 | **Local to Field** | Converts locals to static fields in `<Module>` |
| 23 | **Method Scattering** | Splits methods into chained sub-methods |
| 24 | **Method Inliner** | Inlines small methods to break call graphs |
| 25 | **Proxy Calls** | Routes calls through generated delegate proxies |
| 26 | **Reference Proxy** | Indirects member references through proxy fields |
| 27 | **Call Hiding** | Hides true call targets behind dispatcher methods |
| 28 | **Stack Underflow** | Injects stack tricks that crash naive decompilers |

### Anti-Tampering / Anti-Analysis (8)
| # | Layer | What it does |
|---|---|---|
| 29 | **Anti Debug** | 8-layer detection: `IsAttached`, `IsLogging`, process scan, timing, entry-point guard, background monitor, scattered checks, multi-exit |
| 30 | **Scattered Anti-Debug** | Injects anti-debug checks randomly into ~30% of user methods (second pass) |
| 31 | **Anti VM** | WMI hardware queries: VMware, VirtualBox, Hyper-V → exit |
| 32 | **Anti Dump** | Compiler-styled trap types (`<>c__AntiDump`, `<>f__DumpTrap`) that break MegaDumper / ExtremeDumper |
| 33 | **Anti Memory Dump** | Additional in-process anti-dump traps |
| 34 | **Anti Tamper** | Dual integrity check: file size + assembly bytes checksum |
| 35 | **Anti De4dot** | Decoy types/interfaces/attributes that crash de4dot |
| 36 | **Anti ILDasm** | Marks assembly so `ildasm` refuses to disassemble |
| 37 | **Anti Hook** | Detects API hooks installed by analysis tools |
| 38 | **Anti HTTP** | Blocks runtime HTTP traffic injected by sandboxes / monitors |

### Stealth & Metadata (10)
| # | Layer | What it does |
|---|---|---|
| 39 | **Renamer** | Random names for namespaces / types / methods / fields / properties / events (off by default) |
| 40 | **Junk Code** | Injects fake classes (3-10 fields, 5-15 methods, 2-5 props each). Configurable count (default 50, max 500) |
| 41 | **Hide Methods** | Decompiler-confusing method attributes (`DebuggerHidden`, etc.) |
| 42 | **Fake Attributes** | Misleading `CompilerGenerated` / `Obfuscation` attributes on types |
| 43 | **Watermark** | Encrypted build stamp: UTC timestamp + build ID + signature + dummy noise |
| 44 | **Token Confusion** | Corrupts token tables to mislead resolvers |
| 45 | **Invalid Metadata** | Injects metadata patterns that crash decompilers |
| 46 | **Type Scrambler** | Reorders / scrambles types in the metadata table |
| 47 | **DnSpy Crasher** | Specific patterns that crash dnSpy on load |
| 48 | **Entry Point Mover** | Relocates the entry point to obscure stub |
| — | **Resource Protection** | Satellite-assembly wrap + DeflateStream + 3-layer XOR + decoy resources (15-30 fakes) |

---

## Featured: Runtime Encryption (RE)

The flagship layer — method bodies are AES-256-CBC encrypted at build time and
replaced with stubs. Original IL is **completely removed** from the assembly.

At runtime a custom dispatcher decrypts the IL and rebuilds the method via
`System.Reflection.Emit.DynamicMethod`. The rebuilt method runs from memory
only — it never touches disk in its original form, and is invisible to reflection.

**Architecture:**
- AES-256-CBC decryptor with per-assembly random key + IV
- Full instruction-by-instruction IL rebuilder (all opcodes, branches, locals, EH, tokens)
- Delegate cache — each method is rebuilt once and cached for near-native performance
- Encrypted bodies stored via `RuntimeHelpers.InitializeArray` + FieldRVA for minimal PE overhead

**Before (original IL visible in dnSpy / ILSpy):**
```csharp
public static int Multiply(int x, int y)
{
    return x * y;
}

public static int Fibonacci(int n)
{
    if (n <= 1) return n;
    int a = 0, b = 1;
    for (int i = 2; i <= n; i++)
    {
        int temp = a + b;
        a = b;
        b = temp;
    }
    return b;
}
```

**After (only stubs visible):**
```csharp
public static int Multiply(int x, int y)
{
    return (int)VmybbmOtgN1.UXerZObhCpDJ18(
        729982902 ^ 729982903,
        new object[] { x, y }
    );
}

public static int Fibonacci(int n)
{
    return (int)VmybbmOtgN1.UXerZObhCpDJ18(
        848283082 ^ 848283087,
        new object[] { n }
    );
}
```

**Protection scope:** All eligible static methods. Constructors, `Main`, virtual,
and generic methods are skipped for CLR compatibility.

---

## Renamer

Disabled by default. When enabled, renames code elements to random strings.

| Option | What it renames |
|---|---|
| Namespaces | All namespace names |
| Types | Classes, structs, enums |
| Methods | Methods (skips ctors, `Main`, virtual, `InitializeComponent`) |
| Fields | Field names |
| Properties | Property names |
| Events | Event names |

**Settings:**
- **Length:** Random name length (5-50 chars)
- **Prefix:** Prepended to each name (default: `$MASON~`)
- **Chars:** Character set used for random names

**Safety:** Auto-skips entry points, ctors, virtual / abstract methods,
WinForms `InitializeComponent`, property accessors, event handlers, runtime
special names, serializable fields, and resource-related types.

---

## How to Use

1. **Browse** or **drag & drop** your `.exe` / `.dll`
2. Tick the protections you want (or **Select All**)
3. Configure the renamer if needed
4. Click **Protect**
5. Pick where to save the protected file

---

## v2 vs v1 — What Changed

| Aspect | v1 | v2 |
|---|---|---|
| **Protections (toggles)** | 7 | **48 + Renamer** |
| **Anti Debug** | Single `Debugger.IsAttached` check | 8-layer system + scattered second pass |
| **Anti Tamper** | — | File size + checksum dual verification |
| **Anti De4dot / ILDasm / Hook / HTTP / MemDump** | — | All available |
| **Resources** | Plain XOR | Satellite + Deflate + 3-layer XOR + decoy resources |
| **VM Obfuscation** | — | Two engines: int-stack v1 + object-stack v2 (handles `newobj` / boxing) |
| **Runtime Encryption** | — | AES-256 + `DynamicMethod` rebuild |
| **Method Body Encryption / Field Encryption / Array Encryption** | — | All available |
| **Cross-Reference / Polymorphic / Delegate / Mutation Encryption** | — | All available |
| **Calli / Local2Field / Proxy / Reference Proxy / Call Hiding / Method Scattering / Inliner** | — | All available |
| **Control Flow Flattening v2 / Opaque Predicates / Branch Confusion / Stack Underflow** | — | All available |
| **Code Virtualization** | — | Available |
| **Stealth (Junk / Watermark / Hide / Fake / TokenConfusion / InvalidMetadata / TypeScrambler / DnSpyCrasher / EntryPointMover)** | Partial | Full |
| **Renamer Safety** | Could break WinForms / resources | Smart skipping (WinForms, serializable, resources) |
| **Stability** | P/Invoke crashes on some systems | All managed, no native P/Invoke crashes |
| **Drag & Drop** | — | Available |

---

## Requirements

- .NET Framework 4.7.2+
- dnlib 4.5.0 (included under `packages/`)
- Visual Studio 2019 / 2022 (or MSBuild 15.0+ from .NET Framework)

---

## Build

```
Open MasonProtector.sln in Visual Studio
Build → Rebuild Solution (Debug or Release)
```

Or via MSBuild directly:
```
MSBuild MasonProtector.sln /p:Configuration=Release
```

---

## Project Structure

```
MasonProtector.sln
MasonProtector/
├── Builder.cs / Builder.Designer.cs / Builder.resx   # WinForms UI
├── Program.cs                                        # Entry point
├── Core/
│   ├── Obfuscation.cs                                # Pipeline orchestrator
│   ├── PreAnalysis.cs                                # Reflection / serialization detection
│   ├── PolyEngine.cs / Design.cs                     # Shared engine helpers
│   ├── Engine/ProtectionSettings.cs                  # All toggle flags
│   └── Protections/
│       ├── Anti/         (10 modules)  Debug, VM, Tamper, Dump, MemDump, ILDasm, De4dot, Hook, HTTP, ScatterAntiDebug
│       ├── Encryption/   (15 modules)  String, Int, Mutation, Constants, Composition, Field, Array, Numeric, MethodBody, CrossRef, Polymorphic, Delegate, RuntimeEnc, BodyVault(+Runtime), TypeCloner
│       ├── Obfuscation/  (15 modules)  ControlFlow, CFF2, OpaquePredicate, BranchConfusion, VMObf, CodeVirt, Calli, Local2Field, MethodScattering, MethodInliner, ProxyCalls, ReferenceProxy, CallHiding, StackUnderflow, NumericObf
│       └── Stealth/      (11 modules)  Renamer, JunkCode, Watermark, HideMethods, FakeAttributes, TokenConfusion, InvalidMetadata, TypeScrambler, MetadataConfusion, EntryPointMover, DnSpyCrasher
docs/
└── MasonBook.pdf                                     # Full technical reference
packages/
└── dnlib.4.5.0/                                      # NuGet dependency (net45)
```

---

## License

MIT License — see [LICENSE](LICENSE) for details.

---

**MasonGroup (Freemasonry)** — Battal Alqhtani & Turki Alotibi
