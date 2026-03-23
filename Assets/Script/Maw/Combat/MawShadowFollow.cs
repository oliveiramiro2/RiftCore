using UnityEngine;

public class MawShadowFollow : MonoBehaviour
{
  public Transform player;
  public Rigidbody2D playerRb;

  public float followTime = 1f;
  public float predictTime = 0.1f;

  private float followSpeed = 4.5f;

  float timer;

  void OnEnable()
  {
    timer = 0f;
  }

  void Update()
  {
    if (timer < followTime && playerRb.linearVelocityX > 0.1f)
    {
      Vector2 predictedPos =
          (Vector2)player.position +
          playerRb.linearVelocity * predictTime;

      transform.position = Vector2.Lerp(
          transform.position,
          new Vector2(predictedPos.x, transform.position.y),
          followSpeed * Time.deltaTime
      );

      timer += Time.deltaTime;
    }
    else
    {
      transform.position = Vector2.Lerp(
         transform.position,
         new Vector2(player.position.x, transform.position.y),
         followSpeed * Time.deltaTime
     );
    }
  }
}