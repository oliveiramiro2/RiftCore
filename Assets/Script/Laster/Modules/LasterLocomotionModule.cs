using UnityEngine;

public class LasterLocomotionModule : MonoBehaviour
{
  private LasterController owner;
  private PlayerController target;
  public bool hasTeleported = false;
  public bool canFloat = true;

  void Start()
  {
    target = GameObject.FindAnyObjectByType<PlayerController>();
  }

  void Update()
  {
    if (owner.IsDead) return;
    if (owner.canTeleport && !hasTeleported)
    {
      hasTeleported = true;
      StartCoroutine(TeleportRoutine());
      return;
    }

    
  }

  public void Setup(LasterController entity)
  {
    owner = entity;
  }

  public void MoveTowardsPlayer()
  {
    float dist = Vector2.Distance(transform.position, target.transform.position);
    if (owner == null || target == null || dist < 0.1f)
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
    yield return null;
  }
}