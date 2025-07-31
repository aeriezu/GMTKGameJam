using UnityEngine;

public class Enemy : Objective
{
    public void Kill()
    {
        CompleteObjective();
    }

    public override void CompleteObjective()
    {
        GrantTime();
        Destroy(gameObject); // or trigger death animation
    }
}
