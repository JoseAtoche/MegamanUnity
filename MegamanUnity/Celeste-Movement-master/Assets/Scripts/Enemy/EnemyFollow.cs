using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float radiodevision;
    public float velocidad;
    public GameObject jugador;
    Vector3 posicioninicial;

    // Start is called before the first frame update
    void Start()
    {
        posicioninicial = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 objetivo = posicioninicial;
        float dist = Vector3.Distance(jugador.transform.position, transform.position);
        if (dist < radiodevision) objetivo = jugador.transform.position;
        float fixedSpeed = velocidad * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, objetivo, fixedSpeed);

        Debug.DrawLine(transform.position, objetivo, Color.green);



    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radiodevision);

    }
}
