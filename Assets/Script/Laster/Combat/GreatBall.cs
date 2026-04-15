using UnityEngine;

public class GreatBall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 0.5f);
    }

    void Update()
    {
        transform.Translate(Vector2.down * 5f * Time.deltaTime);
    }
}
