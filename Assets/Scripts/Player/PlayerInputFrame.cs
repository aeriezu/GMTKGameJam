using UnityEngine;

[System.Serializable]
public class PlayerInputFrame
{
    public Vector2 moveInput;
    public bool isMoving;
    public bool flipX;

    public PlayerInputFrame(Vector2 move, bool moving, bool flip)
    {
        moveInput = move;
        isMoving = moving;
        flipX = flip;
    }
}
