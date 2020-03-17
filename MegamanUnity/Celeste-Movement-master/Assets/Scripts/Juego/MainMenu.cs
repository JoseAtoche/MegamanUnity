using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        GameObject.FindGameObjectWithTag("Guardar").GetComponent<GuardadoAutomatico>().nuevapartida = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ContinuarGame()
    {
        GameObject.FindGameObjectWithTag("Guardar").GetComponent<GuardadoAutomatico>().nuevapartida = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}