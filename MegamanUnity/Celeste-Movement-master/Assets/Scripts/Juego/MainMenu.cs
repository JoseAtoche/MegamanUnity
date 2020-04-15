using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Método que se activa cuando le das a comenzar juego (Nueva partida)
    /// </summary>
    public void PlayGame()
    {
        GameObject.FindGameObjectWithTag("Guardar").GetComponent<GuardadoAutomatico>().nuevaPartida = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// Método que se activa cuando le das a Continuar
    /// </summary>
    public void ContinuarGame()
    {
        GameObject.FindGameObjectWithTag("Guardar").GetComponent<GuardadoAutomatico>().nuevaPartida = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    /// <summary>
    /// Método que se activa cuando le das a salir
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}