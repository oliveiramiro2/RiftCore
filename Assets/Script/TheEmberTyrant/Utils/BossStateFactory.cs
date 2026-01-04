using UnityEngine;

public class BossStateFactory : MonoBehaviour
{
  [HideInInspector] public BossStateMachine bossSM;
  [HideInInspector] public BossController owner;
  [HideInInspector] public BossAnimationBridge animatorBridge;
  [HideInInspector] public TargetingModule targetingModule;
  [HideInInspector] public TETBossPhysics bossPhysics;
  [HideInInspector] public LocomotionModule locomotionModule;
  [HideInInspector] public AttackModule attackModule;

  public bool isIdle = true;


  public TETIdle TETIdle { get; private set; }
  public TETWalk TETWalk { get; private set; }
  public TETAttack TETAttack { get; private set; }

  void Awake()
  {
    TETIdle = new TETIdle();
    TETWalk = new TETWalk();
    TETAttack = new TETAttack();
  }

  public void InitializeTransitions()
  {
    bossSM.AddTransition(TETIdle, TETWalk, () => !owner.TargetingModule.IsPlayerClose(5f) && !owner.IsDead);
    bossSM.AddTransition(TETIdle, TETAttack, () => !owner.AttackModule.isAttacking && owner.AttackModule.attackRequested && owner.AttackModule.finishAttack && !owner.IsDead);
    bossSM.AddTransition(TETWalk, TETAttack, () => !owner.AttackModule.isAttacking && owner.AttackModule.attackRequested && owner.AttackModule.finishAttack && !owner.IsDead);
    bossSM.AddTransition(TETWalk, TETIdle, () => owner.TargetingModule.IsPlayerClose(5f) && !owner.IsDead);
    bossSM.AddTransition(TETAttack, TETIdle, () => owner.AttackModule.finishAttack && !owner.AttackModule.isAttacking && !owner.IsDead);
  }
}