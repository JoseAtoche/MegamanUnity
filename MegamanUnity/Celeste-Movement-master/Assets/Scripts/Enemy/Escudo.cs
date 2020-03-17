using UnityEngine;

public class Escudo : MonoBehaviour
{
    public float velocidad;
    private Animator animator;
    private Vector3 objetivo;
    private Vector3 posicionfinal;
    private Vector3 posicioninicial;
    public bool derecha;

    // Start is called before the first frame update
    private void Start()
    {
        posicioninicial = transform.position;
        animator = GetComponent<Animator>();
        posicionfinal = new Vector3(posicioninicial.x - 3, posicioninicial.y, posicioninicial.z);
    }

    // Update is called once per frame
    private void Update()
    {
        if (transform.position == posicioninicial)
        {

            derecha = false;
            //  animator.SetBool("Andando", false);
            animator.SetTrigger("GiroIzquierda");
            animator.ResetTrigger("GiroDerecha");
            objetivo = posicionfinal;
        }
        else if (transform.position == posicionfinal)
        {
            derecha = true;

            //   animator.SetBool("Andando", false);
            animator.SetTrigger("GiroDerecha");
            animator.ResetTrigger("GiroIzquierda");

            objetivo = posicioninicial;
        }

        animator.SetBool("Andando", true);

        float fixedSpeed = velocidad * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, objetivo, fixedSpeed);

        transform.position = new Vector3(transform.position.x, posicioninicial.y, transform.position.z);
    }






}