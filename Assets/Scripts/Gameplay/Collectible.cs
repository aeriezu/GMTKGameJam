using UnityEngine;

public class Collectible : Objective
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CompleteObjective();
        }
    }

    public override void CompleteObjective()
    {
        GrantTime();
        Destroy(gameObject);
    }
}
