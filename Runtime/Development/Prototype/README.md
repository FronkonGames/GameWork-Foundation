# Prototype

Drop-in `MonoBehaviour` components for rapid scene prototyping. All types live in `FronkonGames.GameWork.Foundation.Prototype` and are marked for **prototype use only**, not production gameplay systems.

See the demo scene: [PrototypeDemo.unity](../../../Demos/Prototype/PrototypeDemo.unity).

Most components extend `CachedMonoBehaviour`, which caches `transform` and `rigidbody` references automatically.

---

## Cameras

### FirstPersonCamera

Mouse-look first-person camera. Requires a `Camera` component.

| Field | Default | Description |
|---|---|---|
| `smoothRotation` | `10` | Slerp speed toward target rotation |
| `mouseSensitivity` | `(1, 1)` | Mouse X/Y multiplier |
| `pitchLimits` | `(-80, 80)` | Vertical look clamp in degrees |
| `cursorLock` | `true` | Lock cursor while right mouse button held |

**Controls:** hold **right mouse button** to look. Cursor locks while the button is held.

```c#
using FronkonGames.GameWork.Foundation.Prototype;
using UnityEngine;

// Scene:
//   PlayerBody , capsule + movement (WASD moves the body)
//     └ HeadCamera , Camera + FirstPersonCamera (local Y ≈ 1.6)

public class FpsCameraRig : MonoBehaviour
{
  [SerializeField] private Transform playerBody;
  [SerializeField] private FirstPersonCamera headCamera;

  private void OnValidate()
  {
    if (headCamera == null && transform.childCount > 0)
      headCamera = transform.GetChild(0).GetComponent<FirstPersonCamera>();
  }

  private void Update()
  {
    float forward = Input.GetAxis("Vertical");
    float strafe  = Input.GetAxis("Horizontal");
    playerBody.Translate(new Vector3(strafe, 0.0f, forward) * 5.0f * Time.deltaTime);
  }
}

// Inspector values on FirstPersonCamera (HeadCamera):
//   smoothRotation   → 12.0f
//   mouseSensitivity → (2.0f, 2.0f)
//   pitchLimits      → (-75.0f, 75.0f)
//   cursorLock       → true
```

---

### ThirdPersonCamera

Orbits a follow target with scroll-zoom, obstacle avoidance, and smooth lag.

| Field | Default | Description |
|---|---|---|
| `follow` |, | Target `Transform` |
| `followOffset` | `(0, 1.5, 0)` | Pivot offset from target |
| `directionOffset` | `(0, 0, 2)` | Extra offset in camera space |
| `distanceRange` | `(5, 8)` | Min/max orbit distance |
| `rotationRange` | `(-10, 60)` | Vertical angle limits |
| `rotationSensitivity` | `10` | Mouse rotation speed |
| `rotationSpeed` | `0.5` | Smoothing (1 = instant) |
| `hardFollow` | `0.75` | How tightly the pivot tracks the target |
| `obstacleLayer` | Everything | Layers that block the camera |
| `collisionOffset` | `1` | Pull-back distance from obstacles |
| `cursorLock` | `true` | Lock cursor while rotating |

**Controls:** hold **right mouse button** to orbit; **scroll wheel** adjusts distance. Raycasts shorten the camera when blocked.

```c#
using FronkonGames.GameWork.Foundation.Prototype;
using UnityEngine;

// Scene:
//   PlayerBody , capsule the camera orbits
//   MainCamera , Camera + ThirdPersonCamera

public class ChaseCameraRig : MonoBehaviour
{
  [SerializeField] private Transform playerBody;
  [SerializeField] private ThirdPersonCamera orbitCamera;

  private void OnValidate()
  {
    if (orbitCamera == null)
      orbitCamera = GetComponent<ThirdPersonCamera>();
  }
}

// Inspector values on ThirdPersonCamera (MainCamera):
//   follow          → playerBody
//   followOffset    → (0.0f, 1.6f, 0.0f)   // head height
//   distanceRange   → (4.0f, 10.0f)        // scroll zoom range
//   rotationRange   → (-15.0f, 45.0f)
//   obstacleLayer   → Default, Environment, Props
//   hardFollow      → 0.6f                  // slight lag while prototyping movement
```

---

### FreeCamera

Fly camera for level inspection. Requires a `Camera` component.

| Field | Default | Description |
|---|---|---|
| `movementSpeed` | `10` | Base move speed |
| `movementSmothness` | `10` | Position lerp factor |
| `rotationSmothness` | `10` | Rotation lerp factor |
| `mouseSensitivity` | `5` | Look sensitivity |
| `turboMultiply` | `5` | Speed multiplier with Shift |
| `cursorLock` | `true` | Lock cursor while looking |

**Controls**

