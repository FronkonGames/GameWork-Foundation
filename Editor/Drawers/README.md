# Property Drawers

Editor implementations for Foundation [inspector attributes](../../Runtime/Attributes/README.md). Each drawer is registered with `[CustomPropertyDrawer(typeof(...), true)]` and lives in namespace `FronkonGames.GameWork.Foundation`.

Apply attributes on runtime fields; drawers run automatically in the default Unity inspector (no custom `Editor` class required). For fully custom layouts, combine attributes with the [Custom Inspector](../Inspector/README.md) base class.

See the live demo: [AttributesDemo.unity](../../Demos/Attributes/AttributesDemo.unity) and [AttributesDemo.cs](../../Demos/Attributes/AttributesDemo.cs).

---

## Why use it

Unity draws `[SerializeField]` fields with generic controls. Foundation attributes add validation, layout, conditionals, and pickers while keeping runtime types clean.

Drawers handle the Editor side:

- **Type-safe UI**, sliders, min/max ranges, scene index popups, folder/file pickers.
- **Reset buttons**, numeric drawers expose `Styles.RefreshIcon` to restore configured defaults.
- **Conditionals**, show, hide, enable, or disable fields based on a sibling `bool`.
- **Visual feedback**, error coloring for wrong types, null references, or invalid passwords.

Attribute definitions and usage examples are in [Runtime/Attributes/README.md](../../Runtime/Attributes/README.md). This document maps each attribute to its drawer and describes Editor behavior.

---

## How drawers work

| Base class | Used for | Example |
|---|---|---|
| `PropertyDrawer` | Replaces or wraps a serialized field | `SliderPropertyDrawer` |
| `DecoratorDrawer` | Draws extra UI without owning a field value | `MessageBoxDecoratorDrawer` |

Most drawers call `EditorGUI.PropertyField` after adjusting layout, labels, or `GUI.enabled`. Numeric and slider drawers reserve 18px on the right for the reset icon button.

Shared resources:

- [`Styles.RefreshIcon`](../Inspector/Styles.cs), reset button glyph
- [`Settings.Editor`](../../Runtime/Settings.cs), spacing, error color, file-button width, title metrics

All drawers use `true` in `CustomPropertyDrawer` so attributes apply to derived attribute types as well.

---

## Attribute → drawer map

### Layout

| Attribute | Drawer | Field type | Behavior |
|---|---|---|---|
| [`Title`](../../Runtime/Attributes/TitleAttribute.cs) | [TitlePropertyDrawer.cs](./TitlePropertyDrawer.cs) | Any | Bold heading, gray rule, then default field |
| [`MessageBox`](../../Runtime/Attributes/MessageBoxAttribute.cs) | [MessageBoxDecoratorDrawer.cs](./MessageBoxDecoratorDrawer.cs) | Decorator | `EditorGUI.HelpBox` above the next field |
| [`Label`](../../Runtime/Attributes/LabelAttribute.cs) | [LabelPropertyDrawer.cs](./LabelPropertyDrawer.cs) | Any | Custom label and tooltip |
| [`Indent`](../../Runtime/Attributes/IndentAttribute.cs) | [IndentPropertyDrawer.cs](./IndentPropertyDrawer.cs) | Any | Adds `indentAttribute.level` to `EditorGUI.indentLevel` |

### Numeric (`int` / `float`)

All numeric drawers show a reset button. Wrong types render a red error label.

