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
using FronkonGames.GameWork.Foundation;
using Random = UnityEngine.Random;

/// <summary>
/// Draw test.
/// </summary>
[ExecuteInEditMode]
public sealed class DrawDemo : MonoBehaviour
{
  [SerializeField]
  private GameObject player;

  [SerializeField]
  private GameObject[] enemies;

  [SerializeField, Range(0.0f, 360.0f)]
  private float arcAngle = 40.0f;

  private readonly Vector3[] points = new Vector3[100];

  private const int MaxHits = 10;
  private Ray playerRay;

  private void OnEnable()
  {
    const float size = 5.0f;
    for (int i = 0; i < points.Length; ++i)
    {
      points[i].x = Random.Range(-size, size);
      points[i].y = Random.Range(0.0f, size * 0.5f);
      points[i].z = Random.Range(-size, size);
    }
  }

  private void Update()
  {
    DebugDraw.DottedLine(player.transform.position, new Vector3(0.0f, 0.5f, 0.0f));
    
    DebugDraw.Point(new Vector3(0.0f, 0.5f, 0.0f), 0.4f, Color.white);
    DebugDraw.Point(new Vector3(0.5f, 0.5f, 0.0f));
    DebugDraw.Point(new Vector3(0.0f, 0.5f, 0.5f));
    DebugDraw.Point(new Vector3(-0.5f, 0.5f, 0.0f));
    DebugDraw.Point(new Vector3(0.0f, 0.5f, -0.5f));

    DebugDraw.Sphere(new Vector3(0.0f, 0.5f, 0.0f), 0.5f);
    DebugDraw.Diamond(new Vector3(0.0f, 0.5f, 0.0f));
    DebugDraw.Cube(new Vector3(0.0f, 0.5f, 0.0f), 1.0f);

    DebugDraw.SolidCircle(new Vector3(0.0f, 0.5f, 0.0f), 0.2f, null, player.transform.rotation);
    DebugDraw.Circle(new Vector3(0.0f, 0.5f, 0.0f), 0.21f, null, player.transform.rotation);
    DebugDraw.Cone(new Vector3(0.0f, 1.0f, 0.0f), Quaternion.Euler(-90.0f, 0.0f, 0.0f), arcAngle / 10.0f);
    
    points.Draw(0.1f, Color.cyan);

    player.DrawName();
    player.transform.Draw(3.0f);
    player.GetComponent<Renderer>().bounds.Draw();
    
    for (int i = 0; i < enemies.Length; ++i)
    {
      enemies[i].DrawName();
      DebugDraw.SolidArc(enemies[i].transform.position, enemies[i].transform.rotation, 3.0f, arcAngle);
      player.GetComponent<Renderer>().bounds.Draw();
    }

    PlayerRaycast();
  }

  private void PlayerRaycast()
  {
    playerRay.origin = player.transform.position + new Vector3(0.0f, 0.5f, 0.0f);
    playerRay.direction = player.transform.forward;

    RaycastHit[] playerHits = new RaycastHit[MaxHits];
    int hits = Physics.RaycastNonAlloc(playerRay, playerHits, 100.0f);
    if (hits > 0)
      playerRay.Draw(playerHits);
  }
}
