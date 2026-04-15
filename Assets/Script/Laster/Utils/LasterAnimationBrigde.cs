using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class LasterAnimationBridge : MonoBehaviour
{
  private Animator animator;
  private LasterController owner;

  private readonly string idleParam = "Idle";
  private readonly string deathParam = "Death";

  private readonly string LaserAttackParam = "Laser";
  private readonly string SwordArenaAttackParam = "SwordArena";
  private readonly string SlashAttackParam = "Slash";
  private readonly string GreatBallAttackParam = "GreatBall";

  private readonly string TeleportOutParam = "Teleport";
  private readonly string TeleportInParam = "TeleportIn";

  void Awake()
  {
    animator = GetComponent<Animator>();
  }

  public void Setup(LasterController controller)
  {
    owner = controller;
  }


  public void LasterIdle()
  {
    if (owner.IsDead) return;
    animator.Play(idleParam);
  }

  public void LasterDeath()
  {
    animator.Play(deathParam);
  }

  public void PlayLaserAttack()
  {
    animator.Play(LaserAttackParam);
  }

  public void PlaySwordArenaAttack()
  {
    animator.Play(SwordArenaAttackParam);
  }

  public void PlaySlashAttack()
  {
    animator.Play(SlashAttackParam);
  }

  public void PlayGreatBallAttack()
  {
    animator.Play(GreatBallAttackParam);
  }

  public void PlayTeleportOut()
  {
    animator.Play(TeleportOutParam);
  }

  public void PlayTeleportIn()
  {
    animator.Play(TeleportInParam);
  }

  // timers for animation transitions can be handled here if needed
}

public static class LasterAnimatorExtensions
{
  public static bool SMIsCurrentAnimationFinished(this Animator animator, int layer = 0)
  {
    AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(layer);

    return info.normalizedTime >= 0.95f;
  }
}