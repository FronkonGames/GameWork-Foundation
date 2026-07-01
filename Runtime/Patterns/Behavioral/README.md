# Behavioral Patterns

Behavioral patterns in `FronkonGames.GameWork.Foundation` for communication, state, and interchangeable algorithms. All implementations are generic, you supply the concrete types and game logic.

| Pattern | Folder | Role |
|---|---|---|
| Chain of Responsibility | [ChainOfResponsibility](./ChainOfResponsibility/) | Pass a request along a handler chain until one handles it |
| Command | [Command](./Command/) | Encapsulate actions with optional undo/redo |
| Mediator | [Mediator](./Mediator/) | Centralize communication between components |
| Memento | [Memento](./Memento/) | Capture and restore object state |
| Observer | [Observer](./Observer/) | Notify subscribers when something changes |
| Signal | [Signal](./Signal/) | ScriptableObject-based event bus with auto-wiring |
| State | [State](./State/) | Finite state machine with transitions |
| Strategy | [Strategy](./Strategy/) | Swap algorithms at runtime |
| Visitor | [Visitor](./Visitor/) | Add operations to visitable types without modifying them |

---

## Chain of Responsibility

**Why use it:** Decouple the sender from the handlers. The caller sends one request; each handler either processes it or forwards it down the chain. Add, remove, or reorder steps (shield → armor → health) without touching the code that fired the request. Useful for damage pipelines, input validation, event filtering, and any process that runs through a variable sequence of checks.

[Handler.cs](./ChainOfResponsibility/Handler.cs), abstract base with `SetNext` and `HandleNext`.

| Type | Description |
|---|---|
| `IHandler<TRequest, TResponse>` | Handler contract |
| `Handler<TRequest, TResponse>` | Chain management base class |

Override `Handle`. Return a result when you can handle the request; otherwise call `HandleNext(request)`.

```c#
using FronkonGames.GameWork.Foundation;

// Damage pipeline: Shield → Armor → Health
public class ShieldHandler : Handler<DamagePacket, int>
{
  public override int Handle(DamagePacket packet)
  {
    if (packet.HasShield == true)
      return packet.Amount - packet.ShieldAbsorb;

    return HandleNext(packet);
  }
}

public class HealthHandler : Handler<DamagePacket, int>
{
  public override int Handle(DamagePacket packet) => packet.Amount;
}

// Scene: CombatSystem
var shield = new ShieldHandler();
var health = new HealthHandler();
shield.SetNext(health).SetNext(/* ... */);

int remaining = shield.Handle(incomingDamage);
```

---

## Command

**Why use it:** Turn an action into an object you can store, queue, replay, or reverse. The invoker does not need to know how the action works, only that it can `Execute`, `Undo`, and `Redo`. Ideal for level editors, turn-based undo, input replay, macro recording, and deferred action queues where the same operation must be triggered from UI, AI, or network code.

[Command.cs](./Command/Command.cs), [CommandInvoker.cs](./Command/CommandInvoker.cs)

| Type | Parameters | Description |
|---|---|---|
| `Command` |, | No-arg command |
| `Command<T>` | 1 | Single-parameter command |
| `Command<T0, T1>` | 2 | Two-parameter command |
| `Command<T0, T1, T2>` | 3 | Three-parameter command |
| `CommandInvoker` |, | Execute, undo, redo stacks |

Receivers implement `ICommandReceiver` (and generic variants) with `DoAction` / `UndoAction`.

```c#
using FronkonGames.GameWork.Foundation;

public class MoveUnitReceiver : ICommandReceiver<UnityEngine.Vector3>
{
  private UnityEngine.Vector3 lastPosition;
  private readonly UnitMover unit;

  public MoveUnitReceiver(UnitMover unit) => this.unit = unit;

  public bool DoAction(UnityEngine.Vector3 target)
  {
    lastPosition = unit.Position;
    unit.MoveTo(target);
    return true;
  }

  public void UndoAction() => unit.MoveTo(lastPosition);
}

// Scene: LevelEditor
var invoker = new CommandInvoker();
var receiver = new MoveUnitReceiver(selectedUnit);
var moveCommand = new Command<UnityEngine.Vector3>(receiver, newPosition);

if (invoker.Execute(moveCommand) == true)
  RefreshGizmo();

invoker.Undo(); // revert last move
invoker.Redo(); // replay it
```

---

## Mediator

**Why use it:** Stop components from referencing each other directly. Instead of the HUD, audio, and VFX systems all knowing about combat, one mediator receives events and routes them. Adding a new listener (camera shake, analytics) means changing one class, not every sender. Use when a web of bidirectional dependencies would otherwise grow with every new feature.

