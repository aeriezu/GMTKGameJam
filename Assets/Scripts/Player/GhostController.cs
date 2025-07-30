using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public List<PlayerInputFrame> replayInputs;
    public float moveTime = 0.2f;

    private Vector2 startPosition;
    private Vector2 targetPosition;
    private bool isMoving = false;
    private float elapsed = 0f;
    private int currentFrame = 0;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
        targetPosition = startPosition;
    }

    public void StartReplay(List<PlayerInputFrame> inputs)
    {
        replayInputs = inputs;
        currentFrame = 0;
        isMoving = false;
        elapsed = 0f;
        startPosition = transform.position;
        targetPosition = startPosition;
    }

    private void FixedUpdate()
    {
        if (replayInputs == null || currentFrame >= replayInputs.Count)
            return;

        if (isMoving)
        {
            elapsed += Time.fixedDeltaTime;
            Vector2 lerpedPos = Vector2.Lerp(startPosition, targetPosition, elapsed / moveTime);
            transform.position = new Vector3(lerpedPos.x, lerpedPos.y, -0.1f);  // lock Z here

            if (elapsed >= moveTime)
            {
                transform.position = new Vector3(targetPosition.x, targetPosition.y, -0.1f);  // lock Z here too
                isMoving = false;
                elapsed = 0f;
                currentFrame++;
            }
        }
        else
        {
            PlayerInputFrame input = replayInputs[currentFrame];
            Vector2 moveDir = input.moveInput;

            animator.SetBool("IsMoving", input.isMoving);
            spriteRenderer.flipX = input.flipX;

            if (moveDir != Vector2.zero)
            {
                startPosition = transform.position;
                targetPosition = startPosition + moveDir;

                isMoving = true;
                elapsed = 0f;
            }
            else
            {
                currentFrame++;
            }
        }
    }
}
