using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{

    private List<Enemy> _enemyPool = new();

    private Enemy PeekEnemies()
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

    public void ClearEnemies()
    {
        foreach (Enemy enemy in _enemyPool)
        {
            enemy.gameObject.SetActive(false);
        }
    }

    public void SpawnEnemies(GameObject enemyPrefab, Transform[] spawnPoints)
    {
        for (int i = 0; i < spawnPoints.Length; i += 1)
        {
            Transform spawnPoint = spawnPoints[i];
            Enemy enemyController = PeekEnemies();
            if (enemyController == null)
            {
                GameObject enemyObj = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
                //enemyObj.transform.position = spawnPoint.position;
                enemyController = enemyObj.GetComponent<Enemy>();
            }
            else {
                enemyController.gameObject.transform.position = spawnPoint.position;
            }
            enemyController.speed = Random.Range(2, 3);
            _enemyPool.Add(enemyController);
            enemyController.gameObject.SetActive(true);
        }
    }
}
