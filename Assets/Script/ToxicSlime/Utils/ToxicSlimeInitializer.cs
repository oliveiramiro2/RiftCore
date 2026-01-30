using UnityEngine;

[RequireComponent(typeof(ToxicSlimeController))]
[RequireComponent(typeof(ToxicSlimeStateMachine))]
[RequireComponent(typeof(ToxicSlimeStateFactory))]
[RequireComponent(typeof(ToxicSlimeAnimationBridge))]
public class ToxicSlimeInitializer : MonoBehaviour
{
  void Awake()
  {
    var controller = GetComponent<ToxicSlimeController>();
    var sm = GetComponent<ToxicSlimeStateMachine>();
    var factory = GetComponent<ToxicSlimeStateFactory>();
    var animator = GetComponent<ToxicSlimeAnimationBridge>();

    controller.SetupModules(sm, factory, animator);


    sm.Setup(controller);

    factory.bossSM = sm;
    factory.owner = controller;

    factory.InitializeTransitions();

    sm.Initialize(factory.ToxicSlimeIdle);
  }
}