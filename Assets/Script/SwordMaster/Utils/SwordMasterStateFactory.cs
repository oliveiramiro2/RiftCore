using UnityEngine;

public class SwordMasterStateFactory : MonoBehaviour
{
  [HideInInspector] public SwordMasterStateMachine bossSM;
  [HideInInspector] public SwordMasterController owner;

  public bool isIdle = true;


  public SwordMasterIdle SwordMasterIdle { get; private set; }
  public SwordMasterAttack SwordMasterAttack { get; private set; }
  public SwordMasterDeath SwordMasterDeath { get; private set; }

  void Awake()
  {
    SwordMasterIdle = new SwordMasterIdle();
    SwordMasterAttack = new SwordMasterAttack();
    SwordMasterDeath = new SwordMasterDeath();
  }

  public void InitializeTransitions()
  {

    bossSM.AddTransition(SwordMasterIdle, SwordMasterAttack, () => owner.isAttacking);
    bossSM.AddTransition(SwordMasterAttack, SwordMasterIdle, () => !owner.isAttacking);
    bossSM.AddAnyTransition(SwordMasterDeath, () => owner.IsDead);

  }
}