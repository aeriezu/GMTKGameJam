using UnityEngine;
using UnityEngine.Events;

public class TimeLoopManager : MonoBehaviour
{
    public float startingTime = 10f;
    public float currentTime;
    public UnityEvent OnLoopTriggered;

    private bool isLooping = false;

    void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        if (isLooping) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            TriggerLoop();
        }
    }

    public void AddTime(float amount)
    {
        currentTime += amount;
    }

    private void TriggerLoop()
    {
        isLooping = true;

        Debug.Log("Loop triggered!");

        // Call any hooked-up rewind/ghost systems
        OnLoopTriggered?.Invoke();

        // Add delay, effects, or reset logic here
        Invoke(nameof(ResetLoop), 0.1f); // Delay for visual sync
    }

    private void ResetLoop()
    {
        currentTime = startingTime;
        isLooping = false;
    }
}