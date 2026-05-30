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
        
        float desfase = Mathf.Sin(Time.time * velocidad) * distancia * factorInversion;

        transform.position = new Vector3(posicionInicial.x, posicionInicial.y, posicionInicial.z + desfase);
    }
}
