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

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// Drawing of objects for development.
  /// </summary>
  /// <remarks>Only available in the Editor</remarks>
  [DefaultExecutionOrder(-999999999)]
  public sealed class DrawManager : MonoBehaviour
  {
    public static DrawManager Instance
    {
      get
      {
        if (instance != null)
          return instance;

        GameObject gameObject = new GameObject("Draw Manager") { hideFlags = HideFlags.HideInHierarchy };
        DontDestroyOnLoad(gameObject);
        instance = gameObject.AddComponent<DrawManager>();

        return instance;
      }
    }
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics() => instance = null;

    private static DrawManager instance;

    private readonly List<Action> updateActions = new List<Action>();
    private readonly List<Action> fixedUpdateActions = new List<Action>();
    private readonly List<Action> guiActions = new List<Action>();

    public void RegisterUpdateAction(Action action) => updateActions.Add(action);

    public void RegisterFixedUpdateAction(Action action) => fixedUpdateActions.Add(action);

    public void RegisterOnGUIAction(Action action) => guiActions.Add(action);    

    private void Update()
    {
      int count = updateActions.Count;
      for (int i = 0; i < count; ++i)
        updateActions[i]?.Invoke();

      updateActions.Clear();
    }

    private void FixedUpdate()
    {
      int count = fixedUpdateActions.Count;
      for (int i = 0; i < count; ++i)
        fixedUpdateActions[i]?.Invoke();
      
      fixedUpdateActions.Clear();
    }

    private void OnGUI()
    {
      int count = guiActions.Count;
      for (int i = 0; i < count; ++i)
        guiActions[i]?.Invoke();

      guiActions.Clear();
    }    
  }
}
