using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [Space]
    [Header("Objects y hitboxes")]

    //Prefab de la bala
    public GameObject bala;

    ///Cañon del jugador
    public GameObject cannon;

    //Biometal para el ataque especial
    public GameObject biometal;

    //Script de colision
    private Collision coll;

    //Espacio que ocupa la espada derecha
    public GameObject colliderDerderecha;

    //Espacio que ocupa la espada izquierda
    public GameObject colliderIzquierda;

    //La sombra trasera que se activa al 50%
    public GameObject ghostFueza;

    [HideInInspector]
    public Rigidbody2D rb;

    //Script de animacion
    private AnimationScript animacion;

    [Space]
    [Header("Estadísticas")]
    public float speed = 10;

    public float jumpForce = 50;
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;
    public float dashSpeed = 20;
    public float cooldown = 2;
    private float nextFireTime = 0;
    private float cooldownSonido = 0;

    [Space]
    [Header("Boleanos")]
    public bool canMove;

    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;
    public bool isDashing;

    [Space]
    //suelo tocado
    private bool groundTouch;

    //Si está realizando dash
    public bool hasDashed;

    //A que lado tiene volteada la animacion
    public int side = 1;

    [Space]
    [Header("Partículas")]
    public ParticleSystem dashParticle;

    public ParticleSystem jumpParticle;
    public ParticleSystem wallJumpParticle;
    public ParticleSystem slideParticle;

    [Space]
    [Header("Controladores De ataques y lugar")]

    //El numero de ataque actual
    public int ataque = 0;

    public bool disparoPotenciado = false;
    private float tiempoEsperaEspada = 0;
    public float tiempoEspecialArriba = 0;
    public int saltos = 0;
    public Boolean combo;

    //Booleanos para saber que ataques/movimientos está realizando y hacia donde está mirando
    public bool derecha;

    public bool bigAtack = false;
    public bool shoot = false;
    public bool onleader = false;
    public bool upattack = false;

    [Space]
    private AudioSource audioSource;

    [Header("Sonidos")]
    public AudioClip ataque1;

    public AudioClip ataque2;
    public AudioClip ataque3;
    public AudioClip dash;
    public AudioClip disparo;
    public AudioClip muerte;
    public AudioClip saltoPared;
    public AudioClip suelo;

    [Space]
    [Header("Botones")]
    //Asignacion de botones
    public string botonDash = "Fire1";

    public string botonDisparo = "Fire2";
    public string botonSaltar = "Jump";
    public string botonEspada = "Fire3";

    private Quaternion quaterion = new Quaternion(0, 0, 0, 0);
    private Vector3 vector = new Vector3();

    // Start is called before the first frame update
    private void Start()
    {
        try
        {
            audioSource = GetComponent<AudioSource>();
            derecha = true;
            coll = GetComponent<Collision>();
            rb = GetComponent<Rigidbody2D>();
            animacion = GetComponentInChildren<AnimationScript>();
            GameObject.FindGameObjectWithTag("Guardar").GetComponent<GuardadoAutomatico>().Load();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        //Si toco el suelo se permite saltar
        if (coll.onGround == true) { saltos = 1; }

        //Esto es un potenciador cuando mi vida es menor a 50
        if (GameObject.FindObjectOfType<PlayerController>().scriptVida.vida < 50)
        {
            ghostFueza.SetActive(true);
            Fuerza();
        }
        else
        {
            ghostFueza.SetActive(false);
        }
        //Si la vida es menor o igual a 0 muere
        if (GameObject.FindObjectOfType<PlayerController>().scriptVida.vida <= 0)
        {
            StartCoroutine(Morir());
        }

        //Establece el movimiento General
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);

        Walk(dir);

        animacion.SetHorizontalMovement(x, y, rb.velocity.y);

        //establece el sonido de la pisada si el personaje se mueve
        if ((x != 0 && y == 0) && Time.time > cooldownSonido && coll.onGround)
        {
            audioSource.PlayOneShot(suelo);
            cooldownSonido = Time.time + 0.3f;
        }

        //Si colisiono con el suelo y no estoy dasheando
        if (coll.onGround && !isDashing)
        {
            wallJumped = false;
            GetComponent<BetterJumping>().enabled = true;
        }

        //Si estoy cogiendome a la pared pero no dasheando, esto es para caer poco a poco
        if (wallGrab && !isDashing)
        {
            rb.gravityScale = 0;
            if (x > .2f || x < -.2f)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }

            float speedModifier = y > 0 ? .5f : 1;

            rb.velocity = new Vector2(rb.velocity.x, y * (speed * speedModifier));
        }
        else
        {
            rb.gravityScale = 3;
        }

        //Si colisiono con la pared pero no con el suelo
        if (coll.onWall && !coll.onGround)
        {
            //Dar la vuelta a al hora de los muros

            if (Time.time > nextFireTime && Input.GetButtonDown(botonDisparo))
            {
                shoot = true;

                //Controlador para saber donde disparar cuando estoy cogiendome a un muro

                vector = derecha ? new Vector3(transform.position.x + 0.9f, transform.position.y + 0.3f, transform.position.z) :
                    new Vector3(transform.position.x - 0.9f, transform.position.y + 0.3f, transform.position.z);

                audioSource.PlayOneShot(disparo);

                Instantiate(bala, vector, quaterion);
                nextFireTime = Time.time + cooldown;
            }

            if (x != 0 && !wallGrab)
            {
                wallSlide = true;
                WallSlide();
            }
        }

        //Si no estoy en colision contra la pared o si estoy en colision con el suelo
        if (!coll.onWall || coll.onGround)
        {
            wallSlide = false;
        }

        //Si pulso saltar
        if (Input.GetButtonDown(botonSaltar))
        {
            //Pulso el trigger en la animacion de saltar
            animacion.SetTrigger("jump");

            //Si estoy en el suelo
            if (coll.onGround)
            {
                //upattack=false;
                Jump(Vector2.up, false);
                audioSource.PlayOneShot(saltoPared);
            }
            if (coll.onWall && !coll.onGround)
            {
                WallJump();
                audioSource.PlayOneShot(saltoPared);
            }
        }

        //Establezco el dash
        // if (Input.GetButtonDown(botondash) && !hasDashed)
        if (Input.GetButtonDown(botonDash) && saltos > 0)
        {
            if (xRaw != 0 || yRaw != 0)
            {
                Dash(xRaw, yRaw);

                audioSource.PlayOneShot(dash);
            }
        }

        //Establezco si toqué el suelo
        if (coll.onGround && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
        }

        if (!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }

        WallParticle(y);

        if (wallGrab || wallSlide || !canMove)
        {
            return;
        }

        //Modifico la animacion para que mire a un lado u otro ademas de la modificacion del Collider del ataque

        if (x > 0)
        {
            side = 1;
            animacion.Flip(side);
            derecha = true;

            //Establece los Collider del Golpe
            colliderDerderecha.SetActive(true);
            colliderIzquierda.SetActive(false);
        }
        else
        if (x < 0)
        {
            derecha = false;
            side = -1;
            animacion.Flip(side);

            //Establece los Collider del Golpe

            colliderDerderecha.SetActive(false);
            colliderIzquierda.SetActive(true);
        }

        //Si no estás en la animacion del daño se permite golpear
        if (!animacion.animacionDaño)
        {
            AtaqueGeneral();
        }
    }

    /// <summary>
    /// Es el controlador de ataques
    /// </summary>
    private void AtaqueGeneral()
    {
        //Establezco el tiempo necesario para disparar
        if (Time.time > nextFireTime && Input.GetButtonDown(botonDisparo) && ataque == 0)
        {
            shoot = true;

            //Establezcola bala en el lugar adecuado segun a donde mire
            vector = derecha ? new Vector3(transform.position.x + 0.9f, transform.position.y + 0.3f, transform.position.z) :
            new Vector3(transform.position.x - 0.9f, transform.position.y + 0.3f, transform.position.z);
            //Audio del disparo
            audioSource.PlayOneShot(disparo);
            //Inizalizacion de la bala
            Instantiate(bala, vector, quaterion);
            nextFireTime = Time.time + cooldown;
        }
        //Ataque de espada fuerte, cuando pulso arriba  
        if (Input.GetButtonDown(botonEspada) && Input.GetAxis("Vertical") > 0 && rb.velocity.x == 0 && coll.onGround)
        {
            upattack = true;
            ataque = 1;
            jumpForce = jumpForce * 1.2f;
            audioSource.PlayOneShot(ataque1);
            Jump(Vector2.up, false);
            jumpForce = jumpForce / 1.2f;
            tiempoEspecialArriba = Time.time + cooldown - 0.1f;
        }

        //combo de la espada, cada vez que pulso un ataque nuevo se ejecuta junto a su sonido
        if (Input.GetButtonDown(botonEspada) && Input.GetAxis("Vertical") == 0 && !upattack && !animacion.anim.GetCurrentAnimatorStateInfo(0).IsName("run") &&
            !animacion.anim.GetCurrentAnimatorStateInfo(0).IsName("runattack") && Input.GetAxis("Horizontal") == 0 && (ataque == 0 || combo))
        {
            switch (ataque)
            {
                case 0:
                    ataque = 1;
                    audioSource.PlayOneShot(ataque1);
                    combo = true;

                    break;

                case 1:
                    ataque = 2;
                    audioSource.PlayOneShot(ataque2);
                    combo = true;
                    break;

                case 2:
                    ataque = 3;
                    audioSource.PlayOneShot(ataque3);
                    combo = true;

                    break;

                case 3:
                    ataque = 0;
                    combo = false;

                    break;
            }
            tiempoEsperaEspada = Time.time + cooldown;
        }
        //Si llevo mas de este tiempo sin pulsar el botón mi ataque se pone en 0 de nuevo
        else if (Time.time > tiempoEsperaEspada - 0.3 && combo == true)
        {
            ataque = 0;
            combo = false;
        }
        else if (Input.GetButtonDown(botonEspada) && Input.GetAxis("Vertical") == 0 && !upattack && Input.GetAxis("Horizontal") != 0 && !combo)
        {
            ataque = 1;
            audioSource.PlayOneShot(ataque1);
            combo = true;
            tiempoEsperaEspada = Time.time + cooldown * 1.5f;
        }
        if (upattack && (Time.time > tiempoEspecialArriba))
        {
            upattack = false;
            ataque = 0;
        }
    }

    /// <summary>
    /// Cuando toco el suelo, se activan las particulas y activan/desactivan los booleanos pertinentes
    /// </summary>
    private void GroundTouch()
    {
        hasDashed = false;
        isDashing = false;
        saltos = 1;

        side = animacion.sr.flipX ? -1 : 1;

        jumpParticle.Play();
    }

    /// <summary>
    /// Realizacion del Dash,  a la direccion deseada junto a la animacion
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void Dash(float x, float y)
    {
        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
        FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));

        hasDashed = true;
        saltos--;

        // anim.SetTrigger("dash");

        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);

        rb.velocity += dir.normalized * dashSpeed;
        StartCoroutine(DashWait());
    }

    /// <summary>
    /// A la vez que se mueve realiza la cola roja
    /// </summary>
    /// <returns></returns>
    private IEnumerator DashWait()
    {
        FindObjectOfType<GhostTrail>().ShowGhost();
        StartCoroutine(GroundDash());
        DOVirtual.Float(14, 0, .8f, RigidbodyDrag);

        dashParticle.Play();
        rb.gravityScale = 0;
        GetComponent<BetterJumping>().enabled = false;
        wallJumped = true;
        isDashing = true;

        yield return new WaitForSeconds(.3f);

        dashParticle.Stop();
        rb.gravityScale = 3;
        GetComponent<BetterJumping>().enabled = true;
        wallJumped = false;
        isDashing = false;
    }

    /// <summary>
    /// Activacion de la animacion de fuerza
    /// </summary>
    private void Fuerza()
    {
        FindObjectOfType<GhostTrail>().ShowGhost();
    }

    private IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        if (coll.onGround)
        {
            hasDashed = false;
        }
    }

    /// <summary>
    /// Permite saltar en la pared
    /// </summary>
    private void WallJump()
    {
        if ((side == 1 && coll.onRightWall) || side == -1 && !coll.onRightWall)
        {
            side *= -1;
            animacion.Flip(side);
        }

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;

        Jump((Vector2.up / 1.5f + wallDir / 1.5f), true);

        wallJumped = true;
    }

    private void WallSlide()
    {
        if (coll.wallSide != side)
        {
            animacion.Flip(side * -1);
        }

        if (!canMove)
        {
            return;
        }

        bool pushingWall = false;
        if ((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb.velocity.x;

        rb.velocity = new Vector2(push, -slideSpeed);
    }

    private void Walk(Vector2 dir)
    {
        if (!canMove)
        {
            return;
        }
        else

        if (wallGrab)
        {
            return;
        }
        else

        if (!wallJumped)
        {
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        }
    }

    //Salto sabiendo si está o no en pared
    private void Jump(Vector2 dir, bool wall)
    {
        slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
        ParticleSystem particle = wall ? wallJumpParticle : jumpParticle;

        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;

        particle.Play();
    }

    private IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    private void RigidbodyDrag(float x)
    {
        rb.drag = x;
    }

    private void WallParticle(float vertical)
    {
        ParticleSystem.MainModule main = slideParticle.main;

        if (wallSlide || (wallGrab && vertical < 0))
        {
            slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
            main.startColor = Color.white;
        }
        else
        {
            main.startColor = Color.clear;
        }
    }

    private int ParticleSide()
    {
        int particleSide = coll.onRightWall ? 1 : -1;
        return particleSide;
    }

    //Activa el objeto biometal para tenerlo visualmente
    public void ActivarBiometal()
    {
        biometal.SetActive(true);
    }

    //desctiva el objeto biometal para no tenerlo visualmente

    public void DesactivarBiometal()
    {
        biometal.SetActive(false);
    }

    /// <summary>
    /// Cuando muere la camara se establece estática y suena la muerte del personaje ademas de destruir todo lo referente al personaje que sea vistoso
    /// </summary>
    /// <returns></returns>
    public IEnumerator Morir()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamaraFollow>().enabled = false;
        this.transform.GetChild(0).parent = null;

        audioSource.PlayOneShot(muerte);
        audioSource.PlayOneShot(muerte);
        audioSource.PlayOneShot(muerte);
        Destroy(GameObject.Find("GhostFuerza"));
        Destroy(GameObject.Find("Visual"));
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}