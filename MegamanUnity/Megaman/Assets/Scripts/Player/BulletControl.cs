using UnityEngine;


/// <summary>
/// Se encarga de controlar la bala del JUGADOR
/// </summary>
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
        //Esto hace que la bala vaya a la direccion que mira y controla tanto sobre el suelo como si está contra la pared
        if ((movimiento.side == -1 && !movimiento.wallSlide) || (movimiento.side == 1 && movimiento.wallSlide))
        {
            bulletRB.velocity = new Vector2(-bulletSpeed, bulletRB.velocity.y);
        }else 
        if ((movimiento.side == 1 && !movimiento.wallSlide) || (movimiento.side == -1 && movimiento.wallSlide))
        {
            bulletRB.velocity = new Vector2(bulletSpeed, bulletRB.velocity.y);
        }
    }

    /// <summary>
    /// Elimina la bala en X tiempo
    /// </summary>
    private void Update()
    {
        Destroy(gameObject, bulletLife);
    }

    /// <summary>
    /// Al entrar en el trigger detecta que tipo de entidad es, para atacar de un modo u otro
    /// </summary>
    /// <param name="hitInfo"></param>
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Entity_life enemy = hitInfo.GetComponent<Entity_life>();
        if (enemy != null)
        {//Si la entidad es una de las carabelas del jefe
            if (enemy.name == "Carabela 1" || enemy.name == "Carabela 2" || enemy.name == "Carabela 3" || enemy.name == "Carabela 4")
            {
                enemy.vida -= (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Entity_life>().vida <= 50) ? (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().disparoPotenciado) ? 3 * 2 : 3 : (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().disparoPotenciado) ? 2 * 2 : 2;
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().disparoPotenciado = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().DesactivarBiometal();
            }
            //Si la entidad NO es invulnerable y no es el escudo
            else if (!enemy.GetComponent<Entity_life>().invulnerable && enemy.tag != "escudo")
            {
                enemy.GetComponent<Entity_life>().StopAllCoroutines();
                enemy.GetComponent<Entity_life>().invulnerable = true;
                enemy.GetComponent<Entity_life>().Invoke("UndoInvincible", 2);

                enemy.vida -= (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Entity_life>().vida <= 50) ? (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().disparoPotenciado) ? 3 * 2 : 3 : (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().disparoPotenciado) ? 2 * 2 : 2;
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().disparoPotenciado = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().DesactivarBiometal();

                enemy.GetComponent<Entity_life>().StartCoroutine(enemy.GetComponent<Entity_life>().FlashSprite());
            }
            //Si la entidad es el escudi pero no es invulnerable
            else if (!enemy.GetComponent<Entity_life>().invulnerable && enemy.tag == "escudo")
            {
                enemy.DanioEscudo(this.transform.position.x);
                GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Movement>().disparoPotenciado = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().DesactivarBiometal();
            }
            //Sea cual sea destruye la bala
            Destroy(gameObject);
        }

        //  Instantiate(impactEffect, transform.position, transform.rotation);
    }
}