using UnityEngine;

public class SwitchObjective : Objective
{
    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!activated && other.CompareTag("Player"))
        {
            activated = true;
            CompleteObjective();
        }
    }

    public override void CompleteObjective()
    {
        GrantTime();
        // You could change sprite or play an animation here
        Debug.Log("Switch activated!");
    }
}
