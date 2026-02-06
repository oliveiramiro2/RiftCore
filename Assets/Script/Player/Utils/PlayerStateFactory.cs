using UnityEngine;

public class PlayerStateFactory : MonoBehaviour
{
    [HideInInspector] public PlayerStateMachine playerSM;
    [HideInInspector] public PlayerController owner;
    [HideInInspector] public PlayerInputReader inputReader;
    [HideInInspector] public PlayerPhysics physicsModule;
    [HideInInspector] public PlayerLocomotion locomotionModule;
    [HideInInspector] public PlayerJumpModule jumpModule;
    [HideInInspector] public PlayerAttackModule attackModule;
    [HideInInspector] public PlayerAnimatorBridge animatorBridge;
    [HideInInspector] public PlayerDashModule dashModule;
    [HideInInspector] public IFrames iFrames;
    [HideInInspector] public PlayerDamageHandler damageHandler;
    [HideInInspector] public AbilityManager abilities;

    public State<PlayerController> Idle { get; private set; }
    public State<PlayerController> Run { get; private set; }
    public State<PlayerController> Jump { get; private set; }
    public State<PlayerController> Fall { get; private set; }
    public State<PlayerController> Attack { get; private set; }
    public State<PlayerController> Dash { get; private set; }
    public State<PlayerController> Spell { get; private set; }

    void Awake()
    {
        Idle = new PlayerIdleState();
        Run = new PlayerRunState();
        Jump = new PlayerJumpState();
        Fall = new PlayerFallState();
        Attack = new PlayerAttackState();
        Dash = new PlayerDashState();
        Spell = new PlayerCastSpellState();
    }

    public void InitializeTransitions()
    {
        // Idle
        playerSM.AddTransition(Idle, Run, () => !owner.IsDead && Mathf.Abs(inputReader.MoveInput.x) > 0.1f && owner.canMove);
        playerSM.AddTransition(Run, Idle, () => !owner.IsDead && Mathf.Abs(inputReader.MoveInput.x) < 0.1f);
        playerSM.AddTransition(Idle, Fall, () => !owner.IsDead && !physicsModule.isGrounded && physicsModule.rb.linearVelocityY <= 0);
        playerSM.AddTransition(Run, Fall, () => !owner.IsDead && !physicsModule.isGrounded && physicsModule.rb.linearVelocityY <= 0);

        // Pulo
        playerSM.AddTransition(Idle, Jump, () => !owner.IsDead && jumpModule.CanJump() && owner.canMove);
        playerSM.AddTransition(Run, Jump, () => !owner.IsDead && jumpModule.CanJump() && owner.canMove);
        playerSM.AddTransition(Jump, Fall, () => !owner.IsDead && physicsModule.rb.linearVelocityY <= 0);
        playerSM.AddTransition(Fall, Idle, () => !owner.IsDead && physicsModule.isGrounded && physicsModule.rb.linearVelocityY <= 0);

        // Dash
        playerSM.AddAnyTransition(Dash, () => !owner.IsDead && inputReader.DashPressed && dashModule.CanDash() && owner.canMove && !owner.isCastingSpell);
        playerSM.AddTransition(Dash, Fall, () => !owner.IsDead && !dashModule.IsDashing() && physicsModule.rb.linearVelocityY <= 0);
        playerSM.AddTransition(Dash, Idle, () => !owner.IsDead && !dashModule.IsDashing() && physicsModule.isGrounded && Mathf.Abs(inputReader.MoveInput.x) < 0.1f);
        playerSM.AddTransition(Dash, Run, () => !owner.IsDead && !dashModule.IsDashing() && physicsModule.isGrounded && Mathf.Abs(inputReader.MoveInput.x) > 0.1f);
        playerSM.AddTransition(Dash, Jump, () => !owner.IsDead && !dashModule.IsDashing() && jumpModule.CanJump());


        // Attack
        playerSM.AddAnyTransition(Attack, () => !owner.IsDead && owner.canMove && inputReader.AttackPressed && !attackModule.IsOnCooldown() && !owner.isCastingSpell);

        playerSM.AddTransition(Attack, Fall,
            () => !owner.IsDead && !physicsModule.isGrounded
               && !attackModule.IsInComboWindow()
        && animatorBridge.IsCurrentAnimationFinished());

        playerSM.AddTransition(Attack, Run,
            () => !owner.IsDead && physicsModule.isGrounded && Mathf.Abs(inputReader.MoveInput.x) > 0.1f
               && !attackModule.IsInComboWindow()
        && animatorBridge.IsCurrentAnimationFinished());

        playerSM.AddTransition(Attack, Idle,
            () => !owner.IsDead && physicsModule.isGrounded && Mathf.Abs(inputReader.MoveInput.x) < 0.1f
               && !attackModule.IsInComboWindow()
        && animatorBridge.IsCurrentAnimationFinished());

        // Spell
        playerSM.AddAnyTransition(Spell, () => !owner.IsDead && owner.canMove && inputReader.CastSpell && !owner.isCastingSpell);
        playerSM.AddTransition(Spell, Idle, () => !owner.IsDead && owner.canMove
        && !owner.isCastingSpell && Mathf.Abs(inputReader.MoveInput.x) < 0.1f);
        playerSM.AddTransition(Spell, Run, () => !owner.IsDead && owner.canMove
        && !owner.isCastingSpell && Mathf.Abs(inputReader.MoveInput.x) > 0.1f);
    }
}