using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SwordMasterAnimationBridge : MonoBehaviour
{
  private Animator animator;

  private readonly string idleParam = "idle";

  private readonly string runParam = "run";

  //private readonly string tripleParam = "3attacks";
  //private readonly string explosionParam = "explosion";
  //private readonly string stormParam = "storm";
  //private readonly string parryParam = "parry";
  //private readonly string windSlashParam = "windSlash";


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

    return info.normalizedTime >= 0.85f;
  }
}