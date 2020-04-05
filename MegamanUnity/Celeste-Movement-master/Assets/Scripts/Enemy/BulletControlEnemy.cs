using UnityEngine;

public class BulletControlEnemy : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody2D bulletRB;
    public float bulletSpeed;
    public float bulletLife;
    private Vector3 posiciondeseada;
    float y;
    private void Awake()
    {
        bulletRB = GetComponent<Rigidbody2D>();

        this.transform.rotation = new Quaternion(0, 0, 0, 0);


        if (transform.position.x > GameObject.FindGameObjectWithTag("Player").transform.position.x)
        {
            float m = (GameObject.FindGameObjectWithTag("Player").transform.position.y - transform.position.y) / (GameObject.FindGameObjectWithTag("Player").transform.position.x - transform.position.x);



            posiciondeseada = new Vector3(-2000, (m * -2000) + (GameObject.FindGameObjectWithTag("Player").transform.position.x), GameObject.FindGameObjectWithTag("Player").transform.position.z);

        }
        else
        {
            float m = (transform.position.y - GameObject.FindGameObjectWithTag("Player").transform.position.y) / (transform.position.x - GameObject.FindGameObjectWithTag("Player").transform.position.x);




            posiciondeseada = new Vector3(2000, (m * 2000) + (GameObject.FindGameObjectWithTag("Player").transform.position.x), GameObject.FindGameObjectWithTag("Player").transform.position.z);
        }
    }



    // Update is called once per frame
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, posiciondeseada, bulletSpeed);
        //este comando me obliga a poner 1. donde estoy 2. donde voy 3. la velocidad


        Destroy(gameObject, bulletLife);
        //y este me dice cuanto tiempo vivo
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