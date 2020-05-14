using UnityEngine;

public class ControlAnimationWaltz : StateMachineBehaviour
{
    private float timer;
    public float minTime;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = minTime;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("waltz");
    }
}