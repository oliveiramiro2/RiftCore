using UnityEngine;

public class MawController : BaseEntity
{
  [Header("Boss runtime")]
  public float moveSpeed = 4f;
  public bool IsFacingRight() => transform.localScale.x > 0;
  public bool Phase2() => currentHealth <= (maxHealth * 0.5f);

  public MawEventsManager events;

  public Transform PlayerTransform { get; private set; }

  public MawStateMachine MawSM { get; private set; }
  public MawStateFactory MawStateFactory { get; private set; }
  public MawAnimationBridge AnimatorBridge { get; private set; }
  public MawLocomotionModule Locomotion { get; private set; }
  public MawAttackModule Attack { get; private set; }

  public bool isAttacking = false;
  public bool canTeleport = false;
  public bool canFollowPlayer = false;
  public bool isMoving = false;
  public bool hasStaffSummoned = false;

  protected override void Awake()
  {
    base.Awake();
    PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
  }

  void Update()
  {
    if (MawSM == null || !canMove)
    {

      return;
    }
    MawSM.UpdateStateMachine();
  }

  public void SetupModules(MawStateMachine sm, MawStateFactory factory, MawAnimationBridge animator,
      MawLocomotionModule locomotionModule, MawAttackModule attackModule)
  {
    MawSM = sm;
    MawStateFactory = factory;
    AnimatorBridge = animator;
    Locomotion = locomotionModule;
    Attack = attackModule;
  }

  public void FlipX(bool faceRight)
  {
    transform.localScale = new Vector3(faceRight ? 1 : -1, 1, 1);
  }
}