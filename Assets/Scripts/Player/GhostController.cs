using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public List<PlayerInputFrame> replayInputs;
    public float moveDelay = 0.2f;
    public float gridSize = 1f;

    private int currentFrame = 0;
    private bool isReplaying = false;

    public void StartReplay(List<PlayerInputFrame> inputs)
    {
        if (inputs == null || inputs.Count == 0)
        {
            Debug.LogWarning("No inputs to replay.");
            return;
        }

        replayInputs = inputs;
        currentFrame = 0;
        isReplaying = true;

        StartCoroutine(ReplayMovement());
    }

    private IEnumerator ReplayMovement()
    {
        while (isReplaying && currentFrame < replayInputs.Count)
        {
            Vector2 dir = replayInputs[currentFrame].moveInput;

            if (dir != Vector2.zero)
            {
                Vector3 targetPos = transform.position + (Vector3)(dir.normalized * gridSize);
                transform.position = targetPos;
            }

            currentFrame++;
            yield return new WaitForSeconds(moveDelay);
        }

        isReplaying = false;
    }
}
