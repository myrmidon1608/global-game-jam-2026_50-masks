using UnityEngine;

public class Environment : MonoBehaviour
{
    private float _rotationSpeed = 100;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Rotate(transform.localRotation.x,
            Mathf.Lerp(0, -360, Time.deltaTime / _rotationSpeed),
            transform.localRotation.z);
    }
}
