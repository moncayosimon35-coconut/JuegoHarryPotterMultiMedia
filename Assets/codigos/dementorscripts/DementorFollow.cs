using UnityEngine;

using UnityEngine.SceneManagement;


public class DementorFollow : MonoBehaviour
{
    public Transform objetivo;         // Harry
    public Transform meta;             // Plataforma final

    public float velocidad = 2f;       // inicio lento
    public float velocidadMax = 7f;
    public float aumentoVelocidad = 0.3f;

    public float gravedad = 20f;

    private CharacterController controller;
    private Vector3 velocidadY;

    // Detectar si se queda atascado
    private Vector3 posicionAnterior;
    private float tiempoSinMoverse = 0f;

    public float tiempoAntesDeVolar = 1.2f;
    public float fuerzaVolar = 6f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (objetivo == null) return;

        
float distancia = Vector3.Distance(transform.position, objetivo.position);

// 🔥 SI TOCA → MUERE
if (distancia < 0.9f)
{
    UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    return;
}


        // --------- DETECTAR SI ESTÁ ATASCADO ----------
        float distanciaMovida = Vector3.Distance(transform.position, posicionAnterior);

        if (distanciaMovida < 0.05f || controller.velocity.magnitude < 0.1f)
        {
            tiempoSinMoverse += Time.deltaTime;
        }
        else
        {
            tiempoSinMoverse = 0f;
        }

        posicionAnterior = transform.position;

        // --------- ACELERAR CON EL TIEMPO ----------
        if (velocidad < velocidadMax)
        {
            velocidad += aumentoVelocidad * Time.deltaTime;
        }

        // --------- ACELERAR CERCA DE LA META ----------
        if (meta != null)
        {
            float distanciaMeta = Vector3.Distance(transform.position, meta.position);

            if (distanciaMeta < 10f)
            {
                velocidad += 2f * Time.deltaTime;
            }
        }

        // --------- DIRECCIÓN HACIA HARRY ----------
        
Vector3 direccion = objetivo.position - transform.position;

// 🔥 IMPORTANTE
direccion.y = 0f;

// evitar que intente bajarse encima
if (controller.isGrounded == false)
{
    direccion.y = 0f;
}

        





        Vector3 movimiento = direccion.normalized * velocidad;

        // --------- SI ESTÁ ATASCADO → SALTAR / VOLAR ----------
        
if (tiempoSinMoverse > tiempoAntesDeVolar)
{
    // Subir un poco
    velocidadY.y = fuerzaVolar;

    // Mover hacia adelante
    Vector3 forward = direccion.normalized;

    // 🔥 ESCAPE LATERAL (izquierda o derecha)
    Vector3 lateral = transform.right * Random.Range(-1f, 1f);

    // Combinar movimientos
    Vector3 escape = (forward + lateral).normalized * velocidad * 3f;

    controller.Move((escape + velocidadY) * Time.deltaTime);

    return;
}


        // --------- GRAVEDAD NORMAL ----------
        if (controller.isGrounded)
        {
            velocidadY.y = -2f;
        }
        else
        {
            velocidadY.y -= gravedad * Time.deltaTime;
        }

        // --------- MOVIMIENTO FINAL ----------
        Vector3 movimientoFinal = movimiento + velocidadY;

        controller.Move(movimientoFinal * Time.deltaTime);

        
        


    }
}
