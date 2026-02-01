using System.Collections;
using UnityEngine;

public class SafePlatforms : MonoBehaviour
{
    [SerializeField]
    private GameObject[] safePlatforms;

    public GameObject mainPlatform;

    private int safePlatformsSpawned = 0;

    [SerializeField]
    private int maxPlatforms = 3;

    private Coroutine showSafePlatformCoroutine = null;
    private Coroutine hideMainPlatformCoroutine = null;


    void Update()
    {
        if (showSafePlatformCoroutine == null && hideMainPlatformCoroutine == null && safePlatformsSpawned < maxPlatforms)
        {
            showSafePlatformCoroutine = StartCoroutine(SafePlatformVisible());
        }
    }

    IEnumerator SafePlatformVisible()
    {
        yield return new WaitForSeconds(2f);
        int index = Random.Range(0, safePlatforms.Length);
        safePlatforms[index].gameObject.SetActive(true);
        hideMainPlatformCoroutine = StartCoroutine(MainPlatformInvisible());
        showSafePlatformCoroutine = null;
    }

    IEnumerator MainPlatformInvisible()
    {
        yield return new WaitForSeconds(7f);
        mainPlatform.gameObject.SetActive(false);
        yield return new WaitForSeconds(5f);
        mainPlatform.gameObject.SetActive(true);
        for (int i = 0; i < safePlatforms.Length; i++)
        {
            safePlatforms[i].gameObject.SetActive(false);
        }
        safePlatformsSpawned++;
        hideMainPlatformCoroutine = null;
    }
}
