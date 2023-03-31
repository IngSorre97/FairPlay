using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected int health;

    public abstract void StopAttack();
    public abstract void Damage(int damage);
    protected abstract void Death();
}
