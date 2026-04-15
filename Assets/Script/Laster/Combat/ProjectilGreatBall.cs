using UnityEngine;

public class ProjectilGreatBall : MonoBehaviour
{
    private int speed = 5, direction = 1;
    public bool isSecond = false;
    private Vector2 movement;

    private Vector3 shootDir;

    void Start()
    {
        Destroy(gameObject, 3f);
        movement = isSecond ? Vector2.left : Vector2.right;
        speed = Random.Range(6, 8);
        direction = movement.x < 0 ? 5 : -5;
        shootDir = Quaternion.Euler(0, 0, Random.Range(0, direction) * transform.position.y) * movement;
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * shootDir);
    }
}
