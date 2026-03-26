using UnityEngine;

public class Crow : MonoBehaviour
{
    [Header("Movimento Horizontal")]
    public float speed = 2f;
    public float leftLimit = -5f;
    public float rightLimit = 5f;

    [Header("Movimento Orgânico")]
    public float amplitude = 0.2f;
    public float frequency = 2f;

    [Header("Comportamento")]
    public float waitTime = 1f;

    int direction = 1;
    float waitTimer;
    bool waiting;

    float baseY;

    void Start()
    {
        baseY = transform.position.y;
    }

    void Update()
    {
        HandleMovement();
        HandleFloating();
        HandleFlip();
    }

    void HandleMovement()
    {
        if (waiting)
        {
            waitTimer += Time.deltaTime;

            if (waitTimer >= waitTime)
            {
                waiting = false;
                waitTimer = 0f;
                direction *= -1;
            }

            return;
        }

        transform.position += Vector3.right * direction * speed * Time.deltaTime;

        if (transform.position.x > rightLimit || transform.position.x < leftLimit)
        {
            waiting = true;
        }
    }

    void HandleFloating()
    {
        float yOffset = Mathf.Sin(Time.time * frequency) * amplitude;

        transform.position = new Vector3(
            transform.position.x,
            baseY + yOffset,
            transform.position.z
        );
    }

    void HandleFlip()
    {
        if (direction > 0 && transform.localScale.x < 0)
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        else if (direction < 0 && transform.localScale.x > 0)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}