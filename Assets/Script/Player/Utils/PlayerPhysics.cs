using UnityEngine;


[DisallowMultipleComponent]
public class PlayerPhysics : MonoBehaviour
{
  public Transform groundCheck;
  public LayerMask groundLayer;
  public float groundCheckRadius = 0.1f;
  public PhysicsMaterial2D physics2D;

  private PlayerController player;


  [Header("Runtime")]
  public bool isGrounded;
  public float lastGroundedTime;


  void Update()
  {
    if (!isGrounded)
    {
      rb.sharedMaterial = physics2D;
    }
    else
    {
      rb.sharedMaterial = null;
    }
  }

  public Rigidbody2D rb;


  void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
  }

  public void Initialize(PlayerController player)
  {
    this.player = player;
  }


  public void GroundCheck()
  {
    if (groundCheck == null) return;
    bool grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    if (grounded)
    {
      isGrounded = true;
      lastGroundedTime = Time.time;
    }
    else
    {
      isGrounded = false;
    }
  }


  public void MoveHorizontal(float targetSpeed)
  {
    rb.linearVelocity = new Vector2(targetSpeed / player.slowVelocity, rb.linearVelocityY);
  }


  public void StopHorizontal()
  {
    rb.linearVelocity = new Vector2(0f, rb.linearVelocityY);
  }
}