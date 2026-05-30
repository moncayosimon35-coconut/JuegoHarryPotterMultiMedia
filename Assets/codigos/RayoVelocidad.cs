using UnityEngine;

public class RayoVelocidad : MonoBehaviour
{
    [Header("Configuración del boost")]
    public float multiplicadorVelocidad = 2f;  
    public float duracionBoost = 3f;            

    [Header("Opcional")]
    public bool desaparecerAlTocar = false;      
    public float tiempoReaparicion = 5f;       

    private bool enCooldown = false;

    private void OnTriggerEnter(Collider other)
    {
        if (enCooldown) return;

        Movimientoharry harry = other.GetComponent<Movimientoharry>();
        if (harry != null)
        {
            harry.AplicarBoost(multiplicadorVelocidad, duracionBoost);

            if (desaparecerAlTocar)
                StartCoroutine(CooldownRayo());
        }
    }

    private System.Collections.IEnumerator CooldownRayo()
    {
        enCooldown = true;
        GetComponent<Renderer>().enabled = false;   
        GetComponent<Collider>().enabled  = false;  

        yield return new WaitForSeconds(tiempoReaparicion);

        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider>().enabled  = true;
        enCooldown = false;
    }
}