| Input | Action |
|---|---|
| **WASD** / arrow keys | Move forward/back and strafe |
| **Right mouse button** + mouse | Look around |
| **Scroll wheel** (while looking) | Increase/decrease fly speed |
| **Left Shift** | Turbo multiplier |
| **Left Ctrl** / **Q** | Descend |
| **Space** / **E** | Ascend |

```c#
using FronkonGames.GameWork.Foundation.Prototype;
using UnityEngine;

// Scene:
//   FlyCamera , detached from the player; Camera + FreeCamera
//   (disable or delete after greyboxing; swap back to the gameplay camera)

public class LevelFlyCamera : MonoBehaviour
{
  [SerializeField] private FreeCamera flyCamera;
  [SerializeField] private Camera gameplayCamera;

  private void OnValidate()
  {
    if (flyCamera == null)
      flyCamera = GetComponent<FreeCamera>();
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.F1) == true)
    {
      bool flyMode = flyCamera.enabled == false;
      flyCamera.enabled = flyMode;
      gameplayCamera.enabled = flyMode == false;
    }
  }
}

// Inspector values on FreeCamera (FlyCamera):
//   movementSpeed      → 15.0f
//   movementSmothness  → 10.0f
//   rotationSmothness  → 10.0f
//   mouseSensitivity   → 6.0f
//   turboMultiply      → 4.0f
//   cursorLock         → true
```

---

## Capture and monitoring

### Screenshooter

Asynchronous GPU screenshot capture via `AsyncGPUReadback`. Works in Edit mode (`[ExecuteAlways]`).

| Field | Default | Description |
|---|---|---|
| `encoder` | PNG | Output format: JPG, PNG, or TGA |
| `key1` / `key2` | Ctrl + C | Hotkey combination |
| `prefix` | `"Screenshot"` | Filename prefix |
| `addTimestamp` | `true` | Append date/time to filename |
| `saveFolder` |, | Output folder (`[Folder]` picker). Falls back to `Assets/` if empty |

Call `Capture()` from code or the inspector **Capture** button. Files are written after the frame ends; the Editor refreshes the Asset Database automatically.

```c#
// Add to any GameObject in the scene.
// Default: Ctrl+C saves PNG to the configured folder.
screenshooter.Capture();
```

---

### HardwareMonitor

Runtime performance overlay driven by UI `Text` references. Exposes live stats as properties and updates labels each frame.

**Public properties:** `FPS`, `MaxFPS`, `AverageFPS`, `FrameTime`, `ReservedMemory`, `AllocatedMemory`, `FreeMemory`, `MonoHeap`, `MonoUsed`.

**Public methods**

| Method | Effect |
|---|---|
| `UnloadUnusedAssets()` | Calls `Resources.UnloadUnusedAssets()` |
| `RunGarbageCollection()` | Forces `GC.Collect()` |
| `ResetCounters()` | Clears FPS history |
| `GetPanel()` | Returns the parent panel `GameObject` |

Assign `processorLabel`, `gpuLabel`, `resolutionLabel`, `fpsLabel`, `memory1Label`, `memory2Label`, `drawCall1Label`, and `drawCall2Label` in the inspector. Draw call and triangle counts are **Editor-only**.

FPS label color changes based on `Settings.FPS` thresholds (good / warning / bad).

```c#
// Wire UI Text elements, then hook buttons:
hardwareMonitor.UnloadUnusedAssets();
hardwareMonitor.RunGarbageCollection();
```

---

## Physics events

### CollisionTest

Fires `UnityEvent`s on `OnCollisionEnter`, `OnCollisionStay`, and `OnCollisionExit` with layer, name, and velocity filtering.

| Field | Description |
|---|---|
| `layerFilter` | Other object must be on one of these layers |
| `nameFilter` | If set, other object's name must match exactly (case-sensitive) |
| `velocityFilter` | On **enter** only: `relativeVelocity.magnitude` must exceed this |
| `onCollisionEnter/Stay/Exit` | `UnityEvent<GameObject, Collision>`, passes self and collision data |
| `debugView` | Draws collision data in the Scene view via `DebugDraw` |

Requires a non-trigger `Collider` and (typically) a `Rigidbody`.

```c#
using FronkonGames.GameWork.Foundation.Prototype;
using UnityEngine;

// Scene:
//   FireBolt , SphereCollider + Rigidbody + CollisionTest

public class FireBoltImpact : MonoBehaviour
{
  [SerializeField] private CollisionTest collisionTest;

  private void OnValidate()
  {
    if (collisionTest == null)
      collisionTest = GetComponent<CollisionTest>();
  }

  public void OnHitEnemy(GameObject self, Collision hit)
  {
    hit.gameObject.GetComponent<EnemyHealth>()?.TakeDamage(25);
    Destroy(self);
  }
}

// Inspector on CollisionTest (FireBolt):
//   layerFilter      → Enemy
//   velocityFilter   → 1.0f
//   debugView        → true
//   onCollisionEnter → FireBoltImpact.OnHitEnemy
```

