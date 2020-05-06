using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicaFinal : MonoBehaviour
{
    bool permiteSaltar = false;
    public void PermitirSaltar()
    {

        permiteSaltar = true;




    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire3") && permiteSaltar)
        {
            SceneManager.LoadScene(0);


        }




    }


    public void Final()
    {




        SceneManager.LoadScene(0);
    }

}
