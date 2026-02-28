using UnityEngine;

[RequireComponent(typeof(SwordMasterController))]
[RequireComponent(typeof(SwordMasterStateMachine))]
[RequireComponent(typeof(SwordMasterStateFactory))]
[RequireComponent(typeof(SwordMasterAnimationBridge))]
[RequireComponent(typeof(SwordMasterLocomotionModule))]
[RequireComponent(typeof(SwordMasterAttackModule))]
public class SwordMasterInitializer : MonoBehaviour
{
  void Awake()
  {
    var controller = GetComponent<SwordMasterController>();
    var sm = GetComponent<SwordMasterStateMachine>();
    var factory = GetComponent<SwordMasterStateFactory>();
    var animator = GetComponent<SwordMasterAnimationBridge>();
    var locomotion = GetComponent<SwordMasterLocomotionModule>();
    var attacks = GetComponent<SwordMasterAttackModule>();

    controller.SetupModules(sm, factory, animator, locomotion, attacks);

    sm.Setup(controller);
    locomotion.Setup(controller);
    attacks.Initialize(controller);

    factory.bossSM = sm;
    factory.owner = controller;

    factory.InitializeTransitions();

    sm.Initialize(factory.SwordMasterIdle);
  }
}