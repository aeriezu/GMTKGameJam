using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;              // Speed of grid movement
    public LayerMask obstacleMask;            // Optional: block movement if something is in the way

    private bool isMoving = false;
    private Vector2 moveDirection;
    private Vector3 targetPosition;

    [HideInInspector]
    public List<PlayerInputFrame> recordedInputs = new List<PlayerInputFrame>();

    [HideInInspector]
    public Vector3 startPosition;
    [HideInInspector]
    public Quaternion startRotation;

    void Start()
    {
        SaveStartPoint();
        targetPosition = transform.position;
    }

    void Update()
    {
        if (isMoving) return;

        // Get input
        float x = Input.GetKeyDown(KeyCode.RightArrow) ? 1 :
                  Input.GetKeyDown(KeyCode.LeftArrow)  ? -1 : 0;

        float y = Input.GetKeyDown(KeyCode.UpArrow)    ? 1 :
                  Input.GetKeyDown(KeyCode.DownArrow)  ? -1 : 0;

        moveDirection = new Vector2(x, y);

        if (moveDirection != Vector2.zero)
        {
            Vector3 destination = transform.position + (Vector3)moveDirection;

            // Optional: block movement if something is in the way
            if (!Physics2D.OverlapCircle(destination, 0.1f, obstacleMask))
            {
                recordedInputs.Add(new PlayerInputFrame(moveDirection));
                StartCoroutine(MoveToPosition(destination));
            }
        }
    }

    private System.Collections.IEnumerator MoveToPosition(Vector3 destination)
    {
        isMoving = true;
        while ((destination - transform.position).sqrMagnitude > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = destination;
        isMoving = false;
    }

    public void SaveStartPoint()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    public void ClearRecording()
    {
        recordedInputs.Clear();
    }
}
