using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    public int heart = 100;
    public float velocidad;
    public Slider heartBar;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        heartBar.value = heart;
        //  int aleatorio = Random.Range(1, 8);
        switch (1)
        {
            case 1:
                thornadus();

                //flotar();
                break;
            case 2:
                thornadus();

                // levantarse();
                break;
            case 3:
                thornadus();
                break;
            case 4:
                thornadus();

                // evilWaltz();
                break;
            case 5:
                thornadus();

                // scythe();
                break;
            case 6:
                thornadus();

                // quartetBurst();
                break;
            case 7:
                thornadus();

                // hailNocturne();
                break;
            case 8:
                thornadus();

                //guillotine();
                break;




        }




    }


    void flotar()
    {





        anim.SetBool("floatPrometheus", true);
    }
    void levantarse()
    {




        anim.SetBool("getup", true);
    }
    void thornadus()
    {


        float fixedSpeed = velocidad * Time.deltaTime;
        Vector3 final = new Vector3(transform.position.x, transform.position.y + 100, transform.position.z);


        transform.position = Vector3.MoveTowards(transform.position, final, fixedSpeed);

        anim.SetBool("thornadus", true);
    }
    void evilWaltz()
    {




        anim.SetBool("guillotine", true);
    }
    void scythe()
    {




        anim.SetBool("fall", true);
    }
    void quartetBurst()
    {




        anim.SetBool("burst", true);
    }
    void hailNocturne()
    {


        anim.SetBool("nocturne", true);
    }
    void guillotine()
    {


        anim.SetBool("guillotine", true);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //PlayerController player = collision.GetComponent<PlayerController>();
        //if (player != null)
        //{
        //    if (!player.invulnerable)
        //    {
        //        player.vida = player.vida - 10;
        //    }
        //}
    }
    




}
