using UnityEngine;

public class RotacionProp : MonoBehaviour
{
    public float velocidadRotacion = 100f;

    void Update()
    {
        
        transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
    }
}
