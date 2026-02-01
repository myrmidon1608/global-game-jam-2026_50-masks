using System.Collections;
using UnityEngine;

public class SpringTrap : MonoBehaviour
{
    public GameObject activeTrap;
    public GameObject inactiveTrap;

    [SerializeField] 
    float launchForce = 12f;

    private Coroutine resetTrapCoroutine = null;
    private Player currentPlayer;

    void Start()
    {

    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && resetTrapCoroutine == null)
        {
            inactiveTrap.SetActive(false);
            activeTrap.SetActive(true);

            currentPlayer = other.GetComponent<Player>();
            currentPlayer.launched = true;

            Rigidbody playerRb = other.GetComponent<Rigidbody>();

            if (playerRb != null)
            {
                Vector3 horizontalVelocity = new Vector3(playerRb.linearVelocity.x, 0f, playerRb.linearVelocity.z);

                Vector3 launchDir;
                if (horizontalVelocity.magnitude > 0.1f)
                {
                    launchDir = -horizontalVelocity.normalized;
                } 
                else
                {
                    launchDir = Vector3.up;
                }


                launchDir += Vector3.up;
                launchDir.Normalize();

                playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, 0f, playerRb.linearVelocity.z);
                playerRb.AddForce(launchDir * launchForce, ForceMode.Impulse);
            }

            resetTrapCoroutine = StartCoroutine(ResetTrap());
        }
    }

    IEnumerator ResetTrap()
    {
        yield return new WaitForSeconds(5f);
        inactiveTrap.SetActive(true);
        activeTrap.SetActive(false);
        resetTrapCoroutine = null;
        currentPlayer.launched = false;
        currentPlayer = null;
    }
}
