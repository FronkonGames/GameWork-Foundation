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
  /// <summary> Follow a target. </summary>
  public class Follower : BaseMonoBehaviour
  {
    /// <summary> Type of movement. </summary>
    public enum MovementType
    {
      /// <summary> Moves the object using its Transform. </summary>
      Transform,

      /// <summary> Use RigidBody.MovePosition. Needs a RigidBody. </summary>
      MovePosition,

      /// <summary> Use RigidBody.AddForce. Needs a RigidBody. </summary>
      AddForce
    }

    /// <summary> Following the target or already reached it? </summary>
    public bool IsFollowing { get; private set; } = true;

    /// <summary> The target to follow. </summary>
    public Transform Target { get { return target; } set { target = value; } }

    [SerializeField]
    private MovementType movementType;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private bool lookAtTarget;

    [SerializeField]
    private float moveSpeed = 0.25f;

    [SerializeField]
    private float force = 100.0f;

    [SerializeField]
    private float rotateSpeed = 0.25f;

    [SerializeField]
    private float minDistanceToTarget = 0.0f;
    
    [SerializeField]
    private float maxDistanceToTarget = 0.0f;

    [SerializeField]
    private bool ignoreX;

    [SerializeField]
    private bool ignoreY;

    [SerializeField]
    private bool ignoreZ;

    private void Update()
    {
      if (target != null)
      {
        IsFollowing = true;
        float sqrDistance = (this.transform.position - target.position).sqrMagnitude;

        if (minDistanceToTarget > 0.0f)
        {
          DebugDraw.Circle(this.transform.position, minDistanceToTarget);

          IsFollowing = sqrDistance > (minDistanceToTarget * minDistanceToTarget);
        }

        if (maxDistanceToTarget > 0.0f)
        {
          DebugDraw.Circle(this.transform.position, maxDistanceToTarget);

          IsFollowing = IsFollowing == true && sqrDistance < (maxDistanceToTarget * maxDistanceToTarget);
        }

        if (IsFollowing == true)
        {
          Vector3 targetPosition = target.transform.position;
          targetPosition.x = ignoreX == true ? this.transform.position.x : targetPosition.x;
          targetPosition.y = ignoreY == true ? this.transform.position.y : targetPosition.y;
          targetPosition.z = ignoreZ == true ? this.transform.position.z : targetPosition.z;

          Vector3 direction = (targetPosition - this.transform.position).normalized;
          DebugDraw.Arrow(this.transform.position, direction);

          switch (movementType)
          {
            case MovementType.Transform:
              transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
              break;
            case MovementType.MovePosition:
              if (rigidbody != null)
                rigidbody.MovePosition(Vector3.MoveTowards(this.transform.position, targetPosition, moveSpeed * Time.deltaTime));
              else
                transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
              break;
            case MovementType.AddForce:
              if (rigidbody != null)
                rigidbody.AddForce(force * Time.deltaTime * direction);
              else
                transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
              break;
          }

          if (lookAtTarget == true)
          {
            Vector3 targetDirection = targetPosition - this.transform.position;
            Vector3 newDirection = Vector3.RotateTowards(this.transform.forward, targetDirection, rotateSpeed * Time.deltaTime, 0.0f);

            transform.rotation = Quaternion.LookRotation(newDirection);
          }
        }
      }
    }
  }
}
