using UnityEngine;

public class LimitsArena : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            other.transform.position = respawnPoint.position;
        }
    }
}
