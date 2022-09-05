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
using UnityEngine;

namespace FronkonGames.GameWork.Foundation
{
  /// <summary>
  /// Destroy objects.
  /// </summary>
  [CreateAssetMenu(fileName = "Destroy", menuName = "Game:Work/Development/Command/Destroy")]
  public class DestroyCommand : DevelopmentCommand
  {
    public DestroyCommand()
    {
      Id = "destroy";
      Usage = "destroy 'gameobject-name'";
      Description = "Destroy GameObject.";
    }

    public override bool Execute(string[] args)
    {
      if (args.Length == 1)
      {
        string name = args[0];

        List<GameObject> gameObjectsToDestroy = new();
        GameObject[] gameObjects = Resources.FindObjectsOfTypeAll<GameObject>();  // @HACK: Active and inactive GameObjects.
        for (int i = 0; i < gameObjects.Length; ++i)
        {
          if (name.Equals(gameObjects[i].name.ToLower()) == true)
            gameObjectsToDestroy.Add(gameObjects[i]);
        }

        for (int i = gameObjectsToDestroy.Count - 1; i >= 0; i--)
          gameObjectsToDestroy[i].SafeDestroy();
#if UNITY_EDITOR
          //DestroyImmediate(gameObjectsToDestroy[i], true);
#else
          Destroy(gameObjectsToDestroy[i]);
#endif

        return true;
      }

      return false;
    }
  }
}
