using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    public EventBus EventBus { get; private set; }

    public SoundManager SoundManager;

    public SpawnManager SpawnManager;

    public StageManager StageManager;

    public LoseGameMenu LoseGameMenu;

    public int CurrentStage { get; private set; } = 1;
    public float TotalTime { get; private set; } = 0;

    protected override void Awake()
    {
        base.Awake();
        EventBus = new EventBus();
    }

    private void Start() {
        StageManager.SwitchStage(CurrentStage);
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

    public void PauseGame() {
        Time.timeScale = 0;
    }

    public bool IsGamePaused() {
        return Time.timeScale == 0;
    }

    public void ResumeGame() {
        Time.timeScale = 1;
    }

    public void QuitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void ResetLevel()
    {
        EventBus.LoseGame.Invoke();
        CurrentStage = 1;
        TotalTime = 0;
        StageManager.SwitchStage(CurrentStage);
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);

        ResumeGame();
    }

    public void UpdateStage() {
        CurrentStage++;
        StageManager.SwitchStage(CurrentStage);
    }

    public void SetTime(float value) {
        TotalTime += value;
    }

    public void OpenLoseGameMenu() {
        PauseGame();
        LoseGameMenu.Open();
    }
}
