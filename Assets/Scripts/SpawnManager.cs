using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{

    private Coroutine spawnCoroutine = null;

    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private int maxEnemies = 5;

    public GameObject enemy;

    // Update is called once per frame
    void Update()
    {
        if (spawnCoroutine == null && GameManager.Instance.enemyCount < maxEnemies)
        {
            spawnCoroutine = StartCoroutine(SpawnEnemies());
        }
    }

    IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(5f);
        int index = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[index];
        Instantiate(enemy, spawnPoint.position, Quaternion.identity);
        spawnCoroutine = null;
    }
}
