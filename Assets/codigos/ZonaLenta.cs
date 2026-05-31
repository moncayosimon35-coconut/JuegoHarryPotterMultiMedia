using UnityEngine;

public class ZonaLenta : MonoBehaviour
{
    public float velocidadReducida = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Movimientoharry movimiento = other.GetComponent<Movimientoharry>();

            if (movimiento != null)
            {
                movimiento.velocidadActual = velocidadReducida;
                movimiento.puedeSaltar = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Movimientoharry movimiento = other.GetComponent<Movimientoharry>();

            if (movimiento != null)
            {
                
                movimiento.velocidadActual = movimiento.speed;
                movimiento.puedeSaltar = true;
            }
        }
    }
}
