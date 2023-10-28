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
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using Unity.Collections;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Asynchronous screen capture. </summary>
  [ExecuteAlways]
  public class Screenshooter : BaseMonoBehaviour
  {
    /// <summary> Image encoders. </summary>
    private enum Encoders
    {
      JPG,
      PNG,
      TGA,
    }

    [SerializeField]
    private Encoders encoder = Encoders.PNG;

    [SerializeField, KeyCode, Label("First key")]
    private KeyCode key1 = KeyCode.LeftControl;

    [SerializeField, KeyCode, Label("Second key")]
    private KeyCode key2 = KeyCode.C;

    [SerializeField]
    private string prefix = "Screenshot";

    [SerializeField]
    private bool addTimestamp = true;

    [SerializeField, Folder]
    private string saveFolder = string.Empty;

    [SerializeField, Space, Button(nameof(Capture))]
    private int dummy;

    private RenderTexture grab;
    private RenderTexture flip;

    private NativeArray<byte> buffer;

    private const string FolderKey = "GameWork.Foundation.Screenshooter.Folder";
    private readonly Vector2 scale = new(1.0f, -1.0f);
    private readonly Vector2 offset = new(0.0f, 1.0f);

    /// <summary> Take a screenshot. </summary>
    public void Capture() => StartCoroutine(CaptureScreenshotAsync());

    private IEnumerator CaptureScreenshotAsync()
    {
      yield return new WaitForEndOfFrame();

      PrepareResources();

      ScreenCapture.CaptureScreenshotIntoRenderTexture(grab);
      Graphics.Blit(grab, flip, scale, offset);

      buffer = new NativeArray<byte>(Screen.width * Screen.height * 4, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
      AsyncGPUReadback.RequestIntoNativeArray(ref buffer, flip, 0, OnReadback);
    }

    private void OnReadback(AsyncGPUReadbackRequest request)
    {
      if (request.hasError == false)
      {
        string path = string.IsNullOrEmpty(prefix) == true ? "Screenshot" : prefix;

        if (addTimestamp == true)
        {
          DateTime now = DateTime.Now;
          path += "_" + now.ToString("yyyy-MM-dd_") + now.Hour + "h-" + now.Minute + "m-" + now.Second + "s";
        }

        if (string.IsNullOrEmpty(saveFolder) == true)
          path = Application.dataPath + "/" + path;
        else
          path = Path.Combine(saveFolder, Path.GetFileName(path));

        NativeArray<byte> encoded = new();
        switch (encoder)
        {
          case Encoders.JPG: encoded = ImageConversion.EncodeNativeArrayToJPG(buffer, flip.graphicsFormat, (uint)flip.width, (uint)flip.height); path += ".jpg"; break;
          case Encoders.PNG: encoded = ImageConversion.EncodeNativeArrayToPNG(buffer, flip.graphicsFormat, (uint)flip.width, (uint)flip.height); path += ".png"; break;
          case Encoders.TGA: encoded = ImageConversion.EncodeNativeArrayToTGA(buffer, flip.graphicsFormat, (uint)flip.width, (uint)flip.height); path += ".tga"; break;
        }

        if (Application.isEditor == true)
        {
          File.WriteAllBytes(path, encoded.ToArray());
          encoded.Dispose();
          buffer.Dispose();
#if UNITY_EDITOR
          UnityEditor.AssetDatabase.Refresh();
#endif
        }
        else
        {
          new System.Threading.Thread(() =>
          {
            System.Threading.Thread.Sleep(100);
            File.WriteAllBytes(path, encoded.ToArray());
            encoded.Dispose();
            buffer.Dispose();
          }).Start();
        }
      }
      else
        Log.Error("AsyncGPUReadback error");
    }

    private void PrepareResources()
    {
      if (grab == null)
        grab = new RenderTexture(Screen.width, Screen.height, 0);

      if (flip == null)
        flip = new RenderTexture(Screen.width, Screen.height, 0);
    }

    private void ReleaseResources()
    {
      GameObjectExtension.SafeDestroy(flip);
      GameObjectExtension.SafeDestroy(grab);
    }

    private void Update()
    {
      if (Input.GetKey(key1) == true && Input.GetKeyDown(key2) == true)
        Capture();
    }

    private void Start()
    {
      PrepareResources();

#if UNITY_EDITOR
      if (UnityEditor.EditorPrefs.GetString(FolderKey) != string.Empty)
        saveFolder = UnityEditor.EditorPrefs.GetString(FolderKey);
#endif
    }

    private void OnDestroy()
    {
      AsyncGPUReadback.WaitAllRequests();

      ReleaseResources();
    }

#if UNITY_EDITOR
    private void OnValidate() => UnityEditor.EditorPrefs.SetString(FolderKey, saveFolder);
#endif
  }
}
