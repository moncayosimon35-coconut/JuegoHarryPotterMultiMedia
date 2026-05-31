using UnityEngine;

public class BoostVelocidad : MonoBehaviour
{
    public float velocidadExtra = 3f;
    public float duracion = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Movimientoharry mov = other.GetComponent<Movimientoharry>();

            if (mov != null)
            {
                mov.AplicarBoostVelocidad(velocidadExtra, duracion);
            }

            Destroy(gameObject);
        }
    }
}