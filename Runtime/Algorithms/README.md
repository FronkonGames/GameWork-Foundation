# Algorithms

Algorithms and data structures for performance-sensitive game code.

## Structures

| Type | Source | When to use |
|---|---|---|
| [ArrayList<T>](./Structures/ArrayList.cs) | Ordered, `List<T>`-like API | Search, insert, ordered removal, or when element order must be preserved |
| [FastList<T>](./Structures/FastList.cs) | Pooled, zero-GC, swap-remove | Hot paths with frequent add/remove where order does not matter |

---

## ArrayList<T>

A general-purpose array-backed list with a `List<T>`-like API and **ordered removal**.

**Benefits over `List<T>`:**

- Lightweight class with no interface overhead beyond `IEnumerable<T>`.
- Direct index access via the indexer and predictable 2x capacity growth.
- Rich search and mutation helpers: `Find`, `FindAll`, `GetRange`, `Reverse`, `Resize`, `Insert`.
- `RemoveAt` preserves element order (unlike swap-remove lists).

**Trade-offs:**

- `RemoveAt` is O(N) because elements are shifted after removal.
- Allocates a new backing array on growth (not pooled like `FastList<T>`).
- `foreach` uses a yield-based enumerator (allocates on the heap).
- Prefer `FastList<T>` when you need zero-GC pooling and O(1) unordered removal.

```c#
// Basic usage
var list = new ArrayList<int>(8);
list.Add(10);
list.Add(20);
list.Insert(1, 15);
list.RemoveAt(0); // O(N), order preserved

// Search and filter
int index = list.IndexOf(15);
int found = list.FindIndex(x => x > 10);
ArrayList<int> matches = list.FindAll(x => x % 2 == 0);

// Convert or copy
int[] array = list.ToArray();
List<int> copy = list.ToList();
ArrayList<int> slice = list.GetRange(0, 2);
```

**Key members:** `Count`, `Capacity`, `IsEmpty`, `First`, `Last`, `Add`, `AddRange`, `Insert`, `Remove`, `RemoveAt`, `Clear`, `Resize`, `Reverse`, `ForEach`, `Contains`, `Exists`, `Find`, `FindAll`, `FindIndex`, `IndexOf`, `GetRange`, `ToArray`, `ToList`.

---

## FastList<T>

A high-performance list that uses `ArrayPool` for memory efficiency and swap-remove for **O(1) element removal**.

**Benefits over `List<T>`:**

- Zero GC allocations: uses `ArrayPool<T>.Shared` to rent/return backing arrays.
- O(1) `RemoveAt`: swaps the removed element with the last element instead of shifting.
- Struct enumerator: `foreach` allocates no heap memory.
- 2x capacity growth: amortized O(1) adds.

**Trade-offs:**

- `RemoveAt` does **not** preserve element order (swap-with-last).
- Implements `IReadOnlyList<T>` (read-only interface) to discourage misuse.
- Must call `Dispose()` to return the pooled array, or use with `using`.

```c#
// Basic usage
using var list = new FastList<int>(16);
list.Add(42);
list.Add(99);
list.RemoveAt(0); // O(1) swap-remove

// Iteration (zero allocation)
foreach (var item in list)
  Debug.Log(item);

// Direct array access (for performance-critical code)
int[] raw = list.GetInternalArray();
for (int i = 0; i < list.Count; i++)
  Process(raw[i]);
```

**Key members:** `Count`, `Capacity`, `Add`, `RemoveAt`, `RemoveLast`, `Remove`, `Contains`, `IndexOf`, `Clear`, `GetInternalArray`, `TrimExcess`, `Dispose`, `GetEnumerator`.

**Swap-remove example:** removing index 1 from `[1, 2, 3, 4]` yields `[1, 4, 3]`, the last element moves into the removed slot.

---

## Tests

- [ArrayList](./../../Test/Algorithms/Algorithms.Structures.Test.cs)
- [FastList](./../../Test/Algorithms/FastList.Test.cs)
