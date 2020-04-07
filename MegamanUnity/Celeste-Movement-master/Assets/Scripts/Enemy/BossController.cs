using System;
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

    public float introDuration = 0.5f;

    public float thornadusDuration = 0.40f;
    public float evilWaltzDuration = 0.40f;
    public float scytheDuration = 0.4f;
    public float quartetBurstDuration = 0.40f;
    public float guillotineDuration = 0.40f;
    public float nocturneDuration = 0.40f;

    public float time = 0;


    public GameObject objetoPadre;
    Vector3 posicionprincipal;

    bool primeravez = false;

    bool cortinacomenzada = false;


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
        posicionprincipal = objetoPadre.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        heartBar.value = scriptvida.vida;

        if (scriptvida.vida < 50)
        {
            Power();
        }

        time += Time.deltaTime;


        switch (state)
        {
            case State.INTRO:
                // do intro stuff
                Intro();
                break;

            case State.THORNADUS:
                Thornadus();
                break;

            case State.WALTZ:

                EvilWaltz();
                break;

            case State.SCYTHE:

                Scythe();
                break;

            case State.BURST:

                QuartetBurst();
                break;

            case State.NOCTURNE:

                HailNocturne();
                break;

            case State.GUILLOTINE:

                Guillotine();
                break;
        }


    }

    private void Intro()
    {
        if (time < introDuration)
        {
            objetoPadre.transform.position = posicionprincipal;


        }
        else
        {


            Aleatorio();



        }
    }

    private void Thornadus()
    {
        if (time < thornadusDuration)
        {

            objetoPadre.transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x + 3, objetoPadre.transform.position.y, objetoPadre.transform.position.z);

            anim.SetBool("Finalizado", false);

            if (!primeravez)
            {
                primeravez = true;
                anim.SetTrigger("thornadus");

            }

            state = State.THORNADUS;
        }
        else
        {


            Aleatorio();



        }
    }

    private void EvilWaltz()
    {
        if (time < evilWaltzDuration)
        {
            objetoPadre.transform.position = posicionprincipal;
            anim.SetBool("Finalizado", false);

            if (!primeravez)
            {
                primeravez = true;
                anim.SetTrigger("waltz");
            }

            state = State.WALTZ;
        }
        else
        {


            Aleatorio();



        }
    }

    private void Scythe()
    {
        if (time < scytheDuration)
        {
            objetoPadre.transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, objetoPadre.transform.position.y, objetoPadre.transform.position.z);

            anim.SetBool("Finalizado", false);


            if (!primeravez)
            {
                primeravez = true;
                anim.SetTrigger("scythe");
            }

            state = State.SCYTHE;
        }
        else
        {


            Aleatorio();



        }
    }

    private void QuartetBurst()
    {
        if (time < quartetBurstDuration)
        {
            objetoPadre.transform.position = posicionprincipal;

            anim.SetBool("Finalizado", false);

            if (!primeravez)
            {
                primeravez = true;
                anim.SetTrigger("burst");
            }
            state = State.BURST;
        }
        else
        {


            Aleatorio();



        }
    }

    private void HailNocturne()
    {
        if (time < nocturneDuration)
        {
            objetoPadre.transform.position = posicionprincipal;

            anim.SetBool("Finalizado", false);

            if (!primeravez)
            {
                primeravez = true;
                anim.SetTrigger("nocturne");
            }
            state = State.NOCTURNE;
        }
        else
        {


            Aleatorio();



        }
    }

    private void Guillotine()
    {
        if (time < guillotineDuration)
        {
            objetoPadre.transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, objetoPadre.transform.position.y, objetoPadre.transform.position.z);
            anim.SetBool("Finalizado", false);

            if (!primeravez)
            {
                primeravez = true;
                anim.SetTrigger("guillotine");
            }
            state = State.GUILLOTINE;
        }
        else
        {




            Aleatorio();



        }
    }

    private void Aleatorio()
    {

        primeravez = false;

        anim.SetBool("Finalizado", true);
        anim.ResetTrigger("thornadus");
        anim.ResetTrigger("guillotine");
        anim.ResetTrigger("burst");
        anim.ResetTrigger("scythe");
        anim.ResetTrigger("waltz");
        anim.ResetTrigger("nocturne");
        time = 0;

        if (!cortinacomenzada)
        {
            // StartCoroutine(EsperarUnSegundo());
            switch (UnityEngine.Random.Range(0, 6))
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
                    Aleatorio();

                    break;

                case 4:

                    state = State.THORNADUS;

                    break;

                case 5:

                    state = State.WALTZ;

                    break;
            }
        }
        Debug.Log(state + " coords " + transform.position.x + " " + transform.position.y);



    }

    private void Power()
    {
        GetComponentInChildren<GhostBoss>().ShowGhost();
    }

    //private IEnumerator DashWait()
    //{
    //    yield return new WaitForSeconds(.3f);
    //}

    private IEnumerator EsperarUnSegundo()
    {

        cortinacomenzada = true;


        yield return new WaitForSeconds(1f);

        cortinacomenzada = false;

    }


}