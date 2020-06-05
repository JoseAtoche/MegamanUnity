using System;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Cambia la escena, esto es solo para el boss final
/// </summary>
public class CambioDeScene : MonoBehaviour
{
    /// <summary>
    /// Cambia a la siguiente escena
    /// </summary>
    public void SiguienteScene()
    {
        try
        {
            PantallaDeCarga.Instancia.CargarEscena(SceneManager.GetActiveScene().buildIndex + 1);
        }
        catch (Exception e)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        finally
        {
            GameObject.Find("Boss").SetActive(false);
        }
    }
}