using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escudo : MonoBehaviour
{


    public float velocidad;
    private Vector3 posicioninicial;
    private Animator animator;
    private Vector3 posicionfinal;
    private Vector3 objetivo;



    // Start is called before the first frame update
    void Start()
    {
        posicioninicial = transform.position;
        animator = GetComponent<Animator>();
        posicionfinal = new Vector3(posicioninicial.x - 3, posicioninicial.y, posicioninicial.z);

    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position == posicioninicial)
        {

            //  animator.SetBool("Andando", false);
            animator.SetTrigger("GiroIzquierda");
            animator.ResetTrigger("GiroDerecha");
            objetivo = posicionfinal;
        }
        if (transform.position == posicionfinal)
        {
            //   animator.SetBool("Andando", false);
            animator.SetTrigger("GiroDerecha");
            animator.ResetTrigger("GiroIzquierda");

            objetivo = posicioninicial;
        }

        animator.SetBool("Andando", true);

        float fixedSpeed = velocidad * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, objetivo, fixedSpeed);

        transform.position = new Vector3(transform.position.x, posicioninicial.y, transform.position.z);
    }
}
