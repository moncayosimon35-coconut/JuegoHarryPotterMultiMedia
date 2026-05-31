using UnityEngine;

public class DanioGato : MonoBehaviour
{
    public int danio = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            VidaHarry vida = other.GetComponent<VidaHarry>();

            if (vida != null)
            {
                vida.RecibirDanio(danio);
            }
        }
    }
}