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

    public GameObject playerMesh;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        maxSpeed = speed;
    }

    void FixedUpdate()
    {
        if (!launched) {
            Vector3 input = moveAction.ReadValue<Vector3>();
            Vector3 direction = new Vector3(input.x, 0f, input.z).normalized;
            //Debug.Log(direction);

            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1.1f, groundLayer)) {
                PhysicsMaterial mat = hit.collider.sharedMaterial;

                if (mat == iceMaterial) {
                    Vector3 forceDir = Vector3.zero;
                    if (direction.x != 0) {
                        forceDir += direction.x * transform.right;
                    }
                    if (direction.z != 0) {
                        forceDir += direction.z * transform.forward;
                    }
                    rb.linearDamping = 1;
                    rb.AddForce(direction.magnitude * speed * forceDir, ForceMode.Acceleration);

                    //Vector3 flatVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
                    //if (flatVelocity.magnitude > maxSpeed) {
                    //    flatVelocity = flatVelocity.normalized * maxSpeed;
                    //    rb.linearVelocity = new Vector3(flatVelocity.x, rb.linearVelocity.y, flatVelocity.z);
                    //}
                } else if (mat == slowMaterial) {
                    rb.linearDamping = 7;
                    rb.AddForce(direction * (speed / 2), ForceMode.Force);
                } else {
                    rb.linearDamping = 5;
                    rb.AddForce(direction * speed, ForceMode.Force);
                }
            } else {
                // falling
                rb.linearDamping = 0;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpringTrap"))
        {
            rb.linearDamping = 0;
        }
        if (other.CompareTag("Meteor"))
        {
            playerMesh.SetActive(false);
            GameManager.Instance.SoundManager.RandomFallSound();
            myCoroutine = StartCoroutine(Resetting());
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
        GameManager.Instance.OpenLoseGameMenu();
    }
}
