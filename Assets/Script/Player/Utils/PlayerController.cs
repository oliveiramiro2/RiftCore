
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputReader))]
[RequireComponent(typeof(PlayerPhysics))]
[RequireComponent(typeof(PlayerAnimatorBridge))]
[RequireComponent(typeof(PlayerStateMachine))]
[RequireComponent(typeof(PlayerLocomotion))]
[RequireComponent(typeof(PlayerJumpModule))]
[RequireComponent(typeof(PlayerStateFactory))]
[RequireComponent(typeof(PlayerAttackModule))]
public class PlayerController : BaseEntity
{
    public PlayerInputReader InputReader { get; private set; }
    public PlayerAnimatorBridge AnimatorBridge { get; private set; }
    public PlayerStateFactory StateFactory { get; private set; }
    public PlayerStateMachine PlayerSM { get; private set; }
    public PlayerPhysics PhysicsModule { get; private set; }
    public PlayerLocomotion LocomotionModule { get; private set; }
    public PlayerJumpModule JumpModule { get; private set; }
    public PlayerAttackModule AttackModule { get; private set; }
    public PlayerDashModule DashModule { get; private set; }
    public IFrames IFrames { get; private set; }
    public PlayerDamageHandler DamageHandler { get; private set; }
    public AbilityManager PlayerAbilities { get; private set; }

    private PlayerInput playerInput;

    [Header("Stats")]
    public float baseMoveSpeed = 8f;
    public float baseJumpForce = 12f;

    [Header("Scriptable's")]
    public PlayerStats stats;
    public PlayerEvents events;

    [Header("hurtbox reference")]
    public BoxCollider2D hurtboxCollider;
    public Hitbox dashHitbox;
    public int buffSwordDamage = 1;


    public bool isCastingSpell = false;

    public void SetupModules(PlayerInputReader input, PlayerStateMachine sm, PlayerPhysics physics,
                             PlayerLocomotion locomotion, PlayerJumpModule jump, PlayerAttackModule attack,
                             PlayerStateFactory factory, PlayerAnimatorBridge animator, PlayerDashModule dash,
                             IFrames frames, PlayerDamageHandler damageHandler, AbilityManager abilities)
    {
        InputReader = input;
        PlayerSM = sm;
        PhysicsModule = physics;
        LocomotionModule = locomotion;
        JumpModule = jump;
        AttackModule = attack;
        StateFactory = factory;
        AnimatorBridge = animator;
        DashModule = dash;
        IFrames = frames;
        DamageHandler = damageHandler;
        PlayerAbilities = abilities;
    }

    protected override void Awake()
    {
        base.Awake();
        playerInput = GetComponent<PlayerInput>();
        playerInput.actions.FindActionMap("UI").Disable();
        playerInput.actions.FindActionMap("Player").Enable();
    }

    void Update()
    {
        if (IsDead || !canMove) return;
        if (PlayerSM == null || InputReader == null)
        {
            Debug.LogWarning("PlayerController: Aguardando Inicialização...");
            return;
        }

        PhysicsModule.GroundCheck();

        if (InputReader.JumpPressed)
            JumpModule.RegisterJumpPressed();

        PlayerSM.UpdateStateMachine();

        if (isSlowed)
        {
            durationSlow -= Time.deltaTime;
            if (durationSlow <= 0)
            {
                isSlowed = false;
                durationSlow = 0f;
                slowVelocity = 1f;
            }
        }

        InputReader.ResetOneFrameInputs();
    }

    void FixedUpdate()
    {
        if (PlayerSM != null)
            PlayerSM.FixedUpdateStateMachine();
    }

    public void FlipX(bool faceRight)
    {
        if (spriteRenderer != null) transform.localScale = new Vector3(faceRight ? 1 : -1, 1, 1);
    }

    public void DisableMovement(float duration)
    {
        StartCoroutine(DisableMovementRoutine(duration));
    }

    private IEnumerator DisableMovementRoutine(float duration)
    {
        canMove = false;
        yield return new WaitForSeconds(duration);
        canMove = true;
    }
}