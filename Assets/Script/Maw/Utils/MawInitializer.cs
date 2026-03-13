using UnityEngine;

[RequireComponent(typeof(MawController))]
[RequireComponent(typeof(MawStateMachine))]
[RequireComponent(typeof(MawStateFactory))]
[RequireComponent(typeof(MawAnimationBridge))]
[RequireComponent(typeof(MawLocomotionModule))]
public class MawInitializer : MonoBehaviour
{
  void Awake()
  {
    var controller = GetComponent<MawController>();
    var sm = GetComponent<MawStateMachine>();
    var factory = GetComponent<MawStateFactory>();
    var animator = GetComponent<MawAnimationBridge>();
    var locomotionModule = GetComponent<MawLocomotionModule>();

    controller.SetupModules(sm, factory, animator, locomotionModule);
    locomotionModule.Setup(controller);

    sm.Setup(controller);

    factory.bossSM = sm;
    factory.owner = controller;

    factory.InitializeTransitions();

    sm.Initialize(factory.MawIdle);
  }
}