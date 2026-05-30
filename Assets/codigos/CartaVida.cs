using UnityEngine;

public class CartaVida : MonoBehaviour
{
    [Header("Configuración")]
    public int vidasQueOtorga = 1;
    public bool desaparecerAlRecoger = true;
    public float tiempoReaparicion = 10f; // 0 = no reaparece

    private bool recogida = false;

    private void OnTriggerEnter(Collider other)
    {
        if (recogida) return;

        if (other.CompareTag("Player"))
        {
            recogida = true;
            VidaJugador.instancia?.AgregarVida(vidasQueOtorga);

            if (desaparecerAlRecoger)
            {
                if (tiempoReaparicion > 0)
                    StartCoroutine(CooldownCarta());
                else
                    gameObject.SetActive(false);
            }
        }
    }

    private System.Collections.IEnumerator CooldownCarta()
    {
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(tiempoReaparicion);

        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
        recogida = false;
    }
}