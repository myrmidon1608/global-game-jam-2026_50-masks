using System.Collections;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPoints;

    public GameObject meteor;

    public bool enableSpawn = false;

    private Coroutine spawnMeteorCoroutine = null;
    private Coroutine stopTimerCoroutine = null;

    private int timer = 0;

    void Update()
    {
        if (stopTimerCoroutine == null)
        {
            timer = Random.Range(7, 20);
            enableSpawn = true;
            stopTimerCoroutine = StartCoroutine(StopSpawning());
        }

        if (spawnMeteorCoroutine == null)
        {
            spawnMeteorCoroutine = StartCoroutine(SpawnMeteors());
        }
    }

    IEnumerator SpawnMeteors()
    {
        yield return new WaitForSeconds(2f);
        int index = Random.Range(0, spawnPoints.Length);
        Instantiate(meteor, spawnPoints[index].position, Quaternion.identity);
        spawnMeteorCoroutine = null;
    }

    IEnumerator StopSpawning()
    {
        yield return new WaitForSeconds(timer);
        enableSpawn = false;
        stopTimerCoroutine = null;
        Destroy(gameObject); // Comment out eventually
    }
}
