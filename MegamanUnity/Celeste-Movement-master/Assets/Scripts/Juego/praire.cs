using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class praire : MonoBehaviour
{
    public GameObject bocadillo;
    public Animator nube;
    bool activado = false;
    public GameObject texto;
    public GameObject tutorial;


    private void Start()
    {

        nube.SetTrigger("grande");


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        nube.ResetTrigger("grande");
        nube.ResetTrigger("pequeño");

        nube.SetTrigger("grande");

        if (activado)
        {

            texto.transform.position = new Vector3(texto.transform.position.x, texto.transform.position.y + 400 + 108, 0);
            activado = false;
            tutorial.SetActive(true);


        }

    }


    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Player" && Input.GetButtonDown("Fire1") && !activado)
        {
            nube.ResetTrigger("grande");


            nube.SetTrigger("pequeño");
            nube.SetTrigger("pequeño");

            texto.transform.position = new Vector3(texto.transform.position.x, texto.transform.position.y - 400 - 108, 0);
            nube.GetComponent<DialogueTrigger>().TriggerDialogue();
            activado = true;



        }

    }



}
