using UnityEngine;

public class BoostSalto : MonoBehaviour
{
    public float saltoExtra = 5f;
    public float duracion = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Movimientoharry mov = other.GetComponent<Movimientoharry>();

            if (mov != null)
            {
                StartCoroutine(AumentarSalto(mov));
            }

            Destroy(gameObject);
        }
    }

    System.Collections.IEnumerator AumentarSalto(Movimientoharry mov)
    {
        mov.jumpForce += saltoExtra;

        yield return new WaitForSeconds(duracion);

        mov.jumpForce -= saltoExtra;
    }
}
