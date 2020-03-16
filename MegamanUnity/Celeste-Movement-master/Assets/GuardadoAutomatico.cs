using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardadoAutomatico : MonoBehaviour
{
    float tiempodeespera = 0;
    public bool prohibidoguardar = false;
    public bool nuevapartida = false;

    private void Awake()
    {


        DontDestroyOnLoad(this);
    }



    private void Start()
    {




    }
    void Update()
    {
        //Cada 10 segundos el juego guarda automáticamente a no ser que esté prohibido guardar por ejemplo cuando luchas contra el boss
        if (Time.time > tiempodeespera && !prohibidoguardar)
        {

            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                GuardarPartida.Save(GameObject.FindGameObjectWithTag("Player"));
                Debug.Log("Guardado correctamente");
            }



            tiempodeespera = Time.time + 10;


        }
    }



    public void Load()
    {

        if (!nuevapartida)

        {

            DatosCheckPoint data = GuardarPartida.Load();
            Vector3 vector = new Vector3(data.x, data.y, data.z);
            GameObject.FindGameObjectWithTag("Player").transform.position = vector;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Entity_life>().vida = data.vida;

            Debug.Log("Cargado correctamente");


        }



    }
}
