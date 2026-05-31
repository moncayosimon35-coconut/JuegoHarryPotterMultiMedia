using UnityEngine;

public class ExtraVida : MonoBehaviour
{
    public int cantidadVida = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            VidaHarry vida = other.GetComponent<VidaHarry>();

            if (vida != null)
            {
                vida.vida += cantidadVida;
                vida.ActualizarUI();
            }

            Destroy(gameObject);
        }
    }
}
