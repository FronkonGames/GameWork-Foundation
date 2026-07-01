# Utils

Miscellaneous static helpers in `FronkonGames.GameWork.Foundation` for launch arguments and persisted player settings. No components or setup required.

| Utility | Source | Role |
|---|---|---|
| Command line | [CommandLineUtils.cs](./CommandLineUtils.cs) | Read flags and values from the process command line |
| Player prefs | [PlayerPrefsUtils.cs](./PlayerPrefsUtils.cs) | Typed `PlayerPrefs` with product-scoped keys |

---

## CommandLineUtils

**Why use it:** Read dev and QA flags at startup without parsing `Environment.GetCommandLineArgs()` yourself. Toggle god mode, skip intros, force a level, or enable debug overlays from a build's launch arguments or Unity's **Player Settings → Resolution and Presentation → Command Line Arguments**.

[CommandLineUtils.cs](./CommandLineUtils.cs)

### Format

Arguments start with `-` and are **case-insensitive**. A flag may stand alone or be followed by a value. Values cannot contain spaces.

```
-game -level 3 -godmode -username PlayerOne
```

| Method | Description |
|---|---|
| `GetArguments()` | All args except the program name |
| `HasArgument(name)` | Whether `-name` is present |
| `GetValue(name, defaultValue)` | Value after `-name`, or default if missing |

```c#
using FronkonGames.GameWork.Foundation;

// Launch: MyGame.exe -level 5 -godmode -username Hero
if (CommandLineUtils.HasArgument("godmode") == true)
  playerInvincible = true;

string levelName = CommandLineUtils.GetValue("level", defaultValue: "1");
string user = CommandLineUtils.GetValue("username", defaultValue: "Guest");
```

In the Unity Editor Test Runner, `GetArguments()` returns a fixed test set so unit tests do not depend on the real process args.

---

## PlayerPrefsUtils

**Why use it:** `PlayerPrefs` only stores `int`, `float`, and `string`. This wrapper adds `bool`, `DateTime`, `Vector2/3/4`, `Quaternion`, and `Color`, and prefixes every key with `Application.productName` so multiple games in the same Unity project do not collide on the same machine.

[PlayerPrefsUtils.cs](./PlayerPrefsUtils.cs)

### Key prefix

Internal key format: `{Application.productName}.{yourKey}`

If `productName` is empty, the raw key is used. Use short logical names (`"music_volume"`), the prefix is applied automatically.

### API

| Category | Methods |
|---|---|
| Key management | `HasKey`, `DeleteKey`, `DeleteAll` |
| Primitives | `Get/SetBool`, `Get/SetInt`, `Get/SetFloat`, `Get/SetString` |
| Unity types | `Get/SetVector2`, `Get/SetVector3`, `Get/SetVector4`, `Get/SetQuaternion`, `Get/SetColor` |
| Other | `Get/SetDateTime` (invariant culture) |

`bool` is stored as `int` (0/1). Vectors, colors, and dates are serialized to `string` via [StringExtensions](../Extensions/System/StringExtensions.cs) and [Unity extensions](../Extensions/Unity/).

```c#
using FronkonGames.GameWork.Foundation;
using UnityEngine;

// Scene: Bootstrap, restore settings on load
if (PlayerPrefsUtils.HasKey("music_volume") == true)
  audioMixer.SetFloat("Music", PlayerPrefsUtils.GetFloat("music_volume", 0.8f));
else
  PlayerPrefsUtils.SetFloat("music_volume", 0.8f);

PlayerPrefsUtils.SetBool("tutorial_done", true);
PlayerPrefsUtils.SetVector3("last_checkpoint", checkpointPosition);
PlayerPrefsUtils.SetColor("ui_accent", accentColor);

// Remember when the player last saved
PlayerPrefsUtils.SetDateTime("last_save", System.DateTime.UtcNow);
```

Call `PlayerPrefs.Save()` from Unity when you need to flush to disk immediately, these helpers delegate to `PlayerPrefs` and do not call `Save()` for you.

---

## Source files

| File | Type |
|---|---|
| [CommandLineUtils.cs](./CommandLineUtils.cs) | Launch argument parsing |
| [PlayerPrefsUtils.cs](./PlayerPrefsUtils.cs) | Typed, prefixed player preferences |

---

## Tests

- [Unity.Test.cs](../../Test/Unity/Unity.Test.cs), `CommandLine` and `PlayerPrefsUtils` tests
