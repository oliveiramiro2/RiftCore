using UnityEngine;

public class SwordMasterController : BaseEntity
{
  [Header("Boss runtime")]
  public float moveSpeed = 12f;
  public bool IsFacingRight() => transform.localScale.x > 0;
  public bool Phase2() => currentHealth <= (maxHealth * 0.5f);

  public Transform PlayerTransform { get; private set; }

  public SwordMasterStateMachine SwordMasterSM { get; private set; }
  public SwordMasterStateFactory SwordMasterStateFactory { get; private set; }
  public SwordMasterAnimationBridge AnimatorBridge { get; private set; }
  public SwordMasterLocomotionModule LocomotionModule { get; private set; }
  public SwordMasterAttackModule Attack { get; private set; }

  public bool isAttacking = false;
  public bool canFollowPlayer = false;

  protected override void Awake()
  {
    base.Awake();
    PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
  }

  void Update()
  {
    if (SwordMasterSM == null || !canMove || IsDead)
    {
      return;
    }
    SwordMasterSM.UpdateStateMachine();
  }

  public void SetupModules(SwordMasterStateMachine sm, SwordMasterStateFactory factory, SwordMasterAnimationBridge animator,
  SwordMasterLocomotionModule locomotion, SwordMasterAttackModule attack)
  {
    SwordMasterSM = sm;
    SwordMasterStateFactory = factory;
    AnimatorBridge = animator;
    LocomotionModule = locomotion;
    Attack = attack;
  }

  public void FlipX(bool faceRight)
  {
    if (spriteRenderer != null) transform.localScale = new Vector3(faceRight ? 1 : -1, 1, 1);
  }
}