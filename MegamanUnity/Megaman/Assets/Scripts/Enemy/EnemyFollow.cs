using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float radioDeVision;
    public float velocidad;
    public GameObject jugador;
    private Vector3 posicioninicial;
    private Animator animator;
    public GameObject bala;
    public float tiempoEspera = 2;
    public float tiempoInicia = 0;

    public Quaternion rotacion;
    public Vector3 objetivo;
    private float dist;

    public bool moverderecha = true;

    public bool moverizquierda = true;


    public LayerMask groundLayer;
    public Vector2 rightOffset, leftOffset;
    public float collisionRadius = 0.25f;

    public AudioSource audioSource;

    public AudioClip disparoSonido;
    public AudioClip muerteSonido;
    public AudioClip caminarSonido;





    // Start is called before the first frame update
    private void Start()
    {
        posicioninicial = transform.position;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (this.transform.GetComponent<Entity_life>().vida <= 0)
        {

            this.enabled = false;

            this.transform.GetComponent<Entity_life>().colisionReal = null;
            audioSource.PlayOneShot(muerteSonido);

        }
        objetivo = posicioninicial;

        dist = Vector3.Distance(jugador.transform.position, transform.position);

        //Si la distacia del jugador es menor al radio de vision y
        //Si estamos a la misma altura te sigue
        if (dist < radioDeVision && (jugador.transform.position.y + 1 > transform.position.y && jugador.transform.position.y - 1 < transform.position.y))
        {
            ComprobarPosicionJugadorParaMoverse();

            ComprobarPosicionJugadorParaDisparar();

            ComprobarRotacion();
            CalcularMovimiento();

            if (objetivo == transform.position)
            {
                animator.SetBool("correr", false);
            }
            else
            {
                animator.SetBool("correr", true);
                //     audioSource.PlayOneShot(caminarSonido);

                animator.ResetTrigger("disparar");
            }
        }
        //Si la distancia es mayor al radio de vision
        else
        {
            //rota al enemigo para hacer que vaya a su lugar predefinido

            transform.rotation = (posicioninicial.x > transform.position.x) ? new Quaternion(0, 180, 0, 0) : new Quaternion(0, 0, 0, 0);

            animator.SetBool("correr", true);
            //    audioSource.PlayOneShot(caminarSonido);

            animator.ResetTrigger("disparar");
        }

        //Si la posicion es la misma que la incial dejamos de correr
        if (transform.position.x == posicioninicial.x)
        {
            animator.SetBool("correr", false);
        }

        float fixedSpeed = velocidad * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, objetivo, fixedSpeed);

        transform.position = new Vector3(transform.position.x, posicioninicial.y, transform.position.z);

        Debug.DrawLine(transform.position, objetivo, Color.green);
    }

    /// <summary>
    /// Transformo la rotacion del cuerpo segun si el jugador está a la derecha o izquierda
    /// </summary>

    private void ComprobarPosicionJugadorParaMoverse()
    {



        //Aqui debemos comprobar distancia hasta el borde
        if (jugador.transform.position.x > transform.position.x)
        {
            if (moverizquierda)
            {
                objetivo = new Vector3(jugador.transform.position.x - (radioDeVision / 1.8f), transform.position.y, jugador.transform.position.z);
            }


        }
        else
        {
            if (moverderecha)
            {
                objetivo = new Vector3(jugador.transform.position.x + (radioDeVision / 1.8f), transform.position.y, jugador.transform.position.z);

            }

        }
    }

    /// <summary>
    /// Comprueba la rotacion del enemigo, para mirar a un lado u otro segun esté el jugador
    /// </summary>
    private void ComprobarRotacion()
    {
        //Si está en el punto deseado se para a mirar al jugador
        if (objetivo.x == transform.position.x)
        {
            if (jugador.transform.position.x > transform.position.x)
            {
                rotacion = new Quaternion(0, 180, 0, 0);
            }
            else
            {
                rotacion = new Quaternion(0, 0, 0, 0);
            }
        }
        else
        {
            //Si no ha llegado al punto calculado mira al punto para dar la sensacion de que anda
            if (objetivo.x < transform.position.x)
            {
                if (moverizquierda)
                {
                    rotacion = new Quaternion(0, 0, 0, 0);
                }
            }
            else
            {
                if (moverderecha)
                {
                    rotacion = new Quaternion(0, 180, 0, 0);
                }
            }
            animator.ResetTrigger("disparar");
        }

        transform.rotation = rotacion;
    }

    /// <summary>
    /// Comprueba si el objetivo está algo más cerca de la mitad de su radio de vision para dispararle
    /// </summary>
    private void ComprobarPosicionJugadorParaDisparar()
    {
        if (dist < radioDeVision / 1.78f)
        {
            tiempoInicia += Time.deltaTime;
            if (tiempoInicia >= tiempoEspera)
            {
                animator.SetBool("correr", false);
                animator.SetTrigger("disparar");
                GameObject balacreada = Instantiate(bala, this.transform.GetChild(0).gameObject.transform.position, this.transform.GetChild(0).gameObject.transform.rotation);
                audioSource.PlayOneShot(disparoSonido);

                //balacreada.GetComponent<Rigidbody2D>().AddForce(this.transform.GetChild(0).gameObject.transform.position * balacreada.GetComponent<BulletControl>().bulletSpeed);
                tiempoInicia = 0;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioDeVision);
    }

    private void CalcularMovimiento()
    {

        if (jugador.transform.position.x > transform.position.x)
        {
            moverderecha = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
            moverizquierda = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);


        }
        else
        {
            moverizquierda = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
            moverderecha = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);
        }






    }
}



