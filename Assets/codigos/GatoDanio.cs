using UnityEngine;

public class GatoDanio : MonoBehaviour
{
    [Header("Cooldown para no quitar vidas seguidas")]
    public float cooldownDanio = 1.5f;
    private float tiempoUltimoDanio = -999f;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (Time.time - tiempoUltimoDanio < cooldownDanio) return;

        if (other.CompareTag("Player"))
        {
            tiempoUltimoDanio = Time.time;
            VidaJugador.instancia?.PerderVida();
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        if (Time.time - tiempoUltimoDanio < cooldownDanio) return;

        if (other.gameObject.CompareTag("Player"))
        {
            tiempoUltimoDanio = Time.time;
            VidaJugador.instancia?.PerderVida();
        }
    }
}