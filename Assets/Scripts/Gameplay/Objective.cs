using UnityEngine;

public abstract class Objective : MonoBehaviour
{
    public float timeReward = 3f;

    public abstract void CompleteObjective();

    protected void GrantTime()
    {
        TimeLoopManager timeLoop = FindFirstObjectByType<TimeLoopManager>();
        if (timeLoop != null)
        {
            timeLoop.AddTime(timeReward);
        }
    }
}
