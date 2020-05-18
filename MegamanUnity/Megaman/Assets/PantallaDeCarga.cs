using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class PantallaDeCarga : MonoBehaviour
{

    public static PantallaDeCarga Instancia { get; private set; }

    public Image imageDeCarga;

    void Awake()
    {
        DefinirSingleton();
    }

    private void DefinirSingleton()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(this);
            imageDeCarga.gameObject.SetActive(false);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CargarEscena(int nombreEscena)
    {
        StartCoroutine(MostrarPantallaDeCarga(nombreEscena));
    }


    private IEnumerator MostrarPantallaDeCarga(int nombreEscena)
    {

        imageDeCarga.gameObject.SetActive(true);
        imageDeCarga.GetComponent<Animator>().SetTrigger("Load");
        imageDeCarga.GetComponent<Animator>().ResetTrigger("Load");



        //Mientras no esté totalmente visible va aumentando su visibilidad

        while (imageDeCarga.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("load") || imageDeCarga.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("start"))
        {
            imageDeCarga.GetComponent<Animator>().SetBool("end", true);

            yield return null;
        }
        AsyncOperation operacion = SceneManager.LoadSceneAsync(nombreEscena);

        //Espera a que haya cargado la nueva escena
        while (operacion.isDone == false)
        {

            yield return null;
        }



        if (operacion.isDone == true)
        {
            if (imageDeCarga.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("end"))
            {

                //Mientras la imagen de carga siga visible va desvaneciéndola

                imageDeCarga.gameObject.SetActive(false);
            }


        }
    }



}

