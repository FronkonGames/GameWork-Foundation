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
using System.Reflection;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// .
  /// </summary>
  [CanEditMultipleObjects]
  [CustomEditor(typeof(UnityEngine.Object), true)]
  public abstract class Inspector : Editor
  {
    private readonly List<SerializedProperty> serializedProperties = new List<SerializedProperty>();

    protected virtual void OnEnable()
    {
    }

    protected virtual void OnDisable()
    {
    }

    public override void OnInspectorGUI()
    {
      UpdateSerializedProperties();

      bool anyCustomAttributes = false;//serializedProperties.Any(p => ReflectionExtensions.HasAttribute<IAttribute>(p) == true);
      if (anyCustomAttributes == true)
      {
      }
      else
        DrawDefaultInspector();
    }

    private void UpdateSerializedProperties()
    {
      serializedProperties.Clear();
      using (var iterator = serializedObject.GetIterator())
      {
        if (iterator.NextVisible(true) == true)
        {
          do
          {
            serializedProperties.Add(serializedObject.FindProperty(iterator.name));
          }
          while (iterator.NextVisible(false) == true);
        }
      }
    }    
  }
}
