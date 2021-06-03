using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacialIldeRandomPlay : StateMachineBehaviour
{
    public float mRandomIdleTime = 5;

    public float mTime = 0;

    private bool mIdleState = false;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        mTime = Random.Range(2, mRandomIdleTime);
        mIdleState = true;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (mTime > 0 && mIdleState)
        {
            mTime -= Time.deltaTime;

        }

        if (mTime <= 0)
        {
            animator.SetTrigger("Blink");
            mTime = mTime = Random.Range(2, mRandomIdleTime);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        mTime = 0;
        mIdleState = false;
    }
}
