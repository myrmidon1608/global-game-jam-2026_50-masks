using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{

    private List<Enemy> _enemyPool = new();

    private Enemy PeekEnemy()
    {
        Enemy availableEnemy = null;
        foreach (Enemy enemy in _enemyPool)
        {
            if (!enemy.gameObject.activeSelf)
            {
                availableEnemy = enemy;
                break;
            }
        }
        return availableEnemy;
    }

    public void Clear()
    {
        foreach (Enemy enemy in _enemyPool)
        {
            Remove(enemy);
        }
    }

    public void SpawnEnemy(GameObject enemyPrefab, Transform spawnPoint) {
        Enemy enemyController = PeekEnemy();
        if (enemyController == null) {
            GameObject enemyObj = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            //enemyObj.transform.position = spawnPoint.position;
            enemyController = enemyObj.GetComponent<Enemy>();
        } else {
            enemyController.gameObject.transform.position = spawnPoint.position;
        }
        enemyController.speed = Random.Range(2, 3);
        _enemyPool.Add(enemyController);
        enemyController.gameObject.SetActive(true);
    }

    public void Remove(Enemy enemy) {
        enemy.gameObject.SetActive(false);
    }
}