| Attribute | Drawer | Notes |
|---|---|---|
| [`Field`](../../Runtime/Attributes/FieldAttribute.cs) | [FieldPropertyDrawer.cs](./FieldPropertyDrawer.cs) | Plain field + reset |
| [`FieldLess`](../../Runtime/Attributes/FieldLessAttribute.cs) | [FieldLessPropertyDrawer.cs](./FieldLessPropertyDrawer.cs) | Only accepts values **&lt;** threshold |
| [`FieldLessEqual`](../../Runtime/Attributes/FieldLessEqualAttribute.cs) | [FieldLessEqualPropertyDrawer.cs](./FieldLessEqualPropertyDrawer.cs) | Only accepts values **≤** threshold |
| [`FieldGreat`](../../Runtime/Attributes/FieldGreatAttribute.cs) | [FieldGreaterPropertyDrawer.cs](./FieldGreaterPropertyDrawer.cs) | Only accepts values **&gt;** threshold |
| [`FieldGreatEqual`](../../Runtime/Attributes/FieldGreatEqualAttribute.cs) | [FieldGreaterEqualPropertyDrawer.cs](./FieldGreaterEqualPropertyDrawer.cs) | Only accepts values **≥** threshold |
| [`Slider`](../../Runtime/Attributes/SliderAttribute.cs) | [SliderPropertyDrawer.cs](./SliderPropertyDrawer.cs) | Slider with clamp and optional snap step |
| [`MinMaxSlider`](../../Runtime/Attributes/MinMaxSliderAttribute.cs) | [MinMaxSliderPropertyDrawer.cs](./MinMaxSliderPropertyDrawer.cs) | Dual-handle range on min + next field (see below) |

`MinMaxSlider` reads the decorated property as **min** and `property.GetEndProperty(true)` as **max**. The max field must be the **next serialized field of the same type** (often `[HideInInspector]`).

```c#
[MinMaxSlider(0, 100, 0, 100), SerializeField]
private int minLevel = 0;

[HideInInspector, SerializeField]
private int maxLevel = 100;
```

### Text and keys

| Attribute | Drawer | Field type | Behavior |
|---|---|---|---|
| [`Password`](../../Runtime/Attributes/PasswordAttribute.cs) | [PasswordPropertyDrawer.cs](./PasswordPropertyDrawer.cs) | `string` | Masked field; red when outside min/max length |
| [`Tag`](../../Runtime/Attributes/TagAttribute.cs) | [TagPropertyDrawer.cs](./TagPropertyDrawer.cs) | `string` | `EditorGUI.TagField`; defaults empty to `"Untagged"` |
| [`KeyCode`](../../Runtime/Attributes/KeyCodeAttribute.cs) | [KeyCodePropertyDrawer.cs](./KeyCodePropertyDrawer.cs) | `KeyCode` | Enum popup + capture-next-key button |

### Paths and scenes

| Attribute | Drawer | Field type | Behavior |
|---|---|---|---|
| [`File`](../../Runtime/Attributes/FileAttribute.cs) | [FilePropertyDrawer.cs](./FilePropertyDrawer.cs) | `string` | Text field + project icon opens `OpenFilePanel` |
| [`Folder`](../../Runtime/Attributes/FolderAttribute.cs) | [FolderPropertyDrawer.cs](./FolderPropertyDrawer.cs) | `string` | Text field + button opens `OpenFolderPanel` |
| [`Scene`](../../Runtime/Attributes/SceneAttribute.cs) | [ScenePropertyDrawer.cs](./ScenePropertyDrawer.cs) | `int` | Popup of scenes in Build Settings (`name [index]`) |

`File` and `Folder` support `relativeToProject` on the attribute to store paths under `Assets/…` via `ToRelativePath` / `ToAbsolutePath`.

### Editability

| Attribute | Drawer | Behavior |
|---|---|---|
| [`NotEditable`](../../Runtime/Attributes/NotEditableAttribute.cs) | [NotEditablePropertyDrawer.cs](./NotEditablePropertyDrawer.cs) | Field visible, `GUI.enabled = false` |
| [`OnlyEditableInEditor`](../../Runtime/Attributes/OnlyEditableInEditorAttribute.cs) | [OnlyEnableInEditPropertyDrawer.cs](./OnlyEnableInEditPropertyDrawer.cs) | Enabled when not playing |
| [`OnlyEditableInPlay`](../../Runtime/Attributes/OnlyEditableInPlayAttribute.cs) | [OnlyEnableInPlayPropertyDrawer.cs](./OnlyEnableInPlayPropertyDrawer.cs) | Enabled during Play mode |

### Conditionals

All conditionals reference a **bool** field on the same component by name (`nameof` recommended). The drawer resolves the path by replacing the current property name in `property.propertyPath`.

