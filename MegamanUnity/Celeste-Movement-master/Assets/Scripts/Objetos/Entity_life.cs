using System.Collections;
using UnityEngine;


[System.Serializable]
public class Entity_life : MonoBehaviour
{


    public int vida = 100;
    public bool invulnerable;
    public SpriteRenderer spriteRenderer;

    public Collider2D colisionreal;
    public Collider2D colisionespadaizquierda;
    public Collider2D colisionespadaderecha;


    private void Update()
    {
        if (vida <= 0)
        {

            Destroy(gameObject);


        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {





        //Colision del nuevo personaje
        //El enemigo va a detectar si está colisionando con la hitbox correcta del jugador
        //De esta forma le hará daño a el JUGADOR

        //Tambien ve si alguna bala enemiga le golpeó
        if (collision == colisionreal.GetComponent<BoxCollider2D>())
        {
            Entity_life entity_jugador = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Entity_life>();
            if (!entity_jugador.invulnerable)
            {

                entity_jugador.StopAllCoroutines();
                entity_jugador.invulnerable = true;
                entity_jugador.Invoke("UndoInvincible", 2);

                if (vida <= 50) { entity_jugador.vida -= 6; } else { entity_jugador.vida -= 3; }


                entity_jugador.StartCoroutine(entity_jugador.FlashSprite());



            }



        }


        //El enemigo detectará si ha entrado en el collider de las espadas del jugador
        //Y SI ESTÁ ATACANDO, RECIBIRÁ DAÑO

        if ((collision == colisionespadaderecha.GetComponent<PolygonCollider2D>() || collision == colisionespadaizquierda.GetComponent<PolygonCollider2D>()) && GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().ataque > 0)
        {
            if (!invulnerable)
            {

                StopAllCoroutines();
                invulnerable = true;
                Invoke("UndoInvincible", 2);


                Entity_life entity_jugador = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Entity_life>();
                if (entity_jugador.vida <= 50)
                {
                    vida -= 8;
                }
                else
                {
                    vida -= 5;
                }

                StartCoroutine(FlashSprite());

            }


        }

        //Detecta si ha colisionado con una bala un enemigo






    }




    public IEnumerator FlashSprite()
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
