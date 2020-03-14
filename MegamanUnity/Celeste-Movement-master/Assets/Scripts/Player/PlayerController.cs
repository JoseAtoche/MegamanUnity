using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public Entity_life scriptvida;


    public Slider heartBar;
    public bool invulnerable;
    Color color;



    // Start is called before the first frame update
    void Start()
    {
        heartBar.maxValue = scriptvida.vida;

    }

    // Update is called once per frame
    void Update()
    {

        heartBar.value = scriptvida.vida;

    }




}
