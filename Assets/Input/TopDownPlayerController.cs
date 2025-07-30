using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TopDownPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 720f;

    private PlayerControls controls;
    private Vector2 moveInput;
    private bool interactPressed;
    private bool attackPressed;

    private Rigidbody rb;

    // Recording inputs per frame
    public List<PlayerInputFrame> recordedInputs = new List<PlayerInputFrame>();
    public bool isRecording = true;

    public Vector3 startPosition;
    public Quaternion startRotation;

    public void SaveStartPoint()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void Awake()
    {
        controls = new PlayerControls();
        rb = GetComponent<Rigidbody>();

        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        controls.Player.Interact.performed += ctx => interactPressed = true;
        controls.Player.Interact.canceled += ctx => interactPressed = false;

        controls.Player.Attack.performed += ctx => attackPressed = true;
        controls.Player.Attack.canceled += ctx => attackPressed = false;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void FixedUpdate()
    {
        // Record inputs in physics frame
        if (isRecording)
        {
            recordedInputs.Add(new PlayerInputFrame(moveInput, interactPressed, attackPressed));
        }

        Vector3 inputDir = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        Vector3 move = inputDir * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);

        if (inputDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(inputDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.fixedDeltaTime);
        }
    }

    // Call this to clear recorded inputs (e.g. on loop start)
    public void ClearRecording()
    {
        recordedInputs.Clear();
    }

    public List<PlayerInputFrame> GetRecordedInputs()
    {
        // Return a copy to avoid mutation issues
        return new List<PlayerInputFrame>(recordedInputs);
    }
}
