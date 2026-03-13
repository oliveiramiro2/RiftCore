using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MawAnimationBridge : MonoBehaviour
{
  private Animator animator;

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
    animator.Play(summonStaffParam);
  }

  public void MawHideStaff()
  {
    animator.Play(HideStaffParam);
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
}

public static class MawAnimatorExtensions
{
  public static bool SMIsCurrentAnimationFinished(this Animator animator, int layer = 0)
  {
    AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(layer);

    return info.normalizedTime >= 0.95f;
  }
}