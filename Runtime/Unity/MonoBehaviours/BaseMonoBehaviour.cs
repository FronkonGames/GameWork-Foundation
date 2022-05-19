////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Martin Bustos @FronkonGames <fronkongames@gmail.com>
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of
// the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// MonoBehaviour base.
  /// </summary>
  public abstract class BaseMonoBehaviour : MonoBehaviour
  {
    /// <summary>Transform cached.</summary>
    public new Transform transform      => cachedTransform ?? (cachedTransform = base.transform);

    /// <summary>Animation cached.</summary>
    public new Animation animation      => chachedAnimation ?? (chachedAnimation = this.GetComponent<Animation>());

    /// <summary>AudioSource cached.</summary>
    public new AudioSource audio        => cachedAudio ?? (cachedAudio = this.GetComponent<AudioSource>());

    /// <summary>Collider cached.</summary>
    public new Collider collider        => cachedCollider ?? (cachedCollider = this.GetComponent<Collider>());

    /// <summary>RigidBody cached.</summary>
    public new Rigidbody rigidbody      => cachedRigidbody ?? (cachedRigidbody = this.GetComponent<Rigidbody>());

    /// <summary>Animator cached.</summary>
    public Animator animator            => cachedAnimator ?? (cachedAnimator = this.GetComponent<Animator>());

    /// <summary>Collider2D cached.</summary>
    public new Collider2D collider2D    => cachedCollider2D ?? (cachedCollider2D = this.GetComponent<Collider2D>());

    /// <summary>Rigidbody2D cached.</summary>
    public new Rigidbody2D rigidbody2D  => cachedRigidbody2D ?? (cachedRigidbody2D = this.GetComponent<Rigidbody2D>());

    /// <summary>RectTransform cached.</summary>
    public RectTransform rectTransform  => cachedRectTransform ?? (cachedRectTransform = this.GetComponent<RectTransform>());

    [HideInInspector, System.NonSerialized]
    private Transform cachedTransform;

    [HideInInspector, System.NonSerialized]
    private Animation chachedAnimation;

    [HideInInspector, System.NonSerialized]
    private AudioSource cachedAudio;

    [HideInInspector, System.NonSerialized]
    private Animator cachedAnimator;

    [HideInInspector, System.NonSerialized]
    private Collider cachedCollider;

    [HideInInspector, System.NonSerialized]
    private Rigidbody cachedRigidbody;

    [HideInInspector, System.NonSerialized]
    private Collider2D cachedCollider2D;

    [HideInInspector, System.NonSerialized]
    private Rigidbody2D cachedRigidbody2D;

    [HideInInspector, System.NonSerialized]
    private RectTransform cachedRectTransform;

    /// <summary>
    /// Clear all cached components.
    /// </summary>
    public void ClearCachedComponents()
    {
      cachedTransform = null;
      chachedAnimation = null;
      cachedAudio = null;
      cachedAnimator = null;
      cachedCollider = null;
      cachedRigidbody = null;
      cachedCollider2D = null;
      cachedRigidbody2D = null;
      cachedRectTransform = null;
    }
  }
}
