using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    GameManager gameManager;
    SoundManager soundManager;

    Transform player;
    NavMeshAgent agent;

    [SerializeField] float speed = 2.5f;

    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        soundManager = FindFirstObjectByType<SoundManager>();

        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Start()
    {
        gameManager.enemyCount++;
    }

    void Update()
    {
        if (player == null) return;

        agent.SetDestination(player.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death"))
        {
            gameManager.enemyCount--;
            Destroy(gameObject);
        }
    }
}
