using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SwordMasterAnimationBridge : MonoBehaviour
{
  private Animator animator;

  private readonly string idleParam = "idle";

  private readonly string runParam = "run";

  private readonly string thirdAttackParam = "3attack";
  private readonly string secondAttackParam = "2attack";
  private readonly string firstAttackParam = "1attack";
  private readonly string explosionParam = "explosion";
  private readonly string stormParam = "storm";
  private readonly string parryParam = "parry";
  private readonly string counterAttackParam = "counterAttack";
  private readonly string windSlashParam = "windSlash";


  void Awake()
  {
    animator = GetComponent<Animator>();
  }

  public void SwordMasterIdle()
  {
    animator.Play(idleParam);
  }

  public void SwordMasterRun()
  {
    animator.Play(runParam);
  }

  public void SwordMasterCanTeleport()
  {
    gameObject.GetComponent<SwordMasterController>().canTeleport = true;
  }

  public void SwordMasterTripleAttack()
  {
    animator.Play(thirdAttackParam, 0, 0);
  }

  public void SwordMasterSecondAttack()
  {
    animator.Play(secondAttackParam, 0, 0);
  }

  public void SwordMasterFirstAttack()
  {
    animator.Play(firstAttackParam, 0, 0);
  }

  public void SwordMasterExplosion()
  {
    animator.Play(explosionParam);
  }

  public void SwordMasterStorm()
  {
    animator.Play(stormParam);
  }

  public void SwordMasterParry()
  {
    animator.Play(parryParam);
  }

  public void SwordMasterCounterAttack()
  {
    animator.Play(counterAttackParam);
  }

  public void SwordMasterWindSlash()
  {
    animator.Play(windSlashParam);
  }


  public bool SMIsCurrentAnimationFinished()
  {
    return animator.SMIsCurrentAnimationFinished();
  }
}

public static class SwordMasterAnimatorExtensions
{
  public static bool SMIsCurrentAnimationFinished(this Animator animator, int layer = 0)
  {
    AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(layer);

    return info.normalizedTime >= 0.95f;
  }
}