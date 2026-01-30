using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class ToxicSlimeLocomotionModule : MonoBehaviour
{

  private ToxicSlimeController owner;
  private CircleCollider2D rollCollider;
  private CapsuleCollider2D normalCollider;

  void Awake()
  {
    rollCollider = GetComponent<CircleCollider2D>();
    normalCollider = GetComponent<CapsuleCollider2D>();
    rollCollider.enabled = false;
    normalCollider.enabled = true;
  }

  public void Setup(ToxicSlimeController entity)
  {
    owner = entity;
  }

  public void Patroling(ToxicSlimeController entity)
  {
    float targetSpeed = entity.IsFacingRight() ? entity.MoveSpeed : -entity.MoveSpeed;
    entity.Physics.ToxicSlimeMoveHorizontal(targetSpeed);
  }

  public void FlipTowardsTarget(ToxicSlimeController entity)
  {
    if (entity.PlayerTransform != null)
    {
      bool shouldFaceRight = entity.PlayerTransform.position.x > entity.transform.position.x;
      entity.FlipX(shouldFaceRight);
    }
  }

  public void Roll(float duration, float rotations)
  {
    rollCollider.enabled = true;
    normalCollider.enabled = false;
    StartCoroutine(RollRoutine(duration, rotations));
  }

  private IEnumerator RollRoutine(float duration, float rotations)
  {
    float elapsed = 0f;

    float startZ = transform.eulerAngles.z;
    float targetZ = owner.IsFacingRight() ? startZ - (360f * rotations) : startZ + (360f * rotations);

    while (elapsed < duration)
    {
      elapsed += Time.deltaTime;
      float t = elapsed / duration;

      float easedT = TemporalMath.EaseInOut(t);

      float z = Mathf.Lerp(startZ, targetZ, easedT);
      transform.rotation = Quaternion.Euler(0f, 0f, z);

      yield return null;
    }

    // força alinhamento final
    transform.rotation = Quaternion.Euler(0f, 0f, targetZ % 360f);

    normalCollider.enabled = true;
    rollCollider.enabled = false;
  }

  public void BossCanRoll()
  {
    owner.canRoll = true;
  }

  public void BossCannotRoll()
  {
    owner.canRoll = false;
  }
}