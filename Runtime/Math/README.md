# Math

Mathematical constants, random utilities, and geometry helpers in `FronkonGames.GameWork.Foundation`. All types are static, no components or setup required.

`Rand` wraps `UnityEngine.Random` with gameplay-oriented helpers. `MathUtils` is a `partial` class split across angle, trigonometry, and curve files.

---

## MathConstants

[MathConstants.cs](./MathConstants.cs), shared float constants safer than scattering magic numbers.

| Constant | Value / role |
|---|---|
| `Pi`, `PiHalf`, `Pi2`, `Tau` | Circle constants |
| `E`, `GoldenRatio` | Common math constants |
| `Deg2Rad`, `Rad2Deg` | Angle unit conversion |
| `OneThird`, `TwoThirds`, `OneSixth` | Fraction literals |
| `Epsilon` | Unity-safe epsilon (prefer over `float.Epsilon`) |
| `Infinity`, `NegativeInfinity` | Infinities |
| `NaNVector2/3/4`, `InfinityVector2/3/4` | Sentinel vectors |
| `EmptyRay` | Default `Ray` |

```c#
using FronkonGames.GameWork.Foundation;

float radians = 90.0f * MathConstants.Deg2Rad;
bool equal = Mathf.Abs(a - b) < MathConstants.Epsilon;
```

---

## Rand

[Rand.cs](./Rand.cs), random numbers, directions, dice rolls, weighted picks, and probability checks.

### Scalars and dice

| Member | Description |
|---|---|
| `Value` | Float in `[0, 1]` |
| `Sign` / `Direction1D` | `+1` or `-1` at 50% |
| `Range(int/float min, max)` | Inclusive random range |
| `D4`, `D6`, `D10`, `D20`, `D100` | Tabletop-style die rolls |

### Vectors and orientation

| Member | Description |
|---|---|
| `OnUnitCircle` / `Direction2D` | Random unit vector on XY circle |
| `InUnitCircle`, `InUnitSquare` | Random point in 2D unit shapes |
| `OnUnitSphere` / `Direction3D` | Random unit 3D direction |
| `InUnitSphere`, `InUnitCube` | Random point in 3D unit shapes |
| `Angle` | Random radians in `[0, Tau]` |
| `Rotation` | Uniform random `Quaternion` |

### Gameplay helpers

| Method | Description |
|---|---|
| `PickWeighted(weights)` | Weighted index for loot tables, AI, spawns |
| `NextNormal(mean, stdDev)` | Normally distributed sample (Box-Muller) |
| `Chance(n, d)` | `true` when random in `[0, d]` ≤ `n` |
| `ChancePercent(%)` | Percentage-based roll (0–100) |
| `Lerp(a, b)` | Random interpolation for `float`, `Vector2/3`, `Color` |

```c#
using FronkonGames.GameWork.Foundation;

int lootIndex = Rand.PickWeighted(new[] { 50.0f, 30.0f, 15.0f, 5.0f });

if (Rand.ChancePercent(25.0f) == true)
  ApplyCriticalHit();

Vector3 scatter = origin + Rand.OnUnitSphere * radius;
float jitter = Rand.NextNormal(mean: 100.0f, stdDev: 10.0f);
```

---

## MathUtils

Partial static class across three files.

### Angles, [MathUtils.Angle.cs](./MathUtils.Angle.cs)

| Method | Description |
|---|---|
| `AngToDir(radian)` | Convert angle to unit `Vector2` on XY plane |
| `ClampAngle(angle, min, max)` | Clamp degrees with 360° wrap-around |
| `ClampAngleNormalized(angle, min, max)` | Normalize then clamp within a degree range |

```c#
float yaw = MathUtils.ClampAngle(cameraYaw, -80.0f, 80.0f);

Vector2 aim = MathUtils.AngToDir(Rand.Angle);
```

### Trigonometry, [MathUtils.Trigonometry.cs](./MathUtils.Trigonometry.cs)

Thin `Mathf` wrappers plus reciprocal and historic trig helpers.

| Standard | Extra |
|---|---|
| `Sin`, `Cos`, `Tan` | `Csc`, `Sec`, `Cot` |
| `Asin`, `Acos`, `Atan`, `Atan2` | `Ver`, `Cvs`, `Crd` |

### Curves, [MathUtils.Curve.cs](./MathUtils.Curve.cs)

| Method | Description |
|---|---|
| `Bezier(start, end, t, controlPoints)` | De Casteljau evaluation for `Vector2` or `Vector3` |

With no control points, behaves as linear `Lerp`. Used by [GizmoDraw](../Development/Draw/GizmoDraw.cs) for curve visualization.

```c#
Vector3[] handles = { midPoint + Vector3.up * 2.0f };
Vector3 point = MathUtils.Bezier(start, end, t: 0.5f, handles);
```

---

## Source files

| File | Type |
|---|---|
| [MathConstants.cs](./MathConstants.cs) | Constants |
| [Rand.cs](./Rand.cs) | Random utilities |
| [MathUtils.Angle.cs](./MathUtils.Angle.cs) | `MathUtils`, angles |
| [MathUtils.Trigonometry.cs](./MathUtils.Trigonometry.cs) | `MathUtils`, trig |
| [MathUtils.Curve.cs](./MathUtils.Curve.cs) | `MathUtils`, Bezier curves |

---

## Related APIs

- [EnumExtensions](../Extensions/System/EnumExtensions.cs), `PickRandom`, `PickWeighted` on enums (uses `Rand` internally)
- [GizmoDraw](../Development/Draw/GizmoDraw.cs), `WireBezier` / `WireBezier2D` curve drawing

---

## Tests

- [Math.Random.Test.cs](../../Test/Math/Math.Random.Test.cs)
- [MathUtils.Test.cs](../../Test/Math/MathUtils.Test.cs)
- [MathUtils.Curve.Test.cs](../../Test/Math/MathUtils.Curve.Test.cs)
- [MathConstants.Test.cs](../../Test/Math/MathConstants.Test.cs)
