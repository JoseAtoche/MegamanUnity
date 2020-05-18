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
            GameObject.Find("Cutscene").SetActive(false);

            PantallaDeCarga.Instancia.CargarEscena(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void PasarEscena()
    {
        GameObject.Find("Cutscene").SetActive(false);

        PantallaDeCarga.Instancia.CargarEscena(SceneManager.GetActiveScene().buildIndex + 1);
    }
}