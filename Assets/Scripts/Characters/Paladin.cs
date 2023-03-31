using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Paladin : MonoBehaviour
{
    private Goblin targetGoblin = null;

    [SerializeField] private AIDestinationSetter setter;
    [SerializeField] private AIPath AIPath;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    [SerializeField] private AttackZone attackZone;
    [SerializeField] private int damage = 10;


    private void OnEnable()
    {
        GameManager.GameStateChanged += UpdateGameState;
        GameManager.OnGoblinKilled += OnGoblinKilled;
    }
    void Playing()
    {
        StartCoroutine(SearchGoblin());
    }

    private void OnGoblinKilled(Goblin goblin)
    {
        if (goblin == targetGoblin)
        {
            targetGoblin = null;
            animator.SetInteger("Attack", 0);
        }
            
    }
    private void UpdateGameState(GameState newState)
    {
        switch(newState)
        {
            case GameState.Playing:
                Playing();
                break;
        }
    }

    private IEnumerator SearchGoblin()
    {
        while(true)
        {
            if (targetGoblin == null)
            {
                targetGoblin = GameManager.Instance.GetNearestGoblin(transform.position);
                if (targetGoblin != null)
                    SetDestination();
            }

            yield return new WaitForSeconds(Parameters.Instance.paladinWait);
        }
    }

    private void SetDestination()
    {
        setter.target = targetGoblin.transform;
    }

    void FixedUpdate()
    {
        if (AIPath.velocity != Vector3.zero)
            animator.SetInteger("AnimState", 1);
        else
            animator.SetInteger("AnimState", 0);

        if (AIPath.velocity.x != 0)
        {
            Vector3 scale = gameObject.transform.localScale;
            scale.x = AIPath.velocity.x > 0 ? 1 : -1;
            gameObject.transform.localScale = scale;
        }

        if (!attackZone.isEmpty)
        {
            animator.SetInteger("Attack", 1);
            foreach (Character character in attackZone.enemies)
                if (character != null)
                    character.Damage(damage);
        } else
            animator.SetInteger("Attack", 0);
    }


}
