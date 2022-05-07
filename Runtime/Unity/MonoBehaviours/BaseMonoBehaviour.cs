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
    public new Transform transform => cachedTransform != null ? cachedTransform : cachedTransform = base.transform;

    public new Animation animation => chachedAnimation != null ? chachedAnimation : chachedAnimation = this.GetComponent<Animation>();

    public new AudioSource audio => cachedAudio != null ? cachedAudio : cachedAudio = this.GetComponent<AudioSource>();

    public new Collider collider => cachedCollider != null ? cachedCollider : cachedCollider = this.GetComponent<Collider>();

    public new Rigidbody rigidbody => cachedRigidbody != null ? cachedRigidbody : cachedRigidbody = this.GetComponent<Rigidbody>();

    public Animator animator => cachedAnimator != null ? cachedAnimator : cachedAnimator = this.GetComponent<Animator>();

    public new Collider2D collider2D => cachedCollider2D != null ? cachedCollider2D : cachedCollider2D = this.GetComponent<Collider2D>();

    public new Rigidbody2D rigidbody2D => cachedRigidbody2D != null ? cachedRigidbody2D : cachedRigidbody2D = this.GetComponent<Rigidbody2D>();

    public RectTransform rectTransform => cachedRectTransform != null ? cachedRectTransform : cachedRectTransform = this.GetComponent<RectTransform>();

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
