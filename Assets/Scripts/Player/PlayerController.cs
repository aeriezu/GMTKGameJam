using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveTime = 0.2f;
    private Vector2 input;
    private bool isMoving = false;

    private PlayerControls controls;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public List<PlayerInputFrame> recordedInputs = new List<PlayerInputFrame>();
    public bool isRecording = true;

    public Vector3 startPosition;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => input = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => input = Vector2.zero;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    private void Update()
    {
        if (isMoving) return;

        // Restrict diagonal movement if desired
        if (input.x != 0) input.y = 0;

        if (input != Vector2.zero)
        {
            animator.SetBool("IsMoving", true);

            if (input.x > 0) spriteRenderer.flipX = false;
            else if (input.x < 0) spriteRenderer.flipX = true;

            Vector3 targetPos = transform.position + new Vector3(input.x, input.y, 0);
            StartCoroutine(MoveToPosition(targetPos));
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        if (isRecording)
        {
            recordedInputs.Add(new PlayerInputFrame(input, animator.GetBool("IsMoving"), spriteRenderer.flipX));
        }
    }

    private IEnumerator MoveToPosition(Vector3 target)
    {
        isMoving = true;
        Vector3 startPos = transform.position;
        float elapsed = 0f;

        while (elapsed < moveTime)
        {
            transform.position = Vector3.Lerp(startPos, target, elapsed / moveTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = target;
        isMoving = false;
    }

    private void Start()
    {
        Vector3 pos = transform.position;
        transform.position = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), pos.z);
    }

    public void ClearRecording()
    {
        recordedInputs.Clear();
    }

    public void SaveStartPoint()
    {
        startPosition = transform.position;
    }
}
