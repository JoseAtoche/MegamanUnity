using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    private Animator anim;
    private Movement move;
    private Collision coll;

    [HideInInspector]
    public SpriteRenderer sr;

    private float tiempoDeEspera;
    private float dañado;

    private void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponentInParent<Collision>();
        move = GetComponentInParent<Movement>();
        sr = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Se encarga de poner la animacion necesaria del movimiento en todo momento automáticamente
    /// </summary>

    private void Update()
    {
        anim.SetBool("onGround", coll.onGround);
        anim.SetBool("onWall", coll.onWall);

        anim.SetBool("wallSlide", move.wallSlide);

        anim.SetBool("isDashing", move.isDashing);

        anim.SetBool("Shoot", move.shoot);
        move.shoot = false;

        anim.SetBool("BigAttack", move.bigAtack);

        anim.SetBool("upattack", move.upattack);
        anim.SetInteger("ataque", move.ataque);

        if (GameObject.FindObjectOfType<PlayerController>().scriptVida.vida < 50)
        {
            anim.SetBool("dañado", true);
        }
        else
        {
            anim.SetBool("dañado", false);
        }

        if (GameObject.FindObjectOfType<PlayerController>().scriptVida.invulnerable == true && (Time.time > dañado))
        {
            anim.SetTrigger("recibodaño");


            dañado = Time.time + 2f;
        }






        //anim.SetBool("canMove", move.canMove);
        // anim.SetBool("onRightWall", coll.onRightWall);
        //anim.SetBool("wallGrab", move.wallGrab);
        /// anim.SetBool("OnLeader", move.onleader);
    }

    //Establece las animaciones respecto al movimiento
    public void SetHorizontalMovement(float x, float y, float yVel)
    {
        anim.SetFloat("HorizontalAxis", x);
        anim.SetFloat("VerticalAxis", y);
        anim.SetFloat("VerticalVelocity", yVel);

        if (x != 0)
        {
            anim.SetBool("Moviendose", true);
            tiempoDeEspera = Time.time;
        }

        if (Time.time > tiempoDeEspera + 0.03)
        {
            anim.SetBool("Moviendose", false);
        }
    }

    /// <summary>
    /// Me permite activar cualquier trigger
    /// </summary>
    /// <param name="trigger">nombre del trigger</param>
    public void SetTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }

    /// <summary>
    /// Rota visualmente la animacion
    /// </summary>
    /// <param name="side">1 Derecha | -1 Izquierda</param>
    public void Flip(int side)
    {
        if (move.wallGrab || move.wallSlide)
        {
            if (side == -1 && sr.flipX)
            {
                return;
            }

            if (side == 1 && !sr.flipX)
            {
                return;
            }
        }

        bool state = (side == 1) ? false : true;
        sr.flipX = state;
    }
}