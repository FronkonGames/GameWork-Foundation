# Development Console

An in-game developer console for running debug commands at runtime. Commands are `ScriptableObject` assets you assign to a `DevelopmentConsole` component.

See the demo scene: [ConsoleDemo.unity](../../../Demos/Console/ConsoleDemo.unity).

---

## Setup

1. Create a GameObject and add the [DevelopmentConsole](./DevelopmentConsole.cs) component.
2. Create command assets via **Create > Game:Work > Development > Command > …** (or use the assets in [Commands](./Commands/)).
3. Drag the command assets into the **Commands** list on the component.

```c#
// DevelopmentConsole inspector
[SerializeField] private KeyCode showKey = KeyCode.Backslash;  // default: \
[SerializeField] private List<DevelopmentCommand> commands;
```

---

## Using the console

| Action | Input |
|---|---|
| Open console | `showKey` (default `\`) |
| Close console | `Escape`, `showKey`, **X** button, or `close` command |
| Run command | Type command and press **Enter** |
| Command history | **Up** / **Down** arrows |

Input is trimmed and lowercased before parsing. The first token is matched against each command's `Id`; remaining tokens are passed as `args` to `Execute`.

If a command returns `false`, the console logs a warning with its `Usage` string. Unknown commands log `Invalid command '…'`.

---

## Built-in commands

All built-in commands live in [Commands](./Commands/).

### quit

Exits the application. In the Editor, stops Play mode instead of quitting Unity.

| | |
|---|---|
| **Id** | `quit` |
| **Usage** | `quit` |
| **Args** | None |
| **Source** | [QuitCommand.cs](./Commands/QuitCommand.cs) |

```
quit
```

---

### close

Hides the development console without quitting the game.

| | |
|---|---|
| **Id** | `close` |
| **Usage** | `close` |
| **Args** | None |
| **Source** | [CloseCommand.cs](./Commands/CloseCommand.cs) |

```
close
```

---

### destroy

Destroys **every** `GameObject` in the scene whose name matches the argument (case-insensitive). Searches active and inactive objects via `Resources.FindObjectsOfTypeAll`. Uses `SafeDestroy`.

| | |
|---|---|
| **Id** | `destroy` |
| **Usage** | `destroy 'gameobject-name'` |
| **Args** | `name`, object name (lowercase) |
| **Source** | [DestroyCommand.cs](./Commands/DestroyCommand.cs) |

```
destroy enemy
destroy debug_cube
```

---

### gameobject

Finds the **first** `GameObject` matching the name (case-insensitive, active or inactive) and runs a sub-command on it.

| | |
|---|---|
| **Id** | `gameobject` |
| **Usage** | `gameobject 'name' destroy\|activate\|deactivate\|move\|rotate [x,y,z]` |
| **Args** | `name`, `subcommand`, optional `vector` |
| **Source** | [GameObjectCommand.cs](./Commands/GameObjectCommand.cs) |

**Sub-commands**

| Sub-command | Args | Effect |
|---|---|---|
| `destroy` |, | Destroys the object (`DestroyImmediate` in Editor, `Destroy` at runtime) |
| `activate` |, | `SetActive(true)` |
| `deactivate` |, | `SetActive(false)` |
| `move` | `x,y,z` | Sets `transform.position` |
| `rotate` | `x,y,z` | Applies euler rotation on X, then Y, then Z axes |

Vector arguments use comma-separated floats: `1.0,2.0,3.0`.

```
gameobject player activate
gameobject enemy deactivate
gameobject camera move 0,5,-10
gameobject prop rotate 0,90,0
gameobject temp destroy
```

---

### primitive

Creates a Unity primitive at the world origin, optionally at a given position.

| | |
|---|---|
| **Id** | `primitive` |
| **Usage** | `primitive cube\|sphere\|plane\|cylinder\|capsule [x,y,z]` |
| **Args** | `type`, optional `position` |
| **Source** | [PrimitiveCommand.cs](./Commands/PrimitiveCommand.cs) |

**Types:** `cube`, `sphere`, `plane`, `cylinder`, `capsule`.

```
primitive cube
primitive sphere 0,1,0
primitive capsule 2.5,0,0
```

---

## Command reference

| Command | Example | Description |
|---|---|---|
| `quit` | `quit` | Exit app / stop Play mode |
| `close` | `close` | Hide the console |
| `destroy` | `destroy enemy` | Destroy all objects named `enemy` |
| `gameobject` | `gameobject player activate` | Operate on a single named object |
| `primitive` | `primitive cube 0,1,0` | Spawn a primitive |

---

## Creating custom commands

Subclass [DevelopmentCommand](./DevelopmentCommand.cs) and implement `Execute`. Return `true` on success, `false` to show the `Usage` hint.

```c#
using UnityEngine;

[CreateAssetMenu(fileName = "GodMode", menuName = "Game:Work/Development/Command/GodMode")]
public class GodModeCommand : DevelopmentCommand
{
  public GodModeCommand()
  {
    Id = "godmode";
    Usage = "godmode on|off";
    Description = "Toggle invincibility.";
  }

  public override bool Execute(string[] args)
  {
    if (args.Length != 1)
      return false;

    bool enabled = args[0] == "on";
    PlayerHealth.Invincible = enabled;
    Log.Info($"God mode {(enabled == true ? "enabled" : "disabled")}.");

    return true;
  }
}
```

Each command asset exposes three serialized fields:

| Field | Purpose |
|---|---|
| `Id` | Token used to invoke the command (matched case-insensitively) |
| `Usage` | Shown when `Execute` returns `false` |
| `Description` | Human-readable summary (for your own reference) |

---

## API

| Type | Role |
|---|---|
| [DevelopmentConsole](./DevelopmentConsole.cs) | `MonoBehaviour` that renders the console and dispatches input |
| [DevelopmentCommand](./DevelopmentCommand.cs) | Abstract `ScriptableObject` base for commands |
| [IDevelopmentCommand](./IDevelopmentCommand.cs) | Command contract (`Id`, `Usage`, `Description`, `Execute`) |

Console appearance is configured in [Settings.DevelopmentConsole](../../Settings.cs) (`FontSize`, `Height`, `Margin`, design resolution).
