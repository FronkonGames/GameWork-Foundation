# Optimization Patterns

Performance-oriented patterns in `FronkonGames.GameWork.Foundation` for reducing allocation overhead in hot paths.

| Pattern | Folder | Role |
|---|---|---|
| Object Pool | [ObjectPool](./ObjectPool/) | Reuse instances instead of allocating every time |

---

## Object Pool

**Why use it:** Reuse objects instead of allocating and destroying them every frame. `Instantiate`/`Destroy` and heavy `new` calls cause GC spikes that show up as frame hitches. A pool pre-creates a batch, hands out instances on `Get()`, and recycles them on `Release()` after you reset their state. Essential for projectiles, particle-like VFX, floating damage numbers, list rows, and any burst of short-lived objects with a predictable peak count.

[ObjectPool.cs](./ObjectPool/ObjectPool.cs)

| Member | Description |
|---|---|
| `ObjectPool<T>(createFunc, onGet, onRelease, initialSize)` | Constructor with optional callbacks and pre-warm count |
| `Get()` | Pop from pool or create via `createFunc` |
| `Release(T item)` | Push back after optional `onRelease` callback |
| `Clear()` | Discard all pooled instances |
| `CountAvailable` | Items currently in the pool |

`T` must be a reference type (`class`). Use `onGet` / `onRelease` to reset state, enable/disable `GameObject`s, clear lists, reset timers.

```c#
using FronkonGames.GameWork.Foundation;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
  public void ResetState() => transform.localScale = Vector3.one;
}

// Scene: CoinSpawner, pre-warm 20 coins at startup
var coinPool = new ObjectPool<CoinPickup>(
  createFunc: () =>
  {
    GameObject go = Object.Instantiate(coinPrefab);
    go.SetActive(false);
    return go.GetComponent<CoinPickup>();
  },
  onGet: coin => coin.gameObject.SetActive(true),
  onRelease: coin =>
  {
    coin.ResetState();
    coin.gameObject.SetActive(false);
  },
  initialSize: 20
);

// Spawn
CoinPickup coin = coinPool.Get();
coin.transform.position = spawnPoint;

// Recycle when collected
coinPool.Release(coin);
```

### When to use

| Use pool when | Skip pool when |
|---|---|
| Many short-lived objects (projectiles, VFX, UI rows) | Few instances that live for the whole scene |
| `Instantiate`/`Destroy` causes GC spikes | Object state is too complex to reset safely |
| Peak count is bounded and predictable | One-off objects (boss intro, cutscene props) |

Pre-warm with `initialSize` to avoid allocation during the first burst. `Get()` only calls `createFunc` when the pool is empty.

---

## Tests

- [Patterns.ObjectPool.Test.cs](../../../Test/Patterns/Patterns.ObjectPool.Test.cs)
