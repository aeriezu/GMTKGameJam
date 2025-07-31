using UnityEngine;

public class EnemyReact : MonoBehaviour
{
    public Transform player;
    public Transform ghost;
    public float detectionRange = 5f;

    private Transform currentTarget;

    private void Update()
    {
        float ghostDist = Vector2.Distance(transform.position, ghost.position);
        float playerDist = Vector2.Distance(transform.position, player.position);

        if (ghostDist < detectionRange)
            currentTarget = ghost;
        else if (playerDist < detectionRange)
            currentTarget = player;
        else
            currentTarget = null;

        if (currentTarget != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, 2f * Time.deltaTime);
        }
    }
}
