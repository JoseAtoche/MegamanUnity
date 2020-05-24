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


    /// <summary>
    /// Se salta la escena para hacer más rápido todo
    /// </summary>
    public void PasarEscena()
    {
        GameObject.Find("Cutscene").SetActive(false);

        PantallaDeCarga.Instancia.CargarEscena(SceneManager.GetActiveScene().buildIndex + 1);
    }
}