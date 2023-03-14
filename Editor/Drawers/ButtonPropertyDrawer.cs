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
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Button drawer. </summary>
  [CustomPropertyDrawer(typeof(ButtonAttribute), true)]
  public sealed class ButtonPropertyDrawer : PropertyDrawer
  {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      ButtonAttribute button = (ButtonAttribute)attribute; 
      
      Object target = property.serializedObject.targetObject;
      MethodInfo method = target.GetType().GetMethod(button.methodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
      
      if (method == null)
        GUI.Label(position, $"Method '{button.methodName}' could not be found.");
      else if (method.GetParameters().Length > 0)
        GUI.Label(position, $"Method '{button.methodName}' cannot have parameters.");
      else if (GUI.Button(position, button.label) == true)
        method.Invoke(target, null);
    }
  }
}
