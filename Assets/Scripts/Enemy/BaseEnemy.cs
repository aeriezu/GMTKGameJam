using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BaseEnemy : MonoBehaviour
{
    protected bool isPlayerInRange = false;

    public virtual void OnPlayerEnter(Transform playerTransform)
    {
        isPlayerInRange = true;
    }

    public virtual void OnPlayerExit()
    {
        isPlayerInRange = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerEnter(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerExit();
        }
    }
}
