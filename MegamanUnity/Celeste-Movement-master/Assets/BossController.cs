using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    public int heart = 100;
    public float velocidad;
    public Slider heartBar;

    public bool invulnerable;
    Color color;
    public SpriteRenderer spriteRenderer;




    public float introDuration = 1.0f;

    public float thornadusDuration = 1.0f;
    public float evilWaltzDuration = 1.0f;
    public float scytheDuration = 1.0f;
    public float quartetBurstDuration = 1.0f;
    public float guillotineDuration = 1.0f;
    public float nocturneDuration = 1.0f;

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
        heartBar.value = heart;




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

            final = new Vector3(transform.position.x, transform.position.y + 100, transform.position.z);




            anim.SetBool("thornadus", true);
            state = State.THORNADUS;
            time -= thornadusDuration;
        }


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


    void OnTriggerStay2D(Collider2D collision)
    {
        if (!invulnerable)
        {


            if (collision.gameObject.tag == "Jugador")
            {

                StopAllCoroutines();
                invulnerable = true;
                Invoke("UndoInvincible", 2);
                heart--;
                StartCoroutine(FlashSprite());


            }


        }

    }




    IEnumerator FlashSprite()
    {
        while (true)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.02f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.02f);
        }
    }


    void UndoInvincible()
    {
        invulnerable = false;
        StopAllCoroutines();
        spriteRenderer.enabled = true;
    }
}




