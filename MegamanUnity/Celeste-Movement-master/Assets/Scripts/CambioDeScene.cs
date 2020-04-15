using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioDeScene : MonoBehaviour
{
    /// <summary>
    /// Cambia a la siguiente escena
    /// </summary>
    public void SiguienteScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}