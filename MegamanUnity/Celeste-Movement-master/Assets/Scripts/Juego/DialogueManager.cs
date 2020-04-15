using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //Bloque de texto donde se colocará las frases
    public Text dialogueText;

    private Queue<string> sentences;

    // Use this for initialization
    private void Start()
    {
        sentences = new Queue<string>();
    }

    /// <summary>
    /// Comienza el diálogo que le introduzcas, solo debes introducir un dialogo(texto) y este lo mostará poco a poco por pantalla, frase a frase, ademas de separado en letras que aparecerán poco a poco
    /// </summary>
    /// <param name="dialogue"></param>
    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    /// <summary>
    /// Muestra la siguiente frase
    /// </summary>
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    /// <summary>
    /// Esto es una cortinilla(Hilo) que separa las letras de la frase para mostrarse poco a poco
    /// </summary>
    /// <param name="sentence"></param>
    /// <returns></returns>
    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
        DisplayNextSentence();
    }
}