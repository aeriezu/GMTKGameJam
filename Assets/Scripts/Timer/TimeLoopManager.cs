using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class TimeLoopManager : MonoBehaviour
{
    public float startingTime = 10f;
    public float currentTime;
    public UnityEvent OnLoopTriggered;

    private bool isLooping = false;

    [Header("References")]
    public GameObject ghostPrefab;
    public Transform playerSpawnPoint;
    public GameObject player;

    void Start()
    {
        currentTime = startingTime;
        player.GetComponent<TopDownPlayerController>().SaveStartPoint();
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

        // Spawn ghost
        SpawnGhost();

        // Call any other systems
        OnLoopTriggered?.Invoke();

        // Reset the loop shortly after for sync
        Invoke(nameof(ResetLoop), 0.1f);
    }

    private void SpawnGhost()
    {
        var playerController = player.GetComponent<TopDownPlayerController>();
        if (playerController == null) return;

        // Instantiate the ghost
        Vector3 spawnPos = playerController.startPosition;
        Quaternion spawnRot = playerController.startRotation;
        GameObject ghost = Instantiate(ghostPrefab, spawnPos, spawnRot);

        // Pass the recorded inputs
        GhostController ghostController = ghost.GetComponent<GhostController>();
        if (ghostController != null)
        {
            ghostController.StartReplay(new List<PlayerInputFrame>(playerController.recordedInputs));
        }

        // Reset the player position
        player.transform.position = playerSpawnPoint.position;
        player.transform.rotation = playerSpawnPoint.rotation;

        // Clear input history
        playerController.ClearRecording();
    }

    private void ResetLoop()
    {
        currentTime = startingTime;
        isLooping = false;

        // Save new starting point for next ghost
        player.GetComponent<TopDownPlayerController>().SaveStartPoint();
    }

}
