using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public Rigidbody2D bulletRB;
    public float bulletSpeed;
    public float bulletLife;

    private void Awake()
    {
        bulletRB = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Movement movimiento = player.GetComponent<Movement>();
        if ((movimiento.side == -1 && !movimiento.wallSlide) || (movimiento.side == 1 && movimiento.wallSlide))
        {
            bulletRB.velocity = new Vector2(-bulletSpeed, bulletRB.velocity.y);
        }
        if ((movimiento.side == 1 && !movimiento.wallSlide) || (movimiento.side == -1 && movimiento.wallSlide))
        {
            bulletRB.velocity = new Vector2(bulletSpeed, bulletRB.velocity.y);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        Destroy(gameObject, bulletLife);
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Entity_life enemy = hitInfo.GetComponent<Entity_life>();
        if (enemy != null)
        {
            if (!enemy.GetComponent<Entity_life>().invulnerable && enemy.tag != "escudo")
            {
                enemy.GetComponent<Entity_life>().StopAllCoroutines();
                enemy.GetComponent<Entity_life>().invulnerable = true;
                enemy.GetComponent<Entity_life>().Invoke("UndoInvincible", 2);

                enemy.vida -= (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Entity_life>().vida <= 50) ? 3 : 2;

                enemy.GetComponent<Entity_life>().StartCoroutine(enemy.GetComponent<Entity_life>().FlashSprite());
            }
            else
            {
                enemy.danioparaescudo(this.transform.position.x);




            }

            Destroy(gameObject);
        }

        //  Instantiate(impactEffect, transform.position, transform.rotation);


    }
}