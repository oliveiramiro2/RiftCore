using UnityEngine;

[DisallowMultipleComponent]
public class PlayerJumpModule : MonoBehaviour
{
  private PlayerController player;
  private PlayerPhysics physics;

  private PlayerStats stats => player.stats;

  private float lastJumpPressedTime = -10f;

  [Header("Settings")]
  public float jumpBufferTime = 0.2f;

  // Define o quanto a velocidade é reduzida (0.5 = corta pela metade)
  [Range(0f, 1f)] public float jumpCutMultiplier = 0.5f;

  // Configuração injetada pelo PlayerInitializer
  public void Initialize(PlayerController owner, PlayerPhysics physicsModule)
  {
    this.player = owner;
    this.physics = physicsModule;
  }

  public void RegisterJumpPressed()
  {
    lastJumpPressedTime = Time.time;
  }

  public bool CanJump()
  {
    bool coyoteOK = (Time.time - physics.lastGroundedTime) <= stats.coyoteTime;
    bool bufferedOK = (Time.time - lastJumpPressedTime) <= stats.jumpBufferTime;

    return physics.isGrounded && coyoteOK && bufferedOK;
  }

  public void CancelJump()
  {
    // Só cortamos se ele estiver subindo (velocidade positiva)
    if (player.rb.linearVelocityY > 0)
    {
      // Reduz a velocidade vertical imediatamente
      player.rb.linearVelocity = new Vector2(player.rb.linearVelocityX, player.rb.linearVelocityY * jumpCutMultiplier);
    }
  }

  public void ApplyJump()
  {
    player.rb.linearVelocity = new Vector2(player.rb.linearVelocityX, stats.jumpForce);
    lastJumpPressedTime = -10f;
  }
}
