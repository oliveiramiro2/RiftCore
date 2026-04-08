using UnityEngine;

public class LasterController : BaseEntity
{
  [Header("Boss runtime")]
  public float moveSpeed = 4f;
  public bool IsFacingRight() => transform.localScale.x > 0;
  public bool Phase2() => currentHealth <= (maxHealth * 0.5f);

  public LasterEventsManager events;

  public Transform PlayerTransform { get; private set; }

  public LasterStateMachine LasterSM { get; private set; }
  public LasterStateFactory LasterStateFactory { get; private set; }
  public LasterAnimationBridge AnimatorBridge { get; private set; }
  public LasterLocomotionModule Locomotion { get; private set; }
  //public LasterAttackModule Attack { get; private set; }

  public bool isAttacking = false;
  public bool canTeleport = false;
  public bool isMoving = false;

  protected override void Awake()
  {
    base.Awake();
    PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
  }

  void Update()
  {
    if (LasterSM == null || !canMove)
    {

      return;
    }
    LasterSM.UpdateStateMachine();
  }

  public void SetupModules(LasterStateMachine sm, LasterStateFactory factory, LasterAnimationBridge animator,
      LasterLocomotionModule locomotionModule)
  {
    LasterSM = sm;
    LasterStateFactory = factory;
    AnimatorBridge = animator;
    Locomotion = locomotionModule;
  }

  public void FlipX(bool faceRight)
  {
    transform.localScale = new Vector3(faceRight ? 1 : -1, 1, 1);
  }
}