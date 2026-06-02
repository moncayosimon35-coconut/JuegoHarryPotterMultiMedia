using UnityEngine;

public class AnimacionHarry : MonoBehaviour
{
    public Movimientoharry movimiento;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (movimiento == null) return;

        if (!movimiento.enSuelo)
        {
            animator.Play("Jump");
        }
        else if (movimiento.enMovimiento)
        {
            animator.Play("Running");
        }
        else
        {
            animator.Play("Idle");
        }
    }
}
