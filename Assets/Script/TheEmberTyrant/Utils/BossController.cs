using UnityEngine;

[RequireComponent(typeof(BossStateMachine))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(BossStateMachine))]
[RequireComponent(typeof(BossAnimationBridge))]
[RequireComponent(typeof(LocomotionModule))]
public class BossController : BaseEntity
{

  [Header("Boss runtime")]
  public float MoveSpeed = 2f;
  public bool IsFacingRight() => transform.localScale.x > 0;
  public bool Phase2() => currentHealth <= (maxHealth * 0.5f);


  [Header("Scriptable's")]
  public TetEvents tetEvents;

  public BossStateMachine BossSM { get; private set; }
  public CapsuleCollider2D Collider { get; private set; }
  public BossAnimationBridge AnimatorBridge { get; private set; }
  public BossStateFactory StateFactory { get; private set; }
  public TargetingModule TargetingModule { get; private set; }
  public LocomotionModule LocomotionModule { get; private set; }
  public AttackModule AttackModule { get; private set; }
  public TETBossPhysics BossPhysics { get; private set; }

  public void SetupModules(BossStateMachine sm, BossStateFactory factory, BossAnimationBridge animator, TargetingModule target,
   TETBossPhysics bossPhysics, LocomotionModule locomotion, AttackModule attack)
  {
    BossSM = sm;
    AnimatorBridge = animator;
    StateFactory = factory;
    TargetingModule = target;
    BossPhysics = bossPhysics;
    LocomotionModule = locomotion;
    AttackModule = attack;
  }

  protected override void Awake()
  {
    base.Awake();
    Collider = GetComponent<CapsuleCollider2D>();
  }

  void Update()
  {
    if (BossSM == null || !canMove)
    {
      return;
    }
    BossSM.UpdateStateMachine();
  }


  void FixedUpdate()
  {
    if (BossSM != null)
      BossSM.FixedUpdateStateMachine();
  }

  public void FlipX(bool faceRight)
  {
    if (spriteRenderer != null) transform.localScale = new Vector3(faceRight ? 1 : -1, 1, 1);
  }
}