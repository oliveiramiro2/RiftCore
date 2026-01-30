using UnityEngine;

public class ToxicSlimeController : BaseEntity
{
  [Header("Boss runtime")]
  public float MoveSpeed = 5f;
  public bool IsFacingRight() => transform.localScale.x > 0;
  public bool Phase2() => currentHealth <= (maxHealth * 0.5f);
  public Transform PlayerTransform { get; private set; }


  public ToxicSlimeStateMachine ToxicSlimeSM { get; private set; }
  public ToxicSlimeStateFactory ToxicSlimeStateFactory { get; private set; }
  public ToxicSlimeAnimationBridge AnimatorBridge { get; private set; }
  public ToxicSlimePhysics Physics { get; private set; }
  public ToxicSlimeLocomotionModule Locomotion { get; private set; }
  public ToxicSlimeAttackModule Attack { get; private set; }

  public bool CanMove = true;

  public bool canRoll = false;

  public bool isAttacking = false;

  protected override void Awake()
  {
    base.Awake();
    PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
  }

  void Update()
  {
    if (ToxicSlimeSM == null || !CanMove || IsDead)
    {
      return;
    }
    ToxicSlimeSM.UpdateStateMachine();
  }

  public void SetupModules(ToxicSlimeStateMachine sm, ToxicSlimeStateFactory factory, ToxicSlimeAnimationBridge animator,
  ToxicSlimeLocomotionModule locomotion, ToxicSlimePhysics physics, ToxicSlimeAttackModule attack)
  {
    ToxicSlimeSM = sm;
    ToxicSlimeStateFactory = factory;
    AnimatorBridge = animator;
    Locomotion = locomotion;
    Physics = physics;
    Attack = attack;
  }

  public void FlipX(bool faceRight)
  {
    if (spriteRenderer != null) transform.localScale = new Vector3(faceRight ? 1 : -1, 1, 1);
  }

}
