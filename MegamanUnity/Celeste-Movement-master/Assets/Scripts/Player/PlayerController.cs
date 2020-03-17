using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Entity_life scriptvida;

    public Slider heartBar;
    public bool invulnerable;
    private Color color;

    // Start is called before the first frame update
    private void Start()
    {
        heartBar.maxValue = scriptvida.vida;
    }

    // Update is called once per frame
    private void Update()
    {
        heartBar.value = scriptvida.vida;
    }
}