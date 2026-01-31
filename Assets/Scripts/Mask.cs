using UnityEngine;

public class Mask : MonoBehaviour {

    private float _rotationSpeed = 10;


    void Update() {
        transform.Rotate(transform.localRotation.x,
            Mathf.Lerp(0, 360, Time.deltaTime / _rotationSpeed),
            transform.localRotation.z);
    }
}
