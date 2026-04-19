using UnityEngine;
using System.Collections;

public class SwordBuff : MonoBehaviour, IAbility
{
  public float duration = 5f, cooldown = 12f;

  private bool active;
  public bool isActive;

  public bool CanUse()
  {
    return !active && !GameObject.FindAnyObjectByType<PlayerSpell>().active;
  }

  public void Use(PlayerController player)
  {
    player.canMove = false;
    player.AnimatorBridge.TiggerSwordBuff();
    StartCoroutine(BuffRoutine(player));
  }

  IEnumerator BuffRoutine(PlayerController player)
  {
    isActive = true;
    active = true;
    player.events.OnFocusBuff.Raise();
    yield return new WaitForSeconds(1.5f);
    isActive = false;
    player.PlayerSM.ChangeState(player.StateFactory.Idle);
    player.canMove = true;
    player.AnimatorBridge.ResetTiggerSwordBuff();

    player.events.OnBuff.Raise();
    player.buffSwordDamage += 1;
    yield return new WaitForSeconds(duration);

    player.events.OnEndBuff.Raise();
    player.buffSwordDamage -= 1;

    yield return new WaitForSeconds(cooldown - duration);
    active = false;
  }
}
