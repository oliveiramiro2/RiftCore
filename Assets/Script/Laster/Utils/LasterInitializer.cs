using UnityEngine;

[RequireComponent(typeof(LasterController))]
[RequireComponent(typeof(LasterStateMachine))]
[RequireComponent(typeof(LasterStateFactory))]
[RequireComponent(typeof(LasterAnimationBridge))]
[RequireComponent(typeof(LasterLocomotionModule))]
[RequireComponent(typeof(LasterAttackModule))]
public class LasterInitializer : MonoBehaviour
{
  void Awake()
  {
    var controller = GetComponent<LasterController>();
    var sm = GetComponent<LasterStateMachine>();
    var factory = GetComponent<LasterStateFactory>();
    var animator = GetComponent<LasterAnimationBridge>();
    var locomotionModule = GetComponent<LasterLocomotionModule>();
    var attackModule = GetComponent<LasterAttackModule>();

    controller.SetupModules(sm, factory, animator, locomotionModule, attackModule);
    locomotionModule.Setup(controller);
    animator.Setup(controller);
    attackModule.Setup(controller);

    sm.Setup(controller);

    factory.bossSM = sm;
    factory.owner = controller;

    factory.InitializeTransitions();

    sm.Initialize(factory.LasterIdle);
  }
}