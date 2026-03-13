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
  private readonly string HideStaffParam = "HideStaff";



  private readonly string boneAttackParam = "BoneAttack";
  private readonly string explosionParam = "Explosion";
  private readonly string handAttackParam = "HandAttack";
  private readonly string summonAttackParam = "SummonAttack";
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
    animator.Play(idleParam);
  }

  public void MawDeath()
  {
    animator.Play(deathParam);
  }

  public void MawFloatIn()
  {
    animator.Play(floatInParam);
  }

  public void MawFloatOut()
  {
    animator.Play(floatOutParam);
  }

  public void MawTeleportIn()
  {
    animator.Play(teleportInParam);
  }

  public void MawTeleportOut()
  {
    animator.Play(teleportOutParam);
  }

  public void MawSummonStaff()
  {
    owner.hasStaffSummoned = true;
    animator.Play(summonStaffParam);
  }

  public void MawHideStaff()
  {
    animator.Play(HideStaffParam);
    StartCoroutine(WaitTeleportRoutine());
  }

  public void MawBoneAttack()
  {
    animator.Play(boneAttackParam);
  }

  public void MawExplosion()
  {
    animator.Play(explosionParam);
  }

  public void MawHandAttack()
  {
    animator.Play(handAttackParam);
  }

  public void MawSummonAttack()
  {
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