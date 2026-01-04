using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
  private Animator _animator;

  private void Awake()
  {
    _animator = GetComponent<Animator>();
  }

  public void Play(string animationName)
  {
    _animator.Play(animationName);
  }

  public void SetTrigger(string trigger)
  {
    _animator.SetTrigger(trigger);
  }

  public void SetBool(string param, bool value)
  {
    _animator.SetBool(param, value);
  }

  public void SetFloat(string param, float value)
  {
    _animator.SetFloat(param, value);
  }

  public bool IsPlaying(string animName)
  {
    var stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
    return stateInfo.IsName(animName) && stateInfo.normalizedTime < 1;
  }
}
