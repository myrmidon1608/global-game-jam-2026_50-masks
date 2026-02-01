using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] 
    private float speed = 5f;

    public GameObject explosion;

    private Transform player;

    private Vector3 direction;

    void Start()
    {
        GameObject playerObj = GameObject.Find("Player");
        if (playerObj != null)
            player = playerObj.transform;

        direction = (player.position - transform.position).normalized;
    }

    void Update()
    {
        if (player != null)
        {
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Death"))
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
