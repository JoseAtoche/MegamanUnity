using System;
using System.Collections;
using UnityEngine;

[System.Serializable]
public class Entity_life : MonoBehaviour
{
    public int vida = 100;
    public bool invulnerable;
    public SpriteRenderer spriteRenderer;

    public Collider2D colisionReal;
    private Collider2D colisionEspadaIzquierda;
    private Collider2D colisionEspadaDerecha;
    private bool primeraVez = false;

    public bool permitirAtaque = true;
    public int ataqueAnterior = 0;

    public GameObject finalCutscene;

    private void Start()
    {
        colisionReal = GameObject.FindGameObjectWithTag("objetos").GetComponent<objetosNecesarios>().colisionReal;
        colisionEspadaIzquierda = GameObject.FindGameObjectWithTag("objetos").GetComponent<objetosNecesarios>().colisionEspadaIzquierda;
        colisionEspadaDerecha = GameObject.FindGameObjectWithTag("objetos").GetComponent<objetosNecesarios>().colisionEspadaDerecha;
    }


    private void Update()
    {

        try
        {
            //Comprobacion de la vida, este método NO es para el boss
            if (vida <= 0 && (this.name == "Carabela 1" || this.name == "Carabela 2" || this.name == "Carabela 3" || this.name == "Carabela 4"))
            {
                this.GetComponentInParent<Animator>().SetBool("Muerto", true);

                StartCoroutine(MorirCarabela());
            }
            //Si la vida del BOSS es menor a 0
            else if (vida <= 0 && this.name == "prometheusBoss")
            {
                this.transform.GetChild(1).gameObject.SetActive(true);
                this.transform.GetChild(2).gameObject.SetActive(true);
            }
            else if (this.transform.GetComponent<EnemyFollow>() != null)
            {
                if (this.transform.GetComponent<EnemyFollow>().enabled == false)
                {
                    StopAllCoroutines();

                    Invoke("Explode", 0.2f);

                }

            }
        }
        catch (Exception e)
        {

            Debug.Log(e);

        }

    }

    /// <summary>
    /// Colisión de las entidades
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().ataque == 0 || GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().ataque == ataqueAnterior + 1)
        {
            permitirAtaque = true;
        }

        else
        {
            permitirAtaque = false;
        }



        //El enemigo detectará si ha entrado en el collider de las espadas del jugador
        //Y SI ESTÁ ATACANDO, RECIBIRÁ DAÑO
        if ((collision == colisionEspadaDerecha.GetComponent<PolygonCollider2D>() ||
         collision == colisionEspadaIzquierda.GetComponent<PolygonCollider2D>()) &&
         GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().ataque > 0 &&
         (!invulnerable || GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().combo) && this.GetComponent<Escudo>() == null)
        {
            QuitarVida("espada");
        }

        //Esto comprueba la colisión con el enemigo del escudo y sabe si está a la derecha o a la izquierda respecto a la direcion disparada por el enemigo
        else if (this.GetComponent<Escudo>() != null)
        {
            if (((collision == colisionEspadaDerecha.GetComponent<PolygonCollider2D>() && this.GetComponent<Escudo>().derecha) &&
                  GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().ataque > 0 &&
                  !invulnerable && this.GetComponent<Escudo>() != null) ||

                  (((collision == colisionEspadaIzquierda.GetComponent<PolygonCollider2D>() && !this.GetComponent<Escudo>().derecha) &&
                        GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().ataque > 0 &&
                        !invulnerable && this.GetComponent<Escudo>() != null)))
            {
                QuitarVida("espada");
            }


        }




        //Colision del nuevo personaje
        //El enemigo va a detectar si está colisionando con la hitbox correcta del jugador
        //De esta forma le hará daño a el JUGADOR
        else if (colisionReal != null)
        {
            if (collision == colisionReal.GetComponent<BoxCollider2D>())
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
        }




        ataqueAnterior = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().ataque;
    }

    /// <summary>
    /// Este daño es específico para el escudo, ya que su funcionamiento es algo distinto, esto es SOLO Y EXCLUSIVAMENTE PARA CUANDO LE DA UNA BALA
    /// </summary>
    /// <param name="x">Hay que pasarle el eje X del jugador para que este sepa si está a la derecha o a la izquierda</param>
    public void DanioEscudo(float x)
    {
        try
        {
            if (this.GetComponent<Escudo>().derecha && !invulnerable && x < this.transform.position.x || !this.GetComponent<Escudo>().derecha && !invulnerable && x > this.transform.position.x)
            {
                QuitarVida("pistola");
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    /// <summary>
    /// Invoca al efecto Flash para que el sprite parpadee
    /// </summary>
    /// <returns></returns>
    public IEnumerator FlashSprite()
    {
        //Hace la animacion de daño
        try
        {
            if (this.transform.GetChild(2).GetComponent<AnimationScript>() != null)
            {

                this.transform.GetChild(2).GetComponent<AnimationScript>().anim.SetTrigger("recibodaño");
                this.transform.GetChild(2).GetComponent<AnimationScript>().animacionDaño = true;

            }

        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

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
    private void Explode()
    {
        //Obtiene el objeto Explosion y lo inicia solo 0.15 segundos
        this.transform.GetChild(1).gameObject.SetActive(true);
        Destroy(gameObject, 0.15f);
        //Aleatoriamente te da un objeto de doble de fuerza
        if (UnityEngine.Random.Range(0, 7) == 0 && !primeraVez)
        {
            primeraVez = true;
            Instantiate(GameObject.FindGameObjectWithTag("objetos").GetComponent<objetosNecesarios>().fuerza, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), new Quaternion(0, 0, 0, 0));
        }
    }

    //reactiva la vida de la carabela antes de destruirla
    public IEnumerator MorirCarabela()
    {
        yield return new WaitForSeconds(1f);
        vida = 1;
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Quita la vida del enemigo
    /// </summary>
    /// <param name="arma">Debes poner el arma que acabas de usar para ello</param>
    public void QuitarVida(string arma)
    {
        StopAllCoroutines();
        invulnerable = true;
        Invoke("UndoInvincible", 2);

        if (arma == "espada")
        {
            //Resta vida al enemigo segun la vida del jugador, si es menor a 50 resta 8 si no 5

            vida -= (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Entity_life>().vida <= 50) ? 8 : 5;
        }
        else if (arma == "pistola")
        {
            //Resta vida al enemigo segun la vida del jugador, y su potenciador
            vida -= (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Entity_life>().vida <= 50) ? (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().disparoPotenciado) ? 3 * 2 : 3 : (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().disparoPotenciado) ? 2 * 2 : 2;
        }

        StartCoroutine(FlashSprite());
    }
}