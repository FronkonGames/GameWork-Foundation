# Serialization

Unity's serializer cannot persist several common .NET types (`DateTime`, `TimeSpan`, `Dictionary<TKey, TValue>`, `KeyValuePair<TKey, TValue>`). This folder provides `[Serializable]` wrappers that store data in primitive fields Unity understands, while keeping a familiar API at runtime.

All types live in `FronkonGames.GameWork.Foundation`.

---

## SerializableDateTime

Wraps a full `DateTime` (date and time) into serializable `int` fields plus a `DateTimeKind`. Use it whenever you need to save timestamps, cooldown end dates, or event schedules on a `MonoBehaviour` or `ScriptableObject`.

**Key features**

- Read-only component properties: `Year`, `Month`, `Day`, `Hour`, `Minute`, `Second`, `Millisecond`, `Kind`.
- Implicit conversion to and from `DateTime`, no manual unpacking required.
- Constructors accept either a `DateTime` or individual components.

```c#
using System;
using UnityEngine;
using FronkonGames.GameWork.Foundation;

public class EventSchedule : MonoBehaviour
{
  [SerializeField] private SerializableDateTime nextEvent;

  private void Start()
  {
    // Implicit conversion from DateTime
    nextEvent = DateTime.UtcNow.AddHours(24.0);

    // Implicit conversion to DateTime
    DateTime eventTime = nextEvent;
    if (DateTime.UtcNow >= eventTime)
      TriggerEvent();
  }

  public void SetEvent(int year, int month, int day)
  {
    nextEvent = new SerializableDateTime(year, month, day, 0, 0, 0, DateTimeKind.Utc);
  }
}
```

```c#
// Build from an existing DateTime
DateTime saved = new(2024, 6, 15, 14, 30, 45, 500, DateTimeKind.Local);
SerializableDateTime serializable = saved;   // implicit
DateTime restored = serializable;            // implicit
```

---

## SerializableTime

Wraps a `TimeSpan` duration into serializable day/hour/minute/second/millisecond fields. Ideal for cooldowns, ability durations, timers, and any elapsed-time value that must survive domain reloads or asset saves.

**Key features**

- Struct, no heap allocation when used as a field.
- Factory methods: `FromSeconds`, `FromTicks`, `FromTimeSpan`, `FromDateTime` (extracts time of day).
- Implicit conversion to `TimeSpan` and `DateTime`.
- `TotalDays`, `TotalHours`, `TotalMinutes`, `TotalSeconds`, `TotalMilliSeconds` for aggregate reads.
- `ToString()` outputs `days:hh:mm:ss.mmm`.

```c#
using UnityEngine;
using FronkonGames.GameWork.Foundation;

public class AbilityConfig : ScriptableObject
{
  [SerializeField] private SerializableTime castDuration;
  [SerializeField] private SerializableTime cooldown;

  public float CastDurationSeconds => (float)castDuration.TotalSeconds;

  private void OnEnable()
  {
    castDuration = SerializableTime.FromSeconds(1.5);
    cooldown     = SerializableTime.FromTimeSpan(System.TimeSpan.FromMinutes(30.0));
  }
}
```

```c#
// Create from different sources
SerializableTime fromSeconds = SerializableTime.FromSeconds(3661.5);  // 1h 1m 1.5s
SerializableTime fromSpan    = SerializableTime.FromTimeSpan(new System.TimeSpan(1, 2, 3, 4, 500));
SerializableTime fromTicks   = SerializableTime.FromTicks(System.TimeSpan.TicksPerHour * 5);

// Use like a TimeSpan
System.TimeSpan span = fromSpan;
double hours = fromTicks.TotalHours;  // 5
```

---

## SerializableKeyValuePair&lt;TKey, TValue&gt;

A Unity-serializable replacement for `KeyValuePair<TKey, TValue>`. Used internally by `SerializableDictionary`, but also useful on its own when you need a serializable list of pairs.

**Key features**

