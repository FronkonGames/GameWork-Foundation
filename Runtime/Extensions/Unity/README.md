# Unity extensions

Extension methods for Unity types in `FronkonGames.GameWork.Foundation`. Import the namespace and call methods on the extended type.

See also: [System extensions](../System/README.md).

---

## GameObjects and components

| Class | Highlights |
|---|---|
| [GameObjectExtensions](./GameObjectExtensions.cs) | `SafeDestroy`, `DestroyAfter`, `GetAllChildren`, `ChangeMaterial`, `SetLayer` |
| [ComponentExtensions](./ComponentExtensions.cs) | Component lookup and batch helpers |
| [MonoBehaviourExtensions](./MonoBehaviourExtensions.cs) | `SafeStartCoroutine`, `RestartCoroutine`, `DontDestroyOnLoad`, `ResolveComponentFromChildrenIfNull` |
| [UnityObjectExtensions](./UnityObjectExtensions.cs) | `UnityEngine.Object` utilities |
| [TryGetExtensions](./TryGetExtensions.cs) | Safe `TryGetComponent` patterns |

```c#
using FronkonGames.GameWork.Foundation;

projectile.SafeDestroy();

enemy.DestroyAfter(3.0f);
```

---

## Transforms

[TransformExtensions](./TransformExtensions.cs), the largest Unity extension file.

| Area | Examples |
|---|---|
| Hierarchy | `GetPath`, `FindChildRecursive`, `DestroyChildren`, `GetAllChildren` |
| Position | `SetX/Y/Z`, `SetLocalXYZ`, `TranslateXYZ`, `ResetWorld/Local` |
| Scale | `SetScaleXYZ`, `ScaleByXYZ`, `FlipX/Y/Z`, `ResetScale` |
| Rotation | `RotateAroundX/Y/Z`, `SetRotationY`, `ResetRotation` |
| RectTransform | `ResetRect`, `ExpandFullscreen` (via [RectTransformExtensions](./RectTransformExtensions.cs)) |

```c#
player.transform.SetY(groundHeight);
turretHead.SetLocalRotationY(yawAngle);
```

---

## Vectors and quaternions

| Class | Highlights |
|---|---|
| [Vector2Extensions](./Vector2Extensions.cs) | `Rotate`, 2D math helpers |
| [Vector3Extensions](./Vector3Extensions.cs) | 3D vector utilities |
| [Vector4Extensions](./Vector4Extensions.cs) | 4D vector helpers |
| [Vector2IntExtensions](./Vector2IntExtensions.cs) | Integer grid coordinates |
| [Vector3IntExtensions](./Vector3IntExtensions.cs) | 3D integer vectors |
| [VectorConversionExtensions](./VectorConversionExtensions.cs) | Cast between vector types |
| [QuaternionExtensions](./QuaternionExtensions.cs) | `NearlyEquals`, `AngleTo`, `IsIdentity`, `Normalized` |
| [QuaternionConversionExtensions](./QuaternionConversionExtensions.cs) | Quaternion/type conversions |
| [Matrix4x4Extensions](./Matrix4x4Extensions.cs) | `DecomposeTRS`, extract position, rotation, scale |

```c#
Vector2 aim = direction.Rotate(Mathf.PI * 0.25f);

matrix.DecomposeTRS(out Vector3 pos, out Quaternion rot, out Vector3 scale);
```

---

## Colors, sprites, and textures

| Class | Highlights |
|---|---|
| [ColorExtensions](./ColorExtensions.cs) | HSV, hex, HDR (`GetHdrIntensity`, `AdjustHdrIntensity`, `AtHdrIntensity`), tint/shade/tone |
| [Color32Extensions](./Color32Extensions.cs) | `CreateTexture`, equality checks |
| [SpriteExtensions](./SpriteExtensions.cs) | `GetPixels32`, `GetRect`, `IsFullyTransparent` |
| [Texture2DExtensions](./Texture2DExtensions.cs) | Texture manipulation |
| [ImageExtensions](./ImageExtensions.cs) | UI `Image` helpers |
| [MaterialExtensions](./MaterialExtensions.cs) | Material property shortcuts |

