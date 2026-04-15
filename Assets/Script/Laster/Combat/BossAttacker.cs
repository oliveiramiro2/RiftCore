using UnityEngine;

public class BossAttacker : MonoBehaviour, IAttacker
{
  public int damageMultiplier = 1;

  public GameObject GetOwner()
  {
    return gameObject;
  }

  public int GetDamageMultiplier()
  {
    return damageMultiplier;
  }
}

public interface IAttacker
{
  GameObject GetOwner();
  int GetDamageMultiplier();
}