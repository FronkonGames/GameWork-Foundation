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
  /// <summary> Create primitives. </summary>
  [CreateAssetMenu(fileName = "Primitive", menuName = "Game:Work/Development/Command/Primitive")]
  public class PrimitiveCommand : DevelopmentCommand
  {
    public PrimitiveCommand()
    {
      Id = "primitive";
      Usage = "primitive cube|sphere|plane|cylinder|capsule [0.0,0.0,0.0]";
      Description = "Create primitive objects.";
    }

    /// <inheritdoc/>
    public override bool Execute(string[] args)
    {
      GameObject gameObject = null;
      if (args.Length > 0)
      {
        string type = args[0];
        gameObject = type switch
        {
          "cube"     => GameObject.CreatePrimitive(PrimitiveType.Cube),
          "sphere"   => GameObject.CreatePrimitive(PrimitiveType.Sphere),
          "plane"    => GameObject.CreatePrimitive(PrimitiveType.Plane),
          "cylinder" => GameObject.CreatePrimitive(PrimitiveType.Cylinder),
          "capsule"  => GameObject.CreatePrimitive(PrimitiveType.Capsule),
          _ => gameObject
        };

        if (args.Length == 2 && gameObject != null)
        {
          string[] components = args[1].Split(',');
          if (components.Length == 3)
          {
            gameObject.transform.position = args[1].ToVector3();

            return true;
          }
        }
      }

      return gameObject != null;
    }
  }
}
