using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Paladin : MonoBehaviour
{
    private Goblin targetGoblin = null;

    [SerializeField] AIDestinationSetter setter;
    [SerializeField] SpriteRenderer spriteRenderer;

    private void OnEnable()
    {
        GameManager.GameStateChanged += UpdateGameState;
    }
    void Playing()
    {
        StartCoroutine(SearchGoblin());
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

    void Update()
    {
        
    }


}
