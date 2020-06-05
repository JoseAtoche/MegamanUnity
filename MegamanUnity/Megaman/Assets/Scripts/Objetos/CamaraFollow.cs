using UnityEngine;


//Se encarga de que la camara siempre siga al jugador
public class CamaraFollow : MonoBehaviour
{
    public GameObject jugador;

    private Vector3 posicion;

    private void Start()
    {
        posicion = transform.position - jugador.transform.position;
    }

    /// <summary>
    /// La cámara sigue al jugador en todo momento
    /// </summary>
    private void Update()
    {
        transform.position = jugador.transform.position + posicion;
    }
}