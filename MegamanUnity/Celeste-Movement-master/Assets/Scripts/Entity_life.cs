using System;
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Colision del nuevo personaje
        //El enemigo va a detectar si está colisionando con la hitbox correcta del jugador
        //De esta forma le hará daño a el JUGADOR

        if (collision == colisionreal.GetComponent<BoxCollider2D>())
        {
            Entity_life entity_jugador = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Entity_life>();
            if (!entity_jugador.invulnerable)
            {
                entity_jugador.StopAllCoroutines();
                entity_jugador.invulnerable = true;
                entity_jugador.Invoke("UndoInvincible", 2);

                //Resta vida al jugador segun la vida del enemigo, si es menor a 50 resta 6 si no 4
                entity_jugador.vida -= (vida <= 50) ? 6 : 3;

                entity_jugador.StartCoroutine(entity_jugador.FlashSprite());
            }
        }
        else

        //El enemigo detectará si ha entrado en el collider de las espadas del jugador
        //Y SI ESTÁ ATACANDO, RECIBIRÁ DAÑO

        if ((collision == colisionespadaderecha.GetComponent<PolygonCollider2D>() ||
            collision == colisionespadaizquierda.GetComponent<PolygonCollider2D>()) &&
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().ataque > 0 &&
            !invulnerable && this.GetComponent<Escudo>() == null)
        {
            StopAllCoroutines();
            invulnerable = true;
            Invoke("UndoInvincible", 2);

            //Resta vida al enemigo segun la vida del jugador, si es menor a 50 resta 8 si no 5

            vida -= (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Entity_life>().vida <= 50) ? 8 : 5;

            StartCoroutine(FlashSprite());
        }
        else
        {
            if ((collision == colisionespadaderecha.GetComponent<PolygonCollider2D>() && this.GetComponent<Escudo>().derecha) &&
               GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().ataque > 0 &&
               !invulnerable && this.GetComponent<Escudo>() != null)
            {


                StopAllCoroutines();
                invulnerable = true;
                Invoke("UndoInvincible", 2);

                //Resta vida al enemigo segun la vida del jugador, si es menor a 50 resta 8 si no 5

                vida -= (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Entity_life>().vida <= 50) ? 8 : 5;

                StartCoroutine(FlashSprite());
            }
            else
                try
                {
                    if ((collision == colisionespadaizquierda.GetComponent<PolygonCollider2D>() && !this.GetComponent<Escudo>().derecha) &&
                      GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().ataque > 0 &&
                      !invulnerable && this.GetComponent<Escudo>() != null)
                    {
                        StopAllCoroutines();
                        invulnerable = true;
                        Invoke("UndoInvincible", 2);

                        //Resta vida al enemigo segun la vida del jugador, si es menor a 50 resta 8 si no 5

                        vida -= (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Entity_life>().vida <= 50) ? 8 : 5;

                        StartCoroutine(FlashSprite());
                    }

                }
                catch (Exception e) { }




        }
    }



    public void danioparaescudo(float x)
    {


        if (this.GetComponent<Escudo>().derecha && !invulnerable && x < this.transform.position.x)
        {


            StopAllCoroutines();
            invulnerable = true;
            Invoke("UndoInvincible", 2);

            //Resta vida al enemigo segun la vida del jugador, si es menor a 50 resta 8 si no 5

            vida -= (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Entity_life>().vida <= 50) ? 3 : 2;

            StartCoroutine(FlashSprite());
        }
        else if (!this.GetComponent<Escudo>().derecha && !invulnerable && x > this.transform.position.x)
        {
            StopAllCoroutines();
            invulnerable = true;
            Invoke("UndoInvincible", 2);

            //Resta vida al enemigo segun la vida del jugador, si es menor a 50 resta 8 si no 5

            vida -= (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Entity_life>().vida <= 50) ? 3 : 2;

            StartCoroutine(FlashSprite());
        }






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

    private void UndoInvincible()
    {
        invulnerable = false;
        StopAllCoroutines();
        spriteRenderer.enabled = true;
    }
}