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

    private float boostMultiplier = 1f;
    private float boostTimer = 0f;

    private bool dobleSaltoActivo = false;
    private float tiempoDobleSalto = 0f;
    private bool yaHizoDobleSalto = false;
    private bool estabaEnSuelo = false;

    
    [Header("UI Doble Salto (opcional)")]
    public GameObject indicadorDobleSalto; 

    public void AplicarBoost(float multiplicador, float duracion)
    {
        boostMultiplier = multiplicador;
        boostTimer = duracion;
    }

    public void ActivarDobleSalto(float duracion)
    {
        dobleSaltoActivo = true;
        tiempoDobleSalto = duracion;
        yaHizoDobleSalto = false;
        if (indicadorDobleSalto != null)
            indicadorDobleSalto.SetActive(true);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        if (indicadorDobleSalto != null)
            indicadorDobleSalto.SetActive(false);
    }

    void Update()
    {
        
        if (boostTimer > 0f)
        {
            boostTimer -= Time.deltaTime;
            if (boostTimer <= 0f)
                boostMultiplier = 1f;
        }

        
        if (dobleSaltoActivo)
        {
            tiempoDobleSalto -= Time.deltaTime;
            if (tiempoDobleSalto <= 0f)
            {
                dobleSaltoActivo = false;
                if (indicadorDobleSalto != null)
                    indicadorDobleSalto.SetActive(false);
            }
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontal, 0f, vertical);
        if (move.magnitude > 1f) move = move.normalized;

        bool tocandoSuelo = controller != null && (controller.isGrounded || estaEnSueloCorrer);

        if (tocandoSuelo && velocity.y <= 0f)
        {
            velocity.y = -2f;

            if (!estabaEnSuelo)
            {
                estabaEnSuelo = true;
                yaHizoDobleSalto = false;
            }

            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = jumpForce;
                estabaEnSuelo = false;
                estaEnSueloCorrer = false;
                tocandoSuelo = false;
                if (animator != null)
                    animator.Play(nombreAnimacionSalto, -1, 0f);
            }
        }
        else
        {
            estabaEnSuelo = false;
            velocity.y -= gravity * Time.deltaTime;

            if (Input.GetButtonDown("Jump") && dobleSaltoActivo && !yaHizoDobleSalto)
            {
                velocity.y = jumpForce;
                yaHizoDobleSalto = true;
                if (animator != null)
                    animator.Play(nombreAnimacionSalto, -1, 0f);
            }
        }

        if (controller != null)
        {
            Vector3 finalMove = (move * speed * boostMultiplier) + Vector3.up * velocity.y;
            controller.Move(finalMove * Time.deltaTime);
        }
        else
        {
            transform.Translate(move * speed * boostMultiplier * Time.deltaTime, Space.World);
        }

        if (move != Vector3.zero) transform.forward = move;

        bool quiereMoverse = (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f);

        if (animator != null)
        {
            if (!tocandoSuelo && controller != null)
                animator.Play(nombreAnimacionSalto, -1, 0f);
            else if (quiereMoverse)
                animator.Play("Running", -1, 0f);
            else
                animator.Play("Idle", -1, 0f);
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (velocity.y > 0f) { estaEnSueloCorrer = false; return; }

        if (hit.gameObject.CompareTag("Suelocorrer"))
        {
            if (hit.normal.y > 0.7f) { estaEnSueloCorrer = true; return; }
        }
        estaEnSueloCorrer = false;
    }
}