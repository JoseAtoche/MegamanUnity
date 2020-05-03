using UnityEngine;
using UnityEngine.SceneManagement;

public class Controlador_Intro : MonoBehaviour
{
    /// <summary>
    /// Si pulso cualquier tecla la cinemática parará
    /// </summary>
    private void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void PasarEscena() {



        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}