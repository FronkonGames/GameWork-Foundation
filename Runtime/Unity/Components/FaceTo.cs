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
  /// <summary> Orients the object so that it faces a target. </summary>
  public class FaceTo : BaseMonoBehaviour
  {
    /// <summary> The target you are looking at. </summary>
    public Transform Target { get { return target; } set { target = value; } }

    [SerializeField]
    private Transform target;

    [SerializeField]
    private bool lockX;

    [SerializeField]
    private bool lockY;

    [SerializeField]
    private bool lockZ;

    private Vector3 eulerOriginal;

    private void Awake() => eulerOriginal = this.gameObject.transform.eulerAngles;

    private void Update()
    {
      Vector3 euler = eulerOriginal;

      if (target != null)
      {
        DebugDraw.Arrow(this.transform.position, (target.transform.position - this.transform.position).normalized);

        Vector3 eulerTarget = Quaternion.LookRotation(target.position - this.gameObject.transform.position).eulerAngles;
        euler.x = lockX == true ? eulerOriginal.x : eulerTarget.x;
        euler.y = lockY == true ? eulerOriginal.y : eulerTarget.y;
        euler.z = lockZ == true ? eulerOriginal.z : eulerTarget.z;
      }

      this.gameObject.transform.rotation = Quaternion.Euler(euler);
    }
  }
}
