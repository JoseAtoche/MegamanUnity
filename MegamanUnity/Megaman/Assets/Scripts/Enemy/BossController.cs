using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;

    public float velocidad;
    public Slider heartBar;

    public bool invulnerable;

    public Entity_life scriptVida;

    public float introDuration = 0.5f;

    public float thornadusDuration = 0.40f;
    public float evilWaltzDuration = 0.40f;
    public float scytheDuration = 0.4f;
    public float quartetBurstDuration = 0.40f;
    public float guillotineDuration = 0.40f;
    public float nocturneDuration = 0.40f;
    public AudioClip ataque1;
    public AudioClip ataque2;
    public AudioClip ataque3;
    public AudioClip ataque4;
    public AudioClip ataque5;
    public AudioClip ataque6;

    public float time = 0;

    public GameObject objetoPadre;
    private Vector3 posicionPrincipal;

    private bool primeraVez = false;

    private bool cortinaComenzada = false;

    public GameObject carabela1;
    public GameObject carabela2;
    public GameObject carabela3;
    public GameObject carabela4;
    private AudioSource audioSource;

    public enum State
    {
        INTRO,
        THORNADUS,
        WALTZ,
        SCYTHE,
        BURST,
        NOCTURNE,
        GUILLOTINE
    }

    public State state = State.THORNADUS;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        posicionPrincipal = objetoPadre.transform.position;
    }

    /// <summary>
    /// Pequeño  patron de estados
    /// </summary>
    [System.Obsolete]
    private void Update()
    {
        //La barra de vida se establece a la vida actual del boss
        heartBar.value = scriptVida.vida;

        //Si la vida es menor a 50 le pongo una animacion y fuerza
        if (scriptVida.vida < 50)
        {
            Power();
        }

        time += Time.deltaTime;

        //Estado
        switch (state)
        {
            case State.INTRO:
                // do intro stuff
                Intro();
                break;

            case State.THORNADUS:
                Thornadus();
                break;

            case State.WALTZ:

                EvilWaltz();
                break;

            case State.SCYTHE:

                Scythe();
                break;

            case State.BURST:

                QuartetBurst();
                break;

            case State.NOCTURNE:

                HailNocturne();
                break;

            case State.GUILLOTINE:

                Guillotine();
                break;
        }
    }

    /// <summary>
    /// Estado de introduccion
    /// </summary>
    private void Intro()
    {
        if (time < introDuration)
        {
            objetoPadre.transform.position = posicionPrincipal;
        }
        else
        {
            Aleatorio();
        }
    }

    /// <summary>
    /// Estado del tornado, se mueve hacia arriba, siempre se pone a la derecha del jugador para atacarle
    /// </summary>
    private void Thornadus()
    {
        if (time < thornadusDuration)
        {
            objetoPadre.transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x + 3, objetoPadre.transform.position.y, objetoPadre.transform.position.z);

            anim.SetBool("Finalizado", false);

            if (!primeraVez)
            {
                primeraVez = true;
                anim.SetTrigger("thornadus");
                audioSource.PlayOneShot(ataque5);
            }

            state = State.THORNADUS;
        }
        else
        {
            Aleatorio();
        }
    }

    /// <summary>
    /// Estado del Evil Waltz es un ataque que se pone bajo del  escenario para atacar
    /// </summary>
    private void EvilWaltz()
    {
        if (time < evilWaltzDuration)
        {
            objetoPadre.transform.position = posicionPrincipal;
            anim.SetBool("Finalizado", false);

            if (!primeraVez)
            {
                primeraVez = true;
                anim.SetTrigger("waltz");
            }

            state = State.WALTZ;
        }
        else
        {
            Aleatorio();
        }
    }

    /// <summary>
    /// Se pone encima del jugador para atacarle
    /// </summary>
    private void Scythe()
    {
        if (time < scytheDuration)
        {
            objetoPadre.transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, objetoPadre.transform.position.y, objetoPadre.transform.position.z);

            anim.SetBool("Finalizado", false);

            if (!primeraVez)
            {
                primeraVez = true;
                anim.SetTrigger("scythe");
            }

            state = State.SCYTHE;
        }
        else
        {
            Aleatorio();
        }
    }

    /// <summary>
    /// Ataque que activa 4 carabelas que se mueven a las posiciones incicadas para atacar
    /// </summary>
    [System.Obsolete]
    private void QuartetBurst()
    {
        if (time < quartetBurstDuration)
        {
            objetoPadre.transform.position = posicionPrincipal;

            anim.SetBool("Finalizado", false);

            if (!primeraVez)
            {
                primeraVez = true;
                anim.SetTrigger("burst");

                carabela1.SetActive(true);
                carabela2.SetActive(true);
                carabela3.SetActive(true);
                carabela4.SetActive(true);
            }
            if (carabela1.active == true)
            {
                carabela1.transform.position = Vector3.MoveTowards(carabela1.transform.position, new Vector3(137.46f, -8.13f, 0), 0.5f);
            }
            if (carabela2.active == true)
            {
                carabela2.transform.position = Vector3.MoveTowards(carabela2.transform.position, new Vector3(152.71f, -8.13f, 0), 0.5f);
            }
            if (carabela3.active == true)
            {
                carabela3.transform.position = Vector3.MoveTowards(carabela3.transform.position, new Vector3(152.71f, -15.21f, 0), 0.5f);
            }
            if (carabela4.active == true)
            {
                carabela4.transform.position = Vector3.MoveTowards(carabela4.transform.position, new Vector3(137.46f, -15.21f, 0), 0.5f);
            }

            state = State.BURST;
        }
        else
        {
            carabela1.SetActive(false);
            carabela2.SetActive(false);
            carabela3.SetActive(false);
            carabela4.SetActive(false);

            Aleatorio();
        }
    }

    /// <summary>
    /// Ataque que se realiza desde el centro de la pantalla
    /// </summary>
    private void HailNocturne()
    {
        if (time < nocturneDuration)
        {
            objetoPadre.transform.position = posicionPrincipal;

            anim.SetBool("Finalizado", false);

            if (!primeraVez)
            {
                primeraVez = true;
                anim.SetTrigger("nocturne");
            }
            state = State.NOCTURNE;
        }
        else
        {
            Aleatorio();
        }
    }

    private void Guillotine()
    {
        if (time < guillotineDuration)
        {
            objetoPadre.transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, objetoPadre.transform.position.y, objetoPadre.transform.position.z);
            anim.SetBool("Finalizado", false);

            if (!primeraVez)
            {
                primeraVez = true;
                anim.SetTrigger("guillotine");
            }
            state = State.GUILLOTINE;
        }
        else
        {
            Aleatorio();
        }
    }

    /// <summary>
    /// Es un randomizador, hace que el enemigo ejecute otro ataque, siempre y cuando no sea el mismo que acaba de realizar
    /// </summary>
    private void Aleatorio()
    {
        primeraVez = false;

        anim.SetBool("Finalizado", true);
        anim.ResetTrigger("thornadus");
        anim.ResetTrigger("guillotine");
        anim.ResetTrigger("burst");
        anim.ResetTrigger("scythe");
        anim.ResetTrigger("waltz");
        anim.ResetTrigger("nocturne");
        objetoPadre.transform.position = new Vector3(1000, 1000, 0);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Acabado"))
        {
            time = 0;
            if (!cortinaComenzada)
            {
                // StartCoroutine(EsperarUnSegundo());
                switch (UnityEngine.Random.Range(0, 6))
                {
                    case 0:
                        if (state != State.BURST)
                        {
                            state = State.BURST;
                        }
                        else
                        {
                            Aleatorio();
                        }

                        break;

                    case 1:
                        if (state != State.GUILLOTINE)
                        {
                            state = State.GUILLOTINE;
                        }
                        else
                        {
                            Aleatorio();
                        }

                        break;

                    case 2:
                        if (state != State.NOCTURNE)
                        {
                            state = State.NOCTURNE;
                        }
                        else
                        {
                            Aleatorio();
                        }

                        break;

                    case 3:
                        //if (state != State.SCYTHE)
                        //{
                        //    state = State.SCYTHE;
                        //}
                        //else { Aleatorio(); }
                        Aleatorio();

                        break;

                    case 4:
                        if (state != State.THORNADUS)
                        {
                            state = State.THORNADUS;
                        }
                        else
                        {
                            Aleatorio();
                        }

                        break;

                    case 5:
                        if (state != State.WALTZ)
                        {
                            state = State.WALTZ;
                        }
                        else
                        {
                            Aleatorio();
                        }

                        break;
                }
            }
        }
    }

    private void Power()
    {
        GetComponentInChildren<GhostBoss>().ShowGhost();
    }

    //private IEnumerator DashWait()
    //{
    //    yield return new WaitForSeconds(.3f);
    //}

    private IEnumerator EsperarUnSegundo()
    {
        cortinaComenzada = true;

        yield return new WaitForSeconds(1f);

        cortinaComenzada = false;
    }
}