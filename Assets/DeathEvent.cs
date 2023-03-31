using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEvent : StateMachineBehaviour
{
    [SerializeField] private Sprite dead;
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameManager.Instance.DestroyGoblin(animator.gameObject.GetComponent<Goblin>());
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isDying", true);
    }
}
