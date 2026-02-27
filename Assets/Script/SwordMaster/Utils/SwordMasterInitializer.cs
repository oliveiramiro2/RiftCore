using UnityEngine;

[RequireComponent(typeof(SwordMasterController))]
[RequireComponent(typeof(SwordMasterStateMachine))]
[RequireComponent(typeof(SwordMasterStateFactory))]
[RequireComponent(typeof(SwordMasterAnimationBridge))]
[RequireComponent(typeof(SwordMasterLocomotionModule))]
public class SwordMasterInitializer : MonoBehaviour
{
  void Awake()
  {
    var controller = GetComponent<SwordMasterController>();
    var sm = GetComponent<SwordMasterStateMachine>();
    var factory = GetComponent<SwordMasterStateFactory>();
    var animator = GetComponent<SwordMasterAnimationBridge>();
    var locomotion = GetComponent<SwordMasterLocomotionModule>();

    controller.SetupModules(sm, factory, animator, locomotion);

    sm.Setup(controller);
    locomotion.Setup(controller);

    factory.bossSM = sm;
    factory.owner = controller;

    factory.InitializeTransitions();

    sm.Initialize(factory.SwordMasterIdle);
  }
}