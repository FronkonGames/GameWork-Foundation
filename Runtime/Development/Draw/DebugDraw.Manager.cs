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
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Drawing of objects for development. </summary>
  /// <remarks> Only available in the Editor. </remarks>
  public partial class DebugDraw : CachedMonoBehaviour
  {
    private static DebugDraw Instance
    {
      get
      {
        if (instance == null)
        {
          instance = FindAnyObjectByType<DebugDraw>();
          if (instance == null)
          {
            GameObject manager = new("DebubDraw") { hideFlags = HideFlags.HideAndDontSave };
            instance = manager.AddComponent<DebugDraw>();
          }
        }

        return instance;
      }
    }

    internal static DebugDraw instance;

    private void Awake()
    {
      EditorApplication.playModeStateChanged += OnModeStateChanged;
      UnityEditor.SceneManagement.EditorSceneManager.sceneOpening += OnSceneOpening;

      if (GraphicsSettings.renderPipelineAsset == null)
        Camera.onPostRender += OnDebugRender;
      else
        RenderPipelineManager.endCameraRendering += OnRendered;
    }

    private void OnDestroy()
    {
      EditorApplication.playModeStateChanged += OnModeStateChanged;
      UnityEditor.SceneManagement.EditorSceneManager.sceneOpening -= OnSceneOpening;

      if (GraphicsSettings.renderPipelineAsset == null)
        Camera.onPostRender -= OnDebugRender;
      else
        RenderPipelineManager.endCameraRendering -= OnRendered;
    }

    private void OnRendered(ScriptableRenderContext context, Camera camera) => OnDebugRender(camera);

    private void OnModeStateChanged(PlayModeStateChange state) => Clear();

    private void OnSceneOpening(string path, UnityEditor.SceneManagement.OpenSceneMode mode) => Clear();
  }
}
#endif
