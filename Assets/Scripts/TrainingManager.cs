using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TrainingManager : MonoBehaviour
{
    [Header("Training Buttons")]
    [SerializeField] private Button spearThrowButton;
    [SerializeField] private Button shieldDrillButton;
    [SerializeField] private Button enduranceRunButton;
    [SerializeField] private Button swordPracticeButton;
    [SerializeField] private Button backToGameSceneButton;

    [Header("Feedback")]
    [SerializeField] private TextMeshProUGUI feedbackText;

    private void OnEnable()
{
    if (GameManager.Instance == null) return;

    // Training-Buttons zuweisen
    spearThrowButton.onClick.AddListener(() => Train("Speerwerfen", 10, "Precision"));
    shieldDrillButton.onClick.AddListener(() => Train("Schildübungen", 15, "Strength"));
    enduranceRunButton.onClick.AddListener(() => Train("Ausdauerlauf", 20, "Endurance"));
    swordPracticeButton.onClick.AddListener(() => Train("Schwertkampf", 12, "Agility"));

    // Debug vor Szenenwechsel
    backToGameSceneButton.onClick.AddListener(() =>
    {
        Debug.Log("Vor Szenenwechsel: Stamina = " + CharacterStats.Instance.GetStamina());
        SceneManager.LoadScene("GameScene");
    });
}


    private void Train(string trainingType, int staminaCost, string statToBoost)
    {
        if (CharacterStats.Instance.GetStamina() >= staminaCost)
        {
            GameManager.Instance.AdjustStamina(-staminaCost);
            GameManager.Instance.IncreaseStat(statToBoost, 1);
            feedbackText.text = $"{trainingType} abgeschlossen! +1 {statToBoost}.";
        }
        else
        {
            feedbackText.text = "Nicht genug Ausdauer!";
        }
    }
}