---

### TriggerTest

Fires `UnityEvent`s on trigger enter/stay/exit. Filtering uses **OR** logic: passes if the other object matches the tag, **or** the name, **or** the layer mask.

| Field | Description |
|---|---|
| `tagFilter` | Match by Unity tag (`[Tag]` picker) |
| `nameFilter` | Match by exact object name |
| `layerFilter` | Match by layer mask |
| `onTriggerEnter/Stay/Exit` | `UnityEvent<GameObject, Collider>` |
| `debugView` | Draws the trigger collider each frame |

Requires a `Collider` with **Is Trigger** enabled.

```c#
using FronkonGames.GameWork.Foundation.Prototype;
using UnityEngine;

// Scene:
//   HealthPickup , BoxCollider (Is Trigger) + TriggerTest

public class HealthPickupZone : MonoBehaviour
{
  [SerializeField] private TriggerTest triggerTest;

  public void OnPlayerEnter(GameObject self, Collider other)
  {
    other.GetComponent<PlayerHealth>()?.Heal(50);
    Destroy(self);
  }
}

// Inspector on TriggerTest (HealthPickup):
//   tagFilter        → Player
//   debugView        → true
//   onTriggerEnter   → HealthPickupZone.OnPlayerEnter
```

---

## Motion

### FaceTo

Rotates to look at a target `Transform`, with per-axis locks that preserve the original euler angle on locked axes.

| Field | Description |
|---|---|
| `Target` | Look-at transform (runtime settable) |
| `lockX` / `lockY` / `lockZ` | Freeze individual euler components |
| `debugView` | Draws a `DebugDraw` arrow toward the target |

```c#
using FronkonGames.GameWork.Foundation.Prototype;
using UnityEngine;

// Scene:
//   Turret
//     └ TurretHead , FaceTo

public class TurretSetup : MonoBehaviour
{
  [SerializeField] private Transform playerBody;
  [SerializeField] private FaceTo turretHead;

  private void Start()
  {
    turretHead.Target = playerBody;
  }
}

// Inspector on FaceTo (TurretHead):
//   lockX     → true     // keep barrel level
//   lockZ     → true
//   debugView → true
```

---

### Follower

Moves toward a target using `Vector3.MoveTowards`. Stops when within `minDistanceToTarget`. Optional look-at rotation.

| Field | Default | Description |
|---|---|---|
| `Target` |, | Transform to follow |
| `lookAtTarget` | `false` | Rotate toward target while moving |
| `moveSpeed` | `0.25` | Units per second |
| `rotateSpeed` | `0.25` | Look rotation speed |
| `minDistanceToTarget` | `0` | Stop moving inside this radius |
| `maxDistanceToTarget` | `0` | Reserved (debug arc only) |
| `ignoreX/Y/Z` | `false` | Lock individual position axes |
| `debugView` | `false` | Draws arcs, lines, and object name |

**Property:** `IsFollowing`, `true` while the target is outside `minDistanceToTarget`.

```c#
using FronkonGames.GameWork.Foundation.Prototype;
using UnityEngine;

// Scene:
//   GruntEnemy , Capsule + Follower

public class GruntSpawner : MonoBehaviour
{
  [SerializeField] private Transform playerBody;
  [SerializeField] private Follower gruntAI;

  private void Start()
  {
    gruntAI.Target = playerBody;
  }

  private void Update()
  {
    if (gruntAI.IsFollowing == false)
      Debug.Log("Grunt reached melee range.");
  }
}

// Inspector on Follower (GruntEnemy):
//   moveSpeed            → 3.0f
//   lookAtTarget         → true
//   rotateSpeed          → 4.0f
//   minDistanceToTarget  → 1.5f
//   debugView            → true
```

---

### Mover

Moves along local **forward** (`transform.forward`). Uses `Transform.Translate` when no `Rigidbody` is present; sets `rigidbody.linearVelocity` in `FixedUpdate` otherwise.

| Field | Description |
|---|---|
| `Speed` | Units per second (runtime get/set) |
| `debugView` | Draws forward arrow and object name |

```c#
using FronkonGames.GameWork.Foundation.Prototype;
using UnityEngine;

// Scene:
//   MovingPlatform , cube aimed along the track + Mover

public class ConveyorPlatform : MonoBehaviour
{
  [SerializeField] private Mover platformMover;

  private void OnValidate()
  {
    if (platformMover == null)
      platformMover = GetComponent<Mover>();
  }

  private void Start()
  {
    platformMover.Speed = 5.0f;
    transform.rotation = Quaternion.LookRotation(Vector3.right);  // slide along +X
  }
}
```

---

### Rotator

