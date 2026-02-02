using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundName
{
    FallSound1,
    FallSound2,
    FallSound3,
    FallSound4,
    FallSound5,
    FallSound6,
    SpringTrapSound,
    HorrorScreamSound,
    SilentWeepingSound,
    BirdsChirpingSound,
    HappyToSadSound,
    SadToHorrorSound,
}

public class SoundManager : MonoBehaviour
{
    /*[SerializeField]
    private AudioMixer AudioMixer;*/

    [System.Serializable]
    public struct SoundEntry
    {
        public SoundName name;
        public AudioSource source;
    }

    [SerializeField]
    private SoundEntry[] soundEntries;

    private Dictionary<SoundName, AudioSource> audioDict;

    void Awake()
    {
        audioDict = new Dictionary<SoundName, AudioSource>();

        foreach (var entry in soundEntries)
        {
            audioDict[entry.name] = entry.source;
        }
    }

    void Start()
    {
        //Play(SoundName.GameMusic);
    }

    void Update()
    {
        /*if (audioDict[SoundName.Nuke].isPlaying || audioDict[SoundName.Invincible].isPlaying)
        {
            audioDict[SoundName.Stage1].Pause();
        }
        else
        {
            audioDict[SoundName.Stage1].UnPause();
        }*/
    }

    /*public float GetMixerValue(string name)
    {
        AudioMixer.GetFloat(name, out float value);
        return value;
    }

    public void SetMixerValue(string name, float value)
    {
        AudioMixer.SetFloat(name, value);
    }*/

    public void Play(SoundName sound)
    {
        if (audioDict.TryGetValue(sound, out AudioSource src))
        {
            src.Play();
        }
        else
        {
            Debug.LogWarning("No AudioSource found for " + sound);
        }
    }

    public void Stop(SoundName sound)
    {
        if (audioDict.TryGetValue(sound, out AudioSource src))
        {
            src.Stop();
        }
        else
        {
            Debug.LogWarning("No AudioSource found for " + sound);
        }
    }

    public void Pause(SoundName sound)
    {
        if (audioDict.TryGetValue(sound, out AudioSource src))
        {
            src.Pause();
        }
        else
        {
            Debug.LogWarning("No AudioSource found for " + sound);
        }
    }

    public void UnPause(SoundName sound)
    {
        if (audioDict.TryGetValue(sound, out AudioSource src))
        {
            src.UnPause();
        }
        else
        {
            Debug.LogWarning("No AudioSource found for " + sound);
        }
    }

    public bool IsPlaying(SoundName sound)
    {
        if (audioDict.TryGetValue(sound, out AudioSource src))
        {
            return src.isPlaying;
        }
        return false;
    }

    public void RandomPitch(SoundName sound, float minPitch, float maxPitch)
    {
        if (audioDict.TryGetValue(sound, out AudioSource src))
        {
            src.pitch = Random.Range(minPitch, maxPitch);
            src.Play();
        }
    }

    public void RandomFallSound()
    {
        // Create an array of the fall sounds
        SoundName[] fallSounds = new SoundName[]
        {
            SoundName.FallSound1,
            SoundName.FallSound2,
            SoundName.FallSound3,
            SoundName.FallSound4,
            SoundName.FallSound5,
            SoundName.FallSound6
        };

        int index = Random.Range(0, fallSounds.Length);

        Play(fallSounds[index]);
    }

    /*private void LevelCompleted()
    {
        foreach (var entry in audioDict)
        {
            if (entry.Key != SoundName.Stage1 && entry.Key != SoundName.Invincible)
            {
                entry.Value.Stop();
            }
        }
        audioDict[SoundName.LevelChange].Play();
    }

    private void LevelLost()
    {
        foreach (var entry in audioDict)
        {
            entry.Value.Stop();
        }
        audioDict[SoundName.Defeat].Play();
    }

    private void StageCompleted()
    {
        foreach (var entry in audioDict)
        {
            entry.Value.Stop();
        }
        audioDict[SoundName.StageCleared].Play();
    }*/
}
