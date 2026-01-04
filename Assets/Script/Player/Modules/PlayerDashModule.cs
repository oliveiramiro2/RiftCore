using UnityEngine;


[DisallowMultipleComponent]
public class PlayerDashModule : MonoBehaviour
{
  private PlayerController player;
  private PlayerPhysics physics;
  private PlayerStats stats => player.stats;
  private float lastDashTime = -10f;
  private float dashDuration = 2f;

  // Configuração injetada pelo PlayerInitializer
  public void Initialize(PlayerController owner, PlayerPhysics physicsModule)
  {
    this.player = owner;
    this.physics = physicsModule;
  }

  public bool CanDash()
  {
    bool cooldownOK = (Time.time - lastDashTime) >= stats.dashCooldown;

    return cooldownOK;
  }

  public bool IsDashing()
  {
    return dashDuration > Time.time;
  }

  public void ApplyDash()
  {
    player.rb.gravityScale = 0;

    float direction = player.rb.transform.localScale.x;

    player.events.OnDash.Raise();
    player.rb.linearVelocity = new Vector2(direction * stats.dashForce, 0f);
    dashDuration = Time.time + stats.dashTime;
  }

  public void EndDash(PlayerController player)
  {
    player.rb.gravityScale = player.stats.defaultGravity;
    physics.StopHorizontal();
    lastDashTime = Time.time;
  }
}