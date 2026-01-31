using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public enum TimerState
{
    HAPPY, SAD, CREEPY
}


public class Timer : MonoBehaviour {
    [SerializeField]
    float TimerTotal = 30;

    float SadMaskDuration = 0;
    float AngryMaskDuration = 0;
    private float _timeRemaining;

    [SerializeField]
    GameObject Mask;

    [SerializeField]
    GameObject Volume;
    private TimerState _state;

    private SpriteRenderer _maskRenderer;
    private Volume _globalVolume;

    void Awake() {
        _maskRenderer = Mask.GetComponent<SpriteRenderer>();
        _globalVolume = Volume.GetComponent<Volume>();
        _timeRemaining = TimerTotal;
    }

    private void Start()
    {
        GameManager.Instance.EventBus.LoseGame += Reset;
        CalculateMasksDurations();
    }

    void Update() {
        if (_timeRemaining > 0) {
            _timeRemaining -= Time.deltaTime;
            UpdateColorVolume(Mathf.Lerp(-100, 100, _timeRemaining / 10));

            if (_state == TimerState.SAD && _timeRemaining <= AngryMaskDuration)
            {
                GameManager.Instance.EventBus.MaskCreepyStart.Invoke();
                _state = TimerState.CREEPY;
            }
            else if (_state == TimerState.HAPPY && _timeRemaining <= (AngryMaskDuration + SadMaskDuration))
            {
                GameManager.Instance.EventBus.MaskSadStart.Invoke();
                _state = TimerState.SAD;
            }
        }
        else {
            GameManager.Instance.UpdateStage();
            CalculateMasksDurations(); //New stage, new time durations 
            Reset(); //Change Mask to happy mask on new stage
        }
    }

    private void Reset()
    {
        _timeRemaining = TimerTotal;
        UpdateColorVolume(100);
        GameManager.Instance.EventBus.MaskHappyStart.Invoke();
        _state = TimerState.HAPPY;
    }

    private void UpdateColorVolume(float value) {
        if (_globalVolume.profile.TryGet<ColorAdjustments>(out ColorAdjustments color))
        {
            color.saturation.value = value;
        }
    }

    // Calculating the Masks's duration depending on the stage 
    private void CalculateMasksDurations()
    {
        int stage = GameManager.Instance.CurrentStage;
        // e.g with 60 seconds total time
        switch (stage) {
            case 1:
                // HappyMaskDuration: 30
                SadMaskDuration = TimerTotal / 3; // 20
                AngryMaskDuration = TimerTotal / 6; // 10
                break;

            case 2:
                // HappyMaskDuration: 25
                SadMaskDuration = TimerTotal / 4; // 15
                AngryMaskDuration = TimerTotal / 3; // 20
                break;

            case 3:
                // HappyMaskDuration: 20
                SadMaskDuration = TimerTotal / 3; // 20
                AngryMaskDuration = TimerTotal / 3; // 20
                break;

            case 4:
                // HappyMaskDuration: 15
                SadMaskDuration = TimerTotal / 4; // 15
                AngryMaskDuration = TimerTotal / 2; // 30
                break;

            default:
                // all stages beyond stage 4
                // HappyMaskDuration: 10
                SadMaskDuration = TimerTotal / 3; // 20
                AngryMaskDuration = TimerTotal / 2; // 30
                break;
        }

    }
}
