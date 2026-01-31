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

    //New stuff
    float HappyMaskDuration = 0;
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

    private void Start() {
    }

    void Update() {
        if (_timeRemaining > 0) {
            _timeRemaining -= Time.deltaTime;
            UpdateColorVolume(Mathf.Lerp(-100, 100, _timeRemaining / 10));
            
            if (_state <= TimerState.HAPPY && _timeRemaining >= HappyMaskDuration)
            {
                _state = TimerState.SAD;
            }
            else if (_state <= TimerState.SAD && _timeRemaining >= SadMaskDuration)
            {
                _state = TimerState.CREEPY;
            }
        }
        else {
            _timeRemaining = TimerTotal;
            UpdateColorVolume(100);
            GameManager.Instance.UpdateStage();
            //Change Mask to happy mask on new stage
            CalculateMasksDurations(); //New stage, new time durations 
        }
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
        switch (stage) {
            case 1:
            HappyMaskDuration = TimerTotal/2;
            SadMaskDuration = TimerTotal/3;
            AngryMaskDuration = TimerTotal/6;
            break;

            case 2:
            HappyMaskDuration = TimerTotal/3;
            SadMaskDuration = TimerTotal/3;
            AngryMaskDuration = TimerTotal/3;
            break;

            case 3:
            HappyMaskDuration = TimerTotal/6;
            SadMaskDuration = TimerTotal/3;
            AngryMaskDuration = TimerTotal/2;
            break;

            default:
            HappyMaskDuration = TimerTotal/2;
            SadMaskDuration = TimerTotal/3;
            AngryMaskDuration = TimerTotal/6;
            break;
        }

    }
}
