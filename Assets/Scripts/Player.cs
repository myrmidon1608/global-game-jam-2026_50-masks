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

    private Coroutine myCoroutine = null;

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
        Vector3 direction = new Vector3(input.x, 0f, input.y);
        direction = Vector3.ClampMagnitude(direction, 1f);

        Vector3 velocity = rb.linearVelocity;
        velocity.x = direction.x * speed;
        velocity.z = direction.z * speed;

        rb.linearVelocity = velocity;
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
