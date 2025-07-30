using UnityEngine;
using UnityEngine.InputSystem;

public class GridPlayerController : MonoBehaviour
{
    public float moveTime = 0.2f;
    public float gridSize = 1f;
    private bool isMoving = false;
    private Vector2 input;

    private void Update()
    {
        if (!isMoving && input != Vector2.zero)
        {
            StartCoroutine(MovePlayer());
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        if (context.performed)
        {
            input = new Vector2(Mathf.Round(value.x), Mathf.Round(value.y));
        }
        else if (context.canceled)
        {
            input = Vector2.zero;
        }
    }

    private System.Collections.IEnumerator MovePlayer()
    {
        isMoving = true;

        Vector3 startPos = transform.position;
        Vector3 targetPos = startPos + new Vector3(input.x, input.y, 0f) * gridSize;

        float elapsed = 0f;
        while (elapsed < moveTime)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsed / moveTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }
}
