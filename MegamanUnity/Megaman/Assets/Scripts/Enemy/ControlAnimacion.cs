using UnityEngine;



/// <summary>
/// Controla las animaciones del Boss
/// </summary>
public class ControlAnimacion : StateMachineBehaviour
{
    // public GameObject dialogo;

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //  GameObject.Find("ControladorCinematica").

        //if (dialogo.GetComponent<DialogueTrigger>().controlador.enabled == true)
        //{
        animator.SetBool("burstbool", false);

        switch (UnityEngine.Random.Range(0, 4))
        {
            case 0:
                animator.SetTrigger("thornadus");

                break;

            case 1:

                animator.SetTrigger("guillotine");

                break;

            case 2:
                animator.SetTrigger("burst");
                animator.SetBool("burstbool", true);

                break;

            case 3:
                animator.SetTrigger("waltz");

                break;
        }

        // }
    }
}