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
  /// <summary> Simple third person camera. </summary>
  [RequireComponent(typeof(Camera))]
  public class ThirdPersonCamera : BaseMonoBehaviour
  {
    [SerializeField]
    private Transform follow;

    [SerializeField]
    private Vector3 followOffset = new(0.0f, 1.5f, 0.0f);

    [SerializeField]
    private Vector3 directionOffset = new(0f, 0f, 2.0f);

    [SerializeField]
    private Vector2 distanceRange = new Vector2(5.0f, 8.0f);

    [SerializeField]
    private Vector2 rotationRange = new Vector2(-10.0f, 60.0f);

    [SerializeField]
    private float rotationSensitivity = 10.0f;

    [SerializeField, Range(0.1f, 1.0f)]
    private float rotationSpeed = 0.5f;

    [SerializeField, Range(0.0f, 1.0f)]
    private float hardFollow = 0.75f;

    [SerializeField]
    private LayerMask obstacleLayer = -1;

    [SerializeField]
    private float collisionOffset = 1.0f;

    [SerializeField]
    private bool cursorLock = true;

    private float targetDistance;
    private float animatedDistance;

    private Vector2 targetSphericRotation = Vector2.zero;
    private Vector2 animatedSphericRotation = Vector2.zero;

    private bool rotateCamera = true;
    private RaycastHit obstacleHit;

    private Vector3 targetPosition;

    private void Start()
    {
      targetDistance = (distanceRange.x + distanceRange.y) * 0.5f;
      animatedDistance = distanceRange.y;

      targetSphericRotation = new Vector2(0.0f, 23.0f);
      animatedSphericRotation = targetSphericRotation;
    }

    private void Update()
    {
      if (follow == null)
        return;

      targetDistance -= Input.GetAxis("Mouse ScrollWheel") * 5.0f;

      targetSphericRotation.x += Input.GetAxis("Mouse X") * rotationSensitivity * (rotateCamera == true ? 1.0f : 0.0f);
      targetSphericRotation.y -= Input.GetAxis("Mouse Y") * rotationSensitivity * (rotateCamera == true ? 1.0f : 0.0f);

      if (!obstacleHit.transform)
        targetDistance = Mathf.Clamp(targetDistance, distanceRange.x, distanceRange.y);

      animatedDistance = Mathf.Lerp(animatedDistance, targetDistance, Time.deltaTime * 8.0f);

      targetSphericRotation.y = targetSphericRotation.y.Clamp(rotationRange.x, rotationRange.y);

      if (rotationSpeed < 1.0f)
        animatedSphericRotation = new Vector2(Mathf.LerpAngle(animatedSphericRotation.x, targetSphericRotation.x, Time.deltaTime * 30.0f * rotationSpeed),
                                              Mathf.LerpAngle(animatedSphericRotation.y, targetSphericRotation.y, Time.deltaTime * 30.0f * rotationSpeed));
      else
        animatedSphericRotation = targetSphericRotation;

      Quaternion rotation = Quaternion.Euler(animatedSphericRotation.y, animatedSphericRotation.x, 0.0f);
      transform.rotation = rotation;

      Vector3 targetPosition = follow.transform.position + followOffset;

      if (hardFollow < 1.0f)
        targetPosition = Vector3.Lerp(this.targetPosition, targetPosition, Time.deltaTime * Mathf.Lerp(0.5f, 40.0f, hardFollow));

      this.targetPosition = targetPosition;

      Vector3 followPoint = follow.transform.position + followOffset + transform.TransformVector(directionOffset);
      Quaternion cameraDir = Quaternion.Euler(targetSphericRotation.y, targetSphericRotation.x, 0.0f);
      Ray directionRay = new(followPoint, cameraDir * -Vector3.forward);

      if (Physics.Raycast(directionRay, out obstacleHit, targetDistance + collisionOffset, obstacleLayer, QueryTriggerInteraction.Ignore) == true)
      {
        transform.position = obstacleHit.point - directionRay.direction * collisionOffset;

        directionRay.Draw(obstacleHit);
      }
      else
      {
        Vector3 rotationOffset = transform.rotation * -Vector3.forward * animatedDistance;
        transform.position = targetPosition + rotationOffset + transform.TransformVector(directionOffset);
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
            rotateCamera = true;
          }
        }
        else
        {
          Cursor.lockState = CursorLockMode.None;
          Cursor.visible = true;
          rotateCamera = false;
        }
      }
    }
  }
}
