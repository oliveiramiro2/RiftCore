using UnityEngine;
using System.Collections.Generic;

public class ToxicSlimePhysics : MonoBehaviour
{
  private Rigidbody2D rb;
  private int facingDirection = 1;

  [Header("Camera")]
  public Camera mainCamera;

  [Header("Ground")]
  public float groundY = 0f;

  [Header("Grid Settings")]
  public float spacing = 1.2f;
  public int pointsToSpawn = 6;

  [Header("Debug")]
  public bool drawGizmos = true;
  private List<Vector2> cachedPoints = new List<Vector2>();

  void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  public void ToxicSlimeMoveHorizontal(float targetSpeed)
  {
    rb.linearVelocity = new Vector2(targetSpeed, rb.linearVelocityY);

    if (targetSpeed != 0)
      facingDirection = (int)Mathf.Sign(targetSpeed);
  }

  public void ToxicSlimeStop()
  {
    rb.linearVelocity = new Vector2(0f, 0f);
    if (!mainCamera)
      mainCamera = Camera.main;
  }

  public List<Vector2> GetRandomPoints()
  {
    cachedPoints.Clear();

    float camHeight = mainCamera.orthographicSize * 2f;
    float camWidth = camHeight * mainCamera.aspect;

    float left = mainCamera.transform.position.x - camWidth / 2f;
    float right = mainCamera.transform.position.x + camWidth / 2f;

    for (float x = left; x <= right; x += spacing)
    {
      cachedPoints.Add(new Vector2(x, groundY));
    }

    for (int i = 0; i < cachedPoints.Count; i++)
    {
      int randomIndex = Random.Range(i, cachedPoints.Count);
      (cachedPoints[i], cachedPoints[randomIndex]) = (cachedPoints[randomIndex], cachedPoints[i]);
    }

    int count = Mathf.Min(pointsToSpawn, cachedPoints.Count);
    return cachedPoints.GetRange(0, count);
  }

  void OnDrawGizmos()
  {
    if (!drawGizmos || !mainCamera) return;

    Gizmos.color = Color.cyan;
    float camHeight = mainCamera.orthographicSize * 2f;
    float camWidth = camHeight * mainCamera.aspect;

    float left = mainCamera.transform.position.x - camWidth / 2f;
    float right = mainCamera.transform.position.x + camWidth / 2f;

    for (float x = left; x <= right; x += spacing)
    {
      Gizmos.DrawSphere(new Vector3(x, groundY, 0f), 0.08f);
    }
  }
}