using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject jugador;

    private Vector3 posicion;

    private void Start()
    {
        posicion = transform.position - jugador.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = jugador.transform.position + posicion;
    }
}