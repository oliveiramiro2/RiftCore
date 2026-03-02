using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerPhysics))]
[RequireComponent(typeof(PlayerInputReader))]
public class PlayerLocomotion : MonoBehaviour
{
  private PlayerController player;
  private PlayerPhysics physics;
  private PlayerInputReader input;

  private float lastJumpPressedTime;   // jump buffer
  private float lastGroundedTime;      // coyote time

  private bool jumpReleasedEarly;      // low jump
  private bool isJumpingReleased;      // jump released flag

  [Header("Run Time's")]
  public float stepSoudInterval = 0.4f;
  private float stepSoundTimer = 0f;


  private PlayerStats stats => player.stats;
  private Rigidbody2D rb => physics.rb;

  void Awake()
  {
    isJumpingReleased = false;
  }

  public void Initialize(PlayerController owner, PlayerPhysics physicsModule, PlayerInputReader inputReader)
  {
    this.player = owner;
    this.physics = physicsModule;
    this.input = inputReader;
  }

  void Update()
  {
    CacheTimers();
    ApplyBetterJumpPhysics();
  }

  private void CacheTimers()
  {
    // Jump Buffer
    if (input.JumpPressed)
      lastJumpPressedTime = Time.time;

    // Coyote Time
    if (physics.isGrounded)
      lastGroundedTime = Time.time;
  }

  public void FootstepSoundTick()
  {
    if (stepSoundTimer < stepSoudInterval)
      stepSoundTimer += Time.deltaTime;
    else
    {
      player.events.OnMove.Raise();
      stepSoundTimer = 0f;
    }
  }

  public void MoveAirborne(float inputX)
  {
    float targetSpeed = inputX * stats.moveSpeed;
    float newSpeed = Mathf.Lerp(rb.linearVelocityX,
                            targetSpeed,
                            stats.airAcceleration * Time.deltaTime);

    physics.MoveHorizontal(newSpeed);

    if (!player.IsDead && inputX != 0)
      player.FlipX(inputX > 0);
  }

   public void MoveGrounded(float inputX)
  {
    float targetSpeed = inputX * (stats.moveSpeed / player.slowVelocity);

    Debug.Log("velocidade: " + targetSpeed);

    float newSpeed = Mathf.Lerp(
        rb.linearVelocityX,
        targetSpeed,
        stats.moveSpeed * Time.deltaTime
    );

    physics.MoveHorizontal(newSpeed);

    if (!player.IsDead && inputX != 0)
      player.FlipX(inputX > 0);
  }

  public bool CanJump()
  {
    bool withinCoyote = (Time.time - lastGroundedTime) <= stats.coyoteTime;
    bool withinBuffer = (Time.time - lastJumpPressedTime) <= stats.jumpBufferTime;

    return withinCoyote && withinBuffer && physics.isGrounded;
  }

  public bool TryJump()
  {
    if (!CanJump())
      return false;

    // Reset buffer
    lastJumpPressedTime = -999f;
    lastGroundedTime = -999f;

    jumpReleasedEarly = false;
    isJumpingReleased = true;
    ApplyJumpForce();
    return true;
  }

  public void ApplyJumpForce()
  {
    rb.linearVelocity = new Vector2(rb.linearVelocityX, stats.jumpForce);
  }

  private void ApplyBetterJumpPhysics()
  {
    if (rb.linearVelocityY > 0 && isJumpingReleased && !jumpReleasedEarly)
    {
      // LOW JUMP
      rb.gravityScale = stats.jumpGravityScale;
      jumpReleasedEarly = true;
    }
    else if (rb.linearVelocityY < 0)
    {
      // FALL GRAVITY
      rb.gravityScale = stats.fallGravityScale;
    }
    else
    {
      // NORMAL GRAVITY (subindo sem soltar o botão)
      rb.gravityScale = stats.defaultGravity;
    }
    isJumpingReleased = false;
  }

  public void StopHorizontal()
  {
    physics.StopHorizontal();
  }
}