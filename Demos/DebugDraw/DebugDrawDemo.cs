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
//using FronkonGames.TinyTween;
//using TMPro;

/// <summary> Draw test. </summary>
[ExecuteInEditMode]
public sealed class DebugDrawDemo : MonoBehaviour
{
  [SerializeField]
  private GameObject player;

  [SerializeField]
  private GameObject[] enemies;

  [SerializeField, Range(0.0f, 360.0f)]
  private float arcAngle = 40.0f;

  private readonly Vector3[] points = new Vector3[100];

  private int randomEnemy;
  private int randomPoint;
  private Vector3Int randomTriangle = new();

  private Vector3[] lastRandomTriangle = new Vector3[3];

  private const int MaxHits = 10;
  private Ray playerRay;

  private float updateRandomPoints;

  private void OnEnable()
  {
    const float size = 5.0f;
    for (int i = 0; i < points.Length; ++i)
    {
      points[i].x = Random.Range(-size, size);
      points[i].y = Random.Range(0.0f, size * 0.5f);
      points[i].z = Random.Range(-size, size);
    }

    randomEnemy = Random.Range(0, enemies.Length - 1);
    UpdateRandomPoints();
  }

  private void Update()
  {
    updateRandomPoints += Time.deltaTime;
    if (updateRandomPoints >= 4.0f)
      UpdateRandomPoints();

    // Point
    for (int i = 0; i < points.Length; ++i)
      points[i] += (i % 2 == 0 ? 0.001f : 0.002f) * new Vector3(i % 2 == 0 ? Mathf.Sin(Time.time) : Mathf.Cos(Time.time),
                                                                i % 2 == 0 ? Mathf.Cos(Time.time) : Mathf.Sin(Time.time),
                                                                i % 2 == 0 ? -Mathf.Sin(Time.time) : -Mathf.Cos(Time.time));

    points.Draw(0.075f, Color.cyan);

    Quaternion rotation = Quaternion.Euler(Time.time * 100.0f, Time.time * -100.0f, 0.0f);

    DebugDraw.Point(new Vector3(0.0f, 0.5f, 0.0f), 2.0f, rotation, Color.white, false);
    DebugDraw.Axis(new Vector3(0.5f, 0.5f, 0.0f));
    DebugDraw.Axis(new Vector3(0.0f, 0.5f, 0.5f));
    DebugDraw.Axis(new Vector3(-0.5f, 0.5f, 0.0f));
    DebugDraw.Axis(new Vector3(0.0f, 0.5f, -0.5f));

    // Line.
    DebugDraw.Line(player.transform.position, points[randomPoint], null, Color.cyan * 0.25f, false);

    for (int i = 0; i < enemies.Length; ++i)
      DebugDraw.Line(enemies[i].transform.position, enemies[i < enemies.Length - 1 ? i + 1 : 0].transform.position);

    // Triangle.
    float t = Mathf.SmoothStep(0.0f, 1.0f, updateRandomPoints / 4.0f);
    DebugDraw.Triangle(Vector3.Lerp(lastRandomTriangle[0], points[randomTriangle[0]], t),
                       Vector3.Lerp(lastRandomTriangle[1], points[randomTriangle[1]], t),
                       Vector3.Lerp(lastRandomTriangle[2], points[randomTriangle[2]], t),
                       null, Color.HSVToRGB(t, 1.0f, 1.0f) * 0.75f, false);

    // Arrow.
    DebugDraw.Arrow(enemies[randomEnemy].transform.position, enemies[randomEnemy].transform.rotation, 2.0f);

    // Ray.
    DebugDraw.Ray(enemies[randomEnemy].transform.position, enemies[randomEnemy].transform.rotation * Vector3.right);

    // Circle.
    DebugDraw.CircleSolid(new Vector3(0.0f, 0.5f, 0.0f), 0.2f, rotation);
    DebugDraw.Circle(new Vector3(0.0f, 0.5f, 0.0f), 1.0f, rotation, null, false);

    // Sphere.
    DebugDraw.Sphere(new Vector3(0.0f, 0.5f, 0.0f), 2.0f, rotation);

    // Cube.
    DebugDraw.Cube(new Vector3(0.0f, 0.5f, 0.0f), 0.5f, rotation);

    // Arc.
    for (int i = 0; i < enemies.Length; ++i)
      DebugDraw.Arc(enemies[i].transform.position, 2.0f, arcAngle, enemies[i].transform.rotation);

    DebugDraw.ArcSolid(player.transform.position, 2.0f, arcAngle, player.transform.rotation);

    // Diamond.
    DebugDraw.Diamond(new Vector3(0.0f, 0.5f, 0.0f));

    // Cone.
    DebugDraw.Cone(new Vector3(0.0f, 1.0f, 0.0f), arcAngle / 10.0f, 1.0f, Quaternion.Euler(-90.0f, 0.0f, 0.0f));

    // Bounds.
    player.GetComponent<Renderer>().bounds.Draw();

    // Text.
    for (int i = 0; i < enemies.Length; ++i)
      enemies[i].DrawName();

    player.DrawName();

    /*
    DebugDraw.Cube(new Vector3(0.0f, 0.5f, 0.0f), 1.0f);

    player.DrawName();
    player.transform.Draw(3.0f);

    for (int i = 0; i < enemies.Length; ++i)
    {
      enemies[i].DrawName();
      DebugDraw.SolidArc(enemies[i].transform.position, 3.0f, arcAngle, enemies[i].transform.rotation);
      player.GetComponent<Renderer>().bounds.Draw();
    }

    DebugDraw.Circle(new Vector3(0.0f, 0.5f, 0.0f), 4.0f, null, null, false);

    PlayerRaycast();
    */
  }

  private void UpdateRandomPoints()
  {
    randomPoint = Random.Range(0, points.Length - 1);

    lastRandomTriangle[0] = points[randomTriangle[0]];
    lastRandomTriangle[1] = points[randomTriangle[1]];
    lastRandomTriangle[2] = points[randomTriangle[2]];

    randomTriangle[0] = Random.Range(0, points.Length - 1);
    randomTriangle[1] = Random.Range(0, points.Length - 1);
    randomTriangle[2] = Random.Range(0, points.Length - 1);

    updateRandomPoints = 0.0f;
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
