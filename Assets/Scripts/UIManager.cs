using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button storyButton;
    public Button trainingButton;
    public Button tavernButton;
    public Button missionButton;
    public Button shopButton;
    public Button equipmentButton;

    private void Start()
    {
        // Button-Events verknüpfen
        storyButton.onClick.AddListener(StartStory);
        trainingButton.onClick.AddListener(OpenTraining);
        tavernButton.onClick.AddListener(EnterTavern);
        missionButton.onClick.AddListener(StartMission);
        shopButton.onClick.AddListener(OpenShop);
        equipmentButton.onClick.AddListener(OpenEquipment);
    }

    private void StartStory()
    {
        Debug.Log("Story gestartet!");
        SceneManager.LoadScene("StoryScene");
    }

    private void OpenTraining()
    {
        Debug.Log("Trainingsbereich geöffnet!");
        SceneManager.LoadScene("TrainingScene");
    }

    private void EnterTavern()
    {
        Debug.Log("Taverne betreten!");
        SceneManager.LoadScene("TavernScene");
    }

    private void StartMission()
    {
        Debug.Log("Mission gestartet!");
        SceneManager.LoadScene("MissionScene");
    }

    private void OpenShop()
    {
        Debug.Log("Shop geöffnet!");
        SceneManager.LoadScene("ShopScene");
    }

    private void OpenEquipment()
    {
        Debug.Log("Ausrüstung angezeigt!");
        SceneManager.LoadScene("EquipmentScene");
    }

}
