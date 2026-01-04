using UnityEngine;

[RequireComponent(typeof(BossController))]
[RequireComponent(typeof(BossStateMachine))]
[RequireComponent(typeof(BossStateFactory))]
[RequireComponent(typeof(BossAnimationBridge))]
[RequireComponent(typeof(TargetingModule))]
[RequireComponent(typeof(TETBossPhysics))]
[RequireComponent(typeof(LocomotionModule))]
[RequireComponent(typeof(AttackModule))]
public class BossInitializer : MonoBehaviour
{
  void Awake()
  {
    var controller = GetComponent<BossController>();
    var sm = GetComponent<BossStateMachine>();
    var factory = GetComponent<BossStateFactory>();
    var animator = GetComponent<BossAnimationBridge>();
    var targetPlayer = GetComponent<TargetingModule>();
    var bossPhysics = GetComponent<TETBossPhysics>();
    var locomotion = GetComponent<LocomotionModule>();
    var attack = GetComponent<AttackModule>();

    controller.SetupModules(sm, factory, animator, targetPlayer, bossPhysics, locomotion, attack);


    sm.Setup(controller);

    factory.bossSM = sm;
    factory.owner = controller;
    factory.animatorBridge = animator;
    factory.targetingModule = targetPlayer;
    factory.bossPhysics = bossPhysics;
    factory.locomotionModule = locomotion;
    factory.attackModule = attack;

    factory.InitializeTransitions();

    sm.Initialize(factory.TETIdle);
    attack.initialize(controller);
  }
}