using UnityEngine;

public class PlayerCastSpellState : State<PlayerController>
{
  private float timer = 0f;
  private readonly float duration = 1.5f;

  public override void EnterState(PlayerController entity)
  {
    entity.isCastingSpell = true;
    entity.AnimatorBridge.TiggerSpell();
    timer = duration;
    entity.PlayerAbilities.HandleSpell();
  }

  public override void UpdateState(PlayerController entity)
  {
    timer -= Time.deltaTime;

    if (timer <= 0)
    {
      entity.isCastingSpell = false;
    }
  }

  public override void ExitState(PlayerController entity)
  {
    entity.AnimatorBridge.ResetTiggerSpell();
  }
}