using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoseGameMenu : MonoBehaviour {

    private TMP_Text _menuText;
    private Button _restartButton;
    private Button _quitButton;

    void Awake() {
        _menuText = GetComponentInChildren<TMP_Text>();
        _restartButton = transform.Find("Restart").GetComponent<Button>();
        _quitButton = transform.Find("Quit").GetComponent<Button>();
        _restartButton.onClick.AddListener(Restart);
        _quitButton.onClick.AddListener(Quit);
        gameObject.SetActive(false);
    }

    private void Restart() {
        GameManager.Instance.ResetLevel();
    }

    private void Quit() {
        GameManager.Instance.QuitGame();
    }

    public void Open() {
        double totalTime = (Mathf.Round(GameManager.Instance.TotalTime * 100)) / 100.0;
        _menuText.text = "Total time: " + totalTime.ToString() + " seconds";
        gameObject.SetActive(true);
    }
}
