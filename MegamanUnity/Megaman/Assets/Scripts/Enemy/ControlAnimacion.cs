using UnityEngine;

public class ControlAnimacion : StateMachineBehaviour
{
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
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
    }
}