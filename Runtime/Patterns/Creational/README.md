# Creational Patterns

Creational patterns in `FronkonGames.GameWork.Foundation` for object construction, global access, and service registration. All implementations are generic, subclass or implement the interfaces with your game types.

| Pattern | Folder | Role |
|---|---|---|
| Builder | [Builder](./Builder/) | Fluent step-by-step product construction |
| Factory | [Factory](./Factory/) | Create products by key from registered creators |
| Service Locator | [ServiceLocator](./ServiceLocator/) | Thread-safe registry with dependency checks |
| Singleton | [Singleton](./Singleton/) | Lazy, thread-safe single instances |

---

## Builder

**Why use it:** Construct complex objects step by step with readable, self-documenting code. Optional fields, defaults, and validation can live in fluent methods instead of constructors with many overloads. Use when a type has many configurable properties, spells, quests, dialogue lines, procedural room configs, and you want `Create().WithX().WithY().Build()` instead of a 12-parameter constructor.

[Builder.cs](./Builder/Builder.cs)

| Type | Description |
|---|---|
| `IBuilder<TBuilder, TProduct>` | Builder contract |
| `Builder<TBuilder, TProduct>` | CRTP base with `Create()` and `Build()` |

Subclass with fluent methods that return `this`. The curiously recurring template pattern (`TBuilder : Builder<TBuilder, TProduct>`) enables chaining without casts.

```c#
using FronkonGames.GameWork.Foundation;

public class FireballSpell
{
  public float Damage;
  public float Speed;
  public UnityEngine.Color Color;
}

public class FireballBuilder : Builder<FireballBuilder, FireballSpell>
{
  private readonly FireballSpell spell = new();

  public FireballBuilder WithDamage(float damage)
  {
    spell.Damage = damage;
    return this;
  }

  public FireballBuilder WithSpeed(float speed)
  {
    spell.Speed = speed;
    return this;
  }

  public FireballBuilder WithColor(UnityEngine.Color color)
  {
    spell.Color = color;
    return this;
  }

  public override FireballSpell Build() => spell;
}

// Scene: SpellCaster
FireballSpell fireball = FireballBuilder.Create()
  .WithDamage(25.0f)
  .WithSpeed(12.0f)
  .WithColor(UnityEngine.Color.red)
  .Build();
```

---

## Factory

**Why use it:** Hide concrete types behind a key and a shared interface. Callers ask for `EnemyType.Grunt` or `"bolt"` without knowing which class gets instantiated. Centralizes creation logic, register new types in one place when content expands. Ideal for wave spawners, loot tables, ability systems, and any code that creates objects from data (JSON, ScriptableObjects, network messages).

[Factory.cs](./Factory/Factory.cs)

| Type | Description |
|---|---|
| `Factory<TKey, TProduct>` | Key → parameterless creator |
| `Factory<TKey, TProduct, TParam>` | Key → creator with one parameter |
| `IFactory<...>` | Factory contracts |

Register creators in the constructor with `Register(key, creator)`. Unregistered keys throw `KeyNotFoundException`.

```c#
using FronkonGames.GameWork.Foundation;

public enum EnemyType { Grunt, Archer, Boss }

public interface IEnemy { void SpawnAt(UnityEngine.Vector3 position); }

public class GruntEnemy : IEnemy
{
  public void SpawnAt(UnityEngine.Vector3 position) { /* ... */ }
}

public class EnemyFactory : Factory<EnemyType, IEnemy>
{
  public EnemyFactory()
  {
    Register(EnemyType.Grunt, () => new GruntEnemy());
    Register(EnemyType.Archer, () => new ArcherEnemy());
    Register(EnemyType.Boss, () => new BossEnemy());
  }
}

// Scene: WaveSpawner
var factory = new EnemyFactory();
IEnemy enemy = factory.Create(EnemyType.Grunt);
enemy.SpawnAt(spawnPoint.position);
```

Parameterized factory:

```c#
public class ProjectileFactory : Factory<string, Projectile, float>
{
  public ProjectileFactory()
  {
    Register("arrow", speed => new ArrowProjectile(speed));
    Register("bolt", speed => new BoltProjectile(speed));
  }
}

Projectile bolt = new ProjectileFactory().Create("bolt", speed: 20.0f);
```

---

## Service Locator

**Why use it:** Register services once at bootstrap and retrieve them anywhere by type, with enforced dependency order. A service cannot register until its dependencies are already initialized, which catches setup mistakes early. Use for cross-scene systems (audio, save, analytics) where you need a single registry instead of passing references through every constructor. Prefer dependency injection for larger projects; Service Locator fits rapid prototyping and small-to-medium Unity games.

