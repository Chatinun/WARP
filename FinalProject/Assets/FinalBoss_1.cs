using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss_1 : StateMachineBehaviour
{
    public float startTimeBtwAttack;
    private float timeBtwAttack;

    FinalBoss boss;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<FinalBoss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timeBtwAttack >= startTimeBtwAttack)
        {
            int num = Random.Range(0, 3);
            if(num == 0)
            {
                animator.SetTrigger("LeftHandPunch");
            }
            else if(num == 1)
            {
                animator.SetTrigger("RightHandPunch");
            }else if(num == 2)
            {
                animator.SetTrigger("BeamCharge");
                //boss.AttackBeam(0.5f, 1.5f);
            }
            
            timeBtwAttack = 0;
        }
        else
        {
            timeBtwAttack += Time.deltaTime;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("LeftHandPunch");
        animator.ResetTrigger("RightHandPunch");
        animator.ResetTrigger("BeamCharge");
    }
}
