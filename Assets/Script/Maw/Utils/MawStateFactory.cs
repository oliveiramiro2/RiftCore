using UnityEngine;

public class MawStateFactory : MonoBehaviour
{
  [HideInInspector] public MawStateMachine bossSM;
  [HideInInspector] public MawController owner;

  public bool isIdle = true;


  public MawIdle MawIdle { get; private set; }
  public MawAttack MawAttack { get; private set; }
  public MawDeath MawDeath { get; private set; }

  void Awake()
  {
    MawIdle = new MawIdle();
    MawAttack = new MawAttack();
    MawDeath = new MawDeath();
  }

  public void InitializeTransitions()
  {

    bossSM.AddTransition(MawIdle, MawAttack, () => owner.isAttacking);
    bossSM.AddTransition(MawAttack, MawIdle, () => !owner.isAttacking);
    bossSM.AddAnyTransition(MawDeath, () => owner.IsDead);

  }
}