[IMediator.cs](./Mediator/IMediator.cs), interface only; you implement the routing logic.

| Interface | Description |
|---|---|
| `IMediator<TMessage>` | Fire-and-forget broadcast |
| `IMediator<TRequest, TResponse>` | Request/response |

```c#
using FronkonGames.GameWork.Foundation;

public enum CombatEvent { Hit, Block, Dodge }

public class CombatMediator : IMediator<CombatEvent>
{
  private readonly HUDController hud;
  private readonly AudioDirector audio;

  public CombatMediator(HUDController hud, AudioDirector audio)
  {
    this.hud = hud;
    this.audio = audio;
  }

  public void Send(CombatEvent message)
  {
    hud.ShowCombatFeedback(message);
    audio.PlayCombatCue(message);
  }
}

// Fighters talk through the mediator, not to each other directly
combatMediator.Send(CombatEvent.Hit);
```

---

## Memento

**Why use it:** Save a snapshot of state without exposing internal fields. The originator creates and restores mementos; external code only holds opaque checkpoints. Perfect for undo stacks, level-editor history, checkpoint saves, and any workflow where you need to roll back to a previous configuration without serializing the entire object graph.

[Memento.cs](./Memento/Memento.cs)

| Type | Description |
|---|---|
| `IMemento<T>` | Read-only captured state |
| `Memento<T>` | Concrete memento holder |
| `IOriginator<T>` | Create and restore snapshots |

```c#
using FronkonGames.GameWork.Foundation;
using System.Collections.Generic;

public class LevelEditorState : IOriginator<EditorSnapshot>
{
  public EditorSnapshot Snapshot { get; set; }

  public IMemento<EditorSnapshot> CreateMemento() => new Memento<EditorSnapshot>(Snapshot);

  public void SetMemento(IMemento<EditorSnapshot> memento) => Snapshot = memento.State;
}

// Scene: LevelEditor
var editor = new LevelEditorState();
var history = new Stack<IMemento<EditorSnapshot>>();

editor.Snapshot = CaptureCurrentLayout();
history.Push(editor.CreateMemento());

// ... user edits ...
editor.Snapshot = CaptureCurrentLayout();
history.Push(editor.CreateMemento());

editor.SetMemento(history.Pop()); // undo
```

---

## Observer

**Why use it:** Notify any number of listeners when something changes, without the publisher knowing who is listening. Subscribers come and go at runtime. Replaces polling (`if (scoreChanged)`) and tight callbacks. Common for score/health updates, achievement triggers, quest progress, and any event where one source drives multiple independent reactions.

[Publisher.cs](./Observer/Publisher.cs)

| Type | Arity | Description |
|---|---|---|
| `Publisher` | 0 | Parameterless notifications |
| `Publisher<T>` | 1 | Single-value notifications |
| `Publisher<T0, T1>` | 2 | Two-value notifications |
| `Publisher<T0, T1, T2>` | 3 | Three-value notifications |

Subscribers implement `IObserver` (matching arity) with `OnNotify`.

```c#
using FronkonGames.GameWork.Foundation;

public class ScoreBoard : Publisher<int>
{
  public void PlayerScored(int points) => Notify(points);
}

public class ScoreLabel : IObserver<int>
{
  private int total;

  public void OnNotify(int points) => total += points;
}

// Scene: Arena
var scoreBoard = new ScoreBoard();
var hudLabel = new ScoreLabel();
scoreBoard.AddObserver(hudLabel);

scoreBoard.PlayerScored(100);
scoreBoard.RemoveObserver(hudLabel);
```

---

## Signal

**Why use it:** Decouple senders from receivers using ScriptableObject assets as the event channel. Unlike Observer (code-level), signals are project-wide assets that any MonoBehaviour can reference in the Inspector, no direct references needed. Auto-wiring via `[SignalSubscribe]` eliminates manual Subscribe/Unsubscribe boilerplate. Ideal for cross-system communication (player events → HUD, damage → audio, flooding → visual effects) where the sender should not know who is listening.

[ScriptableSignal.cs](./Signal/ScriptableSignal.cs), [SignalRegistry.cs](./Signal/SignalRegistry.cs), [SignalBinding.cs](./Signal/SignalBinding.cs), [SignalSubscribeAttribute.cs](./Signal/SignalSubscribeAttribute.cs), [ISignalSource.cs](./Signal/ISignalSource.cs)

