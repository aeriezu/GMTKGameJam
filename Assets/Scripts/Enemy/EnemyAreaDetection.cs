using UnityEngine;

public class EnemyDetectionArea : MonoBehaviour
{
    private BaseEnemy enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<BaseEnemy>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy?.OnPlayerEnter(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy?.OnPlayerExit();
        }
    }
}
