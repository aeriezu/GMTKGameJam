using UnityEngine;
using System.Collections;

public class GridPatrolEnemy : BaseEnemy
{
    [Tooltip("Direction to patrol in grid units per step (e.g., (1,0) for right, (0,1) for up)")]
    public Vector2 patrolDirection = Vector2.right;

    [Tooltip("Number of steps to patrol before turning")]
    public int patrolDistance = 3;

    [Tooltip("Time in seconds to move between tiles")]
    public float moveTime = 0.3f;

    private Vector2 startPos;
    private int direction = 1;
    private int stepsMoved = 0;
    private bool isMoving = false;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if (!isMoving)
        {
            StartCoroutine(PatrolStep());
        }

        if (isPlayerInRange)
        {
            Debug.Log("Player touched patrolling enemy! You were caught.");
            // Trigger lose condition here (e.g., UI popup or restart scene)
        }
    }

    private IEnumerator PatrolStep()
    {
        isMoving = true;

        if (stepsMoved >= patrolDistance)
        {
            direction *= -1;
            stepsMoved = 0;
        }

        Vector2 nextPos = (Vector2)transform.position + patrolDirection * direction;
        Vector2 start = transform.position;

        float elapsed = 0f;
        while (elapsed < moveTime)
        {
            transform.position = Vector2.Lerp(start, nextPos, elapsed / moveTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = nextPos;
        stepsMoved++;
        isMoving = false;
    }

    public override void OnPlayerEnter(Transform playerTransform)
    {
        base.OnPlayerEnter(playerTransform);
        // could also directly trigger the lose condition here if desired
        // e.g., GameManager.Instance.PlayerCaught();
    }

    public override void OnPlayerExit()
    {
        base.OnPlayerExit();
    }
}
