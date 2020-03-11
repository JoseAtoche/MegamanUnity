using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{

    public Rigidbody2D bulletRB;
    public float bulletSpeed;
    public float bulletLife;

    void Awake()
    {

        bulletRB = GetComponent<Rigidbody2D>();


    }
    // Start is called before the first frame update
    void Start()
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
    void Update()
    {




        Destroy(gameObject, bulletLife);
    }


    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        BossController enemy = hitInfo.GetComponent<BossController>();
        if (enemy != null)
        {
            enemy.heart = enemy.heart - 1;
            Destroy(gameObject);
        }

        //  Instantiate(impactEffect, transform.position, transform.rotation);

       ;
    }

}
