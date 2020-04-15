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

    private Quaternion rotacion;
    public Vector3 objetivo;
    private float dist;

    // Start is called before the first frame update
    private void Start()
    {
        posicioninicial = transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        objetivo = posicioninicial;

        dist = Vector3.Distance(jugador.transform.position, transform.position);

        //Si da distacia del jugador es menor al radio de vision y
        //Si estamos a la misma altura te sigue
        if (dist < radioDeVision && (jugador.transform.position.y + 1 > transform.position.y && jugador.transform.position.y - 1 < transform.position.y))
        {
            ComprobarPosicionJugadorParaMoverse();

            ComprobarPosicionJugadorParaDisparar();

            ComprobarRotacion();

            if (objetivo == transform.position)
            {
                animator.SetBool("correr", false);
            }
            else
            {
                animator.SetBool("correr", true);
                animator.ResetTrigger("disparar");
            }
        }
        //Si la distancia es mayor al radio de vision
        else
        {
            //rota al enemigo para hacer que vaya a su lugar predefinido

            transform.rotation = (posicioninicial.x > transform.position.x) ? new Quaternion(0, 180, 0, 0) : new Quaternion(0, 0, 0, 0);

            animator.SetBool("correr", true);
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
        if (jugador.transform.position.x > transform.position.x)
        {
            objetivo = new Vector3(jugador.transform.position.x - (radioDeVision / 1.8f), transform.position.y, jugador.transform.position.z);
        }
        else
        {
            objetivo = new Vector3(jugador.transform.position.x + (radioDeVision / 1.8f), transform.position.y, jugador.transform.position.z);
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
                rotacion = new Quaternion(0, 0, 0, 0);
            }
            else
            {
                rotacion = new Quaternion(0, 180, 0, 0);
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
                GameObject balacreada = Instantiate(bala, this.transform.GetChild(0).gameObject.transform.position, transform.rotation);

                balacreada.GetComponent<Rigidbody2D>().AddForce(transform.position);
                tiempoInicia = 0;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radioDeVision);
    }
}