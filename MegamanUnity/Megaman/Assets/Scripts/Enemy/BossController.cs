using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;

    public Slider heartBar;

    public bool invulnerable;

    public Entity_life scriptVida;

    public AudioClip ataque1;
    public AudioClip ataque2;
    public AudioClip ataque3;
    public AudioClip ataque4;
    public AudioClip ataque5;
    public AudioClip ataque6;

    public GameObject objetoPadre;
    private Vector3 posicionPrincipal;

    private bool primeraVez = false;

    public GameObject carabela1;
    public GameObject carabela2;
    public GameObject carabela3;
    public GameObject carabela4;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        posicionPrincipal = objetoPadre.transform.position;
        heartBar.maxValue = scriptVida.vida;
    }

    /// <summary>
    /// movimientos aleatorios
    /// </summary>
    [System.Obsolete]
    private void Update()
    {
        //La barra de vida se establece a la vida actual del boss
        heartBar.value = scriptVida.vida;

        //Si la vida es menor a 50 le pongo una animacion y fuerza
        if (scriptVida.vida < 10)
        {
            Power();
        }

        //Si la animacion es la llamada acabada
        //se teletransporta unos segundos lejos, para poder permitir tiempo de reaccion al jugador
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Acabado") || anim.GetCurrentAnimatorStateInfo(0).IsName("EsperarUnMomento"))
        {
            objetoPadre.transform.position = new Vector3(objetoPadre.transform.position.x + 200, objetoPadre.transform.position.y + 200, objetoPadre.transform.position.z);
            primeraVez = false;
            carabela1.SetActive(false);
            carabela2.SetActive(false);
            carabela3.SetActive(false);
            carabela4.SetActive(false);
            carabela1.transform.position = posicionPrincipal;
            carabela2.transform.position = posicionPrincipal;
            carabela3.transform.position = posicionPrincipal;
            carabela4.transform.position = posicionPrincipal;

            this.transform.GetChild(3).gameObject.SetActive(false);
        }

        //Si acaba la animacion de Acabado vuelve a su posicion original
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Fin") && anim.GetBool("burstbool") != true)
        {
            objetoPadre.transform.position = posicionPrincipal;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Intro"))
        {
            Intro();
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("ScytheThornadus"))
        {
            Thornadus();
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Evil Waltz"))
        {
            EvilWaltz();
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("scytheFall"))
        {
            Scythe();
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Quartet BurstIntro"))
        {
            QuartetBurst();
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hail Nocturne"))
        {
            HailNocturne();
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Guillotine"))
        {
            Guillotine();
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
    }

    /// <summary>
    /// Estado de introduccion
    /// </summary>
    private void Intro()
    {
        objetoPadre.transform.position = posicionPrincipal;
    }

    /// <summary>
    /// Estado del tornado, se mueve hacia arriba, siempre se pone a la derecha del jugador para atacarle
    /// </summary>
    private void Thornadus()
    {
        if (!primeraVez)
        {
            objetoPadre.transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x + 3, objetoPadre.transform.position.y, objetoPadre.transform.position.z);

            primeraVez = true;
            audioSource.PlayOneShot(ataque5);
        }
    }

    /// <summary>
    /// Estado del Evil Waltz es un ataque que se pone bajo del  escenario para atacar
    /// </summary>
    private void EvilWaltz()
    {
        if (!primeraVez)
        {
            objetoPadre.transform.position = posicionPrincipal;

            primeraVez = true;
        }
    }

    /// <summary>
    /// Se pone encima del jugador para atacarle
    /// </summary>
    private void Scythe()
    {
        if (!primeraVez)
        {
            objetoPadre.transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, objetoPadre.transform.position.y, objetoPadre.transform.position.z);

            primeraVez = true;
        }
    }

    /// <summary>
    /// Ataque que activa 4 carabelas que se mueven a las posiciones incicadas para atacar
    /// </summary>
    [System.Obsolete]
    private void QuartetBurst()
    {
        if (!primeraVez)
        {
            objetoPadre.transform.position = new Vector3(posicionPrincipal.x, posicionPrincipal.y - 2, posicionPrincipal.z);

            primeraVez = true;

            carabela1.SetActive(true);
            carabela2.SetActive(true);
            carabela3.SetActive(true);
            carabela4.SetActive(true);
            this.transform.GetChild(3).gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Ataque que se realiza desde el centro de la pantalla
    /// </summary>
    private void HailNocturne()
    {
        if (!primeraVez)
        {
            objetoPadre.transform.position = posicionPrincipal;
            primeraVez = true;
        }
    }

    /// <summary>
    /// Se acerca ak jugador y suena el ataque
    /// </summary>
    private void Guillotine()
    {
        if (!primeraVez)
        {
            objetoPadre.transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, objetoPadre.transform.position.y, objetoPadre.transform.position.z);
            primeraVez = true;
            audioSource.PlayOneShot(ataque2);
        }
    }

    /// <summary>
    /// Se pone en rojo para mostrar la vida
    /// </summary>
    private void Power()
    {
        GetComponentInChildren<GhostBoss>().ShowGhost();
    }

    /// <summary>
    /// Hace sonar el sonido de la Guillotina
    /// </summary>
    public void SonidoGuillotine()
    {
        if (ataque2 == null)
        {
            audioSource.PlayOneShot(GameObject.FindGameObjectWithTag("objetos").GetComponent<objetosNecesarios>().ataque2);
        }
        else
        {
            audioSource.PlayOneShot(ataque2);
        }




    }

    /// <summary>
    /// Hace sonar el sonido del ataque de la carabela
    /// </summary>

    public void SonidoAtaqueCarabela()
    {
        if (ataque2 == null)
        {
            audioSource.PlayOneShot(GameObject.FindGameObjectWithTag("objetos").GetComponent<objetosNecesarios>().ataque1);
        }
        else
        {
            audioSource.PlayOneShot(ataque1);
        }
    }
}