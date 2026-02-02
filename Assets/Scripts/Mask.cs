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
        if (GameManager.Instance.SoundManager.IsPlaying(SoundName.HorrorScreamSound))
        {
            GameManager.Instance.SoundManager.Stop(SoundName.HorrorScreamSound);
        }
        if (GameManager.Instance.SoundManager.IsPlaying(SoundName.SadToHorrorSound))
        {
            GameManager.Instance.SoundManager.Stop(SoundName.SadToHorrorSound);
        }
        if (GameManager.Instance.SoundManager.IsPlaying(SoundName.HorrorGameMusic))
        {
            GameManager.Instance.SoundManager.Stop(SoundName.HorrorGameMusic);
        }
        if (!GameManager.Instance.SoundManager.IsPlaying(SoundName.BirdsChirpingSound))
        {
            GameManager.Instance.SoundManager.Play(SoundName.BirdsChirpingSound);
        }
        if (!GameManager.Instance.SoundManager.IsPlaying(SoundName.HappyGameMusic))
        {
            GameManager.Instance.SoundManager.Play(SoundName.HappyGameMusic);
        }
        HappyMask.SetActive(true);
        SadMask.SetActive(false);
        CreepyMask.SetActive(false);
        CreepyMaskSpotlight.SetActive(false);
    }

    private void ChangeToSadMask() {
        if (GameManager.Instance.SoundManager.IsPlaying(SoundName.BirdsChirpingSound))
        {
            GameManager.Instance.SoundManager.Stop(SoundName.BirdsChirpingSound);
        }
        if (GameManager.Instance.SoundManager.IsPlaying(SoundName.HappyGameMusic))
        {
            GameManager.Instance.SoundManager.Stop(SoundName.HappyGameMusic);
        }
        if (!GameManager.Instance.SoundManager.IsPlaying(SoundName.SadGameMusic))
        {
            GameManager.Instance.SoundManager.Play(SoundName.SadGameMusic);
        }
        if (!GameManager.Instance.SoundManager.IsPlaying(SoundName.HappyToSadSound))
        {
            GameManager.Instance.SoundManager.Play(SoundName.HappyToSadSound);
        }
        if (!GameManager.Instance.SoundManager.IsPlaying(SoundName.SilentWeepingSound))
        {
            GameManager.Instance.SoundManager.Play(SoundName.SilentWeepingSound);
        }
        HappyMask.SetActive(false);
        SadMask.SetActive(true);
        CreepyMask.SetActive(false);
        CreepyMaskSpotlight.SetActive(false);
    }

    private void ChangeToCreepyMask() {
        if (GameManager.Instance.SoundManager.IsPlaying(SoundName.SilentWeepingSound))
        {
            GameManager.Instance.SoundManager.Stop(SoundName.SilentWeepingSound);
        }
        if (GameManager.Instance.SoundManager.IsPlaying(SoundName.HappyToSadSound))
        {
            GameManager.Instance.SoundManager.Stop(SoundName.HappyToSadSound);
        }
        if (GameManager.Instance.SoundManager.IsPlaying(SoundName.SadGameMusic))
        {
            GameManager.Instance.SoundManager.Stop(SoundName.SadGameMusic);
        }
        if (!GameManager.Instance.SoundManager.IsPlaying(SoundName.HorrorGameMusic))
        {
            GameManager.Instance.SoundManager.Play(SoundName.HorrorGameMusic);
        }
        if (!GameManager.Instance.SoundManager.IsPlaying(SoundName.SadToHorrorSound))
        {
            GameManager.Instance.SoundManager.Play(SoundName.SadToHorrorSound);
        }
        if (!GameManager.Instance.SoundManager.IsPlaying(SoundName.HorrorScreamSound))
        {
            GameManager.Instance.SoundManager.Play(SoundName.HorrorScreamSound);
        }
        HappyMask.SetActive(false);
        SadMask.SetActive(false);
        CreepyMask.SetActive(true);
        CreepyMaskSpotlight.SetActive(true);
    }
}
