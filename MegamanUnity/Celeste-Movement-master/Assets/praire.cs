using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class praire : MonoBehaviour
{
    public GameObject bocadillo;
    Transform posicionincial;
    public Animator nube;


    Vector3 posiciondeseada;



    private void Start()
    {
        nube.ResetTrigger("grande");
        nube.ResetTrigger("pequeño");


        posicionincial = bocadillo.transform;
        nube.SetTrigger("grande");
        bocadillo.transform.position = transform.position;
        posiciondeseada = posicionincial.position;

    }



    private void Update()
    {

        bocadillo.transform.position = Vector3.MoveTowards(nube.transform.position, posiciondeseada, 10 * Time.deltaTime);
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        nube.ResetTrigger("grande");
        nube.ResetTrigger("pequeño");


        nube.SetTrigger("grande");
        posiciondeseada = posicionincial.position;



    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Player" && Input.GetButtonDown("Fire1"))
        {
            nube.ResetTrigger("grande");


            posiciondeseada = transform.position;

            nube.SetTrigger("pequeño");





        }








    }



}
