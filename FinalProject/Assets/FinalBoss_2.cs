using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss_2 : StateMachineBehaviour
{
    public float startTimeBtwAttack;
    private float timeBtwAttack;

    FinalBoss boss;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<FinalBoss>();
        animator.ResetTrigger("Enrage");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timeBtwAttack >= startTimeBtwAttack)
        {
            int num = Random.Range(0, 4);
            if (num == 0)
            {
                animator.SetTrigger("EnrageLeftHandPunch");
            }
            else if (num == 1)
            {
                animator.SetTrigger("EnrageRightHandPunch");
            }
            else if (num == 2)
            {
                animator.SetTrigger("EnrageBeamCharge");
                //boss.AttackBeam(0.5f, 1.5f);
            }else if(num == 3)
            {
                animator.SetTrigger("EnrageBothHandPunch");
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
        animator.ResetTrigger("EnrageLeftHandPunch");
        animator.ResetTrigger("EnrageRightHandPunch");
        animator.ResetTrigger("BeamCharge");
    }
}