[ServiceLocator.cs](./ServiceLocator/ServiceLocator.cs), [Service.cs](./ServiceLocator/Service.cs), [ScriptableService.cs](./ServiceLocator/ScriptableService.cs)

| Type | Description |
|---|---|
| `IServiceLocator` | Register, get, unregister services |
| `ServiceLocator` | Thread-safe `ConcurrentDictionary` implementation |
| `IService` | Lifecycle: `OnRegister`, `OnUnregister`, `GetDependencies` |
| `Service` | Plain C# service base |
| `ScriptableService` | `ScriptableObject` service base |
| `ServiceStatus` | `NotInitialized`, `Initialized`, `Failed` |

Registration rules:

- Service must be `NotInitialized` before registering.
- All dependencies must already be registered and `Initialized`.
- Duplicate registration logs a warning and is ignored.
- `OnRegister` sets status to `Initialized`; `OnUnregister` resets it.

```c#
using FronkonGames.GameWork.Foundation;
using System;
using System.Collections.Generic;

public class AudioService : Service { /* PlaySfx, PlayMusic */ }

public class SaveService : Service
{
  public override List<Type> GetDependencies() => new() { typeof(AudioService) };
}

// Scene: Bootstrap (early execution order)
var locator = new ServiceLocator();
locator.Register(new AudioService());
locator.Register(new SaveService());

AudioService audio = (AudioService)locator.Get<AudioService>();
audio.PlaySfx("menu_open");

locator.UnregisterAll();
```

For `ScriptableService`, create assets in the project and register them the same way, useful when service config lives in the Inspector.

---

## Singleton

**Why use it:** Guarantee a single, lazily created instance with thread-safe access. Useful when exactly one object should exist, game session state, input routing, global config. The four variants cover plain C#, scene-bound `MonoBehaviour`, persistent across loads, and `ScriptableObject` assets. Use sparingly: global access is convenient but makes testing and dependencies harder to trace.

Four lazy, thread-safe variants for different Unity lifetimes.

| Type | Lifetime | Source |
|---|---|---|
| `Singleton<T>` | Plain C# class | [Singleton.cs](./Singleton/Singleton.cs) |
| `MonoBehaviourSingleton<T>` | Scene-bound `MonoBehaviour` | [MonoBehaviourSingleton.cs](./Singleton/MonoBehaviourSingleton.cs) |
| `PersistentMonoBehaviourSingleton<T>` | `DontDestroyOnLoad` | [PersistentMonoBehaviourSingleton.cs](./Singleton/PersistentMonoBehaviourSingleton.cs) |
| `ScriptableObjectSingleton<T>` | `Resources` asset | [ScriptableObjectSingleton.cs](./Singleton/ScriptableObjectSingleton.cs) |

### Singleton<T>

```c#
using FronkonGames.GameWork.Foundation;

public sealed class GameRules : Singleton<GameRules>
{
  public float GravityMultiplier = 1.0f;

  private GameRules() { } // required: private ctor
}

float gravity = GameRules.Instance.GravityMultiplier;
```

### MonoBehaviourSingleton<T>

Finds an existing component or creates a new `GameObject`. Not persistent across scenes. Call `base.OnDestroy()` when overriding `OnDestroy`.

```c#
using FronkonGames.GameWork.Foundation;
using UnityEngine;

public class InputRouter : MonoBehaviourSingleton<InputRouter>
{
  public Vector2 MoveAxis { get; private set; }

  void Update() => MoveAxis = /* read input */;
}

// First access creates or finds the instance, avoid calling Instance every frame in hot paths
Vector2 move = InputRouter.Instance.MoveAxis;
```

### PersistentMonoBehaviourSingleton<T>

Same as above, but survives scene loads via `DontDestroyOnLoad`.

```c#
public class GameSession : PersistentMonoBehaviourSingleton<GameSession>
{
  public int CurrentLevel { get; set; }
}
```

### ScriptableObjectSingleton<T>

Asset must live in a `Resources` folder and be named exactly like the type (`GameBalance.asset` for `GameBalance`).

```c#
using UnityEngine;

[CreateAssetMenu(fileName = "GameBalance", menuName = "Game/Balance")]
public class GameBalance : ScriptableObjectSingleton<GameBalance>
{
  public float PlayerSpeed = 6.0f;
}

float speed = GameBalance.Instance.PlayerSpeed;
```

---

## Tests

- [Patterns.Builder.Test.cs](../../../Test/Patterns/Patterns.Builder.Test.cs)
- [Patterns.Factory.Test.cs](../../../Test/Patterns/Patterns.Factory.Test.cs)
- [Patterns.ServiceLocator.Test.cs](../../../Test/Patterns/Patterns.ServiceLocator.Test.cs)
- [Patterns.Singleton.Test.cs](../../../Test/Patterns/Patterns.Singleton.Test.cs)
