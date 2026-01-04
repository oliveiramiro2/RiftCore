using UnityEngine;

public class PlayerAttackState : State<PlayerController>
{
  private int stepPerformed = 0;

  public override void EnterState(PlayerController entity)
  {
    entity.AttackModule.ResetCombo();
    stepPerformed = entity.AttackModule.StartOrAdvanceCombo();

    entity.AnimatorBridge.SetAttackIndex(stepPerformed);
    entity.AnimatorBridge.TriggerAttack();
  }

  public override void UpdateState(PlayerController entity)
  {
    if (entity.AttackModule.IsInComboWindow() && entity.InputReader.AttackBuffered && entity.AnimatorBridge.IsCurrentAnimationFinished())
    {
      stepPerformed = entity.AttackModule.StartOrAdvanceCombo();

      entity.AnimatorBridge.SetAttackIndex(stepPerformed);

      entity.InputReader.ConsumeAttackBuffer();
    }
  }

  public override void ExitState(PlayerController entity)
  {
    entity.AttackModule.StartCooldown();
  }
}
