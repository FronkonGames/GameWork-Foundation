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
using System;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Free camera. </summary>
  [RequireComponent(typeof(Camera))]
  public class FreeCamera : BaseMonoBehaviour
  {
    [SerializeField]
    private float movementSpeed = 10.0f;

    [SerializeField]
    private float movementSmothness = 10.0f;

    [SerializeField]
    private float rotationSmothness = 10.0f;

    [SerializeField]
    private float mouseSensitivity = 5.0f;

    [SerializeField]
    private float turboMultiply = 5.0f;

    private Vector3 currentSpeed;
    private Vector3 currentRotation;

    private float currentTurbo = 1.0f;
    private float speed;
    private float upSpeed;

    private float SpeedFactor(float speed)
    {
      if (speed < 0.1f)
        return 0.01f;
      else if (speed < 0.5f)
        return 0.1f;
      else if (speed < 1.0f)
        return 0.2f;
      else if (speed < 15.0f)
        return 1.0f;
      else if (speed < 50.0f)
        return 5.0f;

      return 10.0f;
    }

    private void Start()
    {
      currentRotation = transform.rotation.eulerAngles;
      speed = movementSpeed;
    }

    private void Update()
    {
      float forward = Input.GetAxis("Vertical") * Time.smoothDeltaTime * speed;
      float right = Input.GetAxis("Horizontal") * Time.smoothDeltaTime * speed;

      currentTurbo = Mathf.Lerp(currentTurbo, Input.GetKey(KeyCode.LeftShift) == true ? turboMultiply : 1.0f, Time.smoothDeltaTime * 5.0f);

      forward *= currentTurbo;
      right *= currentTurbo;

      if (Input.GetMouseButton(1) == true)
      {
        currentRotation.x -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        currentRotation.y += Input.GetAxis("Mouse X") * mouseSensitivity;

        if (Input.GetAxis("Mouse ScrollWheel") < 0.0f)
          speed -= SpeedFactor(speed);
        else if (Input.GetAxis("Mouse ScrollWheel") > 0.0f)
          speed += SpeedFactor(speed);

        speed = Math.Max(0.0f, speed);
      }

      currentSpeed.z = Mathf.Lerp(currentSpeed.z, forward, Time.smoothDeltaTime * movementSmothness);
      currentSpeed.x = Mathf.Lerp(currentSpeed.x, right, Time.smoothDeltaTime * movementSmothness);

      transform.position += transform.forward * currentSpeed.z;
      transform.position += transform.right * currentSpeed.x;
      transform.position += transform.up * currentSpeed.y;

      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(currentRotation), Time.smoothDeltaTime * rotationSmothness);

      if (Input.GetKey(KeyCode.LeftControl) == true || Input.GetKey(KeyCode.Q) == true)
        upSpeed = Mathf.Lerp(upSpeed, 1.0f, Time.smoothDeltaTime * movementSmothness);
      else if (Input.GetButton("Jump") == true || Input.GetKey(KeyCode.E) == true)
        upSpeed = Mathf.Lerp(upSpeed, -1.0f, Time.smoothDeltaTime * movementSmothness);
      else
        upSpeed = Mathf.Lerp(upSpeed, 0.0f, Time.smoothDeltaTime * movementSmothness);

      this.transform.position += currentTurbo * speed * Time.smoothDeltaTime * upSpeed * Vector3.down;
    }

    public void FixedUpdate()
    {
      if (Input.GetMouseButton(1) == true)
      {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
      }
      else
      {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
      }
    }
  }
}
