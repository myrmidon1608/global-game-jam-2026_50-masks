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
        GameManager.Instance.EventBus.MaskSadStart += SpawnEnemies;
        GameManager.Instance.EventBus.MaskHappyStart += _enemyPool.Clear;
        GameManager.Instance.EventBus.LoseGame += _enemyPool.Clear;
    }

    public void SpawnEnemies() {
        for (int i = 0; i < spawnPoints.Length; i += 1) {
            Transform spawnPoint = spawnPoints[i];
            _enemyPool.SpawnEnemy(enemy, spawnPoint);
        }
    }

    public void RemoveEnemy(Enemy enemy) {
        _enemyPool.Remove(enemy);
    }
}
