using System.Collections;
using UnityEngine;

public class Spotlight : MonoBehaviour {

    private Coroutine myCoroutine = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            myCoroutine = StartCoroutine(SpotlightCoroutine(player));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (myCoroutine != null) {
            StopCoroutine(myCoroutine);
            myCoroutine = null;
        }
    }

    private IEnumerator SpotlightCoroutine(Player player) {
        yield return new WaitForSeconds(0.5f);
        player.Die();
    }
}
