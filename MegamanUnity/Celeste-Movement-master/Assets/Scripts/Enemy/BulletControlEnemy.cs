﻿using System.Collections;
using System.Collections.Generic;
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
}