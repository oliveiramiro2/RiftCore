using UnityEngine;

public class SwordMasterController : BaseEntity
{
  [Header("Boss runtime")]
  public float MoveSpeed = 6f;
  public bool IsFacingRight() => transform.localScale.x > 0;
  public bool Phase2() => currentHealth <= (maxHealth * 0.5f);

  public Transform PlayerTransform { get; private set; }

  public SwordMasterStateMachine SwordMasterSM { get; private set; }
  public SwordMasterStateFactory SwordMasterStateFactory { get; private set; }
  public SwordMasterAnimationBridge AnimatorBridge { get; private set; }

  public bool isAttacking = false;

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

  public void SetupModules(SwordMasterStateMachine sm, SwordMasterStateFactory factory, SwordMasterAnimationBridge animator)
  {
    SwordMasterSM = sm;
    SwordMasterStateFactory = factory;
    AnimatorBridge = animator;
  }

  public void FlipX(bool faceRight)
  {
    if (spriteRenderer != null) transform.localScale = new Vector3(faceRight ? 1 : -1, 1, 1);
  }
}