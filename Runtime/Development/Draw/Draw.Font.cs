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
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
#endif

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// Drawing of objects for development.
  /// </summary>
  /// <remarks>Only available in the Editor</remarks>
  public static partial class Draw
  {
#if UNITY_EDITOR
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void ResetStatics()
    {
      subscribedUpdate = false;
      debugTextUpdate.Clear();
      SceneView.duringSceneGui -= SceneViewGUIUpdate;
      subscribedFixed = false;
      debugTextFixed.Clear();
      SceneView.duringSceneGui -= SceneViewGUIFixed;
    }

    private readonly struct DebugText
    {
      public Vector3 Position { get; }
      public object Text { get; }
      public Color Color { get; }
      public Camera Camera { get; }

      public DebugText(Vector3 position, object text, Color color, Camera camera)
      {
        Camera = camera;
        Position = position;
        Text = text;
        Color = color;
      }
    }

    private static GUIStyle TextStyle => EditorStyles.label;

    private static bool subscribedUpdate;
    private static readonly List<DebugText> debugTextUpdate = new List<DebugText>();

    private static bool subscribedFixed;
    private static readonly List<DebugText> debugTextFixed = new List<DebugText>();

    private static Type gameViewType;
    private static Type GameViewType => gameViewType ??= typeof(EditorWindow).Assembly.GetType("UnityEditor.GameView");

    private static EditorWindow gameView;

    private static EditorWindow GameView
    {
      get
      {
        if (gameView != null)
          return gameView;
        Object[] gameViewQuery = Resources.FindObjectsOfTypeAll(GameViewType);
        if (gameViewQuery == null || gameViewQuery.Length == 0)
          return null;
        return gameView = (EditorWindow)gameViewQuery[0];
      }
    }

    private static FieldInfo hasGizmos;
    private static FieldInfo HasGizmos => hasGizmos ??= GameViewType.GetField("m_Gizmos", BindingFlags.NonPublic | BindingFlags.Instance);

    public static bool GameViewGizmosEnabled
    {
      get
      {
        var gameView = GameView;
        if (gameView == null)
          return false;
        var hasGizmos = HasGizmos;
        if (hasGizmos == null)
          return false;
        return (bool)hasGizmos.GetValue(gameView);
      }
    }

    static void WaitForNextUpdate()
    {
      subscribedUpdate = false;
      debugTextUpdate.Clear();
      SceneView.duringSceneGui -= SceneViewGUIUpdate;
    }

    static void WaitForNextFixed()
    {
      subscribedFixed = false;
      debugTextFixed.Clear();
      SceneView.duringSceneGui -= SceneViewGUIFixed;
    }

    private static void SceneViewGUIUpdate(SceneView obj) => SceneViewGUI(obj, debugTextUpdate);

    private static void SceneViewGUIFixed(SceneView obj) => SceneViewGUI(obj, debugTextFixed);

    private static void SceneViewGUI(SceneView obj, List<DebugText> debugTexts)
    {
      if (!Application.isPlaying)
      {
        SceneView.duringSceneGui -= SceneViewGUIFixed;
        SceneView.duringSceneGui -= SceneViewGUIUpdate;
        return;
      }

      if (!obj.drawGizmos)
        return;

      Handles.BeginGUI();

      foreach (DebugText debugText in debugTexts)
        DoDrawText(debugText.Position, debugText.Text, debugText.Color, obj.camera);

      Handles.EndGUI();
    }

    private static void DoDrawText(Vector3 position, object text, Color color, Camera camera)
    {
      if (!WorldToGUIPoint(position, out Vector2 screenPos, camera)) return;
      //------DRAW-------
      string value;
      switch (text)
      {
        case Vector3 vector3:
          value = vector3.ToString("F3");
          break;
        case Vector2 vector2:
          value = vector2.ToString("F3");
          break;
        default:
          value = text.ToString();
          int namespacePart = value.LastIndexOf(" (");
          
          if (namespacePart > 0 && value.Contains(text.GetType().Namespace) == true)
            value = value.Remove(namespacePart, value.Length - namespacePart);

          break;
      }

      var content = new GUIContent(value);
      Rect rect = new Rect(screenPos, TextStyle.CalcSize(content));
      DrawGUIRect(rect, color);
      GUI.Label(rect, content, TextStyle);
      //-----------------
    }

    private static void DrawGUIRect(Rect rect, Color color)
    {
      if (UnityEngine.Event.current.type != EventType.Repaint)
        return;
      Color color1 = GUI.color;
      GUI.color *= color;
      GUI.DrawTexture(rect, EditorGUIUtility.whiteTexture);
      GUI.color = color1;
    }

    /// <summary>
    /// Converts world point to a point in screen space if valid.
    /// </summary>
    /// <param name="query">The world point query</param>
    /// <param name="point">The GUI point. Zero if behind the camera.</param>
    /// <param name="camera">The camera that owns the GUI space.</param>
    /// <returns>True if a valid position in front of the camera.</returns>
    private static bool WorldToGUIPoint(Vector3 query, out Vector2 point, Camera camera)
    {
      Vector3 viewPos = camera.WorldToViewportPoint(query);
      bool behindScreen = viewPos.z < 0;

      if (behindScreen)
      {
        point = Vector2.zero;
        return false;
      }

      Vector2 viewScreenVector = new Vector2(viewPos.x, viewPos.y);
      point = new Vector2(viewScreenVector.x * camera.pixelWidth, (1 - viewScreenVector.y) * camera.pixelHeight);
      return true;
    }    
#endif    
  }
}
