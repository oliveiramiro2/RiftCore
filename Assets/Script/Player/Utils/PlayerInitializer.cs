using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerStateMachine))]
[RequireComponent(typeof(PlayerStateFactory))]
[RequireComponent(typeof(PlayerInputReader))]
[RequireComponent(typeof(PlayerPhysics))]
[RequireComponent(typeof(PlayerAnimatorBridge))]
[RequireComponent(typeof(PlayerLocomotion))]
[RequireComponent(typeof(PlayerJumpModule))]
[RequireComponent(typeof(PlayerAttackModule))]
[RequireComponent(typeof(PlayerDashModule))]
[RequireComponent(typeof(IFrames))]
[RequireComponent(typeof(AbilityManager))]
public class PlayerInitializer : MonoBehaviour
{
  void Awake()
  {
    var controller = GetComponent<PlayerController>();
    var sm = GetComponent<PlayerStateMachine>();
    var factory = GetComponent<PlayerStateFactory>();
    var input = GetComponent<PlayerInputReader>();
    var physics = GetComponent<PlayerPhysics>();
    var animator = GetComponent<PlayerAnimatorBridge>();
    var locomotion = GetComponent<PlayerLocomotion>();
    var jump = GetComponent<PlayerJumpModule>();
    var attack = GetComponent<PlayerAttackModule>();
    var dash = GetComponent<PlayerDashModule>();
    var iframes = GetComponent<IFrames>();
    var damageHandler = GetComponent<PlayerDamageHandler>();
    var abilities = GetComponent<AbilityManager>();

    controller.SetupModules(input, sm, physics, locomotion, jump, attack, factory, animator, dash, iframes, damageHandler, abilities);

    sm.Setup(controller);

    factory.playerSM = sm;
    factory.owner = controller;
    factory.inputReader = input;
    factory.physicsModule = physics;
    factory.locomotionModule = locomotion;
    factory.jumpModule = jump;
    factory.attackModule = attack;
    factory.animatorBridge = animator;
    factory.dashModule = dash;
    factory.iFrames = iframes;
    factory.damageHandler = damageHandler;
    factory.abilities = abilities;

    factory.InitializeTransitions();

    dash.Initialize(controller, physics);
    jump.Initialize(controller, physics);
    locomotion.Initialize(controller, physics, input);
    attack.Initialize(controller);
    input.Initialize(controller);
    abilities.Initialize(controller);

    sm.Initialize(factory.Idle);
  }
}