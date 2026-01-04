using UnityEngine;

[CreateAssetMenu(menuName = "Player/PlayerStats", fileName = "PlayerStats")]
public class PlayerStats : ScriptableObject
{
  [Header("Movement")]
  public float moveSpeed = 6f;

  [Header("Jump")]
  public float jumpForce = 12f;
  public float jumpGravityScale = 1.2f;
  public float fallGravityScale = 2.5f;
  public float defaultGravity = 1.5f;
  public float coyoteTime = 0.15f;        // tempo após sair do chão ainda pode pular
  public float jumpBufferTime = 0.1f;     // tempo antes de tocar o chão que o pulo é guardado
  public float lowJumpMultiplier = 2.5f;  // reduz altura se soltar botão cedo
  [Header("Air Movement")]
  public float airAcceleration = 40f;
  public float airDeceleration = 20f;
  public float airMaxSpeedMultiplier = 0.85f;


  [Header("Dash")]
  public bool canDash = true;
  public float dashForce = 25f;
  public float dashTime = 0.2f;
  public float dashCooldown = 2f;
}
