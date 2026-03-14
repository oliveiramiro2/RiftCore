using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class MawAnimationBridge : MonoBehaviour
{
  private Animator animator;
  private MawController owner;

  private readonly string idleParam = "Idle";
  private readonly string deathParam = "Death";
  private readonly string floatInParam = "FloatingIn";
  private readonly string floatOutParam = "FloatingOut";
  private readonly string teleportInParam = "TeleportIn";
  private readonly string teleportOutParam = "TeleportOut";
  private readonly string summonStaffParam = "SummonStaff";
  private readonly string hideStaffParam = "HideStaff";


  private readonly string boneAttackParam = "BoneAttack";
  private readonly string explosionParam = "Explosion";
  private readonly string handAttackParam = "HandAttack";
  private readonly string finishHandAttackParam = "HideStaff";
  private readonly string summonAttackParam = "HandAttackFinish";
  void Awake()
  {
    animator = GetComponent<Animator>();
  }

  public void Setup(MawController controller)
  {
    owner = controller;
  }


  public void MawIdle()
  {
    if (owner.IsDead) return;
    animator.Play(idleParam);
  }

  public void MawDeath()
  {
    animator.Play(deathParam);
  }

  public void MawFloatIn()
  {
    if (owner.IsDead) return;

    animator.Play(floatInParam);
  }

  public void MawFloatOut()
  {
    if (owner.IsDead) return;

    animator.Play(floatOutParam);
  }

  public void MawTeleportIn()
  {
    if (owner.IsDead) return;

    animator.Play(teleportInParam);
  }

  public void MawTeleportOut()
  {
    if (owner.IsDead) return;
    animator.Play(teleportOutParam);
  }

  public void MawSummonStaff()
  {
    if (owner.IsDead) return;
    owner.hasStaffSummoned = true;
    animator.Play(summonStaffParam);
  }

  public void MawHideStaff()
  {
    if (owner.IsDead) return;
    animator.Play(hideStaffParam);
    StartCoroutine(WaitTeleportRoutine());
  }

  public void MawBoneAttack()
  {
    if (owner.IsDead) return;
    animator.Play(boneAttackParam);
  }

  public void MawExplosion()
  {
    if (owner.IsDead) return;
    animator.Play(explosionParam);
  }

  public void MawHandAttack()
  {
    if (owner.IsDead) return;
    animator.Play(handAttackParam);
  }

  public void MawFinishHandAttack()
  {
    if (owner.IsDead) return;
    animator.Play(finishHandAttackParam);
  }

  public void MawSummonAttack()
  {
    if (owner.IsDead) return;
    animator.Play(summonAttackParam);
  }

  // timers for animation transitions can be handled here if needed
  private IEnumerator WaitTeleportRoutine()
  {
    yield return new WaitForSeconds(1.5f);
    owner.hasStaffSummoned = false;
  }

}

public static class MawAnimatorExtensions
{
  public static bool SMIsCurrentAnimationFinished(this Animator animator, int layer = 0)
  {
    AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(layer);

    return info.normalizedTime >= 0.95f;
  }
}