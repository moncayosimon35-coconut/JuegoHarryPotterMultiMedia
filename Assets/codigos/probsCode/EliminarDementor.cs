using UnityEngine;

public class EliminarDementor : MonoBehaviour
{
    public GameObject dementor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (dementor != null)
            {
                Destroy(dementor);
            }
        }
    }
}