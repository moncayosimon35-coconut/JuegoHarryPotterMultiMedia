using UnityEngine;
using UnityEngine.UI;

public class UIVidas : MonoBehaviour
{
    public static UIVidas instancia;

    [Header("Iconos de vida (arrastra 3 Image de la UI)")]
    public Image[] iconosVida;        

    [Header("Sprites")]
    public Sprite vidaLlena;
    public Sprite vidaVacia;

    private void Awake()
    {
        if (instancia == null) instancia = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        
        if (VidaJugador.instancia != null)
            ActualizarVidas(VidaJugador.instancia.GetVidas());
    }

    public void ActualizarVidas(int vidasActuales)
    {
        for (int i = 0; i < iconosVida.Length; i++)
        {
            if (iconosVida[i] != null)
                iconosVida[i].sprite = i < vidasActuales ? vidaLlena : vidaVacia;
        }
    }
}