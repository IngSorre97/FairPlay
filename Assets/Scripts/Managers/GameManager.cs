using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private GameState _state = GameState.Menu;


    [SerializeField] private Player _player;
    public Player player => _player;

    [SerializeField] private Paladin _paladin;
    public Paladin paladin => _paladin;


    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private GameObject goblinPrefab;

    private List<Goblin> goblinlist = new List<Goblin>();

    private float spawnTime = 3.0f;

    public delegate void GameStateDelegate(GameState state);
    public static event GameStateDelegate GameStateChanged;

    public delegate void GoblinKilled(Goblin goblin);
    public static event GoblinKilled OnGoblinKilled;

    void Start()
    {
        if (Instance == null) Instance = this;
        else return;
    }

    public void OnPlayClicked()
    {
        _state = GameState.Playing;
        GameStateChanged?.Invoke(_state);

        StartCoroutine(SpawnGoblins());
    }

    private IEnumerator SpawnGoblins()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            GameObject goblin = Instantiate(goblinPrefab, GetSpawnPoint().position, Quaternion.identity);
            goblinlist.Add(goblin.AddComponent<Goblin>());
        }
    }

    private Transform GetSpawnPoint()
    {
        int index = Random.Range(0, spawnPoints.Count);
        return spawnPoints[index];
    }

    public void DestroyGoblin(Goblin goblin)
    {
        OnGoblinKilled?.Invoke(goblin);
        goblinlist.Remove(goblin);
        Destroy(goblin.gameObject);
    }

    public Goblin GetNearestGoblin(Vector2 position)
    {
        float minDistance = Mathf.Infinity;
        Goblin nearestGoblin = null;
        foreach(Goblin goblin in goblinlist)
        {
            if (goblin == null) continue;
            if (Vector2.Distance(goblin.transform.position, position) < minDistance)
            {
                nearestGoblin = goblin;
                minDistance = Vector2.Distance(goblin.transform.position, position);
            }
        }
        return nearestGoblin;
    }
}
