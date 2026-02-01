using UnityEngine;

public class Teapot : MonoBehaviour
{

    private int _direction = -1;
    private float _speed = 5f;
    private float _rotationMax = 40f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_direction);
        Debug.Log(transform.localEulerAngles.x);
        Debug.Log(360 + _rotationMax);


        transform.Rotate(Mathf.Lerp(0, _rotationMax * _direction, Time.deltaTime / _speed),
        transform.localRotation.y,
        transform.localRotation.z);

        if (_direction < 0 && transform.localEulerAngles.x <= (360 + (_rotationMax * _direction))) {
            _direction = 1;
        }
        else if (_direction > 0 && transform.localEulerAngles.x <= 1) {
            _direction = -1;
        }
    }
}
