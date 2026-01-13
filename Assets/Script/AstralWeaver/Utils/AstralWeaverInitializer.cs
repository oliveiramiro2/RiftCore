using UnityEngine;


public class AstralWeaverInitializer : MonoBehaviour
{
  void Awake()
  {
    var controller = GetComponent<AstralWeaverController>();
    var sm = GetComponent<AstralWeaverStateMachine>();
    // var factory = GetComponent<BossStateFactory>();
    // var animator = GetComponent<BossAnimationBridge>();
    // var targetPlayer = GetComponent<TargetingModule>();
    // var bossPhysics = GetComponent<TETBossPhysics>();
    // var locomotion = GetComponent<LocomotionModule>();
    // var attack = GetComponent<AttackModule>();

    //controller.SetupModules(sm, factory, animator, targetPlayer, bossPhysics, locomotion, attack);


    //sm.Setup(controller);

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