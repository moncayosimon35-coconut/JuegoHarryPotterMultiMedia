using UnityEngine;

public class Movimientoharry : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = 20f;
    public float jumpForce = 8f;
    public string nombreAnimacionSalto = "Jump";

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

        Vector3 move = new Vector3(horizontal, 0f, vertical);

        if (move.magnitude > 1f)
            move = move.normalized;

        bool tocandoSuelo = controller != null && (controller.isGrounded || estaEnSueloCorrer);

        if (tocandoSuelo && velocity.y <= 0f)
        {
            velocity.y = -2f;

            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = jumpForce;
                estaEnSueloCorrer = false;
                tocandoSuelo = false;
                
                if (animator != null)
                {
                    animator.Play(nombreAnimacionSalto, 0, 0f);
                }
            }
        }
        else
        {
            velocity.y -= gravity * Time.deltaTime;
        }

        if (controller != null)
        {
            Vector3 finalMove = (move * speed) + Vector3.up * velocity.y;
            controller.Move(finalMove * Time.deltaTime);
        }
        else
        {
            transform.Translate(move * speed * Time.deltaTime, Space.World);
        }

        if (move != Vector3.zero)
            transform.forward = move;

        bool quiereMoverse = (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f);

        if (animator != null)
        {
            if (!tocandoSuelo && controller != null)
            {
                animator.Play(nombreAnimacionSalto);
            }
            else if (quiereMoverse)
            {
                animator.Play("Running"); 
            }
            else
            {
                animator.Play("Idle");
            }
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (velocity.y > 0f) 
        {
            estaEnSueloCorrer = false;
            return;
        }

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
