using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SwordMasterAnimationBridge : MonoBehaviour
{
  private Animator animator;

  private readonly string idleParam = "idle";

  void Awake()
  {
    animator = GetComponent<Animator>();
  }

  public void SwordMasterIdle()
  {
    animator.Play(idleParam);
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