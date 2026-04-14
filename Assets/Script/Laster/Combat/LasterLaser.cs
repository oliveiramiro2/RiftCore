using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LasterLaser : MonoBehaviour
{
  public Transform origin;
  public float maxDistance = 20f;
  public LayerMask hitMask;

  private LineRenderer lr;
  public Transform target;

  public float rotationSpeed = 90f;
  private bool invertAngle = true;
  private bool wasInverted = false;

  private float angle;

  void Awake()
  {
    lr = GetComponent<LineRenderer>();
    lr.positionCount = 2;
  }

  private void OnEnable()
  {
    lr.SetPosition(0, origin.position);
    lr.SetPosition(1, origin.position);
    invertAngle = !invertAngle;
    wasInverted = false;
    angle = 0f;
  }

  void Update()
  {
    SweepLaser();
  }

  void SweepLaser()
  {
    angle += rotationSpeed * Time.deltaTime;
    if (wasInverted)
    {

      angle = invertAngle ? -angle : angle;
      wasInverted = true;
    }


    Vector2 direction = new Vector2(
        Mathf.Cos(angle * Mathf.Deg2Rad),
        Mathf.Sin(angle * Mathf.Deg2Rad)
    );

    Fire(direction);
  }

  public void Fire(Vector2 direction)
  {
    Vector2 start = origin.position;
    Vector2 end = start + direction * maxDistance;

    RaycastHit2D hit = Physics2D.Raycast(start, direction, maxDistance, hitMask);

    if (hit.collider != null)
    {
      end = hit.point;
    }

    lr.SetPosition(0, start);
    lr.SetPosition(1, end);
  }
}