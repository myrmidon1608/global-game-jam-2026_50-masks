using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    Transform player;
    NavMeshAgent agent;

    public float speed = 2.5f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Start()
    {
        GameManager.Instance.enemyCount++;
    }

    void Update()
    {
        if (player == null) return;

        agent.SetDestination(player.position);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Death"))
        {
            GameManager.Instance.enemyCount--;
            Destroy(gameObject);
        }
    }
}
