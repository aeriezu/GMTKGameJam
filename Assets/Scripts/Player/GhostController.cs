using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public List<PlayerInputFrame> replayInputs;
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private int currentFrame = 0;
    private bool isReplaying = false;

    private void Awake() => rb = GetComponent<Rigidbody2D>();

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
    }

    private void FixedUpdate()
    {
        if (!isReplaying || currentFrame >= replayInputs.Count)
        {
            isReplaying = false;
            return;
        }

        PlayerInputFrame input = replayInputs[currentFrame];
        Vector2 move = input.moveInput * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);

        currentFrame++;
    }
}
