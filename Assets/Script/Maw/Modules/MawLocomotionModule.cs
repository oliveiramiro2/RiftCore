using UnityEngine;

public class MawLocomotionModule : MonoBehaviour
{
  private MawController owner;
  private PlayerController target;
  private float timer = 0f, floatOutDuration = 0.5f, hideStaffDuration = 2.1f;
  public bool hasTeleported = false;
  public bool canFloat = true;

  void Start()
  {
    target = GameObject.FindAnyObjectByType<PlayerController>();
  }

  void Update()
  {
    if (owner.canTeleport && owner.canFollowPlayer && !hasTeleported)
    {
      hasTeleported = true;
      StartCoroutine(TeleportRoutine());
      return;
    }

    if (owner == null || target == null || !owner.canFollowPlayer || owner.canTeleport)
    {
      timer = 0f;
      return;
    }

    timer += Time.deltaTime;
    if (timer >= hideStaffDuration && owner.hasStaffSummoned)
    {
      owner.AnimatorBridge.MawHideStaff();
      timer = 0f;
    }

    if (owner.hasStaffSummoned) return;

    if (canFloat)
    {
      owner.AnimatorBridge.MawFloatIn();
      canFloat = false;
      timer = 0f;
      return;
    }

    if (timer >= floatOutDuration && !owner.canTeleport)
    {
      MoveTowardsPlayer();
    }
  }

  public void Setup(MawController entity)
  {
    owner = entity;
  }

  public void MoveTowardsPlayer()
  {
    float dist = Vector2.Distance(transform.position, target.transform.position);
    if (owner == null || target == null || !owner.canFollowPlayer || dist < 0.1f)
    {
      StopMovement();
      return;
    }

    Vector2 direction = (target.transform.position - owner.transform.position).normalized;

    owner.rb.linearVelocityX = direction.x * owner.moveSpeed;

    bool shouldFaceRight = direction.x > 0;
    owner.FlipX(shouldFaceRight);
  }

  public void FlipTowardsTarget(Transform target)
  {
    if (target.position.x > owner.rb.position.x)
    {
      transform.localScale = new Vector3(
          Mathf.Abs(transform.localScale.x),
          transform.localScale.y,
          transform.localScale.z);
    }
    else
    {
      transform.localScale = new Vector3(
          -Mathf.Abs(transform.localScale.x),
          transform.localScale.y,
          transform.localScale.z);
    }
  }

  public void StopMovement()
  {
    if (owner != null)
    {
      owner.rb.linearVelocityX = 0;
    }
  }

  private System.Collections.IEnumerator TeleportRoutine()
  {
    if (!owner.hasStaffSummoned)
    {
      owner.AnimatorBridge.MawSummonStaff();

      yield return new WaitForSeconds(1.5f);
    }

    FlipTowardsTarget(target.transform);
    owner.AnimatorBridge.MawTeleportIn();

    yield return new WaitForSeconds(2.5f);

    FlipTowardsTarget(target.transform);
    if (owner != null && target != null)
    {
      owner.transform.position = target.transform.position + new Vector3(
          Random.Range(-2f, 2f),
          0,
          0);
    }
    owner.AnimatorBridge.MawTeleportOut();

    yield return new WaitForSeconds(1.6f);

    FlipTowardsTarget(target.transform);
    owner.AnimatorBridge.MawHideStaff();

    yield return new WaitForSeconds(2f);

    FlipTowardsTarget(target.transform);
    owner.canTeleport = false;
    owner.canFollowPlayer = false;
  }
}