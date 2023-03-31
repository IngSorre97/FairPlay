using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Goblin : Character
{
    private Animator animator;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public override void Damage(int damage)
    {
        health -= damage;
        if (health < 0)
            Death();
        else Hurt();
    }

    protected override void Death()
    {
        animator.SetTrigger("Death");
    }

    private void Hurt()
    {
        animator.SetTrigger("Hurt");
    }

    public override void StopAttack()
    {
        throw new System.NotImplementedException();
    }
}
