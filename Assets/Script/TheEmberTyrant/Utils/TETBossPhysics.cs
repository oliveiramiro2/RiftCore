using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TETBossPhysics : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("Checks")]
    public Transform groundCheck;
    public Transform wallCheck;

    [Header("Layers")]
    public LayerMask groundLayer;
    public LayerMask wallLayer;

    [Header("Settings")]
    public float wallCheckDistance = 0.3f;

    private int facingDirection = 1;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void TETMoveHorizontal(float targetSpeed)
    {
        rb.linearVelocity = new Vector2(targetSpeed, rb.linearVelocityY);

        if (targetSpeed != 0)
            facingDirection = (int)Mathf.Sign(targetSpeed);
    }

    public void TETStopHorizontal()
    {
        rb.linearVelocity = new Vector2(0f, rb.linearVelocityY);
    }

    public bool IsGrounded()
    {
        return Physics2D.Raycast(
            groundCheck.position,
            Vector2.down,
            0.2f,
            groundLayer
        );
    }

    public bool IsWallAhead()
    {
        return Physics2D.Raycast(
            wallCheck.position,
            Vector2.right * facingDirection,
            wallCheckDistance,
            wallLayer
        );
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (wallCheck == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            wallCheck.position,
            wallCheck.position + Vector3.right * facingDirection * wallCheckDistance
        );
    }
#endif
}
