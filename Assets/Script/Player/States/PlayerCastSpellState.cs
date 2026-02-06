using UnityEngine;

public class PlayerCastSpellState : State<PlayerController>
{
  private float timer = 0f;
  private readonly float duration = 1.5f;

  public override void EnterState(PlayerController entity)
  {
    entity.isCastingSpell = true;
    Debug.Log("entrou");
    entity.AnimatorBridge.TiggerSpell();
    timer = duration;
  }

  public override void UpdateState(PlayerController entity)
  {
    timer -= Time.deltaTime;

    Debug.Log("update");
    if (timer <= 0)
    {
      entity.isCastingSpell = false;
    }
  }

  public override void ExitState(PlayerController entity)
  {

    Debug.Log("saiu");
    entity.AnimatorBridge.ResetTiggerSpell();
  }
}