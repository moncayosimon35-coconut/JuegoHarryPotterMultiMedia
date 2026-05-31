using UnityEngine;

public class MovimientoGato : MonoBehaviour
{
    public float distancia = 1.8f;  
    public float velocidad = 2f;  
    public bool invertirDireccion = false; 

    private Vector3 posicionInicial;

    void Start()
    {
        posicionInicial = transform.position;
    }

    void Update()
    {
        float factorInversion = invertirDireccion ? -1f : 1f;
        
        // Calcula el desfase de movimiento usando la función Seno
        float desfase = Mathf.Sin(Time.time * velocidad) * distancia * factorInversion;

        // Aplica el movimiento en el eje Z (Adelante y Atrás)
        transform.position = new Vector3(posicionInicial.x, posicionInicial.y, posicionInicial.z + desfase);
    }
}
