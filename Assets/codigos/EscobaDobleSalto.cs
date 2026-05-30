using UnityEngine;

public class EscobaDobleSalto : MonoBehaviour
{
    [Header("Configuración")]
    public float duracionDobleSalto = 2f;
    public float tiempoReaparicion = 10f;

    private bool recogida = false;

    private void OnTriggerEnter(Collider other)
    {
        if (recogida) return;

        if (other.CompareTag("Player"))
        {
            recogida = true;

            Movimientoharry harry = other.GetComponent<Movimientoharry>();
            if (harry != null)
                harry.ActivarDobleSalto(duracionDobleSalto);

            StartCoroutine(ReaparecerEscoba());
        }
    }

    private System.Collections.IEnumerator ReaparecerEscoba()
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
            r.enabled = false;
        foreach (Collider c in GetComponentsInChildren<Collider>())
            c.enabled = false;

        yield return new WaitForSeconds(tiempoReaparicion);

        foreach (Renderer r in GetComponentsInChildren<Renderer>())
            r.enabled = true;
        foreach (Collider c in GetComponentsInChildren<Collider>())
            c.enabled = true;

        recogida = false;
    }
}