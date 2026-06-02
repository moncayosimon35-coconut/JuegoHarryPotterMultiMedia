using UnityEngine;

public class SalirJuego : MonoBehaviour
{
    public void Salir()
    {
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}