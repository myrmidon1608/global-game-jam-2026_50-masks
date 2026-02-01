using UnityEngine;

public class Teapot : MonoBehaviour
{

    private int _direction = -1;
    private float _rotationMax = 40f;

    [SerializeField]
    private GameObject Tea;
    [SerializeField]
    private float Speed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Mathf.Lerp(0, _rotationMax * _direction, Time.deltaTime / Speed),
        transform.localRotation.y,
        transform.localRotation.z);
        Tea.SetActive(_direction < 0);

        if (_direction < 0 && transform.localEulerAngles.x <= (360 + (_rotationMax * _direction))) {
            _direction = 1;
        }
        else if (_direction > 0 && transform.localEulerAngles.x <= 1) {
            _direction = -1;
        }
    }
}
