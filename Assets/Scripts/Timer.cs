using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Timer : MonoBehaviour {
    [SerializeField]
    float TimerTotal = 30;
    private float _timeRemaining;

    [SerializeField]
    GameObject Mask;

    [SerializeField]
    GameObject Volume;

    private SpriteRenderer _maskRenderer;
    private Volume _globalVolume;

    void Awake() {
        _maskRenderer = Mask.GetComponent<SpriteRenderer>();
        _globalVolume = Volume.GetComponent<Volume>();
        _timeRemaining = TimerTotal;
    }

    private void Start() {
    }

    void Update() {
        if (_timeRemaining > 0) {
            _timeRemaining -= Time.deltaTime;
            UpdateColorVolume(Mathf.Lerp(-100, 100, _timeRemaining / 10));
        }
        else {
            _timeRemaining = TimerTotal;
            UpdateColorVolume(100);
        }
    }

    private void UpdateColorVolume(float value) {
        if (_globalVolume.profile.TryGet<ColorAdjustments>(out ColorAdjustments color))
        {
            color.saturation.value = value;
        }
    }
}
