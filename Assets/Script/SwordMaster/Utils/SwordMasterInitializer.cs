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
    //var physics = GetComponent<SwordMasterPhysics>();
    //var locomotion = GetComponent<SwordMasterLocomotionModule>();
    //var attack = GetComponent<SwordMasterAttackModule>();
    //var colliders = GetComponent<SwordMasterCollidersContact>();

    controller.SetupModules(sm, factory, animator);


    sm.Setup(controller);
    //locomotion.Setup(controller);

    //factory.bossSM = sm;
    factory.owner = controller;

    factory.InitializeTransitions();

    //sm.Initialize(factory.ToxicSlimeIdle);
    //attack.Initialize(controller);
  }
}