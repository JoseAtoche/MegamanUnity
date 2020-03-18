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
    private float fixedSpeed;
    private Vector3 final;

    public float time = 0;


    bool primeravez = true;

    Vector3 posicionAcual;



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

    private void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    private void Update()
    {
        heartBar.value = scriptvida.vida;

        if (scriptvida.vida < 50)
        {
            Dash();
        }

        time += Time.deltaTime;
        if (time >= 0f && time <= 0.05f)
        {
            anim.ResetTrigger("thornadus");
            anim.ResetTrigger("guillotine");
            anim.ResetTrigger("burst");
            anim.ResetTrigger("scythe");
            anim.ResetTrigger("waltz");
            anim.ResetTrigger("nocturne");

            aleatorio();
        }

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
        fixedSpeed = velocidad * Time.deltaTime;
        Debug.Log(state + " Modificación " + final.x + " " + final.y);

        transform.position = Vector3.MoveTowards(posicionAcual, final, fixedSpeed);

    }

    private void intro()
    {
        if (time >= introDuration)
        {
            //anim.SetBool("floatPrometheus", true);
            //state = State.INTRO;
            //time -= introDuration;
        }
    }

    private void thornadus()
    {
        if (time >= thornadusDuration)
        {

            final = new Vector3(transform.position.x, -6.8f, transform.position.z);

            anim.SetTrigger("thornadus");

            state = State.THORNADUS;
            time -= thornadusDuration;
        }
    }

    private void evilWaltz()
    {
        if (time >= evilWaltzDuration)
        {
            final = new Vector3(136.91f, transform.position.y, transform.position.z);

            anim.SetTrigger("waltz");

            state = State.WALTZ;
            time -= evilWaltzDuration;
        }
    }

    private void scythe()
    {
        if (time >= scytheDuration)
        {
            final = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            anim.SetTrigger("scythe");

            state = State.SCYTHE;
            time -= scytheDuration;
        }
    }

    private void quartetBurst()
    {
        if (time >= quartetBurstDuration)
        {
            final = new Vector3(145f, -10.07f, transform.position.z);

            anim.SetTrigger("burst");
            state = State.BURST;
            time -= quartetBurstDuration;
        }
    }

    private void hailNocturne()
    {
        if (time >= nocturneDuration)
        {
            final = new Vector3(146.28f, -14.11f, 0);

            anim.SetTrigger("nocturne");
            state = State.NOCTURNE;
            time -= nocturneDuration;
        }
    }

    private void guillotine()
    {
        if (time >= guillotineDuration)
        {
            final = new Vector3(transform.position.x, -14.11f, transform.position.z);

            anim.SetTrigger("guillotine");
            state = State.GUILLOTINE;
            time -= guillotineDuration;
        }
    }

    private void aleatorio()
    {

        switch (Random.Range(0, 6))
        {
            case 0:
                transform.position = new Vector3(145f, -10.07f, transform.position.z);

                state = State.BURST;

                break;

            case 1:
                transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, -7.15f, transform.position.z);
                state = State.GUILLOTINE;

                break;

            case 2:
                transform.position = new Vector3(146.28f, -14.11f, transform.position.z);
                state = State.NOCTURNE;

                break;

            case 3:
                //  state = State.SCYTHE;
                aleatorio();

                break;

            case 4:
                state = State.THORNADUS;

                break;

            case 5:
                transform.position = new Vector3(144f, GameObject.FindGameObjectWithTag("Player").transform.position.y + 5, transform.position.z);
                state = State.WALTZ;

                break;
        }
        posicionAcual = transform.position;
        Debug.Log(state + " coords " + transform.position.x + " " + transform.position.y);
    }

    private void Dash()
    {
        GetComponentInChildren<GhostBoss>().ShowGhost();
    }

    private IEnumerator DashWait()
    {
        yield return new WaitForSeconds(.3f);
    }
}