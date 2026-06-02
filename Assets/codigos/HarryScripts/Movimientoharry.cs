using UnityEngine;

public class Movimientoharry : MonoBehaviour
{
    [HideInInspector] public float velocidadActual;
    [HideInInspector] public bool puedeSaltar = true;
    public float speed = 5f;
    public float gravity = 20f;
    public float jumpForce = 8f;
    public string nombreAnimacionSalto = "Jump";
    
public bool enSuelo;
public bool enMovimiento;


    
    private CharacterController controller;
    private Vector3 velocity;

    //boost de los probs
    
private Coroutine velocidadRutina;
private Coroutine saltoRutina;

private float velocidadExtraActual = 0f;
private float saltoExtraActual = 0f;

    [SerializeField] private bool estaEnSueloCorrer = false;

    void Start()
    {
        velocidadActual = speed;
        
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
        enSuelo = tocandoSuelo;

        if (tocandoSuelo && velocity.y <= 0f)
        {
            velocity.y = -8f;

            if (Input.GetButtonDown("Jump") && puedeSaltar)
            {
                velocity.y = jumpForce;
                estaEnSueloCorrer = false;
                tocandoSuelo = false;
                
                
            }
        }
        else
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        
if (!controller.isGrounded && velocity.y > -5f)
{
    velocity.y -= 30f * Time.deltaTime;
}


        if (controller != null && controller.enabled)
        {
            Vector3 finalMove = move * velocidadActual;
finalMove.y = velocity.y;

controller.Move(finalMove * Time.deltaTime);
        }
        else
        {
            transform.Translate(move * speed * Time.deltaTime, Space.World);
        }

        if (move != Vector3.zero)
            transform.forward = move;

        bool quiereMoverse = (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f);
        enMovimiento = quiereMoverse;

        
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

    
public void AplicarBoostVelocidad(float extra, float duracion)
{
    if (velocidadRutina != null)
    {
        StopCoroutine(velocidadRutina);
        velocidadActual -= velocidadExtraActual;
    }

    velocidadRutina = StartCoroutine(BoostVelocidad(extra, duracion));
}

System.Collections.IEnumerator BoostVelocidad(float extra, float duracion)
{
    velocidadExtraActual = extra;

    velocidadActual += extra;

    yield return new WaitForSeconds(duracion);

    velocidadActual -= extra;

    velocidadRutina = null;
    velocidadExtraActual = 0f;
}

public void AplicarBoostSalto(float extra, float duracion)
{
    if (saltoRutina != null)
    {
        StopCoroutine(saltoRutina);
        jumpForce -= saltoExtraActual;
    }

    saltoRutina = StartCoroutine(BoostSalto(extra, duracion));
}

System.Collections.IEnumerator BoostSalto(float extra, float duracion)
{
    saltoExtraActual = extra;

    jumpForce += extra;

    yield return new WaitForSeconds(duracion);

    jumpForce -= extra;

    saltoRutina = null;
    saltoExtraActual = 0f;
}

}
