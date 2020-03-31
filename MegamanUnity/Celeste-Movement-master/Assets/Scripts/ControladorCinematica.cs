using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorCinematica : MonoBehaviour
{
    // Start is called before the first frame update

    private ArrayList frases;

    public Text dialogueText;
    private Queue<string> sentences;

    void Start()
    {
        frases = new ArrayList();

        frases.Add("Hola... que tal todo, ya veo que has venido a salvar a tu amiga");
        frases.Add("Pero creo que tu amiga va a quedarse con nosotros por unos años mas");
        frases.Add("¡Al menos hasta que dominemos todo!");
        frases.Add("Por ello, no te dejaré pasar de aquí, al menos... no vivo");
        frases.Add("Prepárate para la batalla, ¡TRAIDOR!");




        ComenzarDialogo();


    }

    public void ComenzarDialogo()
    {


        foreach (string frase in frases)
        {

            StartCoroutine(TypeSentence(frase));

        }



    }

    IEnumerator Wait()
    {

        yield return new WaitForSeconds(1);


    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
}
