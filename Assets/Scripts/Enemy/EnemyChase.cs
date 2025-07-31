using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform player;
    public float chaseRange = 5f;
    public float speed = 3f;

    private void Update()
    {
        float dist = Vector2.Distance(transform.position, player.position);
        if (dist < chaseRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
}
