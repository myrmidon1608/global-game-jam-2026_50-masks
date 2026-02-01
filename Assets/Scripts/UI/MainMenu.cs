using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    private TMP_Text _menuText;
    private Button _startButton;
    private Button _quitButton;

    [SerializeField]
    private GameObject HappyMask;
    [SerializeField]
    private GameObject CreepyMask;

    void Awake() {
        _menuText = GetComponentInChildren<TMP_Text>();
        _startButton = transform.Find("Start").GetComponent<Button>();
        _quitButton = transform.Find("Quit").GetComponent<Button>();
        _startButton.onClick.AddListener(StartGame);
        _quitButton.onClick.AddListener(QuitGame);
    }

    private void StartGame() {
        SceneManager.LoadScene("MainScene");
    }

    public void QuitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
