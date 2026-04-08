using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class LasterAnimationBridge : MonoBehaviour
{
  private Animator animator;
  private LasterController owner;

  private readonly string idleParam = "Idle";
  private readonly string deathParam = "Death";

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