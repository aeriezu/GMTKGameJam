using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackArea;
    public float attackDuration = 0.2f;

    private bool isAttacking = false;
    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Attack.performed += _ => PerformAttack();
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    private void PerformAttack()
    {
        if (isAttacking) return;

        StartCoroutine(DoAttack());
    }

    private System.Collections.IEnumerator DoAttack()
    {
        isAttacking = true;
        attackArea.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        attackArea.SetActive(false);
        isAttacking = false;
    }
}
