# Custom Inspector

A base class and helpers for building custom Unity inspectors without hand-wiring every `SerializedProperty`. Draw private `[SerializeField]` fields by name, get per-field reset buttons, and reuse layout utilities and styles.

Namespace: `FronkonGames.GameWork.Foundation`. Editor assembly only.

See the demo scene: [InspectorDemo.unity](../../Demos/Inspector/InspectorDemo.unity) and [InspectorDemo.cs](../../Demos/Inspector/InspectorDemo.cs).

---

## Why use it

Unity’s default inspector shows every serialized field in declaration order. Custom inspectors give you control over grouping, labels, and conditional UI, but `EditorGUILayout` plus `SerializedProperty` boilerplate adds up fast.

`Inspector` reduces that overhead:

- **Field-by-name drawing**, pass the private field name; reflection reads and writes the value on `target`.
- **Reset buttons**, most controls include a small refresh icon that restores a default you pass (or `default` for the type).
- **Attribute-aware**, `[Label]`, `[Range]`, `[ColorUsage]`, and `[MinMaxSlider]` on the runtime field are picked up automatically.
- **Layout and chrome**, foldouts, titles, confirmation buttons, and `using`-scoped layout groups.
- **Consistent styling**, shared [`Styles`](./Styles.cs) for headers and skin-aware colors.

Use it when you want a tailored inspector for a `MonoBehaviour` or `ScriptableObject` while keeping fields private and serialized on the runtime type.

---

## Quick start

1. Keep fields **private** with `[SerializeField]` on the component (or asset) you inspect.
2. Create a custom editor class that extends [`Inspector`](./Inspector.cs), tagged with `[CustomEditor(typeof(YourType))]`.
3. Override `InspectorGUI()` and call field helpers by name.

```c#
// Runtime, FireballSpell.cs
public sealed class FireballSpell : MonoBehaviour
{
  [SerializeField, Label("Damage", "Hit damage in points")]
  private float damage = 25.0f;

  [SerializeField, Range(0.0f, 100.0f), Label("Charge %")]
  private float charge = 50.0f;

  [SerializeField]
  private bool homing = false;
}

#if UNITY_EDITOR
[CustomEditor(typeof(FireballSpell))]
public sealed class FireballSpellEditor : Inspector
{
  protected override void InspectorGUI()
  {
    Title("Fireball");

    Float("damage", 25.0f);
    Float("charge", 50.0f);
    Toggle("homing");

    if (Button("Test cast") == true)
      ((FireballSpell)target).GetComponent<FireballSpell>(); // invoke play-mode logic
  }
}
#endif
```

The base class calls `serializedObject.Update()`, your `InspectorGUI()`, `serializedObject.ApplyModifiedProperties()`, and marks the target dirty when `GUI.changed` is set.

---

## Inspector base class

| | |
|---|---|
| **Type** | `abstract partial class Inspector : Editor` |
| **Override** | `protected abstract void InspectorGUI()` |
| **Source** | [Inspector.cs](./Inspector.cs) (lifecycle), partials per value type |

### Lifecycle

`OnInspectorGUI()` (do not override):

1. `ResetGUI()`, indent, label width, field width, enabled state.
2. `serializedObject.Update()`
3. Your `InspectorGUI()`
4. `serializedObject.ApplyModifiedProperties()`
5. `DirtyGUI()` if `Changed == true`

`OnEnable()` stores a `productID` derived from the editor type name (used for foldout PlayerPrefs keys).

### GUI state

| Member | Description |
|---|---|
| `IndentLevel` | `EditorGUI.indentLevel` |
| `LabelWidth` | `EditorGUIUtility.labelWidth` |
| `FieldWidth` | `EditorGUIUtility.fieldWidth` |
| `EnableGUI` | `GUI.enabled` |
| `Changed` | `GUI.changed` |

### Layout and chrome

