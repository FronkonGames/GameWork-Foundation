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

namespace FronkonGames.GameWork.Foundation
{
  /// <summary> Material extensions. </summary>
  public static class MaterialExtensions
  {
    /// <summary> Get a bool property from the material by name. </summary>
    public static bool GetBool(this Material self, string name) => self.HasProperty(name) && self.GetInt(name) != 0;

    /// <summary> Get a bool property from the material by ID. </summary>
    public static bool GetBool(this Material self, int id) => self.HasProperty(id) && self.GetInt(id) != 0;

    /// <summary> Set a bool property on the material by name. </summary>
    public static void SetBool(this Material self, string name, bool value)
    {
      if (self.HasProperty(name))
        self.SetInt(name, value ? 1 : 0);
    }

    /// <summary> Set a bool property on the material by ID. </summary>
    public static void SetBool(this Material self, int id, bool value)
    {
      if (self.HasProperty(id))
        self.SetInt(id, value ? 1 : 0);
    }
  }
}
