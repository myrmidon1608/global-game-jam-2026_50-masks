using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    GameManager gameManager;
    SoundManager soundManager;

    Rigidbody rb;
    PlayerInput playerInput;
    InputAction moveAction;

    [SerializeField]
    float speed = 5f;

    [SerializeField] 
    float maxSpeed = 5f;

    private Coroutine myCoroutine = null;

    [SerializeField] 
    LayerMask groundLayer;

    [SerializeField] 
    PhysicsMaterial iceMaterial;
    [SerializeField]
    PhysicsMaterial slowMaterial;

    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        soundManager = FindFirstObjectByType<SoundManager>();
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
    }

    void FixedUpdate()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 direction = new Vector3(input.x, 0f, input.y).normalized;

        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.1f, groundLayer))
        {
            PhysicsMaterial mat = hit.collider.sharedMaterial;

            if (mat == iceMaterial)
            {
                rb.AddForce(direction * speed, ForceMode.Acceleration);

                Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
                if (flatVelocity.magnitude > maxSpeed)
                {
                    flatVelocity = flatVelocity.normalized * maxSpeed;
                    rb.linearVelocity = new Vector3(flatVelocity.x, rb.linearVelocity.y, flatVelocity.z);
                }
            }
            else if (mat == slowMaterial)
            {
                Vector3 velocity = rb.linearVelocity;
                velocity.x = direction.x * (speed * 0.5f);
                velocity.z = direction.z * (speed * 0.5f);
                rb.linearVelocity = velocity;
            }
            else
            {
                Vector3 velocity = rb.linearVelocity;
                velocity.x = direction.x * speed;
                velocity.z = direction.z * speed;
                rb.linearVelocity = velocity;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death"))
        {
            soundManager.RandomFallSound();
            myCoroutine = StartCoroutine(Resetting());
        }
    }

    IEnumerator Resetting()
    {
        yield return new WaitForSeconds(3f);
        gameManager.ResetLevel();
    }
}
