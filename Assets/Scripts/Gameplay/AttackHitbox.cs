using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject); // or call a TakeDamage() function

            // Add time when enemy is hit
            TimeLoopManager loopManager = FindFirstObjectByType<TimeLoopManager>();
            if (loopManager != null)
            {
                loopManager.AddTime(2f); // Adds 2 seconds for example
            }
        }
    }
}
