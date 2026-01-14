using UnityEngine;

[RequireComponent(typeof(AstralWeaverController))]
[RequireComponent(typeof(AstralWeaverStateMachine))]
[RequireComponent(typeof(AstralWeaverStateFactory))]
[RequireComponent(typeof(AstralWeaverAnimationBridge))]
[RequireComponent(typeof(AstralWeaverAttackModule))]
[RequireComponent(typeof(AstralWeaverLocomotionModule))]
public class AstralWeaverInitializer : MonoBehaviour
{
  void Awake()
  {
    var controller = GetComponent<AstralWeaverController>();
    var sm = GetComponent<AstralWeaverStateMachine>();
    var factory = GetComponent<AstralWeaverStateFactory>();
    var animator = GetComponent<AstralWeaverAnimationBridge>();
    var attackModule = GetComponent<AstralWeaverAttackModule>();
    var locomotionModule = GetComponent<AstralWeaverLocomotionModule>();

    controller.SetupModules(sm, factory, animator, attackModule, locomotionModule);


    sm.Setup(controller);

    factory.bossSM = sm;
    factory.owner = controller;

    factory.InitializeTransitions();

    sm.Initialize(factory.AstralWeaverIdle);
  }
}