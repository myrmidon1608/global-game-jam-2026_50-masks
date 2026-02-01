using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    Rigidbody rb;
    PlayerInput playerInput;
    InputAction moveAction;

    [SerializeField]
    float speed;

    float maxSpeed;

    private Coroutine myCoroutine = null;

    [SerializeField] 
    LayerMask groundLayer;

    [SerializeField] 
    PhysicsMaterial iceMaterial;
    [SerializeField]
    PhysicsMaterial slowMaterial;

    public bool launched;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        maxSpeed = speed;
    }

    void Update()
    {
        if (!launched)
        {
            Vector3 input = moveAction.ReadValue<Vector3>();
            Vector3 direction = new Vector3(input.x, 0f, input.z).normalized;

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
                    rb.linearVelocity = direction * (speed / 2);
                }
                else
                {
                    rb.linearVelocity = direction * speed;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Death"))
        {
            GameManager.Instance.SoundManager.RandomFallSound();
            myCoroutine = StartCoroutine(Resetting());
        }
    }

    IEnumerator Resetting()
    {
        yield return new WaitForSeconds(1f);
        GameManager.Instance.ResetLevel();
    }
}
