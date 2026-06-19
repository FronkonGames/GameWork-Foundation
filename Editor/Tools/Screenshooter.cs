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
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Editor tool to capture screenshots of the Game View or any camera. </summary>
  public class Screenshooter : EditorWindow
  {
    private static readonly string PathPrefsKey      = $"{Settings.Editor.EditorPrefs}.Screenshoter.Path";
    private static readonly string FileNamePrefsKey  = $"{Settings.Editor.EditorPrefs}.Screenshoter.FileName";
    private static readonly string SuperSizePrefsKey = $"{Settings.Editor.EditorPrefs}.Screenshoter.SuperSize";

    private const string DefaultPath = "Assets/Screenshots";
    private const string DefaultFileName = "Screenshot_<date>_<time>_<index>";
    private const int DefaultSuperSize = 1;
    private const int MinSuperSize = 1;
    private const int MaxSuperSize = 10;
    private const float LabelWidth = 80.0f;

    private string screenshotPath = string.Empty;
    private string screenshotFileName = string.Empty;
    private int screenshotSuperSize = DefaultSuperSize;
#if UNITY_EDITOR_WIN
    private List<Transform> povs = new();
    private bool povsFoldout = true;
    private Texture2D lastPreview;
#endif
    private Vector2 scroll;
    private string lastMessage;
    private MessageType lastMessageType = MessageType.None;

    [MenuItem("Help/" + Settings.Menus.EditorFolder + "/Tools/Screenshooter")]
    public static void ShowWindow()
    {
      GetWindow<Screenshooter>(false, "Screenshooter");
    }

    private void OnEnable()
    {
      screenshotPath = PathPrefsKey.FromEditorPrefs(DefaultPath);
      screenshotFileName = FileNamePrefsKey.FromEditorPrefs(DefaultFileName);
      screenshotSuperSize = SuperSizePrefsKey.FromEditorPrefs(DefaultSuperSize).Clamp(MinSuperSize, MaxSuperSize);
    }

    private void OnDisable()
    {
      Persist();
#if UNITY_EDITOR_WIN
      DestroyPreview();
#endif
    }

    private void Persist()
    {
      screenshotPath.ToEditorPrefs(PathPrefsKey);
      screenshotFileName.ToEditorPrefs(FileNamePrefsKey);
      screenshotSuperSize.ToEditorPrefs(SuperSizePrefsKey);
    }

    private void OnGUI()
    {
      scroll = EditorGUILayout.BeginScrollView(scroll);

      GUILayout.BeginVertical("Box");
      {
        GUILayout.Label("Save to File", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal();
        {
          GUILayout.BeginVertical();
          {
            if (GUILayout.Button("Game View PNG file") == true)
              SaveGameViewToFile("png");

            if (GUILayout.Button("Game View JPG file") == true)
              SaveGameViewToFile("jpg");
          }
          GUILayout.EndVertical();

          GUILayout.BeginVertical();
          {
            if (GUILayout.Button("Scene View PNG file") == true)
              SaveSceneViewToFile("png");

            if (GUILayout.Button("Scene View JPG file") == true)
              SaveSceneViewToFile("jpg");
          }
          GUILayout.EndVertical();

          if (GUILayout.Button("Open folder", GUILayout.Width(100.0f), GUILayout.ExpandHeight(true)) == true && string.IsNullOrEmpty(screenshotPath) == false)
          {
            Directory.CreateDirectory(screenshotPath);
            EditorUtility.RevealInFinder(screenshotPath + "/");
          }
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10.0f);

        GUILayout.BeginHorizontal();
        {
          GUILayout.Label("Super Size", GUILayout.Width(LabelWidth));
          EditorGUI.BeginChangeCheck();
          screenshotSuperSize = (int)GUILayout.HorizontalSlider(screenshotSuperSize, MinSuperSize, MaxSuperSize);
          if (EditorGUI.EndChangeCheck() == true)
            screenshotSuperSize.ToEditorPrefs(SuperSizePrefsKey);
          GUILayout.Label(screenshotSuperSize.ToString(), GUILayout.Width(32.0f));
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        {
          GUILayout.Label("Path", GUILayout.Width(LabelWidth));
          EditorGUI.BeginChangeCheck();
          screenshotPath = EditorGUILayout.TextField(screenshotPath);
          if (EditorGUI.EndChangeCheck() == true)
            screenshotPath.ToEditorPrefs(PathPrefsKey);

          if (GUILayout.Button("...", GUILayout.Width(22.0f)) == true)
          {
            string selected = EditorUtility.OpenFolderPanel("Select Folder", screenshotPath, string.Empty);
            if (string.IsNullOrEmpty(selected) == false && selected.Equals(screenshotPath) == false)
            {
              screenshotPath = selected;
              screenshotPath.ToEditorPrefs(PathPrefsKey);
            }
          }

          if (GUILayout.Button("Open folder", GUILayout.Width(76.0f)) == true && string.IsNullOrEmpty(screenshotPath) == false)
            EditorUtility.RevealInFinder(screenshotPath + "/");
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        {
          GUILayout.Label("File name", GUILayout.Width(LabelWidth));
          EditorGUI.BeginChangeCheck();
          screenshotFileName = EditorGUILayout.TextField(screenshotFileName);
          if (EditorGUI.EndChangeCheck() == true)
            screenshotFileName.ToEditorPrefs(FileNamePrefsKey);
        }
        GUILayout.EndHorizontal();

        EditorGUILayout.HelpBox("File name tokens:\n<date>  dd-MM-yyyy\n<time>  HH-mm-ss\n<view>  Game / Scene (else '_scene' suffix)\n<index> auto-increment (0000, 0001, ...)", MessageType.Info);
      }
      GUILayout.EndVertical();

      GUILayout.Space(10.0f);

#if UNITY_EDITOR_WIN
      GUILayout.BeginVertical("Box");
      {
        GUILayout.Label("Copy to Clipboard", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal();
        {
          GUILayout.BeginVertical(GUILayout.Height(72.0f));
          {
            if (GUILayout.Button("Game View to Clipboard", GUILayout.ExpandHeight(true)) == true)
              CopyToClipboard(GetCameraTexture(FindFirstObjectByType<Camera>()));

            if (GUILayout.Button("Scene View to Clipboard", GUILayout.ExpandHeight(true)) == true)
              CopyToClipboard(GetSceneViewTexture());
          }
          GUILayout.EndVertical();

          if (lastPreview != null)
            GUILayout.Box(lastPreview, GUILayout.Width(128.0f), GUILayout.Height(72.0f));
          else
            GUILayout.Box("No preview", GUILayout.Width(128.0f), GUILayout.Height(72.0f));
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        {
          if (GUILayout.Button("Cameras to Clipboard", GUILayout.ExpandHeight(true)) == true)
            CopyCamerasToClipboard();

          if (GUILayout.Button("Add active", GUILayout.Width(90.0f)) == true)
          {
            Camera cam = FindFirstObjectByType<Camera>();
            if (cam == null)
              SetMessage("No active camera in the scene.", MessageType.Warning);
            else if (povs.Contains(cam.transform) == true)
              SetMessage("Camera already in the list.", MessageType.Warning);
            else
              povs.Add(cam.transform);
          }

          if (povs.Count > 0 && GUILayout.Button("Clear", GUILayout.Width(60.0f)) == true)
            povs.Clear();
        }
        GUILayout.EndHorizontal();

        povsFoldout = EditorGUILayout.Foldout(povsFoldout, $"Cameras ({povs.Count})", true);
        if (povsFoldout == true)
        {
          int removeIndex = -1;
          for (int i = 0; i < povs.Count; ++i)
          {
            GUILayout.BeginHorizontal();
            {
              povs[i] = (Transform)EditorGUILayout.ObjectField(povs[i], typeof(Transform), true);
              if (GUILayout.Button("X", GUILayout.Width(22.0f)) == true)
                removeIndex = i;
            }
            GUILayout.EndHorizontal();
          }
          if (removeIndex >= 0)
            povs.RemoveAt(removeIndex);
        }
      }
      GUILayout.EndVertical();
#else
      GUILayout.BeginVertical("Box");
      {
        GUILayout.Label("Copy to Clipboard", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox(
          "Clipboard copy is only available in the Windows Unity Editor. It uses System.Drawing and System.Windows.Forms from Editor/Plugins. Use Save to file on this platform.",
          MessageType.Info);
      }
      GUILayout.EndVertical();
#endif

      if (string.IsNullOrEmpty(lastMessage) == false)
        EditorGUILayout.HelpBox(lastMessage, lastMessageType);

      GUILayout.FlexibleSpace();
      EditorGUILayout.EndScrollView();
    }

    private void SetMessage(string message, MessageType type)
    {
      lastMessage = message;
      lastMessageType = type;
      Repaint();
    }

#if UNITY_EDITOR_WIN
    private void SetPreview(Texture2D texture)
    {
      DestroyPreview();
      lastPreview = texture;
    }

    private void DestroyPreview()
    {
      if (lastPreview != null)
      {
        DestroyImmediate(lastPreview);
        lastPreview = null;
      }
    }
#endif

    private string GetScreenshotPath(string extension = "png", string viewName = "Game")
    {
      if (string.IsNullOrEmpty(screenshotPath) == true)
      {
        screenshotPath = DefaultPath;
        screenshotPath.ToEditorPrefs(PathPrefsKey);
      }

      if (string.IsNullOrEmpty(screenshotFileName) == true)
      {
        screenshotFileName = DefaultFileName;
        screenshotFileName.ToEditorPrefs(FileNamePrefsKey);
      }

      string fileName = screenshotFileName;

      if (fileName.Contains("<date>") == true)
      {
        DateTime now = DateTime.Now;
        fileName = fileName.Replace("<date>", now.ToString("dd-MM-yyyy"));
      }

      if (fileName.Contains("<time>") == true)
      {
        DateTime now = DateTime.Now;
        fileName = fileName.Replace("<time>", now.ToString("HH-mm-ss"));
      }

      if (fileName.Contains("<index>") == true)
      {
        int index = 0;
        string candidate;
        do
        {
          candidate = fileName.Replace("<index>", index.ToString("0000"));
          index++;
        }
        while (File.Exists(Path.Combine(screenshotPath, $"{candidate}.{extension}")) == true);

        fileName = candidate;
      }

      if (fileName.Contains("<view>") == true)
        fileName = fileName.Replace("<view>", viewName);
      else if (viewName.Equals("Scene", StringComparison.Ordinal) == true)
        fileName = $"{fileName}_scene";

      return Path.Combine(screenshotPath, $"{fileName}.{extension}");
    }

    private void SaveGameViewToFile(string extension = "png")
    {
      try
      {
        string path = GetScreenshotPath(extension);
        string directory = Path.GetDirectoryName(path);
        if (string.IsNullOrEmpty(directory) == false && Directory.Exists(directory) == false)
          Directory.CreateDirectory(directory);

        ScreenCapture.CaptureScreenshot(path, screenshotSuperSize);
        SetMessage($"Screenshot saved to '{path}'.", MessageType.Info);
      }
      catch (Exception ex)
      {
        SetMessage($"Error saving screenshot: {ex.Message}", MessageType.Error);
        Debug.LogException(ex);
      }
    }

    private void SaveSceneViewToFile(string extension = "png")
    {
      try
      {
        Texture2D texture = GetSceneViewTexture();
        if (texture == null)
        {
          SetMessage("No active Scene View available.", MessageType.Warning);
          return;
        }

        string path = GetScreenshotPath(extension, "Scene");
        string directory = Path.GetDirectoryName(path);
        if (string.IsNullOrEmpty(directory) == false && Directory.Exists(directory) == false)
          Directory.CreateDirectory(directory);

        SaveTextureToFile(texture, path);
        DestroyImmediate(texture);

        SetMessage($"Scene View saved to '{path}'.", MessageType.Info);
      }
      catch (Exception ex)
      {
        SetMessage($"Error saving Scene View: {ex.Message}", MessageType.Error);
        Debug.LogException(ex);
      }
    }

    private void SaveTextureToFile(Texture2D texture, string path)
    {
      string ext = Path.GetExtension(path).ToLowerInvariant();
      byte[] bytes = (ext == ".jpg" || ext == ".jpeg") ? texture.EncodeToJPG() : texture.EncodeToPNG();
      File.WriteAllBytes(path, bytes);
    }

    private Texture2D GetSceneViewTexture()
    {
      SceneView sceneView = SceneView.lastActiveSceneView;
      if (sceneView == null || sceneView.camera == null)
        return null;

      Rect rect = sceneView.position;
      int width = Mathf.Max(1, (int)rect.width * screenshotSuperSize);
      int height = Mathf.Max(1, (int)rect.height * screenshotSuperSize);

      RenderTexture renderTexture = new(width, height, 24);
      Texture2D texture = new(width, height, TextureFormat.ARGB32, false);
      RenderTexture previousActive = RenderTexture.active;
      Camera camera = sceneView.camera;

      try
      {
        camera.targetTexture = renderTexture;
        camera.Render();
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0.0f, 0.0f, width, height), 0, 0);
        texture.Apply();
      }
      finally
      {
        camera.targetTexture = null;
        RenderTexture.active = previousActive;
        renderTexture.Release();
        DestroyImmediate(renderTexture);
      }

      return texture;
    }

#if UNITY_EDITOR_WIN
    private Texture2D GetCameraTexture(Camera camera)
    {
      if (camera == null)
        return null;

      int width = camera.pixelWidth * screenshotSuperSize;
      int height = camera.pixelHeight * screenshotSuperSize;

      RenderTexture renderTexture = new(width, height, 24);
      Texture2D texture = new(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
      RenderTexture previousActive = RenderTexture.active;

      try
      {
        camera.targetTexture = renderTexture;
        camera.Render();
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0.0f, 0.0f, renderTexture.width, renderTexture.height), 0, 0);
        texture.Apply();
      }
      finally
      {
        camera.targetTexture = null;
        RenderTexture.active = previousActive;
        renderTexture.Release();
        DestroyImmediate(renderTexture);
      }

      return texture;
    }

    private void CopyToClipboard(Texture2D texture)
    {
      if (texture == null)
      {
        SetMessage("No camera available to capture.", MessageType.Warning);
        return;
      }

      try
      {
        byte[] bits = texture.EncodeToJPG();
        using (MemoryStream stream = new(bits))
        {
          using (System.Drawing.Image image = System.Drawing.Image.FromStream(stream))
            System.Windows.Forms.Clipboard.SetImage(image);
        }

        SetMessage("Copied to clipboard.", MessageType.Info);
        SetPreview(texture);
      }
      catch (Exception ex)
      {
        SetMessage($"Error copying to clipboard: {ex.Message}", MessageType.Error);
        Debug.LogException(ex);
        DestroyImmediate(texture);
      }
    }

    private void CopyCamerasToClipboard()
    {
      if (povs.Count == 0)
      {
        SetMessage("No cameras in the list. Click 'Add active' to add one.", MessageType.Warning);
        return;
      }

      try
      {
        List<Texture2D> textures = new(povs.Count);
        for (int i = 0; i < povs.Count; ++i)
        {
          Camera cam = povs[i] != null ? povs[i].GetComponent<Camera>() : null;
          textures.Add(GetCameraTexture(cam));
        }

        int totalHeight = 0;
        int maxWidth = 0;
        for (int i = 0; i < textures.Count; ++i)
        {
          if (textures[i] == null)
            continue;

          totalHeight += textures[i].height;
          if (textures[i].width > maxWidth)
            maxWidth = textures[i].width;
        }

        if (totalHeight == 0)
        {
          SetMessage("Cameras in the list have no valid Camera component.", MessageType.Warning);
          return;
        }

        Texture2D combined = new(maxWidth, totalHeight, TextureFormat.ARGB32, false);
        int y = 0;
        for (int i = 0; i < textures.Count; ++i)
        {
          if (textures[i] == null)
            continue;

          Color[] pixels = textures[i].GetPixels();
          combined.SetPixels(0, y, textures[i].width, textures[i].height, pixels);
          y += textures[i].height;
          DestroyImmediate(textures[i]);
        }
        combined.Apply();

        CopyToClipboard(combined);
      }
      catch (Exception ex)
      {
        SetMessage($"Error capturing cameras: {ex.Message}", MessageType.Error);
        Debug.LogException(ex);
      }
    }
#endif
  }
}
