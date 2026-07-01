# Structural Patterns

Structural patterns in `FronkonGames.GameWork.Foundation` for composing objects and swapping behavior at runtime. Both implementations are generic.

| Pattern | Folder | Role |
|---|---|---|
| Composite | [Composite](./Composite/) | Tree structures with parent/child management |
| Decorator | [Decorator](./Decorator/) | Attach interchangeable behavior to a component |

---

## Composite

**Why use it:** Treat a single item and a group of items the same way. A menu node and a menu subtree both support `Add`, `Remove`, and traversal, callers work with the root without knowing how deep the tree goes. Avoids duplicating parent/child logic in every node type. Use for UI menus, behavior-tree nodes, quest hierarchies, inventory folders, or any structure where components nest inside components.

[Composite.cs](./Composite/Composite.cs)

| Type | Description |
|---|---|
| `IComposite<T>` | Composite contract |
| `Composite<T>` | Base with child list, add/remove, indexed access |

Subclass `Composite<T>` (often with `T` being the same type) to build trees. Override `Add` / `Remove` when you need cascade logic or validation.

| Member | Description |
|---|---|
| `Count` | Number of direct children |
| `HasChildren` | Whether any children exist |
| `Add(T child)` | Append a child |
| `Remove(T child)` | Remove a child (returns success) |
| `GetChild(int index)` | Direct child by index |
| `GetChildren()` | Read-only view of children |

```c#
using FronkonGames.GameWork.Foundation;

public class MenuNode : Composite<MenuNode>
{
  public string Label;

  public MenuNode(string label) => Label = label;

  public void Draw()
  {
    RenderLabel(Label);

    for (int i = 0; i < Count; ++i)
      GetChild(i).Draw();
  }
}

// Scene: UI_MainMenu
var root = new MenuNode("Main");
var play = new MenuNode("Play");
var settings = new MenuNode("Settings");
var audio = new MenuNode("Audio");

root.Add(play);
root.Add(settings);
settings.Add(audio);

root.Draw(); // renders root and all descendants
```

Use `Composite<T>` when children share the same interface but you need a lightweight tree without Unity's `Transform` hierarchy.

---

## Decorator

**Why use it:** Change what an object *does* at runtime without subclassing it for every variant. Assign a different `Decorator` to swap behavior, critical hits, armor penetration, speed boosts, while the host class stays unchanged. This implementation swaps one decorator at a time (not a nested stack), which fits buffs and modifiers that replace each other rather than accumulate.

[Decorable.cs](./Decorator/Decorable.cs), [IDecorator.cs](./Decorator/IDecorator.cs)

| Type | Signature | Description |
|---|---|---|
| `Decorable` | `void Operation()` | Delegates to `IDecorator` |
| `Decorable<R>` | `R Operation()` | Returns decorator result or `default` |
| `Decorable<R, T>` | `R Operation(T)` | One-parameter operation |
| `Decorable<R, T0, T1>` | `R Operation(T0, T1)` | Two-parameter operation |

Matching `IDecorator` interfaces implement `Operation`. Assign `Decorator` at runtime to swap behavior without subclassing the host.

```c#
using FronkonGames.GameWork.Foundation;

public class DamageCalculator : Decorable<int, int>
{
}

public class CriticalHitDecorator : IDecorator<int, int>
{
  public int Operation(int baseDamage) => baseDamage * 2;
}

public class ArmorPenetrationDecorator : IDecorator<int, int>
{
  public int Operation(int baseDamage) => baseDamage + 10;
}

// Scene: CombatSystem
var calculator = new DamageCalculator();

calculator.Decorator = new CriticalHitDecorator();
int critDamage = calculator.Operation(15); // 30

calculator.Decorator = new ArmorPenetrationDecorator();
int pierceDamage = calculator.Operation(15); // 25
```

Unlike classic decorator stacks that wrap instances, this implementation swaps a single `Decorator` reference, ideal for power-ups, buffs, difficulty modifiers, or AI behavior profiles that replace each other rather than nest.

---

## Tests

- [Patterns.Composite.Test.cs](../../../Test/Patterns/Patterns.Composite.Test.cs)
- [Patterns.Decorator.Test.cs](../../../Test/Patterns/Patterns.Decorator.Test.cs)
