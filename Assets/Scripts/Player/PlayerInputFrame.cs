using UnityEngine;

[System.Serializable]
public class PlayerInputFrame
{
    public Vector2 moveInput;

    public PlayerInputFrame(Vector2 moveInput)
    {
        this.moveInput = moveInput;
    }
}
