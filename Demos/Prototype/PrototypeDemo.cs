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
using UnityEngine.UI;

namespace FronkonGames.GameWork.Foundation.Prototype
{
  /// <summary> Prototype demo. </summary>
  public class PrototypeDemo : CachedMonoBehaviour
  {
    [SerializeField]
    private FreeCamera freeCamera;

    [SerializeField]
    private ThirdPersonCamera thirdPersonCamera;

    [SerializeField]
    private FirstPersonCamera firstPersonCamera;

    [SerializeField]
    private Text switchCamerasButtonText;

    [SerializeField]
    private Text versionText;

    private enum CamerasCycle
    {
      Free,
      Orbit,
      FPS,
    }

    private CamerasCycle currentCamera = CamerasCycle.Free;

    private void OnEnable()
    {
      versionText.text = Version.MajorMinorCommits;
      switchCamerasButtonText.text = $"Switch cameras ({currentCamera})";
    }

    private void Update()
    {
      DebugDraw.Circle(new Vector3(0.0f, 0.5f, 0.0f), 4.0f, null, null, false);
    }

    public void OnMoverExits(GameObject sender, Collider col)
    {
      col.gameObject.transform.forward = -col.gameObject.transform.forward;
    }

    public void OnPlayerCollide(GameObject sender, Collision col)
    {
      if (col.gameObject.TryGetComponent<CachedMonoBehaviour>(out var behaviour) == true)
      {
        if (behaviour.rigidbody != null)
          behaviour.rigidbody.AddForce(-5.0f * col.contacts[0].normal + new Vector3(0.0f, 5.0f, 0.0f), ForceMode.Impulse);
      }
    }

    public void SwitchCameras()
    {
      switch (currentCamera)
      {
        case CamerasCycle.Free:
          freeCamera.enabled = false;
          thirdPersonCamera.enabled = true;
          firstPersonCamera.gameObject.SetActive(false);
          currentCamera = CamerasCycle.Orbit;
          break;
        case CamerasCycle.Orbit:
          freeCamera.enabled = false;
          thirdPersonCamera.enabled = false;
          firstPersonCamera.gameObject.SetActive(true);
          currentCamera = CamerasCycle.FPS;
          break;
        case CamerasCycle.FPS:
          freeCamera.enabled = true;
          thirdPersonCamera.enabled = false;
          firstPersonCamera.gameObject.SetActive(false);
          currentCamera = CamerasCycle.Free;
          break;
      }

      switchCamerasButtonText.text = $"Switch cameras ({currentCamera})";
    }
  }
}
