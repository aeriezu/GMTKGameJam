using UnityEngine;

public class TopDownCameraFollow : MonoBehaviour
{
    public Transform target; // Assign the player here
    public Vector3 offset = new Vector3(0f, 15f, 0f);
    public float followSpeed = 5f;

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
    }
}
