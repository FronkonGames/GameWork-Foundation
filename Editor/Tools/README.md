# Editor Tools

Editor-only utilities under **Help > Fronkon Games/Game:Work/Foundation > Tools**. Namespace: `FronkonGames.GameWork.Foundation`.

These windows and menu actions speed up day-to-day work in the Unity Editor (capturing views, batch exports, and similar tasks). They do not run in builds.

For runtime screenshot capture during Play mode, use the prototype [Screenshooter](../../Runtime/Development/Prototype/Screenshooter.cs) documented in [Prototype README](../../Runtime/Development/Prototype/README.md#capture-and-monitoring).

---

## Screenshooter

[`Screenshooter`](./Screenshooter.cs) is an `EditorWindow` for capturing the Game View, Scene View, or arbitrary scene cameras to disk or the system clipboard.

| | |
|---|---|
| **Open** | **Help > Fronkon Games/Game:Work/Foundation > Tools > Screenshooter** |
| **Type** | `EditorWindow` |
| **Source** | [Screenshooter.cs](./Screenshooter.cs) |

### Why use it

- Save marketing or QA screenshots without leaving the Editor.
- Copy the current Game or Scene View to the clipboard for quick sharing (**Windows Editor only**).
- Capture several camera POVs in one vertical strip for layout or lighting comparisons.
- Scale output with **Super Size** (1–10) for higher-resolution stills.

### Save to file

| Action | Description |
|---|---|
| **Game View PNG / JPG** | Uses `ScreenCapture.CaptureScreenshot` at the configured path and super size. |
| **Scene View PNG / JPG** | Renders the active Scene View camera into a texture and writes PNG or JPG. |
| **Open folder** | Creates the output folder if needed and reveals it in the OS file browser. |

Default output folder: `Assets/Screenshots`. Default file name pattern: `Screenshot_<date>_<time>_<index>`.

### Copy to clipboard

**Windows Editor only** (`UNITY_EDITOR_WIN`). Clipboard copy is compiled and enabled only on Windows. It depends on `System.Drawing` and `System.Windows.Forms` from [Editor/Plugins](../Plugins/) (`System.Drawing.dll`, `System.Windows.Forms.dll`), which are configured for Windows Editor in their plugin importers.

On macOS and Linux Editors, the **Copy to Clipboard** section is disabled and shows an info box. Use **Save to file** instead.

| Action | Description |
|---|---|
| **Game View to Clipboard** | Renders the first active scene `Camera` and copies a JPG to the clipboard. |
| **Scene View to Clipboard** | Captures the active Scene View and copies to the clipboard. |
| **Cameras to Clipboard** | Stacks captures from the camera list vertically and copies the combined image. |
| **Add active** | Adds the transform of the first scene `Camera` to the list. |
| **Clear** | Empties the camera list. |

A small preview thumbnail appears after a successful clipboard copy (Windows only).

### Settings

All settings persist in EditorPrefs while the window is open and when it closes.

| Setting | Default | Range / notes |
|---|---|---|
| **Super Size** | `1` | `1`–`10`. Multiplies capture width and height. |
| **Path** | `Assets/Screenshots` | Folder panel (`...`) or text field. |
| **File name** | `Screenshot_<date>_<time>_<index>` | Supports tokens (see below). |

EditorPrefs keys (under `Settings.Editor.EditorPrefs`):

- `Screenshoter.Path`
- `Screenshoter.FileName`
- `Screenshoter.SuperSize`

### File name tokens

| Token | Replaced with |
|---|---|
| `<date>` | Current date (`dd-MM-yyyy`) |
| `<time>` | Current time (`HH-mm-ss`) |
| `<view>` | `Game` or `Scene` |
| `<index>` | Auto-increment (`0000`, `0001`, …) until a free filename is found |

If `<view>` is omitted and the capture is from the Scene View, `_scene` is appended to the base name.

**Examples**

```
Screenshot_<date>_<time>_<index>     → Screenshot_19-06-2026_14-30-00_0000.png
<view>_<date>                        → Game_19-06-2026.png (Game View)
hero_cam_<index>                     → hero_cam_0000.jpg
```

### Workflow example

```
1. Open Screenshooter from the Help menu.
2. Set Path to Assets/Screenshots and Super Size to 2 for 2× resolution.
3. Play the scene and click "Game View PNG file".
4. Click "Open folder" to review captures in the Project or OS browser.
```

For multiple camera angles in one image (Windows Editor):

```
1. Add each camera transform to the list (or use "Add active" for the main camera).
2. Click "Cameras to Clipboard" and paste into your image editor or chat.
```

---

## Related

| Item | Location |
|---|---|
| Runtime async capture (`Ctrl+C`, PNG/JPG/TGA) | [Prototype/Screenshooter.cs](../../Runtime/Development/Prototype/Screenshooter.cs) |
| Refresh package version / open repo | [FoundationMenus.cs](../FoundationMenus.cs) (**Help > … > Tools > Refresh version**, **Open repository**) |
| EditorPrefs extensions used by Screenshooter | [StringExtensions.cs](../../Runtime/Extensions/System/StringExtensions.cs), [IntExtensions.cs](../../Runtime/Extensions/System/IntExtensions.cs) |
| Windows clipboard DLLs | [Editor/Plugins](../Plugins/) (`System.Drawing.dll`, `System.Windows.Forms.dll`) |
