using UnityEngine;

[RequireComponent(typeof(SwordMasterController))]
[RequireComponent(typeof(SwordMasterStateMachine))]
[RequireComponent(typeof(SwordMasterStateFactory))]
[RequireComponent(typeof(SwordMasterAnimationBridge))]
public class SwordMasterInitializer : MonoBehaviour
{
  void Awake()
  {
    var controller = GetComponent<SwordMasterController>();
    var sm = GetComponent<SwordMasterStateMachine>();
    var factory = GetComponent<SwordMasterStateFactory>();
    var animator = GetComponent<SwordMasterAnimationBridge>();

    controller.SetupModules(sm, factory, animator);


    sm.Setup(controller);

    factory.bossSM = sm;
    factory.owner = controller;

    factory.InitializeTransitions();

    sm.Initialize(factory.SwordMasterIdle);
  }
}