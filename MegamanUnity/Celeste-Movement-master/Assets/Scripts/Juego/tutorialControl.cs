using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialControl : MonoBehaviour
{
    bool primera = false;
    public GameObject flecha;
    public GameObject tutorial1;
    public GameObject tutorial2;

    // Update is called once per frame
    void Update()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().enabled = false;
        tutorial1.SetActive(true);
        flecha.SetActive(true);


        if (Input.GetButtonDown("Fire1") && !primera)
        {
            if (!primera)
            {
                primera = true;
                flecha.SetActive(false);

            }
            tutorial2.SetActive(true);
            flecha.SetActive(true);





        }
        else if (Input.GetButtonDown("Fire1"))
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().enabled = true;
            this.gameObject.SetActive(false);
            tutorial1.SetActive(false);
            tutorial2.SetActive(false);
            flecha.SetActive(false);

        }



    }
}
