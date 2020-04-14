using UnityEngine;
using UnityEngine.SceneManagement;


public class CambioDeScene : MonoBehaviour
{

    public void SiguienteScene()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


}
