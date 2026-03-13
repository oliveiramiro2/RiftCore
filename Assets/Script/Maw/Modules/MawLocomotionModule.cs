using UnityEngine;

public class MawLocomotionModule : MonoBehaviour
{
  private MawController owner;
  private PlayerController target;
  private float timer = 0f, floatOutDuration = 0.5f;

  void Start()
  {
    target = GameObject.FindAnyObjectByType<PlayerController>();
  }

  void Update()
  {

    if (owner == null || target == null || !owner.canFollowPlayer)
    {
      timer = 0f;
      return;
    }

    timer += Time.deltaTime;
    if (timer >= floatOutDuration)
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
    if (owner == null || target == null || !owner.canFollowPlayer || dist < 1f)
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
}