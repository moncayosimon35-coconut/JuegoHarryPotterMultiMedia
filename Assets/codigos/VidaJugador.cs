using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaJugador : MonoBehaviour
{
    public static VidaJugador instancia;

    [Header("Configuración")]
    public int vidasMaximas = 3;
    private int vidasActuales;

   
    private bool esInmune = false;
    public bool EsInmune => esInmune;

    [Header("Efecto inmunidad")]
    public ParticleSystem efectoInmunidad;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        vidasActuales = vidasMaximas;
    }

    public void PerderVida()
    {
        if (esInmune) return;

        vidasActuales--;
        ActualizarUI();

        if (vidasActuales <= 0)
            GameOver();
    }

    public void ActivarInmunidad(float duracion)
    {
        StartCoroutine(InmunidadCoroutine(duracion));
    }

    private System.Collections.IEnumerator InmunidadCoroutine(float duracion)
    {
        esInmune = true;

        if (efectoInmunidad != null)
            efectoInmunidad.Play();

        yield return new WaitForSeconds(duracion);

        esInmune = false;

        if (efectoInmunidad != null)
            efectoInmunidad.Stop();
    }

    public void AgregarVida(int cantidad = 1)
    {
        vidasActuales = Mathf.Min(vidasActuales + cantidad, vidasMaximas);
        ActualizarUI();
    }

    private void ActualizarUI()
    {
        if (UIVidas.instancia != null)
            UIVidas.instancia.ActualizarVidas(vidasActuales);
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public int GetVidas() => vidasActuales;
}