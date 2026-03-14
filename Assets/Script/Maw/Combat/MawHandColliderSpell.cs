using UnityEngine;

public class MawHandColliderSpell : MonoBehaviour
{
  [SerializeField] private BoxCollider2D handAttackCollider;

  public void EnableHandAttackCollider()
  {
    handAttackCollider.enabled = true;
  }

  public void DisableHandAttackCollider()
  {
    handAttackCollider.enabled = false;
  }
}