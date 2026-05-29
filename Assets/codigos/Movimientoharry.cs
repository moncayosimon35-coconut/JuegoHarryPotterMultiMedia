using UnityEngine;

public class Movimientoharry : MonoBehaviour
{
    public float speed = 5f;

    private Animator animator;
    private CharacterController controller;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontal, 0f, vertical);

        if (move.magnitude > 1f)
            move = move.normalized;

        if (controller != null)
            controller.Move(move * speed * Time.deltaTime);
        else
            transform.Translate(move * speed * Time.deltaTime, Space.World);

        if (move != Vector3.zero)
            transform.forward = move;

        // ✅ BOOL logic
        bool isRunning = move.magnitude > 0.1f;
        animator.SetBool("IsRunning", isRunning);
    }
}