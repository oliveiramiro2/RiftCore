using UnityEngine;
using System.Collections;

public class SwordBuff : MonoBehaviour, IAbility
{
  public float duration = 5f;
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
    // ativa VFX / shader
    yield return new WaitForSeconds(duration);
    // desativa VFX / shader

    active = false;
  }
}
