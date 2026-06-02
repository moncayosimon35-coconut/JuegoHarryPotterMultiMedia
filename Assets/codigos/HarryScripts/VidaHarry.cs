using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VidaHarry : MonoBehaviour
{
    public int vida = 3;

    public GameObject corazon1;
    public GameObject corazon2;
    public GameObject corazon3;

    private bool puedeRecibirDanio = true;
    public float tiempoInvulnerable = 1f;

    public void RecibirDanio(int cantidad)
    {
        if (!puedeRecibirDanio) return;

        vida -= cantidad;

        ActualizarUI();

        if (vida <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            StartCoroutine(Invulnerabilidad());
        }
    }

    public void ActualizarUI()
    {
        
corazon1.SetActive(vida >= 1);
    corazon2.SetActive(vida >= 2);
    corazon3.SetActive(vida >= 3);

    }

    System.Collections.IEnumerator Invulnerabilidad()
    {
        puedeRecibirDanio = false;
        yield return new WaitForSeconds(tiempoInvulnerable);
        puedeRecibirDanio = true;
    }
}