- `[SerializeField]` public `key` and `value` fields (also exposed via `Key` / `Value` properties).
- Implicit conversion to and from `KeyValuePair<TKey, TValue>`.
- `IEquatable` support for comparisons.

```c#
using System.Collections.Generic;
using UnityEngine;
using FronkonGames.GameWork.Foundation;

[System.Serializable]
public class LootTable
{
  [SerializeField] private List<SerializableKeyValuePair<string, float>> dropWeights = new();

  public float GetWeight(string itemId)
  {
    foreach (SerializableKeyValuePair<string, float> entry in dropWeights)
    {
      if (entry.Key == itemId)
        return entry.Value;
    }

    return 0.0f;
  }
}
```

```c#
// Implicit conversion from KeyValuePair
System.Collections.Generic.KeyValuePair<string, int> standard = new("gold", 100);
SerializableKeyValuePair<string, int> serializable = standard;

// Back to KeyValuePair
System.Collections.Generic.KeyValuePair<string, int> restored = serializable;
```

---

## SerializableDictionary&lt;TKey, TValue&gt;

A `Dictionary<TKey, TValue>` that survives Unity serialization via `ISerializationCallbackReceiver`. At save time the dictionary is flattened into a `List<SerializableKeyValuePair<…>>`; at load time the list is rebuilt into a dictionary.

**Key features**

- Full `Dictionary<TKey, TValue>` API, use `Add`, `TryGetValue`, `ContainsKey`, etc. as normal.
- Factory methods: `FromDictionary`, `FromKeyPairValueList` (accepts both standard and serializable pairs).
- On deserialize, **duplicate keys are skipped**, only the first occurrence is kept.

```c#
using System.Collections.Generic;
using UnityEngine;
using FronkonGames.GameWork.Foundation;

public class LocalizationData : ScriptableObject
{
  [SerializeField] private SerializableDictionary<string, string> entries = new();

  public string Get(string key)
  {
    if (entries.TryGetValue(key, out string value) == true)
      return value;

    return key;
  }

  public void Import(Dictionary<string, string> source)
  {
    entries = SerializableDictionary<string, string>.FromDictionary(source);
  }
}
```

```c#
// Populate at runtime
SerializableDictionary<string, int> scores = new();
scores.Add("player", 1200);
scores.Add("rival", 980);

// Simulate a save/load cycle (normally called automatically by Unity)
scores.OnBeforeSerialize();
scores.OnAfterDeserialize();

int playerScore = scores["player"];  // 1200

// Create from an existing dictionary
Dictionary<string, int> source = new() { { "a", 1 }, { "b", 2 } };
SerializableDictionary<string, int> copy =
  SerializableDictionary<string, int>.FromDictionary(source);
```

---

## Choosing the right type

| .NET type | Wrapper | Typical use |
|---|---|---|
| `DateTime` | `SerializableDateTime` | Save timestamps, scheduled events, last-login dates |
| `TimeSpan` | `SerializableTime` | Cooldowns, durations, delays, time limits |
| `KeyValuePair<TKey, TValue>` | `SerializableKeyValuePair<TKey, TValue>` | Serializable pair lists (loot tables, config rows) |
| `Dictionary<TKey, TValue>` | `SerializableDictionary<TKey, TValue>` | Lookup tables (localization, stat modifiers, ID maps) |

---

## Requirements and limitations

- `TKey` and `TValue` must themselves be Unity-serializable (primitives, strings, enums, `[Serializable]` structs/classes, `UnityEngine.Object` references, etc.).
- `SerializableDictionary` does not preserve insertion order in the inspector list (dictionary semantics apply at runtime).
- After modifying a `SerializableDictionary` in code, Unity triggers `OnBeforeSerialize` automatically on save, you do not need to call it manually unless testing.
- `SerializableDateTime.Kind` is stored but hidden in the inspector (`[HideInInspector]`).

---

## Tests

- [SerializationTests](../../../Test/Data/SerializationTests.cs)
