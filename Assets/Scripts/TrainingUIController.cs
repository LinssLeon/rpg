using UnityEngine;

public class TrainingUIController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject trainingPanel; // Panel mit den Trainings-Buttons
    [SerializeField] private GameObject spearThrowPanel; // Panel fürs Speerwerfen-Minispiel

    private void Start()
    {
        ShowTrainingPanel(); // Standardmäßig die Trainings-Buttons anzeigen
    }

    public void ShowTrainingPanel()
    {
        trainingPanel.SetActive(true);
        spearThrowPanel.SetActive(false);
    }

    public void ShowSpearThrowPanel()
    {
        trainingPanel.SetActive(false);
        spearThrowPanel.SetActive(true);
    }
}
