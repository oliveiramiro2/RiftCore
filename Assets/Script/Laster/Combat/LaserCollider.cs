using UnityEngine;

public class LaserCollider : MonoBehaviour
{
  public Transform start;
  public Transform endPoint;
  public Transform player;
  public float maxDistance = 20f;
  public LayerMask hitMask;
  public int damage = 4;



  void CheckCollision()
  {
    Vector2 direction = (player.position - start.position).normalized;
    Vector2 dir = (player.position - start.position).normalized;
    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

    transform.rotation = Quaternion.Euler(0, 0, angle);

    Vector2 origin = (Vector2)start.position;

    RaycastHit2D hit = Physics2D.Raycast(origin, dir, maxDistance, hitMask);

    Debug.Log("Checking laser collision: " + hit.collider?.name);
    Debug.DrawRay(origin, direction * maxDistance, Color.red);

    if (hit.collider != null)
    {
      var damageable = hit.collider.GetComponent<Hurtbox>();

      if (damageable != null)
      {
        damageable.ApplyDamage(damage, direction, 10f);
      }
    }
  }

  void Update()
  {
    CheckCollision();
  }
}