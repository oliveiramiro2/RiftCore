using UnityEngine;
using System.Collections;

public class SwordBuff : MonoBehaviour, IAbility
{
  public float duration = 5f, cooldown = 12f;

  bool active;

  public bool CanUse()
  {
    return !active;
  }

  public void Use(PlayerController player)
  {
    player.canMove = false;
    player.AnimatorBridge.TiggerSwordBuff();
    StartCoroutine(BuffRoutine(player));
  }

  IEnumerator BuffRoutine(PlayerController player)
  {
    active = true;
    yield return new WaitForSeconds(1.5f);

    player.AnimatorBridge.ResetTiggerSwordBuff();
    player.canMove = true;


    player.buffSwordDamage += 1;
    yield return new WaitForSeconds(duration);
    
    player.buffSwordDamage -= 1;

    yield return new WaitForSeconds(cooldown);
    active = false;
  }
}
