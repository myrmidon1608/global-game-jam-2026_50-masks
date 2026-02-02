using UnityEngine;

public class StageManager : MonoBehaviour
{
    public float RotationSpeed = 100;

    public GameObject[] Stages;

    private Stage _currentStage;
    private Player _player;

    private int currentStageIndex = -1;

    [SerializeField] 
    Material teaSkybox;
    [SerializeField] 
    Material tikiSkybox;
    [SerializeField]
    Material snowSkybox;

    void Start() {
        _player = GetComponentInChildren<Player>();
    }

    void Update() {
        transform.Rotate(transform.localRotation.x,
            Mathf.Lerp(0, -360, Time.deltaTime / RotationSpeed),
            transform.localRotation.z);
    }

    public void SwitchStage(int stageIndex) {
        if (_currentStage != null) {
            _currentStage.gameObject.SetActive(false);
        }

        currentStageIndex = (stageIndex - 1) % Stages.Length;

        GameObject newStage = Stages[(stageIndex - 1) % Stages.Length]; // Instantiate(Stages[(stageIndex - 1) % Stages.Length], transform);
        newStage.SetActive(true);
        _currentStage = newStage.GetComponent<Stage>();

        if (Stages[currentStageIndex].name == "TeaShop")
        {
            RenderSettings.skybox = teaSkybox;
        }
        else if (Stages[currentStageIndex].name == "Tiki")
        {
            RenderSettings.skybox = tikiSkybox;
        }
        else if (Stages[currentStageIndex].name == "Snow")
        {
            RenderSettings.skybox = snowSkybox;
        }
    }
}
