using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject jugador;
    private Vector3 posicion;

    
    
    void Start()
    {
        posicion = transform.position - jugador.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = jugador.transform.position + posicion;

    }
}
