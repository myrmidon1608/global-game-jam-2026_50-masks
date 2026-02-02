using UnityEngine;
using System.Collections;

public class Ground : MonoBehaviour
{

    private Collider col;

    [SerializeField]
    PhysicsMaterial[] surfaces;

    private Coroutine surfaceCoroutine = null;

    void Start()
    {
        col = GetComponent<Collider>();
    }

    void Update()
    {
        //if (surfaceCoroutine == null)
        //{
        //    surfaceCoroutine = StartCoroutine(ChangeSurface());
        //}
    }

    IEnumerator ChangeSurface()
    {
        yield return new WaitForSeconds(7.5f);
        int index = Random.Range(0, surfaces.Length);

        col.sharedMaterial = surfaces[index];

        Renderer rend = GetComponent<Renderer>();

        if (index == 0)
        {
            rend.material.color = Color.blue;
        }
        else if (index == 1)
        {
            rend.material.color = Color.white;
        }
        else if (index == 2)
        {
            rend.material.color = new Color32(0, 255, 239, 255);
        }

        surfaceCoroutine = null;
    }
}
