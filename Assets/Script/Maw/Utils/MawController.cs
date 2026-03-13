using UnityEngine;

public class MawController : BaseEntity
{
  [Header("Boss runtime")]
  public float moveSpeed = 4f;
  public bool IsFacingRight() => transform.localScale.x > 0;
  public bool Phase2() => currentHealth <= (maxHealth * 0.5f);

  public Transform PlayerTransform { get; private set; }

  public MawStateMachine MawSM { get; private set; }
  public MawStateFactory MawStateFactory { get; private set; }
  public MawAnimationBridge AnimatorBridge { get; private set; }

  public bool isAttacking = false;
  public bool canTeleport = false;

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

  public void SetupModules(MawStateMachine sm, MawStateFactory factory, MawAnimationBridge animator)
  {
    MawSM = sm;
    MawStateFactory = factory;
    AnimatorBridge = animator;
  }

  public void FlipX(bool faceRight)
  {
    transform.localScale = new Vector3(faceRight ? 1 : -1, 1, 1);
  }
}