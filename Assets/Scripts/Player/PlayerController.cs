using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveTime = 0.2f;

    private Vector2 input;
    private bool isMoving = false;

    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => input = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => input = Vector2.zero;
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    private void Update()
    {
        if (isMoving) return;

        if (input != Vector2.zero)
        {
            // Lock input to cardinal directions only
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
                input.y = 0;
            else
                input.x = 0;

            Vector3 targetPos = transform.position + new Vector3(Mathf.Round(input.x), Mathf.Round(input.y), 0);
            StartCoroutine(MoveToPosition(targetPos));
        }
    }

    private System.Collections.IEnumerator MoveToPosition(Vector3 target)
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

        // Snap precisely to target tile
        transform.position = new Vector3(Mathf.Round(target.x), Mathf.Round(target.y), target.z);

        isMoving = false;
    }

    private void Start()
    {
        // Snap player to grid on start
        Vector3 pos = transform.position;
        transform.position = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), pos.z);
    }
}
