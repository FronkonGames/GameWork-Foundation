# Attributes

Custom inspector attributes for `MonoBehaviour`, `ScriptableObject`, and other serialized types. Each attribute is paired with a property drawer in `Editor/Drawers/`.

See the live demo: [AttributesDemo.cs](../../Demos/Attributes/AttributesDemo.cs).

All attributes are stripped from builds via `[Conditional("UNITY_EDITOR")]` unless noted otherwise.

---

## Layout

### Title

Renders a bold section heading with a separator line. Use it to group related fields in the inspector.

```c#
[Title("Movement Settings")]
[SerializeField] private float speed;
```

### MessageBox

Renders a help box above a field. Supports `None`, `Info`, `Warning`, and `Error` styles. Can be applied multiple times.

```c#
[MessageBox("Values below 0 are clamped at runtime.", MessageBoxAttribute.MessageType.Warning)]
[SerializeField] private float damage;
```

### Label

Replaces the auto-generated field name with a custom label. An optional tooltip can be provided.

```c#
[Label("Movement speed", "Units per second.")]
[SerializeField] private float spd;
```

### Indent

Adds extra inspector indentation to a field. Level `0` leaves the default indent unchanged.

```c#
[Indent(1)]
[SerializeField] private string nestedOption;
```

---

## Numeric fields

All numeric attributes below work on `int` and `float` fields. Most include a reset button (↻) that restores the configured default.

### Field

A plain numeric field with a reset button.

```c#
[Field(50)]
[SerializeField] private int maxHealth;
```

### FieldLess

Only accepts values **strictly less** than the threshold. Reset value is clamped to not exceed the threshold.

```c#
[FieldLess(0, -1)]
[SerializeField] private int negativeOnly = -1;
```

### FieldLessEqual

Only accepts values **less than or equal** to the threshold.

```c#
[FieldLessEqual(0, 0)]
[SerializeField] private int nonPositive;
```

### FieldGreat

Only accepts values **strictly greater** than the threshold. Reset value is clamped to not fall below the threshold.

```c#
[FieldGreat(0, 10)]
[SerializeField] private int positiveOnly;
```

### FieldGreatEqual

Only accepts values **greater than or equal** to the threshold.

```c#
[FieldGreatEqual(10, 10)]
[SerializeField] private int atLeastTen;
```

### Slider

A min/max slider with optional reset and snap step. Values are clamped to the range.

```c#
[Slider(0.0f, 1.0f, 0.5f, 0.1f)]  // min, max, reset, snap
[SerializeField] private float volume;
```

### MinMaxSlider

A dual-handle range slider spanning two fields. Place it on the **min** field; the **max** field must be the next serialized field of the same type (usually hidden).

```c#
[MinMaxSlider(0, 100, 0, 100)]
[SerializeField] private int minLevel = 0;

[HideInInspector]
[SerializeField] private int maxLevel = 100;
```

### ProgressBar

Displays a `float` value as a colored progress bar. Optional RGB color defaults to green.

```c#
[ProgressBar(0.0f, 100.0f, 0.3f, 0.8f, 0.3f)]
[SerializeField] private float health;
```

---

## Text

### Password

Masks the input and highlights invalid lengths. Default range is 6–32 characters.

```c#
[Password]
[SerializeField] private string apiKey;

[Password(minLength: 8, maxLength: 64)]
[SerializeField] private string token;
```

### Tag

Renders Unity's tag picker for a `string` field. Defaults to `"Untagged"` when empty.

```c#
[Tag]
[SerializeField] private string targetTag;
```

---

## Paths and selection

### File

Adds a file-picker button to a `string` path field. By default paths are stored relative to the project folder.

```c#
[File]
[SerializeField] private string configPath;

[File(relativeToProject: false)]
[SerializeField] private string absolutePath;
```

### Folder

Adds a folder-picker button to a `string` path field.

```c#
[Folder]
[SerializeField] private string exportDirectory;
```

### Scene

Renders a popup of scenes in **Build Settings** for an `int` build index.

```c#
[Scene]
[SerializeField] private int menuSceneIndex;
```

### KeyCode

Renders a `KeyCode` enum popup with a button to capture the next key press.

```c#
[KeyCode]
[SerializeField] private KeyCode jumpKey = KeyCode.Space;
```

---

## Editability

### NotEditable

Shows the field but prevents editing in all modes.

```c#
[NotEditable]
[SerializeField] private int computedScore;
```

### OnlyEditableInEditor

Editable in Edit mode only; disabled during Play mode.

```c#
[OnlyEditableInEditor]
[SerializeField] private string editorNotes;
```

### OnlyEditableInPlay

Editable during Play mode only; disabled in Edit mode.

```c#
[OnlyEditableInPlay]
[SerializeField] private string runtimeDebug;
```

---

## Conditionals

All conditional attributes reference a **bool** field on the same component by name (use `nameof`).

### EnableIf

Enables the field when the condition is `true`.

```c#
[SerializeField] private bool useCustomGravity;

[EnableIf(nameof(useCustomGravity))]
[SerializeField] private float gravityScale;
```

### DisableIf

Disables the field when the condition is `true`.

```c#
[SerializeField] private bool lockSettings;

[DisableIf(nameof(lockSettings))]
[SerializeField] private float sensitivity;
```

### ShowIf

Hides the field entirely when the condition is `false`.

```c#
[SerializeField] private bool showAdvanced;

[ShowIf(nameof(showAdvanced))]
[SerializeField] private int iterations;
```

### HideIf

Hides the field when the condition is `true`.

```c#
[SerializeField] private bool hideDebugFields;

[HideIf(nameof(hideDebugFields))]
[SerializeField] private string debugInfo;
```

---

## Validation

### NotNull

Highlights the field in red when a reference-type value is `null`.

```c#
[NotNull]
[SerializeField] private GameObject playerPrefab;
```

### AssetOnly

Restricts an object reference to project assets only (excludes scene objects). Apply to `UnityEngine.Object` fields.

```c#
[AssetOnly]
[SerializeField] private GameObject prefabReference;
```

---

## Actions

### Button

Renders an inspector button that invokes a parameterless method on the target object. The decorated `string` field is not displayed, it only exists to satisfy serialization. Use a separate `[SerializeField]` field or any serialized dummy field.

```c#
[NotEditable]
[SerializeField] private int counter;

[Button("Increase", nameof(Increase))]
[SerializeField] private string increaseButton;

[Button(nameof(Reset))]
[SerializeField] private string resetButton;

private void Increase() => counter++;
private void Reset()    => counter = 0;
```

---

## Inspector visibility

### ShowInInspector

Marks a non-serialized field or property for display in a custom inspector. Use together with the [Custom Inspector](../../Editor/Inspector/README.md) base class.

```c#
[ShowInInspector]
public int RuntimeCount => activeItems.Count;
```

---

## Combining attributes

Attributes stack. A typical field might use several at once:

```c#
[Title("Combat")]
[Indent, Label("Damage multiplier"), Slider(0.0f, 5.0f, 1.0f, 0.1f), SerializeField]
private float damageMultiplier = 1.0f;
```
