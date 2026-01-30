using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ToxicSlimeAnimationBridge : MonoBehaviour
{
  private Animator animator;

  private readonly string idleParam = "idle";
  private readonly string ballStartParam = "ballStart";
  private readonly string ballEndParam = "ballEnd";
  private readonly string slapParam = "slap";
  private readonly string toxicRainParam = "toxicRain";
  private readonly string toxicRainEndParam = "finishToxicRain";
  private readonly string SplashParam = "splash";

  void Awake()
  {
    animator = GetComponent<Animator>();
  }

  public void ToxicSlimeIdle()
  {
    animator.Play(idleParam);
  }

  public void ToxicSlimeSlap()
  {
    animator.Play(slapParam);
  }

  public void ToxicSlimeToxicRain()
  {
    animator.Play(toxicRainParam);
  }

  public void ToxicSlimeToxicRainEnd()
  {
    animator.Play(toxicRainEndParam);
  }

  public void ToxicSlimeSplash()
  {
    animator.Play(SplashParam);
  }

  public void ToxicSlimeBallStart()
  {
    animator.Play(ballStartParam);
  }

  public void ToxicSlimeBallEnd()
  {
    animator.Play(ballEndParam);
  }

  public bool TSIsCurrentAnimationFinished()
  {
    return animator.TSIsCurrentAnimationFinished();
  }
}

public static class ToxicSlimeAnimatorExtensions
{
  public static bool TSIsCurrentAnimationFinished(this Animator animator, int layer = 0)
  {
    AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(layer);

    return info.normalizedTime >= 0.85f;
  }
}