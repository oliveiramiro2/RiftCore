using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AstralWeaverAnimationBridge : MonoBehaviour
{
  private Animator animator;

  private readonly string idleParam = "idle";
  private readonly string energyBallParam = "energyBall";
  private readonly string laserParam = "laser";

  void Awake()
  {
    animator = GetComponent<Animator>();
  }

  public void AstralWeaverIdle()
  {
    animator.Play(idleParam);
  }

  public void AstralWeaverEnergyBall()
  {
    animator.Play(energyBallParam);
  }

  public void AstralWeaverLaser()
  {
    animator.Play(laserParam);
  }
}

public static class AWAnimatorExtensions
{
  public static bool AWIsCurrentAnimationFinished(this Animator animator, int layer = 0)
  {
    AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(layer);

    return info.normalizedTime >= 0.85f;
  }
}