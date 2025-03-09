using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TrainingManager : MonoBehaviour
{
    [Header("Training Buttons")]
    public Button spearThrowButton;
    public Button shieldDrillButton;
    public Button enduranceRunButton;
    public Button swordPracticeButton;
    public Button backToGameSceneButton;

    [Header("UI Elements")]
    public TextMeshProUGUI staminaText;
    public TextMeshProUGUI strengthText;
    public TextMeshProUGUI precisionText;
    public TextMeshProUGUI agilityText;
    public TextMeshProUGUI enduranceText;
    public TextMeshProUGUI feedbackText;

    private void Start()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager nicht gefunden!");
            return;
        }

        // Buttons mit Funktionen verknüpfen
        spearThrowButton.onClick.AddListener(() => Train("Speerwerfen", 10, "Precision"));
        shieldDrillButton.onClick.AddListener(() => Train("Schildübungen", 15, "Strength"));
        enduranceRunButton.onClick.AddListener(() => Train("Ausdauerlauf", 20, "Endurance"));
        swordPracticeButton.onClick.AddListener(() => Train("Schwertkampf", 12, "Agility"));

        backToGameSceneButton.onClick.AddListener(ReturnToGameScene);

        // UI initial laden
        UpdateUI();
    }

    private void Train(string trainingType, int staminaCost, string statToBoost)
    {
        if (GameManager.Instance.GetStamina() >= staminaCost)
        {
            GameManager.Instance.DecreaseStamina(staminaCost);
            GameManager.Instance.IncreaseStat(statToBoost, 1);
            feedbackText.text = $"{trainingType} abgeschlossen! +1 {statToBoost}. Verbleibende Ausdauer: {GameManager.Instance.GetStamina()}";
            UpdateUI();
        }
        else
        {
            feedbackText.text = "Nicht genug Ausdauer für dieses Training!";
        }
    }

    private void UpdateUI()
    {
        staminaText.text = $"Ausdauer: {GameManager.Instance.GetStamina()}";
        strengthText.text = $"Stärke: {GameManager.Instance.GetStrength()}";
        precisionText.text = $"Präzision: {GameManager.Instance.GetPrecision()}";
        agilityText.text = $"Beweglichkeit: {GameManager.Instance.GetAgility()}";
        enduranceText.text = $"Ausdauer: {GameManager.Instance.GetEndurance()}";
    }

    public void ReturnToGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
