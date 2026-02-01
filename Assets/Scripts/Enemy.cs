using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    Transform player;
    NavMeshAgent agent;

    public float speed = 2.5f;

    [SerializeField] 
    float groundCheckDistance = 1.2f;
    [SerializeField] 
    LayerMask groundLayer;

    Rigidbody rb;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

        rb = GetComponent<Rigidbody>();

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

        bool grounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

        if (grounded)
        {
            if (!agent.enabled)
                agent.enabled = true;

            agent.SetDestination(player.position);
        }
        else
        {
            if (agent.enabled)
                agent.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Meteor"))
        {
            GameManager.Instance.enemyCount--;
            Destroy(gameObject);
        }
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
