using UnityEngine;

public class MawStateFactory : MonoBehaviour
{
  [HideInInspector] public MawStateMachine bossSM;
  [HideInInspector] public MawController owner;

  public bool isIdle = true;


  public MawIdle MawIdle { get; private set; }
  public MawAttack MawAttack { get; private set; }
  public MawDeath MawDeath { get; private set; }
  public MawFloating MawFloating { get; private set; }

  void Awake()
  {
    MawIdle = new MawIdle();
    MawAttack = new MawAttack();
    MawDeath = new MawDeath();
    MawFloating = new MawFloating();
  }

  public void InitializeTransitions()
  {

    bossSM.AddTransition(MawIdle, MawAttack, () => owner.isAttacking && !owner.canFollowPlayer);
    bossSM.AddTransition(MawIdle, MawFloating, () => owner.canFollowPlayer && !owner.isAttacking);

    bossSM.AddTransition(MawAttack, MawIdle, () => !owner.isAttacking && !owner.canFollowPlayer);
    bossSM.AddTransition(MawAttack, MawFloating, () => owner.canFollowPlayer && !owner.isAttacking);

    bossSM.AddTransition(MawFloating, MawIdle, () => !owner.canFollowPlayer && !owner.isAttacking);
    bossSM.AddTransition(MawFloating, MawAttack, () => !owner.canFollowPlayer && owner.isAttacking);

    bossSM.AddAnyTransition(MawDeath, () => owner.IsDead);

  }
}