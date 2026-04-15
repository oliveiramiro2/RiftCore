using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Attack Data")]
public class AttackData : ScriptableObject
{
  public int baseDamage = 1;
  public float knockbackForce = 4f;
}