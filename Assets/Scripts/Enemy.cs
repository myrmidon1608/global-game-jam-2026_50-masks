using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameManager gameManager;
    SoundManager soundManager;

    private Transform player;

    [SerializeField] 
    float speed = 2.5f;

    Rigidbody rb;

    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        soundManager = FindFirstObjectByType<SoundManager>();
        rb = GetComponent<Rigidbody>();

        if (player == null)
        {
            GameObject playerObj = GameObject.Find("Player");

            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }

    void Start()
    {
        gameManager.enemyCount++;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector3 direction = player.position - transform.position;
        direction.y = 0;
        direction.Normalize();

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
            
        Vector3 velocity = rb.linearVelocity;
        velocity.x = direction.x * speed;
        velocity.z = direction.z * speed;
        rb.linearVelocity = velocity;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death"))
        {
            gameManager.enemyCount--;
            soundManager.RandomFallSound();
            Destroy(gameObject);
        }
    }
}