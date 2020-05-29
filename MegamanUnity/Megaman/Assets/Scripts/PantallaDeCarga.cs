using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PantallaDeCarga : MonoBehaviour
{
    public static PantallaDeCarga Instancia { get; private set; }

    public Image imageDeCarga;

    private void Awake()
    {
        DefinirSingleton();
    }
    /// <summary>
    /// Establece el objeto como no destruible y en falso
    /// </summary>
    private void DefinirSingleton()
    {//Hace que no se destruya al pasar de escena y la pone a false para que no se vea al iniciar el juego
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
    /// <summary>
    /// Carga la siguiente escena
    /// </summary>
    /// <param name="nombreEscena"></param>
    public void CargarEscena(int nombreEscena)
    {
        StartCoroutine(MostrarPantallaDeCarga(nombreEscena));
    }

    private IEnumerator MostrarPantallaDeCarga(int nombreEscena)
    {//muestra la imagen por pantalla e inicia la animacion
        imageDeCarga.gameObject.SetActive(true);
        imageDeCarga.GetComponent<Animator>().SetTrigger("Load");
        imageDeCarga.GetComponent<Animator>().ResetTrigger("Load");

        //Si aun está en marcha la imagen de carga no carga el escenario siguiuente
        while (imageDeCarga.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("load") ||
            imageDeCarga.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("start"))
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

        //Si está cargada y la animacion ha finalizado oculta el canvas
        if (operacion.isDone == true)
        {

            imageDeCarga.gameObject.SetActive(false);

        }
    }
}