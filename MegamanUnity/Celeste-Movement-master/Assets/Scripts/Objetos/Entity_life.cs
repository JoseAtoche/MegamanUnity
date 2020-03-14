using System.Collections;
using UnityEngine;

public class Entity_life : MonoBehaviour
{


    public int vida = 100;
    private bool invulnerable;
    public SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (!invulnerable)
        {


            if (collision.gameObject.tag == "Player")
            {

                StopAllCoroutines();
                invulnerable = true;
                Invoke("UndoInvincible", 2);
                vida -= 5;
                StartCoroutine(FlashSprite());


            }

            if (collision.gameObject.tag == "Enemy")
            {

                StopAllCoroutines();
                invulnerable = true;
                Invoke("UndoInvincible", 2);
                vida -= 3;
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
