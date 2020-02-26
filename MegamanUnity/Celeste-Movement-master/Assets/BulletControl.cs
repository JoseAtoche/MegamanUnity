using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{

    public Rigidbody2D bulletRB;
    public float bulletSpeed;
    public float  bulletLife; 

        void Awake() {

        bulletRB = GetComponent<Rigidbody2D>();


    }
    // Start is called before the first frame update
    void Start()
    {

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Movement movimiento = player.GetComponent<Movement>();
        if (movimiento.side == -1)
        {

            bulletRB.velocity = new Vector2(-bulletSpeed, bulletRB.velocity.y);


        }
        else {

            bulletRB.velocity = new Vector2(bulletSpeed, bulletRB.velocity.y);



        }
    }

    // Update is called once per frame
    void Update()
    {




        Destroy(gameObject, bulletLife);
    }
}
