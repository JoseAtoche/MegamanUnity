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
    bool primer = false;

    private void Update()
    {

        //Comprobacion de la vida, este método NO es para el boss
        if (vida <= 0 && (this.tag == "Enemy" || this.tag == "escudo") && this.name != "prometheusBoss")
        {
            StopCoroutine(FlashSprite());


            Invoke("Explode", 2f);
        }
        else if (vida <= 0 && this.name == "prometheusBoss")
        {







        }
    }
    /// <summary>
    /// Colusión de las entiddes
    /// </summary>
    /// <param name="collision"></param>
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

        //Esto comprueba la colisión con el enemigo del escudo y sabe si está a la derecha o a la izquierda, ESTE DAÑO ES SOLO PARA LA ESPADA
        else
        {
            try
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
            catch (Exception e) { }
        }
    }


    /// <summary>
    /// Este daño es específico para el escudo, ya que su funcionamiento es algo distinto, esto es SOLO Y EXCLUSIVAMENTE PARA CUANDO LE DA UNA BALA
    /// </summary>
    /// <param name="x">Hay que pasarle el eje X del jugador para que este sepa si está a la derecha o a la izquierda</param>
    public void danioparaescudo(float x)
    {


        if (this.GetComponent<Escudo>().derecha && !invulnerable && x < this.transform.position.x)
        {


            StopAllCoroutines();
            invulnerable = true;
            Invoke("UndoInvincible", 2);

            //Resta vida al enemigo segun la vida del jugador, y su potenciador
            vida -= (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Entity_life>().vida <= 50) ? (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().canonpotenciado) ? 3 * 2 : 3 : (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().canonpotenciado) ? 2 * 2 : 2;

            //  vida -= (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Entity_life>().vida <= 50) ? 3 : 2;

            StartCoroutine(FlashSprite());
        }
        else if (!this.GetComponent<Escudo>().derecha && !invulnerable && x > this.transform.position.x)
        {
            StopAllCoroutines();
            invulnerable = true;
            Invoke("UndoInvincible", 2);

            //Resta vida al enemigo segun la vida del jugador, y su potenciador
            vida -= (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Entity_life>().vida <= 50) ? (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().canonpotenciado) ? 3 * 2 : 3 : (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().canonpotenciado) ? 2 * 2 : 2;

            // vida -= (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Entity_life>().vida <= 50) ? 3 : 2;

            StartCoroutine(FlashSprite());
        }






    }


    /// <summary>
    /// Invoca al efecto Flash para que el sprite parpadee
    /// </summary>
    /// <returns></returns>
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


    /// <summary>
    /// Hace invencible a la entidad
    /// </summary>
    private void UndoInvincible()
    {
        invulnerable = false;
        StopAllCoroutines();
        spriteRenderer.enabled = true;
    }

    /// <summary>
    /// Invoca la explosion al morir la entidad
    /// </summary>
    void Explode()
    {
        //Obtiene el objeto Explosion y lo inicia solo 0.15 segundos
        this.transform.GetChild(1).gameObject.SetActive(true);
        Destroy(gameObject, 0.15f);
        //Aleatoriamente te da un objeto de doble de fuerza
        if (UnityEngine.Random.Range(0, 5) == 0 && !primer)
        {
            primer = true;
            Instantiate(GameObject.FindGameObjectWithTag("objetos").GetComponent<objetosNecesarios>().fuerza, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), new Quaternion(0, 0, 0, 0));

        }


    }
}