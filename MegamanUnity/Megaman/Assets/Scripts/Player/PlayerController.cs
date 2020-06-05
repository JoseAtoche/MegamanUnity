using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Se encarga de controlar la vida del personaje principal
/// </summary>
public class PlayerController : MonoBehaviour
{
    public Entity_life scriptVida;

    public Slider heartBar;
    public bool invulnerable;

    /// <summary>
    /// Establezco el máximo de la barra de vida a valor asignado al jugador
    /// </summary>
    private void Start()
    {
        heartBar.maxValue = scriptVida.vida;
    }

    /// <summary>
    /// Comprueba en todo momento la vida del jugador para bajarla o subirla
    /// </summary>
    private void Update()
    {
        heartBar.value = scriptVida.vida;
    }
}