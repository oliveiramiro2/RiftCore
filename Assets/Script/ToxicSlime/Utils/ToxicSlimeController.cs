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
  public ToxicSlimePhysics ToxicSlimePhysics { get; private set; }
  public ToxicSlimeLocomotionModule ToxicSlimeLocomotionModule { get; private set; }

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
  ToxicSlimeLocomotionModule locomotion, ToxicSlimePhysics physics)
  {
    ToxicSlimeSM = sm;
    ToxicSlimeStateFactory = factory;
    AnimatorBridge = animator;
    ToxicSlimeLocomotionModule = locomotion;
    ToxicSlimePhysics = physics;
  }

  public void FlipX(bool faceRight)
  {
    if (spriteRenderer != null) transform.localScale = new Vector3(faceRight ? 1 : -1, 1, 1);
  }

}
