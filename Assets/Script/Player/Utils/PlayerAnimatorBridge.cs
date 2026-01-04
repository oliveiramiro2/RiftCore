using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerAttackModule))]
[RequireComponent(typeof(PlayerController))]
public class PlayerAnimatorBridge : MonoBehaviour
{
  private Animator animator;
  private PlayerAttackModule attackModule;


  private static readonly int MoveSpeedHash = Animator.StringToHash("MoveSpeed");
  private static readonly int IsJumpingHash = Animator.StringToHash("IsJumping");
  private static readonly int DoJumpHash = Animator.StringToHash("DoJump");
  private static readonly int IsFallingHash = Animator.StringToHash("IsFalling");
  private static readonly int DoFallHash = Animator.StringToHash("Fall");
  private static readonly int DoDashHash = Animator.StringToHash("DoDash");
  private static readonly int IsDashingHash = Animator.StringToHash("IsDashing");
  private static readonly int AttackTriggerHash = Animator.StringToHash("DoAttack");
  private static readonly int AttackIndexHash = Animator.StringToHash("AttackIndex");
  private static readonly int LandtriggerHash = Animator.StringToHash("Land");
  private static readonly int DeathTriggerHash = Animator.StringToHash("Death");


  void Awake()
  {
    animator = GetComponent<Animator>();
    attackModule = GetComponent<PlayerAttackModule>();
  }


  public void SetMoveSpeed(float val)
  {
    animator.SetFloat(MoveSpeedHash, val);
  }


  public void SetJumping(bool val)
  {
    animator.SetBool(IsJumpingHash, val);
  }

  public void TriggerJump()
  {
    animator.SetTrigger(DoJumpHash);
  }

  public void ResetTriggerJump()
  {
    animator.ResetTrigger(DoJumpHash);
  }


  public void SetFalling(bool val)
  {
    animator.SetBool(IsFallingHash, val);
  }

  public void TriggerFall()
  {
    animator.SetTrigger(DoFallHash);
  }

  public void ResetTriggerFall()
  {
    animator.ResetTrigger(DoFallHash);
  }

  public void TriggerDash()
  {
    animator.SetTrigger(DoDashHash);
  }

  public void ResetTriggerDash()
  {
    animator.ResetTrigger(DoDashHash);
  }

  public void SetDashing(bool val)
  {
    animator.SetBool(IsDashingHash, val);
  }

  public void SetAttackIndex(int val)
  {
    animator.SetInteger(AttackIndexHash, val);
  }


  public void TriggerAttack()
  {
    animator.SetTrigger(AttackTriggerHash);
  }

  public void ResetTriggetAttack()
  {
    animator.ResetTrigger(AttackTriggerHash);
  }


  public void TriggerLand()
  {
    animator.SetTrigger(LandtriggerHash);
  }

  public void ResetTriggerLand()
  {
    animator.ResetTrigger(LandtriggerHash);
  }

  public void TriggerDeath()
  {
    animator.SetTrigger(DeathTriggerHash);
  }

  public void ResetTriggerDeath()
  {
    animator.ResetTrigger(DeathTriggerHash);
  }


  public void OpenComboWindowEvent()
  {
    attackModule.OpenComboWindow();
  }

  public void CloseComboWindowEvent()
  {
    attackModule.CloseComboWindow();
  }

  public bool IsCurrentAnimationFinished()
  {
    return animator.IsCurrentAnimationFinished();
  }
}