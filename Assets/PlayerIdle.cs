using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<Character>()?.StopAttack();
    }
}
