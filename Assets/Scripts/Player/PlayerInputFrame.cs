using UnityEngine;

[System.Serializable]
public class PlayerInputFrame
{
    public Vector2 moveInput;
    public bool interactPressed;
    public bool attackPressed;

    public PlayerInputFrame(Vector2 move, bool interact, bool attack)
    {
        moveInput = move;
        interactPressed = interact;
        attackPressed = attack;
    }
}
