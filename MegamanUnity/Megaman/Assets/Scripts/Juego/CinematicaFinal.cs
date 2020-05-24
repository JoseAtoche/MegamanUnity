using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicaFinal : MonoBehaviour
{
    private bool permiteSaltar = false;

    public GameObject video;

    // Update is called once per frame
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