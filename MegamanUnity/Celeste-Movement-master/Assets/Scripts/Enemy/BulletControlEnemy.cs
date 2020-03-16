using UnityEngine;

public class BulletControlEnemy : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody2D bulletRB;
    public float bulletSpeed;
    public float bulletLife;
    private Vector3 posicioninicial;

    private void Awake()
    {
        bulletRB = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        posicioninicial = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, posicioninicial, bulletSpeed);
        Destroy(gameObject, bulletLife);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {




        if (hitInfo == GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<BoxCollider2D>())
        {
            Entity_life entity_jugador = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Entity_life>();
            if (!entity_jugador.invulnerable)
            {

                entity_jugador.StopAllCoroutines();
                entity_jugador.invulnerable = true;
                entity_jugador.Invoke("UndoInvincible", 2);
                entity_jugador.vida -= 1;
                entity_jugador.StartCoroutine(entity_jugador.FlashSprite());
                Destroy(gameObject);



            }



        }



    }

}
