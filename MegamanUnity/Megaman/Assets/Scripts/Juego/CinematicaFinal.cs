using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Se ocupa de controlar la cinemática final
/// </summary>
public class CinematicaFinal : MonoBehaviour
{
    //Booleano que controla si puedes saltar la escena
    private bool permiteSaltar = false;

    public GameObject video;

    /// <summary>
    /// espera continuamente a que pulses el boton para saltar
    /// </summary>
    private void Update()
    {
        if (Input.GetButtonDown("Fire3") && permiteSaltar)
        {
            SceneManager.LoadScene(0);
        }
    }

    /// <summary>
    /// al llegar al final reinicia el juego
    /// </summary>
    public void Final()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Muesta el video en pantalla
    /// </summary>
    public void MostrarVideo()
    {
        video.SetActive(true);
    }

    /// <summary>
    /// Permite saltar la escena
    /// </summary>
    public void PermitirSaltar()
    {
        permiteSaltar = true;
    }
}