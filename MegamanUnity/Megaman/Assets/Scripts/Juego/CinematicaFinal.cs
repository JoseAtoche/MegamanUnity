using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematicaFinal : MonoBehaviour
{
    private bool permiteSaltar = false;

    public void PermitirSaltar()
    {
        permiteSaltar = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Fire3") && permiteSaltar)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void Final()
    {
        SceneManager.LoadScene(0);
    }
}