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
        player.GetComponent<PlayerController>().SaveStartPoint();
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

        SpawnGhost();

        OnLoopTriggered?.Invoke();

        Invoke(nameof(ResetLoop), 0.1f);
    }

    private void SpawnGhost()
    {
        var playerController = player.GetComponent<PlayerController>();
        if (playerController == null) return;

        // Get the saved start position and force z to -0.1f so ghost is in front
        Vector3 spawnPos = playerController.startPosition;
        spawnPos.z = -0.1f;  // Set Z to -0.1 here explicitly
        
        // Use default rotation or identity if you want
        Quaternion spawnRot = Quaternion.identity;

        GameObject ghost = Instantiate(ghostPrefab, spawnPos, spawnRot);

        GhostController ghostController = ghost.GetComponent<GhostController>();
        if (ghostController != null)
        {
            ghostController.StartReplay(new List<PlayerInputFrame>(playerController.recordedInputs));
        }

        // Reset the player position and rotation to start
        player.transform.position = playerController.startPosition;
        player.transform.rotation = spawnRot;

        playerController.ClearRecording();
    }


    private void ResetLoop()
    {
        currentTime = startingTime;
        isLooping = false;

        player.GetComponent<PlayerController>().SaveStartPoint();
    }
}
