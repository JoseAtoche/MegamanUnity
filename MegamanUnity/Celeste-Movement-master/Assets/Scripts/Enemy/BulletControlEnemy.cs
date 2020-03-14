using UnityEngine;

public class BulletControlEnemy : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody2D bulletRB;
    public float bulletSpeed;
    public float bulletLife;
    Vector3 posicioninicial;


    void Awake()
    {
        bulletRB = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        posicioninicial = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, posicioninicial, bulletSpeed);
        Destroy(gameObject, bulletLife);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
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
