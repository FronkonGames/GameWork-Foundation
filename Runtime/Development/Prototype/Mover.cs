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

namespace FronkonGames.GameWork.Foundation.Prototype
{
  /// <summary> Moves an object in his direction. </summary>
  /// <remarks> This component is intended for use in prototypes only. </remarks>
  public class Mover : CachedMonoBehaviour
  {
    /// <summary> Speed. </summary>
    public float Speed { get => speed; set { speed = value; } }

    [SerializeField]
    private float speed = 0.0f;

    [Space, SerializeField]
    private bool debugView;

    private void Update()
    {
      if (rigidbody == null)
        this.transform.Translate(speed * Time.deltaTime * this.transform.forward, Space.Self);

      if (debugView == true)
      {
        DebugDraw.Arrow(this.transform.position, this.transform.forward);
        DebugDraw.Text(this.transform.position, this.gameObject.name);
      }
    }

    private void FixedUpdate()
    {
      if (rigidbody != null)
        rigidbody.velocity = speed * this.transform.forward;
    }
  }
}
