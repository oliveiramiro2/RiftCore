using UnityEngine;

[RequireComponent(typeof(AstralWeaverController))]
[RequireComponent(typeof(AstralWeaverStateMachine))]
[RequireComponent(typeof(AstralWeaverStateFactory))]
[RequireComponent(typeof(AstralWeaverAnimationBridge))]
public class AstralWeaverInitializer : MonoBehaviour
{
  void Awake()
  {
    var controller = GetComponent<AstralWeaverController>();
    var sm = GetComponent<AstralWeaverStateMachine>();
    var factory = GetComponent<AstralWeaverStateFactory>();
    var animator = GetComponent<AstralWeaverAnimationBridge>();

    controller.SetupModules(sm, factory, animator);


    sm.Setup(controller);

    factory.bossSM = sm;
    factory.owner = controller;
    factory.animatorBridge = animator;

    factory.InitializeTransitions();

    sm.Initialize(factory.AstralWeaverIdle);
  }
}