| Method | Description |
|---|---|
| `Separator()` | `EditorGUILayout.Separator()` |
| `Space(float)` | Vertical gap (default from `Settings.Editor.SpaceSeparation`) |
| `FlexibleSpace()` | `GUILayout.FlexibleSpace()` |
| `Line()` | Separator plus horizontal rule |
| `BeginVertical` / `EndVertical` | Vertical group |
| `BeginHorizontal` / `EndHorizontal` | Horizontal group |
| `ResetGUI(...)` | Reset indent, widths, and enabled state |
| `DirtyGUI()` | `EditorUtility.SetDirty(target)` |
| `Foldout(string title)` | Shuriken-style header foldout; state persisted in PlayerPrefs per editor type |
| `Label(string, tooltip?)` | Read-only label |
| `Button(...)` | Standard or styled button |
| `ResetButton()` | Icon button (refresh glyph from `Styles.RefreshIcon`) |
| `ConfirmationButton(...)` | Colored button; returns `true` only if the user confirms the dialog |
| `Title(string label)` | Bold label plus gray underline |

---

## Field helpers

Each helper has two shapes:

- **By field name**, `Toggle("fieldName", reset)` looks up a **private instance field** on `target` via [`GetField`](../../Runtime/Extensions/System/ReflectionExtensions.cs), draws the control, and writes back. Missing fields log a warning.
- **By value**, `Toggle(label, value, reset)` draws a control and returns the new value; you assign it yourself.

Field-name overloads use [`LabelAttribute`](../../Runtime/Attributes/LabelAttribute.cs) for label and tooltip when present; otherwise the field name is converted with `ToWords()`.

### Supported types

| Helper | Field overload | Notes |
|---|---|---|
| `Toggle` | `Toggle(string fieldName, bool reset)` | | |
| `Int` | `Int(string fieldName, int reset)` | Uses slider when field has `[Range]` | |
| `Slider` |, | `Int` / `Float` slider with reset | |
| `IntPopup` | `IntPopup(string fieldName, string[] options, int[] values, int reset)` | | |
| `Float` | `Float(string fieldName, float reset)` | Uses slider when field has `[Range]` | |
| `String` | `String(string fieldName, string reset)` | | |
| `EnumPopup` | `EnumPopup(string fieldName, Enum reset)` | | |
| `Color` | `Color(string fieldName, Color reset)` | Honors `[ColorUsage]` on field | |
| `Vector2` | `Vector2(string fieldName, Vector2 reset)` | `[MinMaxSlider]` → min/max UI | |
| `Vector2` | `Vector2(fieldName, labelX, labelY, reset)` | Per-component rows | |
| `Vector2Int` | `Vector2Int(string fieldName, Vector2Int reset)` | `[MinMaxSlider]` → int min/max | |
| `Vector3` | `Vector3(string fieldName, Vector3 reset)` | | |
| `Vector3` | `Vector3(fieldName, labelX, labelY, labelZ, reset)` | Per-component rows | |
| `Vector4` | `Vector4(string fieldName, Vector4 reset)` | | |
| `Vector4` | `Vector4(fieldName, labelX, labelY, labelZ, labelW, reset)` | Per-component rows | |
| `Texture` | `Texture(fieldName, allowSceneObjects, multiLine)` | `multiLine` uses tall object field | |
| `Object<T>` | `Object<T>(fieldName, allowSceneObjects)` | Generic Unity object reference | |

`MinMax` overloads on `Vector2` and `Vector2Int` draw min/max fields plus `EditorGUILayout.MinMaxSlider`.

Sources: [Inspector.Bool.cs](./Inspector.Bool.cs), [Inspector.Int.cs](./Inspector.Int.cs), [Inspector.Float.cs](./Inspector.Float.cs), [Inspector.String.cs](./Inspector.String.cs), [Inspector.Enum.cs](./Inspector.Enum.cs), [Inspector.Color.cs](./Inspector.Color.cs), [Inspector.Vector2.cs](./Inspector.Vector2.cs), [Inspector.Vector2Int.cs](./Inspector.Vector2Int.cs), [Inspector.Vector3.cs](./Inspector.Vector3.cs), [Inspector.Vector4.cs](./Inspector.Vector4.cs), [Inspector.Texture.cs](./Inspector.Texture.cs), [Inspector.Object.cs](./Inspector.Object.cs).

### Runtime attributes recognized on fields