Continuous local rotation. Without a `Rigidbody`, uses `transform.Rotate`. With a `Rigidbody`, sets `angularVelocity`.

| Field | Description |
|---|---|
| `AngularSpeed` | Degrees per second on X, Y, Z (local space). Runtime get/set. |

```c#
using FronkonGames.GameWork.Foundation.Prototype;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
  [SerializeField] private GameObject coinPrefab;
  [SerializeField] private Vector3 spinSpeed = new(0.0f, 90.0f, 0.0f);

  private void Start()
  {
    GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
    Rotator rotator = coin.AddComponent<Rotator>();
    rotator.AngularSpeed = spinSpeed;
  }
}
```

---

### Swing

Oscillates rotation on yaw, pitch, and roll using cosine interpolation between configurable min/max angles. Stores the original rotation on enable.

| Property | Description |
|---|---|
| `MinMaxYaw` / `MinMaxPitch` / `MinMaxRoll` | Angle range per axis (degrees). Runtime get/set. |
| `SpeedYaw` / `SpeedPitch` / `SpeedRoll` | Oscillation rate per axis. Runtime get/set. |

```c#
using FronkonGames.GameWork.Foundation.Prototype;
using UnityEngine;

public class SignSpawner : MonoBehaviour
{
  [SerializeField] private GameObject signPrefab;

  private void Start()
  {
    GameObject sign = Instantiate(signPrefab, transform.position, transform.rotation);
    Swing swing = sign.AddComponent<Swing>();
    swing.MinMaxYaw  = new Vector2(-15.0f, 15.0f);
    swing.SpeedYaw   = 0.5f;
    swing.MinMaxRoll = Vector2.zero;   // yaw only
  }
}
```

---

## Rendering

### MaterialScroller

Scrolls a material's texture offset each frame. Auto-resolves the `Renderer.sharedMaterial` if none is assigned.

| Property | Default | Description |
|---|---|---|
| `Material` | auto | Material to animate. Runtime get/set. Auto-resolves from `Renderer` on Start if null. |
| `ScrollSpeed` |, | UV offset delta per second. Runtime get/set. |
| `TextureName` | `"_MainTex"` | Shader property name. Runtime get/set. |

Offset wraps with `Remainder(Vector2.one)` to stay in 0–1 range.

```c#
using FronkonGames.GameWork.Foundation.Prototype;
using UnityEngine;

public class BeltSpawner : MonoBehaviour
{
  [SerializeField] private GameObject beltPrefab;
  [SerializeField] private Vector2 uvScroll = new(0.5f, 0.0f);

  private void Start()
  {
    GameObject belt = Instantiate(beltPrefab, transform.position, Quaternion.identity);
    MaterialScroller scroller = belt.AddComponent<MaterialScroller>();
    scroller.Material    = belt.GetComponent<Renderer>().sharedMaterial;
    scroller.ScrollSpeed = uvScroll;
    scroller.TextureName = "_BaseMap";
  }
}
```

---

## Quick reference

| Component | Purpose | Requires |
|---|---|---|
| [FirstPersonCamera](./FirstPersonCamera.cs) | Mouse-look camera | `Camera` |
| [ThirdPersonCamera](./ThirdPersonCamera.cs) | Orbit camera with collision | `Camera`, follow target |
| [FreeCamera](./FreeCamera.cs) | Fly camera | `Camera` |
| [Screenshooter](./Screenshooter.cs) | Async screenshots |, |
| [HardwareMonitor](./HardwareMonitor.cs) | FPS/memory overlay | UI `Text` refs |
| [CollisionTest](./CollisionTest.cs) | Collision events | `Collider`, usually `Rigidbody` |
| [TriggerTest](./TriggerTest.cs) | Trigger events | Trigger `Collider` |
| [FaceTo](./FaceTo.cs) | Look at target |, |
| [Follower](./Follower.cs) | Chase / follow target |, |
| [Mover](./Mover.cs) | Move along forward |, |
| [Rotator](./Rotator.cs) | Spin continuously |, |
| [Swing](./Swing.cs) | Oscillating rotation |, |
| [MaterialScroller](./MaterialScroller.cs) | Scroll UV offset | `Renderer` or material |

---

## Typical greybox setup

```
Player
├── Capsule + Rigidbody
├── Follower (disabled) or custom movement
└── Child: Camera + FirstPersonCamera
   , or —
    Main Camera + ThirdPersonCamera (follow = Player)

Environment
├── Floor with MaterialScroller
├── Trigger zone + TriggerTest (pickup logic via UnityEvents)
└── Moving hazard + Mover + Rotator

Debug
├── HardwareMonitor (Canvas overlay)
├── Screenshooter (any GameObject)
└── FreeCamera (swap to inspect the scene)
```

All prototype components integrate with [DebugDraw](../Draw/README.md) when `debugView` is enabled.
