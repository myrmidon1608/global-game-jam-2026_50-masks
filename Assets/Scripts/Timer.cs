using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Timer : MonoBehaviour {
    public float timeRemaining = 10;

    [SerializeField]
    GameObject Mask;

    [SerializeField]
    GameObject Volume;

    private SpriteRenderer _maskRenderer;
    private Volume _globalVolume;

    void Awake() {
        _maskRenderer = Mask.GetComponent<SpriteRenderer>();
        _globalVolume = Volume.GetComponent<Volume>();
    }

    void Update() {
        if (timeRemaining > 0) {
            timeRemaining -= Time.deltaTime;
        }
        //Debug.Log(timeRemaining);
        if (_globalVolume.profile.TryGet<ColorAdjustments>(out ColorAdjustments color)) {
            color.saturation.value = Mathf.Lerp(-100, 100, timeRemaining / 10);
            Debug.Log(color.saturation.value);
        }
    }
}
