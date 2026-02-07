using UnityEngine;
using System.Collections;

public class PlayerSpell : MonoBehaviour, IAbility
{
  private readonly float cooldown = 8f;
  float timer;

  public GameObject laser;

  void Update()
  {
    if (timer > 0)
      timer -= Time.deltaTime;
  }

  public bool CanUse()
  {
    return timer <= 0;
  }

  public void Use(PlayerController player)
  {
    StartCoroutine(LaserRoutine(player));
  }

  IEnumerator LaserRoutine(PlayerController player)
  {
    yield return new WaitForSeconds(0.8f);
    player.events.OnLaserSpell.Raise();

    yield return new WaitForSeconds(0.3f);

    laser.SetActive(true);
    laser.transform.localScale = new(200, 0.6f, 1);

    yield return new WaitForSeconds(0.5f);
    laser.transform.localScale = Vector3.one;
    laser.SetActive(false);

    timer = cooldown;
  }
}
