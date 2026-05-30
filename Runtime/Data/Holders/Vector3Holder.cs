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
using System.Runtime.CompilerServices;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> ScriptableObject holder for a Vector3 value. </summary>
  [CreateAssetMenu(menuName = Settings.Menus.GameWorkFoundationFolder + "/Data Holders/Vector3 Holder", fileName = "Vector3 Holder")]
  public class Vector3Holder : ScriptableObject, IValueHolder<Vector3>
  {
    [SerializeField]
    private Vector3 value = Vector3.zero;

    /// <summary> Gets the held value. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector3 GetValue() => value;

    /// <summary> Sets the held value. </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetValue(Vector3 newValue) => value = newValue;
  }
}
