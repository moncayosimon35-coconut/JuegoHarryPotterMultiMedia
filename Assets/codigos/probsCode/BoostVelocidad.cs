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
                StartCoroutine(AumentarVelocidad(mov));
            }

            Destroy(gameObject);
        }
    }

    System.Collections.IEnumerator AumentarVelocidad(Movimientoharry mov)
    {
        mov.velocidadActual += velocidadExtra;

        yield return new WaitForSeconds(duracion);

        mov.velocidadActual -= velocidadExtra;
    }
}