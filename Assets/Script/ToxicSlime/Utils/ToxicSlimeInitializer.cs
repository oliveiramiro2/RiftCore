using UnityEngine;

[RequireComponent(typeof(ToxicSlimeController))]
[RequireComponent(typeof(ToxicSlimeStateMachine))]
[RequireComponent(typeof(ToxicSlimeStateFactory))]
[RequireComponent(typeof(ToxicSlimeAnimationBridge))]
[RequireComponent(typeof(ToxicSlimeLocomotionModule))]
[RequireComponent(typeof(ToxicSlimePhysics))]
[RequireComponent(typeof(ToxicSlimeAttackModule))]
public class ToxicSlimeInitializer : MonoBehaviour
{
  void Awake()
  {
    var controller = GetComponent<ToxicSlimeController>();
    var sm = GetComponent<ToxicSlimeStateMachine>();
    var factory = GetComponent<ToxicSlimeStateFactory>();
    var animator = GetComponent<ToxicSlimeAnimationBridge>();
    var physics = GetComponent<ToxicSlimePhysics>();
    var locomotion = GetComponent<ToxicSlimeLocomotionModule>();
    var attack = GetComponent<ToxicSlimeAttackModule>();

    controller.SetupModules(sm, factory, animator, locomotion, physics, attack);


    sm.Setup(controller);
    locomotion.Setup(controller);

    factory.bossSM = sm;
    factory.owner = controller;

    factory.InitializeTransitions();

    sm.Initialize(factory.ToxicSlimeIdle);
    attack.Initialize(controller);
  }
}