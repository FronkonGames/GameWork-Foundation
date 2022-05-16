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
    public new Transform transform
    {
      get
      {
        if (cachedTransform == null)
          cachedTransform = base.transform;

        return cachedTransform;
      }
    }

    public new Animation animation
    {
      get
      {
        if (chachedAnimation == null)
          chachedAnimation = this.GetComponent<Animation>();

        return chachedAnimation;
      }
    }

    public new AudioSource audio
    {
      get
      {
        if (cachedAudio == null)
          cachedAudio = this.GetComponent<AudioSource>();

        return cachedAudio;
      }
    }

    public new Collider collider
    {
      get
      {
        if (cachedCollider == null)
          cachedCollider = this.GetComponent<Collider>();

        return cachedCollider;
      }
    }

    public new Rigidbody rigidbody
    {
      get
      {
        if (cachedRigidbody == null)
          cachedRigidbody = this.GetComponent<Rigidbody>();

        return cachedRigidbody;
      }
    }

    public Animator animator
    {
      get
      {
        if (cachedAnimator == null)
          cachedAnimator = this.GetComponent<Animator>();

        return cachedAnimator;
      }
    }

    public new Collider2D collider2D
    {
      get
      {
        if (cachedCollider2D == null)
          cachedCollider2D = this.GetComponent<Collider2D>();

        return cachedCollider2D;
      }
    }

    public new Rigidbody2D rigidbody2D
    {
      get
      {
        if (cachedRigidbody2D == null)
          cachedRigidbody2D = this.GetComponent<Rigidbody2D>();

        return cachedRigidbody2D;
      }
    }

    public RectTransform rectTransform
    {
      get
      {
        if (cachedRectTransform == null)
          cachedRectTransform = this.GetComponent<RectTransform>();

        return cachedRectTransform;
      }
    }

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
