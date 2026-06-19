# Unit tests

More than 400 automated tests for Foundation runtime APIs. Tests live under `Test/` in per-feature assemblies that reference `FronkonGames.GameWork.Foundation` only (never the Editor assembly).

---

## Requirements

| Requirement | Version / notes |
|---|---|
| Unity Editor | **6000.0** or higher (same as the package) |
| Universal RP | **14.0.11** or higher (project dependency) |
| Test Framework | **1.1.31** or higher (`com.unity.test-framework`) |
| Platform | **Editor only** — test assemblies use `includePlatforms: ["Editor"]` |
| Frameworks | `NUnit.Framework`, `UnityEngine.TestTools` |

There is **no CLI or batch test runner** configured for this package. Run tests inside the Unity Editor with the Test Runner window.

Install or update the Test Framework from **Window > Package Manager** if the Test Runner is missing.

---

## Layout

Tests mirror runtime folders. Each area has its own `.asmdef` with `optionalUnityReferences: ["TestAssemblies"]`.

| Folder | Assembly | Covers |
|---|---|---|
| [Algorithms](./Algorithms/) | `FronkonGames.GameWork.Foundation.Algorithms.test` | Data structures (e.g. `FastList`, `ArrayList`) |
| [Check](./Check/) | `FronkonGames.GameWork.Foundation.Check.Test` | Assert extensions |
| [Components](./Components/) | `FronkonGames.GameWork.Foundation.Components.Test` | `CachedMonoBehaviour`, etc. |
| [Data](./Data/) | `FronkonGames.GameWork.Foundation.Data.Test` | Serialization types |
| [Data/Holders](./Data/Holders/) | `FronkonGames.GameWork.Foundation.Data.Holders.Test` | `Option`, holders |
| [Development](./Development/) | `FronkonGames.GameWork.Foundation.Development.Test` | Profiling utilities |
| [Extensions](./Extensions/) | `FronkonGames.GameWork.Foundation.Extensions.Test` | System and Unity extensions |
| [Math](./Math/) | `FronkonGames.GameWork.Foundation.Math.test` | `Rand`, `MathUtils`, constants |
| [Patterns](./Patterns/) | `FronkonGames.GameWork.Foundation.Patterns.Test` | Design patterns |
| [Unity](./Unity/) | `FronkonGames.GameWork.Foundation.Unity.Test` | Unity-specific helpers |

**Naming** — test files follow `{Feature}.{Method}.Test.cs` (e.g. `Patterns.Singleton.Test.cs`). Test methods use `[Test]` or `[UnityTest]` (coroutine / play-mode style checks that still run in the Editor).

---

## How to run tests

1. Open the project in Unity **6000.0+** with the Test Framework package installed.
2. Open **Window > General > Test Runner** (or **Window > Testing > Test Runner**).
3. Select the **EditMode** tab. All Foundation tests are Edit Mode tests.
4. Optionally filter by assembly or search for a feature name (e.g. `Extensions`, `Patterns`).
5. Click **Run All** to execute the full suite, or **Run Selected** for individual fixtures.

Green results mean the suite passed. Failed tests show the assertion message and stack trace in the Test Runner; expand the entry for details.

Some tests create temporary `GameObject`s or textures; they clean up in the test body or via `DestroyOnDispose` patterns. You do not need to enter Play mode for the default suite.

---

## Reporting failures

If **any test fails** on your machine, please send an email to [fronkongames@gmail.com](mailto:fronkongames@gmail.com) with:

- Unity version and platform (e.g. Windows 11, macOS 14)
- Test Runner mode (**EditMode**)
- Failed test name(s) and the error message from the Test Runner
- Steps to reproduce, if you know what triggered the failure

That helps fix environment-specific or regression issues quickly.

---

## Related

| Item | Location |
|---|---|
| Package requisites | [README.md](../README.md#-requisites) |
| Runtime APIs under test | [Runtime/](../Runtime/) folder READMEs |
| AGENTS.md test conventions | [AGENTS.md](../AGENTS.md) |
