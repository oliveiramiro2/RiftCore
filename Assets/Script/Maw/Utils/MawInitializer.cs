using UnityEngine;

[RequireComponent(typeof(MawController))]
[RequireComponent(typeof(MawStateMachine))]
[RequireComponent(typeof(MawStateFactory))]
[RequireComponent(typeof(MawAnimationBridge))]
public class MawInitializer : MonoBehaviour
{
  void Awake()
  {
    var controller = GetComponent<MawController>();
    var sm = GetComponent<MawStateMachine>();
    var factory = GetComponent<MawStateFactory>();
    var animator = GetComponent<MawAnimationBridge>();

    controller.SetupModules(sm, factory, animator);

    sm.Setup(controller);

    factory.bossSM = sm;
    factory.owner = controller;

    factory.InitializeTransitions();

    sm.Initialize(factory.MawIdle);
  }
}