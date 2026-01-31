using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class ToxicSlimeLocomotionModule : MonoBehaviour
{

  private ToxicSlimeController owner;
  private CircleCollider2D rollCollider;
  private CapsuleCollider2D normalCollider;
  private Transform posPlayer;

  float auxX, auxY, timer = 0f, flipDelay = 0.2f;

  void Awake()
  {
    rollCollider = GetComponent<CircleCollider2D>();
    normalCollider = GetComponent<CapsuleCollider2D>();
    rollCollider.enabled = false;
    normalCollider.enabled = true;
  }

  void Update()
  {
    posPlayer = owner.PlayerTransform;

    if (owner.isAttacking) return;
    timer += Time.deltaTime;
    if (timer >= flipDelay)
    {
      FlipTowardsTarget(owner);
      timer = 0f;
    }

  }

  public void Setup(ToxicSlimeController entity)
  {
    owner = entity;
  }

  public void Rolling(ToxicSlimeController entity)
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

  public void Roll(float duration, float rotations, bool disableCollider = true)
  {
    rollCollider.enabled = true;
    normalCollider.enabled = false;
    StartCoroutine(RollRoutine(duration, rotations, disableCollider));
  }

  public void JumpAtTarget(float height, float duration)
  {
    StartCoroutine(JumpToTarget(height, duration));
  }

  private IEnumerator RollRoutine(float duration, float rotations, bool disableCollider = true)
  {
    float elapsed = 0f;

    float startZ = transform.eulerAngles.z;
    float targetZ = owner.IsFacingRight() ? startZ - (360f * rotations) : startZ + (360f * rotations);

    while (elapsed < duration)
    {
      elapsed += Time.deltaTime;
      float t = elapsed / duration;

      float easedT = TemporalMath.EaseOut(t);

      float z = Mathf.Lerp(startZ, targetZ, easedT);
      transform.rotation = Quaternion.Euler(0f, 0f, z);

      yield return null;
    }

    transform.rotation = Quaternion.Euler(0f, 0f, targetZ % 360f);

    if (disableCollider)
      EnableNormalCollider();
  }

  private void EnableNormalCollider()
  {
    normalCollider.enabled = true;
    rollCollider.enabled = false;
  }

  private IEnumerator JumpToTarget(float height, float duration)
  {
    Vector2 start = transform.position;
    float time = 0f;

    while (time < duration)
    {

      time += Time.deltaTime;
      float t = time / duration;

      if (duration * 0.6 > time)
      {
        auxX = posPlayer.position.x;
        auxY = posPlayer.position.y;
      }

      Vector2 pos = Vector2.Lerp(start, new Vector2(auxX, auxY), t);

      float yOffset = height * 4 * (t - 0.5f) * (t - 0.5f) * -1 + height;
      pos.y += yOffset;

      transform.position = pos;
      yield return null;
    }

    transform.position = new Vector2(auxX, auxY);
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