
using UnityEngine;
using UnityEngine.SceneManagement;

public class MuerteSuicidio : MonoBehaviour
{
    public GameObject dementor;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject == dementor)
        {
            SceneManager.LoadScene("GameOver"); 
        }
    }
}
