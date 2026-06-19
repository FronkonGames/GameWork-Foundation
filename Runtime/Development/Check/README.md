# Check

Runtime guard-clause helpers for validating function arguments and preconditions. When a check fails, `Check` throws an `Exception` with a message that includes the source file, member name, and line number.

Designed for **fail-fast debugging** during development, not as a replacement for production error handling.

---

## When checks run

Almost every `Check` method is marked `[Conditional("UNITY_ASSERTIONS")]`. That means:

- **Active** when `UNITY_ASSERTIONS` is defined (Editor and Development builds by default).
- **Stripped entirely** from Release builds, zero runtime cost in shipping players.

`Check.Assert(bool, string)` is always available and logs via `Debug.LogError` before throwing (except during unit tests).

```c#
#if UNITY_ASSERTIONS
// Checks are compiled in
#else
// Check.True(value) and similar calls disappear at compile time
#endif
```

---

## Basic usage

Validate inputs at the top of a method before doing any work:

```c#
using UnityEngine;
using FronkonGames.GameWork.Foundation;

public void ApplyDamage(GameObject target, float damage, Vector3 hitPoint)
{
  Check.IsNotNull(target);
  Check.IsWithin(damage, 0.0f, 100.0f);
  Check.Greater(hitPoint, Vector3.zero);

  target.GetComponent<Health>()?.TakeDamage(damage);
}
```

On failure you get a message like: `CombatSystem.cs:ApplyDamage:12 'target' must not be null.`

---

## Boolean

| Method | Passes when |
|---|---|
| `True(bool)` | Value is `true` |
| `False(bool)` | Value is `false` |

```c#
Check.True(isInitialized);
Check.False(isDisposed);
```

---

## Null, type, and collections

| Method | Passes when |
|---|---|
| `IsNull(object)` | Value is `null` |
| `IsNotNull(object)` | Value is not `null` |
| `IsNotNullOrEmpty<T>(ICollection<T>)` | Collection is not `null` and `Count > 0` |
| `OfType(object, Type)` | Value is an instance of the given type |
| `IsAssignableFrom(Type, object)` | Value's runtime type can be assigned to the given type |

```c#
Check.IsNotNull(player);
Check.IsNotNullOrEmpty(enemies);

Check.OfType(component, typeof(Rigidbody));
Check.IsAssignableFrom(typeof(IHealth), damageable);
```

---

## Strings

| Method | Passes when |
|---|---|
| `IsNotNullOrEmpty(string)` | String is not `null` or empty |
| `Length(string, int)` | Exact character count |
| `MaxLength(string, int)` | Length ≤ max |
| `MinLength(string, int)` | Length ≥ min |

```c#
Check.IsNotNullOrEmpty(playerName);
Check.MaxLength(playerName, 32);
Check.MinLength(password, 8);
Check.Length(saveCode, 6);
```

---

## Numeric comparisons

Works with any `struct` implementing `IComparable<T>`: `int`, `float`, `double`, etc.

| Method | Passes when |
|---|---|
| `Equal(a, b)` | `a` equals `b` |
| `NotEqual(a, b)` | `a` does not equal `b` |
| `Less(a, b)` | `a < b` |
| `LessOrEqual(a, b)` | `a ≤ b` |
| `Greater(a, b)` | `a > b` |
| `GreaterOrEqual(a, b)` | `a ≥ b` |
| `Negative(value)` | Value is strictly less than zero |
| `NotNegative(value)` | Value is zero or positive |
| `IsWithin(value, min, max)` | `min ≤ value ≤ max` (inclusive) |
| `IsBetween(value, min, max)` | `min < value < max` (exclusive) |

```c#
Check.Equal(level, expectedLevel);
Check.Greater(score, 0);
Check.IsWithin(health, 0.0f, maxHealth);
Check.IsBetween(progress, 0.0f, 1.0f);   // excludes 0 and 1
Check.NotNegative(ammo);
```

---

