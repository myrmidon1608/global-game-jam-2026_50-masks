using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{

    private Coroutine spawnCoroutine = null;

    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private int maxEnemies = 5;

    public GameObject enemy;

    private List<Enemy> _enemyPool = new();

    private void Start()
    {
        GameManager.Instance.EventBus.MaskHappyStart += RemoveEnemies;
        GameManager.Instance.EventBus.LoseGame += RemoveEnemies;
        GameManager.Instance.EventBus.MaskSadStart += SpawnEnemies;
    }

    private void Update()
    {
        //if (spawnCoroutine == null && GameManager.Instance.enemyCount < maxEnemies)
        //{
        //    spawnCoroutine = StartCoroutine(SpawnEnemiesCoroutinue());
        //}
    }

    private void SpawnEnemies() {
        for (int i = 0; i < maxEnemies; i += 1) {
            Transform spawnPoint = spawnPoints[i];
            GameObject enemyObj = Instantiate(enemy, spawnPoint.position, Quaternion.identity);
            Enemy enemyController = enemyObj.GetComponent<Enemy>();
            enemyController.speed = Random.Range(2, 3);
            _enemyPool.Add(enemyController);
        }
    }

    private void RemoveEnemies() {
        foreach (Enemy enemy in _enemyPool) {
            if (enemy.gameObject) Destroy(enemy.gameObject);
        }
        _enemyPool.Clear();
    }

    IEnumerator SpawnEnemiesCoroutinue()
    {
        yield return new WaitForSeconds(5f);
        int index = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[index];
        Instantiate(enemy, spawnPoint.position, Quaternion.identity);
        spawnCoroutine = null;
    }
}
