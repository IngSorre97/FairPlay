using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    private List<Character> _enemies = new List<Character>();
    public List<Character> enemies => _enemies;

    public bool isEmpty => _enemies.Count == 0;

    public void OnEnable()
    {
        GameManager.OnGoblinKilled += OnGoblinKilled;
    }

    private void OnGoblinKilled(Goblin goblin)
    {
        _enemies.Remove(goblin);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Damageable"))
        {
            _enemies.Add(collision.gameObject.GetComponent<Character>());
            Debug.Log(_enemies.Count);
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Damageable"))
        {
            _enemies.Remove(collision.gameObject.GetComponent<Character>());
            Debug.Log(_enemies.Count);
        }
    }
}
