using UnityEngine;

public class TargetingModule : MonoBehaviour
{
  private Transform bossTransform;
  public Transform player;

  void Awake()
  {
    bossTransform = transform;
  }

  public TargetingModule(Transform boss)
  {
    bossTransform = boss;
  }

  public Vector2 PlayerPosition => player.position;

  public float DistanceToPlayer =>
      Vector2.Distance(bossTransform.position, player.position);

  public Vector2 DirectionToPlayer =>
      (player.position - bossTransform.position).normalized;

  public bool IsPlayerOnRight =>
      player.position.x > bossTransform.position.x;

  public bool IsPlayerClose(float maxDistance) =>
      DistanceToPlayer <= maxDistance;
}