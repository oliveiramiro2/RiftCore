using UnityEngine;

public class ToxicSlimePhysics : MonoBehaviour
{
  private Rigidbody2D rb;
  private int facingDirection = 1;

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
  }
}