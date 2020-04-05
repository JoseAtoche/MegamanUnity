using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float radiodevision;
    public float velocidad;
    public GameObject jugador;
    private Vector3 posicioninicial;
    private Animator animator;
    public GameObject bala;
    public float tiempoespera = 2;
    public float tiempoinicia = 0;

    Quaternion rotacion;
    public Vector3 objetivo;
    float dist;
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

        //Si da distacia del jugador es menor al radio de vision
        //Si estamos a la misma altura te sigue
        if (dist < radiodevision && (jugador.transform.position.y + 1 > transform.position.y && jugador.transform.position.y - 1 < transform.position.y))
        {
            comprobarPosicionJugadorParaMoverse();

            comprobarPosicionJugadorParaDisparar();

            comprobarRotacion();

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

    //Transformo la rotacion del cuerpo segun si el jugador está a la derecha o izquierda

    private void comprobarPosicionJugadorParaMoverse()
    {



        if (jugador.transform.position.x > transform.position.x)
        {

            objetivo = new Vector3(jugador.transform.position.x - (radiodevision / 1.8f), transform.position.y, jugador.transform.position.z);
        }
        else
        {

            objetivo = new Vector3(jugador.transform.position.x + (radiodevision / 1.8f), transform.position.y, jugador.transform.position.z);
        }



    }
    private void comprobarRotacion()
    {
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

    //Si la distancia es menor al radio entre 2 debe disparar
    private void comprobarPosicionJugadorParaDisparar()
    {

        if (dist < radiodevision / 1.78f)
        {

            tiempoinicia += Time.deltaTime;
            if (tiempoinicia >= tiempoespera)
            {

                animator.SetBool("correr", false);
                animator.SetTrigger("disparar");
                GameObject balacreada = Instantiate(bala, this.transform.GetChild(0).gameObject.transform.position, transform.rotation);

                balacreada.GetComponent<Rigidbody2D>().AddForce(transform.position);
                tiempoinicia = 0;
            }

        }



    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radiodevision);
    }
}