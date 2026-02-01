using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    public EventBus EventBus { get; private set; }

    public SoundManager SoundManager;

    public SpawnManager SpawnManager;

    public int CurrentStage { get; private set; } = 1;
    public float TotalTime { get; private set; } = 0;

    protected override void Awake()
    {
        base.Awake();
        EventBus = new EventBus();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }

        //Debug.Log(enemyCount);
    }

    public void ResetLevel()
    {
        EventBus.LoseGame.Invoke();
        CurrentStage = 1;
        TotalTime = 0;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void UpdateStage() {
        CurrentStage++;
    }

    public void SetTime(float value) {
        TotalTime += value;
    }
}