| Attribute | Effect in inspector |
|---|---|
| [`Label`](../../Runtime/Attributes/LabelAttribute.cs) | Custom label and tooltip |
| `[Range]` | Int/float drawn as slider |
| `[ColorUsage]` | Alpha and HDR flags on color field |
| [`MinMaxSlider`](../../Runtime/Attributes/MinMaxSliderAttribute.cs) | Min/max slider for `Vector2` or `Vector2Int` |

More attribute-driven drawers live under [Editor/Drawers](../Drawers/); `Inspector` complements those for full custom-editor layouts.

---

## Layout groups

[`DisposableGroups.cs`](./DisposableGroups.cs) provides `using`-friendly scopes:

```c#
using (new HorizontalGroup("box"))
{
  Button("Spawn");
  Button("Clear");
}

using (new VerticalGroup(true))  // disabled group
{
  Label("Read-only section");
}

using (new IndentGroup())
{
  Float("nestedValue");
}
```

| Type | Description |
|---|---|
| `HorizontalGroup` | `EditorGUILayout.BeginHorizontal`; optional style, options, or disabled state |
| `VerticalGroup` | `EditorGUILayout.BeginVertical`; optional disabled state |
| `IndentGroup` | Increments `EditorGUI.indentLevel`; restored on `Dispose` |

---

## Styles

[`Styles`](./Styles.cs) is a static cache of editor GUI resources:

| Member | Use |
|---|---|
| `RefreshIcon` | Reset button icon |
| `Header`, `HeaderCheckbox`, `MiniLabel`, `MiniLabelButton` | Foldouts and compact labels |
| `WhiteTexture`, `BlackTexture`, `TransparentTexture` | 1×1 fills |
| `PaneOptionsIcon` | Builtin pane options glyph |
| `Splitter`, `HeaderBackground` | Skin-aware colors |
| `NormalColor`, `BackgroundColorEnabled`, `SelectedFocusedColor`, … | List/selection tints |

Use these when building custom inspector UI that should match Foundation foldouts and headers.

---

## Demo reference

[InspectorDemoEditor](../../Demos/Inspector/InspectorDemo.cs) exercises every field helper, foldouts, and a boxed button row:

```c#
protected override void InspectorGUI()
{
  Title("This is an example of Custom Inspector");

  Toggle("toggle");
  EnumPopup("messageType");
  Int("intValue");
  IntPopup("intPopup", new[] { "Klaatu", "Barada", "Nictu" }, new[] { 0, 1, 2 });
  Vector2("vector2Multi", "A", "B");
  Color("colorHDRValue");
  Texture("textureMulti", false, true);
  Object<Object>("objectValue");

  using (new HorizontalGroup("box"))
  {
    Button("Button A");
    Button("Button B");
  }
}
```

Open **Demos/Inspector/InspectorDemo** in the Editor and select the demo object to see the result.

---

## Gotchas

- **Private fields only**, `GetField` uses `BindingFlags.NonPublic | BindingFlags.Instance`. Public fields are not found; use `[SerializeField] private`.
- **Field names are exact**, pass the C# field name (`damage`), not the Inspector label from `[Label]`.
- **Reflection vs SerializedProperty**, field helpers write directly to the object. For multi-object editing edge cases or `[SerializeReference]` graphs, prefer explicit `SerializedProperty` handling for those fields.
- **Editor-only editors**, wrap custom editor classes in `#if UNITY_EDITOR` or place them in an Editor asmdef (as in the demo).
- **Foldout persistence**, foldout open/closed state is stored in PlayerPrefs per editor type and foldout title.

---

## Related

| Item | Location |
|---|---|
| Custom property drawers | [Editor/Drawers](../Drawers/) |
| Inspector attributes (`Label`, `MinMaxSlider`, …) | [Runtime/Attributes/README.md](../../Runtime/Attributes/README.md) |
| `GetField` / reflection helpers | [ReflectionExtensions.cs](../../Runtime/Extensions/System/ReflectionExtensions.cs) |
| Editor Tools (Screenshooter, etc.) | [Editor/Tools/README.md](../Tools/README.md) |
