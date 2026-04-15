using UnityEngine;

public class ExplosionGreatBall : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 2.1f);
    }
}
