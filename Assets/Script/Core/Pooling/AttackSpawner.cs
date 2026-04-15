using UnityEngine;

public class AttackSpawner : MonoBehaviour
{
  public GameObject hitboxPrefab;

  public void SpawnAttack(Vector2 position, Quaternion rotation)
  {
    GameObject obj = Instantiate(hitboxPrefab, position, rotation);

    var hitbox = obj.GetComponent<HitboxV2>();
    var attacker = gameObject.GetComponent<IAttacker>();

    hitbox.Init(attacker);
  }
}