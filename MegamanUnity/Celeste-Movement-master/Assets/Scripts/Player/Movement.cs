using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [Space]
    [Header("Objects")]
    public GameObject bala;

    public GameObject cannon;

    private Collision coll;

    [HideInInspector]
    public Rigidbody2D rb;

    private AnimationScript anim;

    [Space]
    [Header("Stats")]
    public float speed = 10;

    public float jumpForce = 50;
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;
    public float dashSpeed = 20;
    public float cooldown = 2;
    private float nextFireTime = 0;
    private float cooldownsonido = 0;

    [Space]
    [Header("Booleans")]
    public bool canMove;

    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;
    public bool isDashing;

    [Space]
    private bool groundTouch;

    public bool hasDashed;

    public int side = 1;

    [Space]
    [Header("Polish")]
    public ParticleSystem dashParticle;

    public ParticleSystem jumpParticle;
    public ParticleSystem wallJumpParticle;
    public ParticleSystem slideParticle;

    [Space]
    [Header("Controladores")]
    public GameObject colliderderecha;

    public GameObject colliderizquierda;
    public GameObject GhostFuerza;
    public int ataque = 0;

    [Space]
    [Header("De ataques y lugar")]
    public bool derecha;

    public bool bigAtack = false;
    public bool shoot = false;
    public bool onleader = false;
    public bool upattack = false;

    [Space]
    [Header("Sonidos")]
    public AudioClip ataque1;

    public AudioClip ataque2;
    public AudioClip ataque3;
    public AudioClip dash;
    public AudioClip disparo;
    public AudioClip muerte;
    public AudioClip saltopared;
    public AudioClip suelo;
    private AudioSource audioSource;
    private Quaternion quaterion = new Quaternion(0, 0, 0, 0);
    private Vector3 vector = new Vector3();

    private float tiempo_espera_espada = 0;
    public int saltos = 0;


    public string botondash = "Fire1";
    public string botondisparo = "Fire2";
    public string botonsaltar = "Jump";
    public string botonespada = "Fire3";



    public bool canonpotenciado = false;

    public GameObject biometal;





    // Start is called before the first frame update
    private void Start()
    {
        try
        {
            audioSource = GetComponent<AudioSource>();
            derecha = true;
            coll = GetComponent<Collision>();
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponentInChildren<AnimationScript>();
            GameObject.FindGameObjectWithTag("Guardar").GetComponent<GuardadoAutomatico>().Load();
        }
        catch (Exception e) { }
    }

    // Update is called once per frame
    private void Update()
    {
        if (coll.onGround == true) { saltos = 1; }



        //Esto es un potenciador cuando mi vida es menor a 50
        if (GameObject.FindObjectOfType<PlayerController>().scriptvida.vida < 50)
        {
            GhostFuerza.SetActive(true);
            Fuerza();
        }
        else
        {
            GhostFuerza.SetActive(false);
        }

        if (/*GameObject.FindObjectOfType<PlayerController>().scriptvida.vida > -10 &&*/ GameObject.FindObjectOfType<PlayerController>().scriptvida.vida <= 0)
        {
            StartCoroutine(Morir());
            /* GameObject.FindObjectOfType<PlayerController>().scriptvida.vida = -15;*/
        }

        //Establece el movimiento General
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);

        Walk(dir);

        anim.SetHorizontalMovement(x, y, rb.velocity.y);

        if ((x != 0 && y == 0) && Time.time > cooldownsonido && coll.onGround)
        {
            audioSource.PlayOneShot(suelo);
            cooldownsonido = Time.time + 0.3f;
        }

        //Si colisiono con el suelo y no estoy dasheando
        if (coll.onGround && !isDashing)
        {
            wallJumped = false;
            GetComponent<BetterJumping>().enabled = true;
        }

        //Si estoy cogiendome a la pared pero no dasheando
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

            if (Time.time > nextFireTime && Input.GetButtonDown(botondisparo))
            {
                shoot = true;

                //Controlador para saber donde disparar cuando estoy cogiendome a un muro

                vector = derecha ? new Vector3(transform.position.x + 0.9f, transform.position.y + 0.3f, transform.position.z) : new Vector3(transform.position.x - 0.9f, transform.position.y + 0.3f, transform.position.z);

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
        if (Input.GetButtonDown(botonsaltar))
        {
            //Pulso el trigger en la animacion de saltar
            anim.SetTrigger("jump");

            //Si estoy en el suelo
            if (coll.onGround)
            {
                //upattack=false;
                Jump(Vector2.up, false);
                audioSource.PlayOneShot(saltopared);
            }
            if (coll.onWall && !coll.onGround)
            {
                WallJump();
                audioSource.PlayOneShot(saltopared);
            }
        }

        //Establezco el dash
        // if (Input.GetButtonDown(botondash) && !hasDashed)
        if (Input.GetButtonDown(botondash) && saltos > 0)
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

        //Modifico la animacion para que mire a un lado u otro ademas de la modificacion del Collider

        if (x > 0)
        {
            side = 1;
            anim.Flip(side);
            derecha = true;

            //Establece los Collider del Golpe
            colliderderecha.SetActive(true);
            colliderizquierda.SetActive(false);
        }
        else
        if (x < 0)
        {
            derecha = false;
            side = -1;
            anim.Flip(side);

            //Establece los Collider del Golpe

            colliderderecha.SetActive(false);
            colliderizquierda.SetActive(true);
        }

        //Establezco el tiempo necesario para disparar
        if (Time.time > nextFireTime && Input.GetButtonDown(botondisparo))
        {
            shoot = true;

            //Establezcola bala en el lugar adecuado segun a donde mire
            vector = derecha ? new Vector3(transform.position.x + 0.9f, transform.position.y + 0.3f, transform.position.z) : new Vector3(transform.position.x - 0.9f, transform.position.y + 0.3f, transform.position.z);

            audioSource.PlayOneShot(disparo);

            Instantiate(bala, vector, quaterion);
            nextFireTime = Time.time + cooldown;
        }

        if (Input.GetButtonDown(botonespada) && y > 0)
        {
            upattack = true;
        }

        //combo de la espada
        if (Input.GetButtonDown(botonespada))
        {
            switch (ataque)
            {
                case 0:
                    ataque = 1;
                    audioSource.PlayOneShot(ataque1);

                    break;

                case 1:
                    ataque = 2;
                    audioSource.PlayOneShot(ataque2);

                    break;

                case 2:
                    ataque = 3;
                    audioSource.PlayOneShot(ataque3);

                    break;

                case 3:
                    ataque = 0;

                    break;
            }
            tiempo_espera_espada = Time.time + cooldown;
        }
        //Si llevo mas de este tiempo sin pulsar el botón mi ataque se pone en 0 de nuevo
        else if (Time.time > tiempo_espera_espada - 0.3)
        {
            ataque = 0;
        }
    }

    private void GroundTouch()
    {
        hasDashed = false;
        isDashing = false;
        saltos = 1;

        side = anim.sr.flipX ? -1 : 1;

        jumpParticle.Play();
    }

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

    private void Fuerza()
    {
        //Camera.main.transform.DOComplete();
        //Camera.main.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
        //FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));
        FindObjectOfType<GhostForce>().ShowGhost();
    }

    private IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        if (coll.onGround)
        {
            hasDashed = false;
        }
    }

    private void WallJump()
    {
        if ((side == 1 && coll.onRightWall) || side == -1 && !coll.onRightWall)
        {
            side *= -1;
            anim.Flip(side);
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
            anim.Flip(side * -1);
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
    public void activarBiometal()
    {
        biometal.SetActive(true);


    }
    public void desactivarBiometal()
    {

        biometal.SetActive(false);


    }

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