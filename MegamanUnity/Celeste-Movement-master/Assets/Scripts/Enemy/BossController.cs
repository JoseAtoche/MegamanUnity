using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;

    public float velocidad;
    public Slider heartBar;

    public bool invulnerable;

    public Entity_life scriptvida;


    public float introDuration = 1.0f;

    public float thornadusDuration = 4.0f;
    public float evilWaltzDuration = 4.0f;
    public float scytheDuration = 4.0f;
    public float quartetBurstDuration = 5.0f;
    public float guillotineDuration = 4.0f;
    public float nocturneDuration = 4.0f;

    float fixedSpeed;
    Vector3 final;


    public float time = 0;

    public enum State
    {
        INTRO,
        THORNADUS,
        WALTZ,
        SCYTHE,
        BURST,
        NOCTURNE,
        GUILLOTINE
    }

    public State state = State.THORNADUS;
    void Start()
    {
        anim = GetComponent<Animator>();
        fixedSpeed = velocidad * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        heartBar.value = scriptvida.vida;




        time += Time.deltaTime;

        switch (state)
        {
            case State.INTRO:
                // do intro stuff
                intro();
                break;

            case State.THORNADUS:
                thornadus();
                break;
            case State.WALTZ:

                evilWaltz();
                break;
            case State.SCYTHE:

                scythe();
                break;
            case State.BURST:

                quartetBurst();
                break;
            case State.NOCTURNE:

                hailNocturne();
                break;
            case State.GUILLOTINE:

                guillotine();
                break;




        }
        transform.position = Vector3.MoveTowards(transform.position, final, fixedSpeed);
        if (time == 0f)
        {
            anim.SetBool("thornadus", false);
            anim.SetBool("guillotine", false);
            anim.SetBool("burst", false);
            anim.SetBool("fall", false);
            anim.SetBool("waltz", false);
            anim.SetBool("nocturne", false);





            aleatorio();

        }



    }


    void intro()
    {

        if (time >= introDuration)
        {
            //anim.SetBool("floatPrometheus", true);
            //state = State.INTRO;
            //time -= introDuration;
        }






    }

    void thornadus()
    {
        if (time >= thornadusDuration)
        {

            final = new Vector3(transform.position.x, transform.position.y + 300, transform.position.z);




            anim.SetBool("thornadus", true);
            state = State.THORNADUS;
            time -= thornadusDuration;
        }


    }
    void evilWaltz()
    {
        if (time >= evilWaltzDuration)
        {

            final = new Vector3(transform.position.x, transform.position.y + 100, transform.position.z);



            anim.SetBool("waltz", true);

            state = State.GUILLOTINE;
            time -= guillotineDuration;

        }



    }
    void scythe()
    {

        if (time >= scytheDuration)
        {

            final = new Vector3(transform.position.x, transform.position.y + 100, transform.position.z);



            anim.SetBool("fall", true);
            state = State.SCYTHE;
            time -= scytheDuration;

        }





    }
    void quartetBurst()
    {
        if (time >= quartetBurstDuration)
        {

            final = new Vector3(transform.position.x, transform.position.y + 100, transform.position.z);



            anim.SetBool("burst", true);
            state = State.BURST;
            time -= quartetBurstDuration;

        }




    }
    void hailNocturne()
    {
        if (time >= nocturneDuration)
        {

            final = new Vector3(transform.position.x, transform.position.y + 100, transform.position.z);



            anim.SetBool("nocturne", true);
            state = State.NOCTURNE;
            time -= nocturneDuration;

        }


    }
    void guillotine()
    {
        if (time >= guillotineDuration)
        {

            final = new Vector3(transform.position.x + 100, transform.position.y, transform.position.z);



            anim.SetBool("guillotine", true);
            state = State.GUILLOTINE;
            time -= guillotineDuration;
        }


    }






    void aleatorio()
    {
        switch (Random.Range(0, 6))
        {

            case 0:
                state = State.BURST;

                break;
            case 1:
                state = State.GUILLOTINE;

                break;
            case 2:
                state = State.NOCTURNE;

                break;
            case 3:
                state = State.SCYTHE;

                break;
            case 4:
                state = State.THORNADUS;

                break;
            case 5:
                state = State.WALTZ;

                break;
        }







    }

}




