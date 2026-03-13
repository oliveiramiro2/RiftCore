using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MawAnimationBridge : MonoBehaviour
{
  private Animator animator;

  private readonly string idleParam = "Idle";
  private readonly string deathParam = "Death";
  private readonly string floatInParam = "FloatingIn";
  private readonly string floatOutParam = "FloatingOut";


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
}

public static class MawAnimatorExtensions
{
  public static bool SMIsCurrentAnimationFinished(this Animator animator, int layer = 0)
  {
    AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(layer);

    return info.normalizedTime >= 0.95f;
  }
}