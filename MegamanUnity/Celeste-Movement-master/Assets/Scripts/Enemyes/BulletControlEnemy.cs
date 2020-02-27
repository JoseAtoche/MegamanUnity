using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControlEnemy : MonoBehaviour
{
    // Start is called before the first frame update

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






    }

    // Update is called once per frame
    void Update()
    {




        Destroy(gameObject, bulletLife);
    }
}
