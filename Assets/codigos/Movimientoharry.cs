using UnityEngine;

public class Movimientoharry : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = 20f;

    private Animator animator;
    private CharacterController controller;
    private Vector3 velocity;
    [SerializeField] private bool estaEnSueloCorrer = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 1. Cálculo de dirección
        Vector3 move = new Vector3(horizontal, 0f, vertical);

        if (move.magnitude > 1f)
            move = move.normalized;

        // 2. Control Físico del Character Controller
        if (controller != null)
        {
            if (controller.isGrounded || estaEnSueloCorrer)
            {
                velocity.y = -2f; 
            }
            else
            {
                velocity.y -= gravity * Time.deltaTime;
            }

            Vector3 finalMove = (move * speed) + Vector3.up * velocity.y;
            controller.Move(finalMove * Time.deltaTime);
        }
        else
        {
            transform.Translate(move * speed * Time.deltaTime, Space.World);
        }

        // 3. Rotación del personaje
        if (move != Vector3.zero)
            transform.forward = move;

        // 4. SOLUCIÓN FEROZ: Control Directo de Animación (Sin pasar por parámetros ni flechas)
        bool quiereMoverse = (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f);

        if (animator != null)
        {
            if (quiereMoverse)
            {
                // Fuerza al Animator a reproducir "Running" en el estado actual de forma fluida
                animator.Play("Running"); 
            }
            else
            {
                // Regresa a "Idle" de inmediato si se detiene
                animator.Play("Idle");
            }
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Suelocorrer"))
        {
            if (hit.normal.y > 0.7f) 
            {
                estaEnSueloCorrer = true;
                return;
            }
        }
        
        estaEnSueloCorrer = false;
    }
}
