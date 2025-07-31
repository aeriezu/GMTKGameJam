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

    public GameObject attackArea;
    public float attackDuration = 0.2f;
    private bool isAttacking = false;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => input = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => input = Vector2.zero;
        controls.Player.Attack.performed += _ => TriggerMeow();

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    private void Start()
    {
        // Snap to nearest tile
        Vector3 pos = transform.position;
        transform.position = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), pos.z);
    }

    private void Update()
    {
        if (isMoving || isAttacking) return;

        // Optional: Restrict diagonal movement
        if (input.x != 0) input.y = 0;

        if (input != Vector2.zero)
        {
            animator.SetBool("IsMoving", true);

            // Flip sprite and set attack area position
            if (input.x > 0)
            {
                spriteRenderer.flipX = false;
                attackArea.transform.localPosition = new Vector3(
                    Mathf.Abs(attackArea.transform.localPosition.x),
                    attackArea.transform.localPosition.y
                );
            }
            else if (input.x < 0)
            {
                spriteRenderer.flipX = true;
                attackArea.transform.localPosition = new Vector3(
                    -Mathf.Abs(attackArea.transform.localPosition.x),
                    attackArea.transform.localPosition.y
                );
            }

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

    private void TriggerMeow()
    {
        if (!isAttacking)
            StartCoroutine(MeowAction());
    }

    private IEnumerator MeowAction()
    {
        isAttacking = true;
        animator.SetTrigger("Meow");

        // Optional: play meow sound
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null) audio.Play();

        // Optional: Enable attack hitbox (like scratching)
        attackArea.SetActive(true);

        yield return new WaitForSeconds(attackDuration);

        attackArea.SetActive(false);
        isAttacking = false;
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
