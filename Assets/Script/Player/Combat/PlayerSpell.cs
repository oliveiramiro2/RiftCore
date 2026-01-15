using UnityEngine;

public class PlayerSpell : MonoBehaviour, IAbility
{
  public float cooldown = 4f;
  float timer;

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
    // spawn spell
    Debug.Log("Casting Spell");
    timer = cooldown;
  }
}