```c#
Color hdr = baseColor.AtHdrIntensity(2.0f);
Color parsed = "#FF8800".FromHex();
```

---

## Physics and spatial

| Class | Highlights |
|---|---|
| [ColliderExtensions](./ColliderExtensions.cs) | Collider bounds and queries |
| [RigidbodyExtensions](./RigidbodyExtensions.cs) | Rigidbody velocity and forces |
| [RaycastHitExtensions](./RaycastHitExtensions.cs) | Hit info helpers |
| [LayerMaskExtensions](./LayerMaskExtensions.cs) | `IsInLayerMask`, layer utilities |
| [BoundsExtensions](./BoundsExtensions.cs) | Bounds math |
| [BoundsIntExtensions](./BoundsIntExtensions.cs) | Integer bounds |
| [CameraExtensions](./CameraExtensions.cs) | Camera frustum and screen helpers |

---

## UI and layout

| Class | Highlights |
|---|---|
| [RectTransformExtensions](./RectTransformExtensions.cs) | Margins, size, `ExpandFullscreen`, `ToWorldBounds` |
| [RectExtensions](./RectExtensions.cs) | `WithRoundedCoordinates`, `CutVertically`, `AddHorizontalPadding`, `AlignMiddleVertically` |
| [RectLineIntersectionHelper](./RectLineIntersectionHelper.cs) | `GetLineIntersections`, line/rect crossing points |
| [ButtonExtensions](./ButtonExtensions.cs) | UI button helpers |
| [ScrollRectExtensions](./ScrollRectExtensions.cs) | Scroll view utilities |
| [CanvasGroupExtensions](./CanvasGroupExtensions.cs) | `SetActive` with alpha and interactable control |

```c#
Rect padded = panelRect.AddHorizontalPadding(8.0f);

LineIntersectionResult hit = bounds.GetLineIntersections(segmentStart, segmentEnd);
```

---

## Audio

| Class | Highlights |
|---|---|
| [AudioExtensions](./AudioExtensions.cs) | `ToDecibel`, `ToLinear`, amplitude/decibel conversion |
| [AudioSourceExtensions](./AudioSourceExtensions.cs) | `SetPitch`, `SetVolume` with random ranges |
| [AudioMixerExtensions](./AudioMixerExtensions.cs) | Mixer parameter helpers |

```c#
float db = linearAmplitude.ToDecibel();
float linear = (-6.0f).ToLinear();
```

---

## Animation, particles, and rendering

| Class | Highlights |
|---|---|
| [AnimatorExtensions](./AnimatorExtensions.cs) | Animator state helpers |
| [ParticleSystemExtensions](./ParticleSystemExtensions.cs) | Particle control |
| [RendererExtensions](./RendererExtensions.cs) | Renderer material access |
| [MeshExtensions](./MeshExtensions.cs) | Mesh data utilities |
| [MeshFilterExtensions](./MeshFilterExtensions.cs) | MeshFilter helpers |

---

## Async and events

| Class | Highlights |
|---|---|
| [AsyncOperationExtensions](./AsyncOperationExtensions.cs) | `AsyncOperation` await helpers |
| [AwaitableExtensions](./AwaitableExtensions.cs) | Unity 6 `Awaitable` → `Task` conversion |
| [UnityEventExtensions](./UnityEventExtensions.cs) | Event wiring utilities |

---

## Collections and flags

| Class | Highlights |
|---|---|
| [CollectionExtensions](./CollectionExtensions.cs) | `AddIfMissing` |
| [ReadOnlyCollectionExtensions](./CollectionExtensions.cs) | `IndexOf` on `IReadOnlyCollection<T>` |
| [EnumFlagsExtensions](./EnumFlagsExtensions.cs) | Flags enum helpers |

---

## Misc

| Class | Highlights |
|---|---|
| [Tailwind](./Tailwind.cs) | Tailwind-style spacing and sizing shortcuts for UI layout |

---

## Tests

Unit tests live under [Test/Extensions](../../../Test/Extensions/).
