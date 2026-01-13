using UnityEngine;

[RequireComponent(typeof(AstralWeaverController))]
[RequireComponent(typeof(AstralWeaverStateMachine))]
[RequireComponent(typeof(AstralWeaverStateFactory))]
public class AstralWeaverInitializer : MonoBehaviour
{
  void Awake()
  {
    var controller = GetComponent<AstralWeaverController>();
    var sm = GetComponent<AstralWeaverStateMachine>();
    var factory = GetComponent<AstralWeaverStateFactory>();
    // var animator = GetComponent<BossAnimationBridge>();
    // var targetPlayer = GetComponent<TargetingModule>();
    // var bossPhysics = GetComponent<TETBossPhysics>();
    // var locomotion = GetComponent<LocomotionModule>();
    // var attack = GetComponent<AttackModule>();

    controller.SetupModules(sm, factory);


    sm.Setup(controller);

    // factory.bossSM = sm;
    // factory.owner = controller;
    // factory.animatorBridge = animator;
    // factory.targetingModule = targetPlayer;
    // factory.bossPhysics = bossPhysics;
    // factory.locomotionModule = locomotion;
    // factory.attackModule = attack;

    // factory.InitializeTransitions();

    // sm.Initialize(factory.TETIdle);
    // attack.initialize(controller);
  }
}