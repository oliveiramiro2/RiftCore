using UnityEngine;

public class AstralWeaverAttackModule : MonoBehaviour
{
  public bool isAttacking = false;
  [SerializeField] private float attackCooldown = 3f;
  public bool canAttackTimer = true;
  private float attackTimer = 0f;

  void Update()
  {
    if (!canAttackTimer) return;
    isAttacking = false;
    attackTimer += Time.deltaTime;
    if (attackTimer >= attackCooldown)
    {
      isAttacking = true;
      attackTimer = 0f;
    }
  }
}