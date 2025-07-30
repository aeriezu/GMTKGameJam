using UnityEngine;

public class GridSnapCamera : MonoBehaviour
{
    public Transform target;
    public float gridSize = 1f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 pos = target.position;
        Vector3 snapped = new Vector3(
            Mathf.Round(pos.x / gridSize) * gridSize,
            Mathf.Round(pos.y / gridSize) * gridSize,
            transform.position.z
        );

        transform.position = snapped;
    }
}
