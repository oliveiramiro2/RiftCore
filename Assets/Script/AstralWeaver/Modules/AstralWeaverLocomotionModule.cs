using UnityEngine;

public class AstralWeaverLocomotionModule : MonoBehaviour
{
  [SerializeField] private Transform[] teleportPoints;

  public void TeleportToRandomPoint(AstralWeaverController entity)
  {
    if (teleportPoints.Length == 0) return;

    int index = Random.Range(0, teleportPoints.Length);
    Transform targetPoint = teleportPoints[index];
    entity.transform.position = targetPoint.position;
  }

  public void FlipTowardsTarget(AstralWeaverController entity)
  {
    if (entity.PlayerTransform != null)
    {
      bool shouldFaceRight = entity.PlayerTransform.position.x > entity.transform.position.x;
      entity.FlipX(shouldFaceRight);
    }
  }
}