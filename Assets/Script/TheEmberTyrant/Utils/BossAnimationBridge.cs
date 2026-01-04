using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BossController))]
[RequireComponent(typeof(AttackModule))]
public class BossAnimationBridge : MonoBehaviour
{
  private Animator animator;
  public BossController controller;
  public AttackModule attackModule;
  public ColliderContact colliders;

  private readonly string idleParam = "idle";
  private readonly string runParam = "walk";
  private readonly string punchParam = "firepunch";
  private readonly string ballParam = "fireball";
  private readonly string blastParam = "fireblast";
  private readonly string dashParam = "firedash";
  private readonly string pillarParam = "firepillar";

  void Awake()
  {
    animator = GetComponent<Animator>();
    controller = GetComponent<BossController>();
    attackModule = GetComponent<AttackModule>();
  }

  public void TETIdle()
  {
    animator.Play(idleParam);
  }

  public void TETRun()
  {
    animator.Play(runParam);
  }

  public void TETPunch()
  {
    animator.Play(punchParam);
  }

  public void TETFireball()
  {
    animator.Play(ballParam);
  }

  public void TETFireblast()
  {
    animator.Play(blastParam);
  }

  public void TETFiredash()
  {
    animator.Play(dashParam);
  }

  public void TETFirepillar()
  {
    animator.Play(pillarParam);
  }

  public void FireBallSpawnEvent()
  {
    attackModule.FireBallSpawn();
  }

  public void StartingDash()
  {

    controller.tetEvents.OnDash.Raise();
    controller.rb.AddForceX(controller.IsFacingRight() ? 10f : -10f, ForceMode2D.Impulse);
  }

  public void StartingExplosion()
  {
    controller.tetEvents.OnExplosion.Raise();
    colliders.EnableExplosion();
  }

  public void FinishExplosion()
  {
    colliders.DisableExplosion();
  }

  public void StartingPunch()
  {
    colliders.EnablePunch();
  }

  public void FinishPunch()
  {
    colliders.DisablePunch();
  }

  public bool TETIsCurrentAnimationFinished()
  {
    return animator.TETIsCurrentAnimationFinished();
  }

  public void TETAttackAnimationPlaying()
  {
    attackModule.isAttacking = true;
  }

  public void TETAttackAnimationEnded()
  {
    attackModule.isAttacking = false;
  }
}

public static class TETAnimatorExtensions
{
  public static bool TETIsCurrentAnimationFinished(this Animator animator, int layer = 0)
  {
    AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(layer);

    return info.normalizedTime >= 0.9f;
  }
}