| Type | Description |
|---|---|
| `ScriptableSignal` | Parameter-less event asset. Emit with `Emit()`, subscribe with `Subscribe(Action)` |
| `ScriptableSignal<T>` | 1-parameter event asset. Emit with `Emit(T)`, subscribe with `Subscribe(Action<T>)` |
| `ScriptableSignal<T0, T1>` | 2-parameter event asset |
| `ScriptableSignal<T0, T1, T2>` | 3-parameter event asset |
| `SignalRegistry` | Runtime lookup mapping signal types to their ScriptableObject assets |
| `SignalBinding` | Scans `[SignalSubscribe]` fields and auto-subscribes them via the registry |
| `SignalSubscribeAttribute` | Marks a field for auto-wiring to a signal asset |
| `ISignalSource` | Interface for bootstraps/managers that register signals into the registry |

**Workflow:**
1. Create a concrete signal class (e.g. `class PlayerJumpedSignal : ScriptableSignal {}`).
2. Create the ScriptableObject asset via **Create → FronkonGames → Signal**.
3. Create a manager MonoBehaviour implementing `ISignalSource`, assign the asset in the Inspector.
4. In consumers, mark a field with `[SignalSubscribe(typeof(PlayerJumpedSignal))]`.
5. Call `Bind(this)` in `OnEnable`, `Unbind(this)` in `OnDisable`.

```c#
using FronkonGames.GameWork.Foundation;

// 1. Define signal types (plain classes, no logic)
public class PlayerJumpedSignal : ScriptableSignal { }
public class HealthChangedSignal : ScriptableSignal<float> { }

// 2. Manager registers signals
public class SignalManager : MonoBehaviour, ISignalSource
{
  [SerializeField] private PlayerJumpedSignal playerJumped;
  [SerializeField] private HealthChangedSignal healthChanged;

  public void RegisterSignals(SignalRegistry registry)
  {
    registry.Register(playerJumped);
    registry.Register(healthChanged);
  }
}

// 3. Consumers auto-wire via attribute
public class PlayerHUD : MonoBehaviour
{
  [SignalSubscribe(typeof(PlayerJumpedSignal))]
  private bool jumped;

  [SignalSubscribe(typeof(HealthChangedSignal))]
  private float health;

  void OnEnable()  => SignalBinding.Instance.Bind(this);
  void OnDisable() => SignalBinding.Instance.Unbind(this);

  void Update()
  {
    if (jumped == true)
    {
      jumped = false; // reset flag
      ShowJumpEffect();
    }
  }
}

// 4. Player emits signals
public class Player : MonoBehaviour
{
  [SerializeField] private PlayerJumpedSignal playerJumped;
  [SerializeField] private HealthChangedSignal healthChanged;

  public void Jump()
  {
    playerJumped.Emit();
  }

  public void TakeDamage(float amount)
  {
    healthChanged.Emit(currentHealth);
  }
}
```

**Manual subscription** (without attributes):

```c#
public class AudioManager : MonoBehaviour
{
  [SerializeField] private PlayerJumpedSignal playerJumped;

  void OnEnable()  => playerJumped.Subscribe(OnPlayerJumped);
  void OnDisable() => playerJumped.Unsubscribe(OnPlayerJumped);

  private void OnPlayerJumped() => AudioSource.PlayOneShot(jumpClip);
}
```

---

## State

**Why use it:** Replace growing `switch` / `if-else` blocks with isolated state classes, each owning its own enter/update/exit logic. Transitions are declared separately with conditions, making AI and flow logic readable and testable. Use for enemy behavior (patrol → chase → flee), player states (idle → run → jump), UI flows (menu → settings → confirm), and game phases (intro → play → game over).

[StateMachine.cs](./State/StateMachine.cs), [State.cs](./State/State.cs), [Transition.cs](./State/Transition.cs), [Condition.cs](./State/Condition.cs)

| Type | Description |
|---|---|
| `State` | Base state with `OnEnter`, `OnUpdate`, `OnExit` |
| `StateMachine` | Manages current state, transitions, and `Update` |
| `Condition` | Wraps `Func<bool>` for transition guards |
| `Transition` | Links `From` → `To` when condition passes |

Call `Update()` each frame (or tick). The machine evaluates transitions from the current state in registration order, first match wins.

