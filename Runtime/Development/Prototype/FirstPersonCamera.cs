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
  /// <summary> Simple first person camera. </summary>
  /// <remarks> This component is intended for use in prototypes only. </remarks>
  [RequireComponent(typeof(Camera))]
  public class FirstPersonCamera : CachedMonoBehaviour
  {
    [SerializeField]
    private float smoothRotation = 10.0f;

    [SerializeField]
    private Vector2 mouseSensitivity = Vector2.one;

    [SerializeField]
    private Vector2 pitchLimits = new(-80.0f, 80.0f);

    [SerializeField]
    private bool cursorLock = true;

    private Vector2 mouse;

    private void OnEnable()
    {
      mouse = new Vector2(this.transform.rotation.eulerAngles.x, this.transform.rotation.eulerAngles.y);

      this.transform.rotation = Quaternion.Euler(mouse.y, mouse.x, 0.0f);
    }

    private void Update()
    {
      if (Input.GetMouseButton(1) == true)
      {
        mouse.x += Input.GetAxis("Mouse X") * mouseSensitivity.x;
        mouse.y -= Input.GetAxis("Mouse Y") * mouseSensitivity.y;

        mouse.y = mouse.y.Clamp(pitchLimits.x, pitchLimits.y);

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.Euler(mouse.y, mouse.x, 0.0f), smoothRotation * Time.deltaTime);
      }
    }

    public void FixedUpdate()
    {
      if (cursorLock == true)
      {
        if (Input.GetMouseButton(1) == true)
        {
          if (Application.isFocused == true)
          {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
          }
        }
        else
        {
          Cursor.lockState = CursorLockMode.None;
          Cursor.visible = true;
        }
      }
    }
  }
}