## Unity vectors and quaternions

`Vector2`, `Vector3`, and `Vector4` overloads compare **magnitude** for ordering (`Less`, `Greater`, etc.). `Equal` / `NotEqual` accept either another vector or a `float` magnitude (uses `NearlyEquals` for floats).

`Quaternion` supports `Equal` and `NotEqual` only.

```c#
Vector3 velocity = rigidbody.linearVelocity;

Check.Greater(velocity, Vector3.zero);        // magnitude > 0
Check.Greater(velocity, 5.0f);                // magnitude > 5
Check.Equal(direction, Vector3.forward);
Check.Equal(direction, 1.0f);                 // magnitude ≈ 1
Check.Equal(rotation, Quaternion.identity);
```

---

## Reflection

| Method | Passes when |
|---|---|
| `Interface(Type)` | Type is an interface |
| `NotInterface(Type)` | Type is not an interface |

```c#
Check.Interface(typeof(IDisposable));
Check.NotInterface(typeof(MonoBehaviour));
```

---

## Custom assertions

Use `Check.Assert` for conditions not covered by the built-in helpers:

```c#
Check.Assert(inventory.HasSpace(item), "Inventory is full.");
Check.Assert(phase == GamePhase.Playing, "Action only allowed during play.");
```

---

## Practical patterns

**Public API guard**, validate every parameter a caller can pass incorrectly:

```c#
public void SpawnEnemy(GameObject prefab, int count, Transform spawnPoint)
{
  Check.IsNotNull(prefab);
  Check.IsNotNull(spawnPoint);
  Check.Greater(count, 0);
  Check.IsWithin(count, 1, maxSpawnCount);

  for (int i = 0; i < count; ++i)
    Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
}
```

**State guard**, assert internal invariants before a sensitive operation:

```c#
private void Fire()
{
  Check.True(isReady);
  Check.NotNegative(ammo);
  Check.IsNotNullOrEmpty(activeProjectiles);

  ammo--;
  SpawnProjectile();
}
```

**Configuration guard**, catch bad ScriptableObject data early:

```c#
public void Initialize(WeaponConfig config)
{
  Check.IsNotNull(config);
  Check.Greater(config.damage, 0.0f);
  Check.IsWithin(config.fireRate, 0.1f, 10.0f);
  Check.IsNotNull(config.projectilePrefab);
}
```

---

## Check vs UnityEngine.Assertions

| | `Check` | `UnityEngine.Assertions.Assert` |
|---|---|---|
| Active when | `UNITY_ASSERTIONS` defined | `UNITY_ASSERTIONS` defined |
| Failure | Throws `Exception` | Throws `AssertionException` |
| Message | File, member, line, parameter name | Custom or generated |
| Vector support | Magnitude-based overloads | None |
| Stripped in release | Yes | Yes |

Use `Check` when you want readable guard clauses with consistent messages across your game code. Use `UnityEngine.Assertions` when you need tight integration with Unity Test Framework assertion types.

---

## Source files

| File | Methods |
|---|---|
| [Check.cs](./Check.cs) | `Assert` |
| [Check.Boolean.cs](./Check.Boolean.cs) | `True`, `False` |
| [Check.Type.cs](./Check.Type.cs) | `IsNull`, `IsNotNull`, `IsNotNullOrEmpty`, `OfType`, `IsAssignableFrom` |
| [Check.String.cs](./Check.String.cs) | `IsNotNullOrEmpty`, `Length`, `MaxLength`, `MinLength` |
| [Check.Numeric.cs](./Check.Numeric.cs) | `Equal`, `NotEqual`, `Less`, `LessOrEqual`, `Greater`, `GreaterOrEqual`, `Negative`, `NotNegative`, `IsWithin`, `IsBetween` |
| [Check.Reflective.cs](./Check.Reflective.cs) | `Interface`, `NotInterface` |

---

## Tests

- [CheckTests](../../../Test/Check/CheckTests.cs)
