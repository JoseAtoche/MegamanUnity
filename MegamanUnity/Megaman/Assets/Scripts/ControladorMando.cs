using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorMando : MonoBehaviour
{
    //Por defecto no hay mando conectado
    public bool conectado = false;
    public Text texto;
    public Animator animacion;
    void Update()
    {
        string[] names = Input.GetJoystickNames();
        //Si se conecta un mando pero no estaba anteriormente conectado
        if (names.Length > 0)
        {
            if (names[0] == "Controller (XBOX 360 For Windows)" && !conectado)
            {
                //Establece el tecto deseado
                texto.text = "Mando Conectado";
                conectado = true;
                //Establece la animacion
                animacion.SetTrigger("Aparecer");

            }
            //Si se desconecta el mando y estaba conectado
            else if (names[0] != "Controller (XBOX 360 For Windows)" && conectado)
            {
                //Establece el tecto deseado

                texto.text = "Mando desconectado";
                conectado = false;
                //Establece la animacion
                animacion.SetTrigger("Aparecer");

            }
        }



    }
}
