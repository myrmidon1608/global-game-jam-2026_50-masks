using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField]
    float Force;

    private void OnTriggerStay(Collider other) {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null) {
            rb.AddForce(transform.forward * Force, ForceMode.Acceleration);
        }
    }
}
