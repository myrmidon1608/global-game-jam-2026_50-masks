using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    GameManager gameManager;

    private Coroutine spawnCoroutine = null;

    [SerializeField]
    private Transform[] spawnPoints;

    [SerializeField]
    private int maxEnemies = 5;

    public GameObject enemy;

    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (spawnCoroutine == null && gameManager.enemyCount < maxEnemies)
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
