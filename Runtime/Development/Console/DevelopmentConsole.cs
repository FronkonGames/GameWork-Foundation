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
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Development Console. </summary>
  public class DevelopmentConsole : CachedMonoBehaviour
  {
    /// <summary> Show console. </summary>
    public bool Show
    {
      get => show;
      set
      {
        if (value == true && show == false)
          needFocus = true;

        show = value;
      }
    }

    [SerializeField]
    private KeyCode showKey = KeyCode.Backslash;

    [SerializeField]
    private List<DevelopmentCommand> commands = new();

    private bool show;
    private string input;
    private float lastInputTime = -1.0f;

    private bool needFocus = true;

    private readonly List<string> history = new();
    private int historyPointer;

    private Matrix4x4 originalGUIMatrix;

    private GUIStyle styleTextArea, styleTextX;

    private const string TextInputName = "ConsoleTextInput";

    private bool AcceptNewInput() => Time.time - lastInputTime > Settings.DevelopmentConsole.AcceptNewCommandTime;

    private void LateUpdate()
    {
      if (Show == false && AcceptNewInput() == true && Input.GetKeyUp(showKey) == true)
      {
        lastInputTime = Time.time;

        Show = true;
      }
    }

    public void OnGUI()
    {
      if (Show == true)
      {
        ProcessInput();

        PushGUIMatrix();

        ConsoleGUI();

        PullGUIMatrix();
      }
    }

    private void ProcessInput()
    {
      Event e = Event.current;
      if (e.type != EventType.Layout && e.type != EventType.Repaint)
      {
        if (e.keyCode == KeyCode.Return)
        {
          ProcessCommand();
          e.Use();
        }
        else if (e.keyCode == KeyCode.DownArrow && history.Count > 0 && historyPointer > 0)
        {
          historyPointer--;
          input = history[historyPointer];
          e.Use();
        }
        else if (e.keyCode == KeyCode.UpArrow && historyPointer < history.Count)
        {
          input = history[historyPointer];
          historyPointer++;
          e.Use();
        }
        else if (e.keyCode == KeyCode.Escape || e.keyCode == showKey)
        {
          Show = false;
          e.Use();
        }
      }
    }

    private void PushGUIMatrix()
    {
      float ratioWidth = Screen.width / Settings.DevelopmentConsole.DesignScreenWidth;
      float ratioHeight = Screen.height / Settings.DevelopmentConsole.DesignScreenHeight;

      originalGUIMatrix = GUI.matrix;

      GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(ratioWidth, ratioHeight, 1.0f));
    }

    private void ConsoleGUI()
    {
      styleTextArea ??= new(EditorStyles.textArea)
      {
        fontSize = Settings.DevelopmentConsole.FontSize,
        alignment = TextAnchor.MiddleLeft,
        fontStyle = FontStyle.Italic,
      };

      styleTextX ??= new(EditorStyles.textArea)
      {
        fontSize = Settings.DevelopmentConsole.FontSize,
        alignment = TextAnchor.MiddleCenter,
        fontStyle = FontStyle.Bold,
      };

      GUILayout.BeginArea(new Rect(Settings.DevelopmentConsole.Margin,
                                   Settings.DevelopmentConsole.Margin,
                                   Settings.DevelopmentConsole.DesignScreenWidth - (Settings.DevelopmentConsole.Margin * 2.0f),
                                   Settings.DevelopmentConsole.Height), GUI.skin.box);
      {
        GUILayout.BeginHorizontal();
        {
          float height = Settings.DevelopmentConsole.Height - (Settings.DevelopmentConsole.Margin * 2.0f);

          GUI.SetNextControlName(TextInputName);
          input = GUILayout.TextField(input, styleTextArea, GUILayout.ExpandHeight(true));

          if (GUILayout.Button("X", styleTextX, GUILayout.Width(Settings.DevelopmentConsole.Height), GUILayout.Height(height)) == true)
            Show = false;
        }
        GUILayout.EndHorizontal();
      }
      GUILayout.EndArea();

      if (needFocus == true)
      {
        GUI.FocusControl(TextInputName);

        needFocus = false;
      }
    }

    private void PullGUIMatrix() => GUI.matrix = originalGUIMatrix;

    private void ProcessCommand()
    {
      if (string.IsNullOrEmpty(input) == false)
      {
        input = input.Trim();

        history.Add(input);
        historyPointer = 0;

        string[] parts = input.Trim().ToLowerInvariant().Split(' ');
        if (parts.Length > 0)
        {
          string id = parts[0];
          DevelopmentCommand command = null;

          for (int i = 0; i < commands.Count && command == null; ++i)
          {
            if (id.Equals(commands[i].Id) == true)
              command = commands[i];
          }

          if (command != null)
          {
            if (command.Execute(parts.Sub(1, parts.Length - 1)) == false)
              Log.Warning($"Error executing command '{id}'. Usage: {command.Usage}");
          }
          else
            Log.Warning($"Invalid command '{id}'");
        }

        input = string.Empty;
      }
    }
  }
}