```c#
using FronkonGames.GameWork.Foundation;

public class PatrolState : State
{
  public override void OnEnter() => /* start patrol path */;
  public override void OnUpdate() => /* follow waypoints */;
}

public class ChaseState : State
{
  public override void OnEnter() => /* alert animation */;
  public override void OnUpdate() => /* pursue target */;
}

// Scene: Enemy_Guard
var fsm = new StateMachine();
var patrol = new PatrolState();
var chase = new ChaseState();

fsm.AddTransition(patrol, chase, () => guard.CanSeePlayer == true);
fsm.AddTransition(chase, patrol, () => guard.CanSeePlayer == false);

fsm.ChangeState(patrol);

void Update()
{
  fsm.Update();
}
```

Set `Running = false` to pause automatic transition evaluation while still calling `OnUpdate`.

---

## Strategy

**Why use it:** Swap an algorithm at runtime without changing the class that uses it. The client holds a strategy reference and calls `OnExecute`, weapon type, movement mode, or AI decision logic can change on equip, buff, or difficulty toggle. Avoids subclass explosion (`SwordAttack`, `BowAttack`, `MagicAttack`…) in favor of composable, interchangeable behaviors.

[IStrategy.cs](./Strategy/IStrategy.cs), interface only; no base class.

| Interface | Signature |
|---|---|
| `IStrategy` | `void OnExecute()` |
| `IStrategy<R>` | `R OnExecute()` |
| `IStrategy<R, T>` | `R OnExecute(T value)` |
| `IStrategy<R, T0, T1>` | `R OnExecute(T0, T1)` |
| `IStrategy<R, T0, T1, T2>` | `R OnExecute(T0, T1, T2)` |

```c#
using FronkonGames.GameWork.Foundation;

public class MeleeDamageStrategy : IStrategy<int, int>
{
  public int OnExecute(int baseDamage) => baseDamage * 2;
}

public class RangedDamageStrategy : IStrategy<int, int>
{
  public int OnExecute(int baseDamage) => baseDamage + 5;
}

public class Weapon
{
  public IStrategy<int, int> DamageStrategy { get; set; }

  public int Attack(int baseDamage) => DamageStrategy?.OnExecute(baseDamage) ?? 0;
}

// Swap behavior at runtime (power-up, weapon swap, difficulty)
var sword = new Weapon { DamageStrategy = new MeleeDamageStrategy() };
int damage = sword.Attack(10); // 20
```

---

## Visitor

**Why use it:** Add new operations to existing types without modifying their source code. Each visitor implements what happens when it meets a visitable (heal, poison, inspect, serialize). New item types only need `Accept`; new effects only need a new visitor class. Useful when you have stable entity types but frequently add new interactions, potions, traps, power-ups, debug tools.

[IVisitor.cs](./Visitor/IVisitor.cs), [IVisitable.cs](./Visitor/IVisitable.cs)

| Type | Description |
|---|---|
| `IVisitable` | Marker for types that accept visitors |
| `IVisitor<T>` where `T : IVisitable` | `Visit(T visitable)` |

The visitable type defines `Accept` and delegates to `visitor.Visit(this)`.

```c#
using FronkonGames.GameWork.Foundation;

public class PlayerHero : IVisitable
{
  public int Health { get; set; } = 100;

  public void Accept(IVisitor<PlayerHero> visitor) => visitor.Visit(this);
}

public class HealPotion : IVisitor<PlayerHero>
{
  private readonly int amount;

  public HealPotion(int amount) => this.amount = amount;

  public void Visit(PlayerHero hero) => hero.Health += amount;
}

public class DamageTrap : IVisitor<PlayerHero>
{
  public void Visit(PlayerHero hero) => hero.Health -= 25;
}

// Scene: Dungeon
var hero = new PlayerHero();
hero.Accept(new HealPotion(30));
hero.Accept(new DamageTrap());
```

---

## Tests

- [Patterns.ChainOfResponsibility.Test.cs](../../../Test/Patterns/Patterns.ChainOfResponsibility.Test.cs)
- [Patterns.Command.Test.cs](../../../Test/Patterns/Patterns.Command.Test.cs)
- [Patterns.Mediator.Test.cs](../../../Test/Patterns/Patterns.Mediator.Test.cs)
- [Patterns.Memento.Test.cs](../../../Test/Patterns/Patterns.Memento.Test.cs)
- [Patterns.Observer.Test.cs](../../../Test/Patterns/Patterns.Observer.Test.cs)
- [Patterns.Signal.Test.cs](../../../Test/Patterns/Patterns.Signal.Test.cs)
- [Patterns.State.Test.cs](../../../Test/Patterns/Patterns.State.Test.cs)
- [Patterns.Strategy.Test.cs](../../../Test/Patterns/Patterns.Strategy.Test.cs)
- [Patterns.Visitor.Test.cs](../../../Test/Patterns/Patterns.Visitor.Test.cs)
