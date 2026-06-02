
using UnityEngine;
using UnityEngine.SceneManagement;

public class GanarAlTocar : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Victoria");
        }
    }
}
