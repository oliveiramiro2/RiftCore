using UnityEngine;

public class SwordMasterColliders : MonoBehaviour
{
  public BoxCollider2D firstAttackCollider;
  public BoxCollider2D secondAttackCollider;
  public BoxCollider2D thirdAttackCollider;
  public BoxCollider2D parryCollider;
  public BoxCollider2D counterAttackCollider;

  public void EnableFirstAttackCollider()
  {
    firstAttackCollider.enabled = true;
  }

  public void DisableFirstAttackCollider()
  {
    firstAttackCollider.enabled = false;
  }

  public void EnableSecondAttackCollider()
  {
    secondAttackCollider.enabled = true;
  }

  public void DisableSecondAttackCollider()
  {
    secondAttackCollider.enabled = false;
  }

  public void EnableThirdAttackCollider()
  {
    thirdAttackCollider.enabled = true;
  }

  public void DisableThirdAttackCollider()
  {
    thirdAttackCollider.enabled = false;
  }


  public void EnableParryCollider()
  {
    parryCollider.enabled = true;
  }

  public void DisableParryCollider()
  {
    parryCollider.enabled = false;
  }

  public void EnableCounterAttackCollider()
  {
    counterAttackCollider.enabled = true;
  }

  public void DisableCounterAttackCollider()
  {
    counterAttackCollider.enabled = false;
  }
}