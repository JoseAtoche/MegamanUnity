using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Entity_life scriptVida;

    public Slider heartBar;
    public bool invulnerable;
    private Color color;

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