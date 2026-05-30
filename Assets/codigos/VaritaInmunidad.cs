using UnityEngine;

public class VaritaInmunidad : MonoBehaviour
{
    [Header("Configuración")]
    public float duracionInmunidad = 3f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            VidaJugador.instancia?.ActivarInmunidad(duracionInmunidad);
            gameObject.SetActive(false);
        }
    }
}