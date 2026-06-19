# System extensions

Extension methods for .NET types in `FronkonGames.GameWork.Foundation`. Import the namespace and call methods on the extended type.

See also: [Unity extensions](../Unity/README.md).

---

## Collections and LINQ

| Class | Highlights |
|---|---|
| [ArrayExtensions](./ArrayExtensions.cs) | `Append`, `Remove`, `Shuffle`, `Sub`, `Random`, `FindClosestIndex`, `Exclude` |
| [ListExtensions](./ListExtensions.cs) | List helpers and mutations |
| [IListExtensions](./IListExtensions.cs) | `IList<T>` utilities |
| [EnumerableExtensions](./EnumerableExtensions.cs) | `Buffer`, `RollingWindow`, `TryPullFromDictionary`, `SelectWhere`, `Count` |
| [DictionaryExtensions](./DictionaryExtensions.cs) | `SelectDictionary`, `Normalize`, `SumTogether`, `RandomKey`, `GetKeysByValue` |
| [HashSetExtensions](./HashSetExtensions.cs) | Set operations |
| [EnumeratorExtensions](./EnumeratorExtensions.cs) | `ToEnumerable` |

```c#
using FronkonGames.GameWork.Foundation;

int[][] chunks = playerIds.Buffer(size: 4).ToArray();

Dictionary<string, float> weights = lootTable.Normalize();

IEnumerable<int> values = keys.TryPullFromDictionary(keyList, lookup);
```

---

## Primitives

| Class | Highlights |
|---|---|
| [IntExtensions](./IntExtensions.cs) | Clamping, snapping, range checks |
| [FloatExtensions](./FloatExtensions.cs) | `NearlyEquals`, `Snap`, `Remap`, angle helpers |
| [DoubleExtensions](./DoubleExtensions.cs) | `IsBetween`, `ApproximatelyEquals`, `Normalize`, `ToRadians` |
| [LongExtensions](./LongExtensions.cs) | Long integer helpers |
| [ByteExtensions](./ByteExtensions.cs) | `IsBitSet`, `SetBit`, `ToggleBit`, `ToBinaryString` |
| [ComparableExtensions](./ComparableExtensions.cs) | Generic comparison helpers |

---

## Strings

[StringExtensions](./StringExtensions.cs), parsing, formatting, validation, and path helpers.

| Area | Methods |
|---|---|
| Parsing | `ToInt`, `ToFloat`, `ToVector2/3/4`, `ToColor`, `ToQuaternion`, `ToBoolean` |
| Validation | `IsValidIdentifier`, `IsValidEmail`, `IsValidLatinUsername` |
| Substrings | `GetSubstringBefore/After/BeforeLast/AfterLast` |
| Paths | `ToAbsolutePath`, `ToRelativePath`, `RemoveInvalidFileCharacters` |
| Encoding | `ToBase64`, `FromBase64`, `ToMD5`, `Compress`, `Decompress`, `Encrypt`, `Decrypt` |
| Formatting | `Capitalized`, `ToCamelCase`, `ToTitleCase`, `ToWords`, `Similarity` |

```c#
Vector3 spawn = "1.0,2.0,3.0".ToVector3();

if (fieldName.IsValidIdentifier() == true)
  GenerateProperty(fieldName);
```

---

## Enums

[EnumExtensions](./EnumExtensions.cs), enum utilities and random selection (uses `Rand`).

- `GetValues<T>`, `ToInt`, `ToUInt`, `GetMaxValue`
- `HasAnyFlag`, `HasNoneOfFlags`
- `PickRandom<T>`, `PickWeighted<T>`, `PickBetween<T>`, `PickUpTo<T>`

```c#
Difficulty diff = EnumExtensions.PickWeighted<Difficulty>(new[] { 0.6f, 0.3f, 0.1f });
```

---

## Reflection and types

| Class | Highlights |
|---|---|
| [ReflectionExtensions](./ReflectionExtensions.cs) | `GetFieldAtPath`, `GetFieldRecursive`, `GetSerializedFields`, `HasAttribute<T>` |
| [TypeExtensions](./TypeExtensions.cs) | `IsUnitySerializable`, `IsSubclassOfRawGeneric`, `InheritsFrom`, `IsNullable`, `IsEmpty`, `GetShortAssemblyName` |
| [ObjectExtensions](./ObjectExtensions.cs) | General object helpers |

```c#
FieldInfo field = target.GetFieldAtPath("stats.health.max");
bool serializable = typeof(PlayerData).IsUnitySerializable();
```

---

## Functional and objects

| Class | Highlights |
|---|---|
| [FunctionalExtensions](./FunctionalExtensions.cs) | Functional composition helpers |
| [ActionExtensions](./ActionExtensions.cs) | Delegate utilities |
| [KeyValuePairExtensions](./KeyValuePairExtensions.cs) | Pair helpers |

---

## Async and cancellation

| Class | Highlights |
|---|---|
| [TaskExtensions](./TaskExtensions.cs) | Task chaining and utilities |
| [CancellationTokenSourceExtensions](./CancellationTokenSourceExtensions.cs) | CTS helpers |

---

## Disposables

[DisposableExtensions](./DisposableExtensions.cs), RAII-style cleanup via `using` statements.

| Method | Effect |
|---|---|
| `AsDisposable(Action)` | Wrap an action as `IDisposable` |
| `DestroyOnDispose` | Destroy `Object` or collections on dispose |
| `EnableThenDisable` | Temporarily enable a `GameObject` |
| `DisposeTemporaryTexture` | Return a `RenderTexture` to the pool |
| `DisposeAll` | Dispose arrays or lists of `IDisposable` |

```c#
using (gameObject.EnableThenDisable())
{
  preview.SetActive(true);
  BakeMesh();
}
```

---

## Date and time

| Class | Highlights |
|---|---|
| [DateTimeExtensions](./DateTimeExtensions.cs) | `ToUnixEpoch`, `ToUnixEpochMillis` |
| [TimeSpanExtensions](./TimeSpanExtensions.cs) | TimeSpan formatting and math |

---

## Tests

Unit tests live under [Test/Extensions](../../../Test/Extensions/).
