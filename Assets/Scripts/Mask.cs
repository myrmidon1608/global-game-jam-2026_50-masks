using UnityEngine;

public class Mask : MonoBehaviour {

    private float _rotationSpeed = 10;

    [SerializeField]
    private GameObject HappyMask;
    [SerializeField]
    private GameObject SadMask;
    [SerializeField]
    private GameObject CreepyMask;
    [SerializeField]
    private GameObject CreepyMaskSpotlight;

    private void Start() {
        GameManager.Instance.EventBus.MaskHappyStart += ChangeToHappyMask;
        GameManager.Instance.EventBus.MaskSadStart += ChangeToSadMask;
        GameManager.Instance.EventBus.MaskCreepyStart += ChangeToCreepyMask;
        ChangeToHappyMask();
    }

    void Update() {
        transform.Rotate(transform.localRotation.x,
            Mathf.Lerp(0, 360, Time.deltaTime / _rotationSpeed),
            transform.localRotation.z);
    }

    private void ChangeToHappyMask() {
        HappyMask.SetActive(true);
        SadMask.SetActive(false);
        CreepyMask.SetActive(false);
        CreepyMaskSpotlight.SetActive(false);
    }

    private void ChangeToSadMask() {
        HappyMask.SetActive(false);
        SadMask.SetActive(true);
        CreepyMask.SetActive(false);
        CreepyMaskSpotlight.SetActive(false);
    }

    private void ChangeToCreepyMask() {
        HappyMask.SetActive(false);
        SadMask.SetActive(false);
        CreepyMask.SetActive(true);
        CreepyMaskSpotlight.SetActive(true);
    }
}
