using UnityEngine;

public class ControlAnimacionGuillotine : StateMachineBehaviour
{
    private float timer;
    public float minTime;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Tiempo minimo que dura la animacion
        timer = minTime;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("guillotine");
    }
}