using UnityEngine;

public class SwordMasterLocomotionModule : MonoBehaviour
{
  private SwordMasterController owner;
  private PlayerController target;

  void Start()
  {
    target = GameObject.FindAnyObjectByType<PlayerController>();
  }

  void Update()
  {

    if (owner == null || target == null || !owner.canFollowPlayer)
    {
      return;
    }

    MoveTowardsPlayer();
  }

  public void Setup(SwordMasterController entity)
  {
    owner = entity;
  }

  public void MoveTowardsPlayer()
  {
    float dist = Vector2.Distance(transform.position, target.transform.position);
    if (owner == null || target == null || !owner.canFollowPlayer || dist < 1f)
    {
      owner.AnimatorBridge.SwordMasterIdle();
      return;
    }

    owner.AnimatorBridge.SwordMasterRun();
    Vector2 direction = (target.transform.position - owner.transform.position).normalized;
    owner.transform.Translate(owner.MoveSpeed * Time.deltaTime * direction);

    bool shouldFaceRight = direction.x > 0;
    owner.FlipX(shouldFaceRight);
  }
}