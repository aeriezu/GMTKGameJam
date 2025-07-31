using UnityEngine;

public class InstantKillEnemy : BaseEnemy
{
    private void Update()
    {
        if (isPlayerInRange)
        {
            Debug.Log("Player caught! Restarting...");
            // trigger restart or lose condition
        }
    }
}
