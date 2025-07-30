using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public List<PlayerInputFrame> replayInputs;
    public float moveSpeed = 5f;
    public float rotateSpeed = 720f;

    private Rigidbody rb;
    private int currentFrame = 0;
    private bool isReplaying = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

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

    void FixedUpdate()
    {
        if (!isReplaying || replayInputs == null || currentFrame >= replayInputs.Count)
        {
            isReplaying = false;
            return;
        }

        PlayerInputFrame input = replayInputs[currentFrame];

        Vector3 inputDir = new Vector3(input.moveInput.x, 0, input.moveInput.y).normalized;
        Vector3 move = inputDir * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);

        if (inputDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(inputDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.fixedDeltaTime);
        }

        currentFrame++;
    }
}
