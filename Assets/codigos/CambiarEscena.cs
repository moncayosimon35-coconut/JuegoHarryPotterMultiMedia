using UnityEngine;
using UnityEngine.SceneManagement; // Requisito obligatorio

public class CambiarEscena : MonoBehaviour
{
    // Método público para que el botón lo pueda detectar
    public void CargarNivelJuego()
    {
        SceneManager.LoadScene("NivelJuego");
    }
}
