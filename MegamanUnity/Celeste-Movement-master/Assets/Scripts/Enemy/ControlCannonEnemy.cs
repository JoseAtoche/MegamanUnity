using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCannonEnemy : MonoBehaviour
{
    public GameObject bala;
    public GameObject cannon;
    public float tiempoinicia = 0;
    public float tiempoespera = 2;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        tiempoinicia += Time.deltaTime;
        if (tiempoinicia >= tiempoespera)
        {

            GameObject balacreada = Instantiate(bala, cannon.transform.position, cannon.transform.rotation);
            balacreada.GetComponent<Rigidbody2D>().AddForce(cannon.transform.position);
            balacreada.transform.SetParent(transform);
            tiempoinicia = 0;
        }
    }




}
