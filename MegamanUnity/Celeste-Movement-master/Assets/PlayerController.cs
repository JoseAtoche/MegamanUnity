using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int vida = 100;
    public Slider heartBar;
    public bool invulnerable;
    Color color;
    public  SpriteRenderer spriteRenderer;



    // Start is called before the first frame update
    void Start()
    {
        heartBar.maxValue = vida;

    }

    // Update is called once per frame
    void Update()
    {

        heartBar.value = vida;

    }



    void OnTriggerStay2D(Collider2D collision)
    {
        if (!invulnerable)
        {
            if (collision.gameObject.tag == "Enemy")
            { StopAllCoroutines();
            invulnerable = true;
            Invoke("UndoInvincible", 1);
            vida--;
            StartCoroutine(FlashSprite());

            
           }

           
        }

    }
 


    IEnumerator FlashSprite()
    {
        while (true)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.02f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.02f);
        }
    }


    void UndoInvincible()
    {
        invulnerable = false;
        StopAllCoroutines();
        spriteRenderer.enabled = true;
    }
}
