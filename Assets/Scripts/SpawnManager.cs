using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private Transform[] spawnPoints;

    public GameObject enemy;

    private EnemyPool _enemyPool;

    private void Awake()
    {
        _enemyPool = GetComponentInChildren<EnemyPool>();
    }

    private void Start()
    {
        GameManager.Instance.EventBus.MaskHappyStart += _enemyPool.ClearEnemies;
        GameManager.Instance.EventBus.LoseGame += _enemyPool.ClearEnemies;
        GameManager.Instance.EventBus.MaskSadStart += SpawnEnemies;
    }

    private void SpawnEnemies() {
        _enemyPool.SpawnEnemies(enemy, spawnPoints);
    }
}
