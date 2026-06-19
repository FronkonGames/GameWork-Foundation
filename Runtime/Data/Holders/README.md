# Data Holders

Typed ScriptableObject holders for decoupled data configuration:

```c#
// Create a holder asset: Create > GameWork > Data Holders > Float Holder
[CreateAssetMenu(menuName = "GameWork/Data Holders/Float Holder")]
public class FloatHolder : ScriptableObject, IValueHolder<float>
{
  [SerializeField] private float value = 0f;
  public float GetValue() => value;
  public void SetValue(float newValue) => value = newValue;
}

// Use Option for flexible local/global configuration
[System.Serializable]
public class Option<TValue, THolder> where THolder : ScriptableObject, IValueHolder<TValue>
{
  // Mode: Disabled, LocalValue, GlobalValue
  // Value returns local or global based on mode
}
```

Available holders: Bool, Int, Float, String, Color, Vector2/3/4, Quaternion, GameObject, Transform, AudioClip, Material, Sprite, Texture, LayerMask, Collider, Collider2D, Rigidbody, Rigidbody2D, RectTransform.
