using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float radiodevision;
    public float velocidad;
    public GameObject jugador;
    private Vector3 posicioninicial;
    private Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        posicioninicial = transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 objetivo = posicioninicial;
        Quaternion rotacion;
        float dist = Vector3.Distance(jugador.transform.position, transform.position);

        //Si da distacia del jugador es menor al radio de vision
        //Si estamos a la misma altura te sigue
        if (dist < radiodevision && (jugador.transform.position.y + 1 > transform.position.y && jugador.transform.position.y - 1 < transform.position.y))
        {
            //Transformo la rotacion del cuerpo segun si el jugador está a la derecha o izquierda

            if (jugador.transform.position.x > transform.position.x)
            {
                rotacion = new Quaternion(0, 180, 0, 0);
                objetivo = new Vector3(jugador.transform.position.x + (radiodevision / 1.2f), jugador.transform.position.y, jugador.transform.position.z);
            }
            else
            {
                rotacion = new Quaternion(0, 0, 0, 0);
                objetivo = new Vector3(jugador.transform.position.x - (radiodevision / 1.2f), jugador.transform.position.y, jugador.transform.position.z);
            }

            //Si la distancia es menor al radio entre 2 debe disparar

            if (dist < radiodevision / 1.2f)
            {
                animator.SetBool("correr", false);
                animator.SetBool("disparar", true);
            }
            else
            {
                //Y establezco al enemigo como corriendo
                animator.SetBool("correr", true);
                transform.rotation = rotacion;
            }
        }
        //Si la distancia es mayor al radio de vision
        else
        {
            //rota al enemigo para hacer que vaya a su lugar predefinido

            transform.rotation = (posicioninicial.x > transform.position.x) ? new Quaternion(0, 180, 0, 0) : new Quaternion(0, 0, 0, 0);

            //Establecemos el disparar a false
            animator.SetBool("disparar", false);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radiodevision);
    }
}