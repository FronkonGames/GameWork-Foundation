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
  /// <summary> Gently swing. </summary>
  /// <remarks> This component is intended for use in prototypes only. </remarks>
  public class Swing : CachedMonoBehaviour
  {
    [SerializeField]
    private Vector2 minMaxYaw = new(-30.0f, 30.0f);

    [SerializeField]
    private float speedYaw = 0.25f;

    [SerializeField]
    private Vector2 minMaxPitch = new(-30.0f, 30.0f);

    [SerializeField]
    private float speedPitch = 0.25f;

    [SerializeField]
    private Vector2 minMaxRoll = new(-30.0f, 30.0f);

    [SerializeField]
    private float speedRoll = 0.25f;

    private Quaternion originalRotation;
    private Vector3 swing;

    private void OnEnable()
    {
      originalRotation = this.transform.rotation;
      swing = Vector3.zero;
    }

    private void Update()
    {
      swing.x += speedYaw * Time.deltaTime;
      swing.x -= Mathf.Floor(swing.x);

      swing.y += speedPitch * Time.deltaTime;
      swing.y -= Mathf.Floor(swing.y);

      swing.z += speedRoll * Time.deltaTime;
      swing.z -= Mathf.Floor(swing.z);

      float yaw = Mathf.Lerp(minMaxYaw.x, minMaxYaw.y, 0.5f - 0.5f * Mathf.Cos(2.0f * Mathf.PI * swing.x));
      float pitch = Mathf.Lerp(minMaxPitch.x, minMaxPitch.y, 0.5f - 0.5f * Mathf.Cos(2.0f * Mathf.PI * swing.y));
      float roll = Mathf.Lerp(minMaxRoll.x, minMaxRoll.y, 0.5f - 0.5f * Mathf.Cos(2.0f * Mathf.PI * swing.z));

      this.transform.rotation = Quaternion.Euler(roll, yaw, pitch) * originalRotation;
    }
  }
}