| Attribute | Drawer | When active |
|---|---|---|
| [`EnableIf`](../../Runtime/Attributes/EnableIfAttribute.cs) | [EnableIfPropertyDrawer.cs](./EnableIfPropertyDrawer.cs) | Enabled if condition is `true` |
| [`DisableIf`](../../Runtime/Attributes/DisableIfAttribute.cs) | [DisableIfPropertyDrawer.cs](./DisableIfPropertyDrawer.cs) | Disabled if condition is `true` |
| [`ShowIf`](../../Runtime/Attributes/ShowIfAttribute.cs) | [ShowIfPropertyDrawer.cs](./ShowIfPropertyDrawer.cs) | Visible if condition is `true`; height 0 when hidden |
| [`HideIf`](../../Runtime/Attributes/HideIfAttribute.cs) | [HideIfPropertyDrawer.cs](./HideIfPropertyDrawer.cs) | Hidden if condition is `true` |

Missing condition fields log a warning and fall back to visible/enabled.

### Validation and actions

| Attribute | Drawer | Behavior |
|---|---|---|
| [`NotNull`](../../Runtime/Attributes/NotNullAttribute.cs) | [NotNullPropertyDrawer.cs](./NotNullPropertyDrawer.cs) | Red tint when `objectReferenceValue` is null |
| [`Button`](../../Runtime/Attributes/ButtonAttribute.cs) | [ButtonPropertyDrawer.cs](./ButtonPropertyDrawer.cs) | Inspector button; invokes parameterless method via reflection |

`Button` replaces the field UI entirely. The serialized field is a dummy holder; only the button is shown. Methods must be parameterless (public or non-public).

```c#
[NotEditable, SerializeField]
private int counter;

[Button(nameof(Reset)), SerializeField]
private string resetButton;

private void Reset() => counter = 0;
```

---

## Attributes without drawers here

These runtime attributes exist but are **not** implemented in `Editor/Drawers/`:

| Attribute | Notes |
|---|---|
| [`ProgressBar`](../../Runtime/Attributes/ProgressBarAttribute.cs) | Documented in Attributes README; no drawer in this folder yet |
| [`AssetOnly`](../../Runtime/Attributes/AssetOnlyAttribute.cs) | Documented in Attributes README; no drawer in this folder yet |
| [`ShowInInspector`](../../Runtime/Attributes/ShowInInspectorAttribute.cs) | Used with [Custom Inspector](../Inspector/README.md), not a property drawer |

---

## Stacking attributes

Attributes compose on a single field. Order on the field matters for layout decorators (`MessageBox`, `Title`) and for `MinMaxSlider` pairing.

```c#
[MessageBox("Tune combat values for this enemy tier.", MessageBoxAttribute.MessageType.Info)]
[Title("Combat")]
[Indent, Label("Damage multiplier"), Slider(0.0f, 5.0f, 1.0f, 0.1f), SerializeField]
private float damageMultiplier = 1.0f;

[SerializeField]
private bool useCustomGravity;

[Indent, EnableIf(nameof(useCustomGravity)), SerializeField]
private float gravityScale;
```

Open **Demos/Attributes/AttributesDemo** in the Editor to see every drawer in one inspector.

---

## Gotchas

- **Default inspector only**, drawers apply when Unity draws `SerializedProperty` fields. A fully custom `Editor` that never calls `PropertyField` will not run these drawers unless you draw properties explicitly.
- **Condition field scope**, `EnableIf` / `ShowIf` resolve sibling fields on the same `SerializedObject`. Nested structs use property paths; condition names must match the bool field name at that nesting level.
- **Bool conditions only**, conditional drawers read `sourcePropertyValue.boolValue`. Other types are not supported.
- **Type errors**, mismatched types (e.g. `Slider` on `string`) show a red inline error instead of crashing.
- **Button field hidden**, the `Button` attribute field is not shown as a text field; decorate a dummy `string` field solely to host the attribute.
- **Scene index**, `Scene` lists `EditorBuildSettings.scenes`; scenes not in Build Settings do not appear.

---

## Related

| Item | Location |
|---|---|
| Attribute API and examples | [Runtime/Attributes/README.md](../../Runtime/Attributes/README.md) |
| Custom inspector base (alternative to drawers) | [Editor/Inspector/README.md](../Inspector/README.md) |
| Shared editor styles | [Editor/Inspector/Styles.cs](../Inspector/Styles.cs) |
| Path helpers (`ToRelativePath`, `Snap`, …) | [Runtime/Extensions](../../Runtime/Extensions/) |
