using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState { MainMenu, Exploration, Training, Battle }
    public GameState currentState { get; private set; }

    [Header("Character UI Elements")]
    [SerializeField] private TextMeshProUGUI characterNameText;
    [SerializeField] private TextMeshProUGUI staminaText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private TextMeshProUGUI drachmeText;

    [Header("Training Stats UI Elements")]
    [SerializeField] private TextMeshProUGUI strengthText;
    [SerializeField] private TextMeshProUGUI precisionText;
    [SerializeField] private TextMeshProUGUI agilityText;
    [SerializeField] private TextMeshProUGUI enduranceText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(InitializeUI(scene.name));
    }

    private IEnumerator InitializeUI(string sceneName)
    {
        yield return new WaitForEndOfFrame(); // Sicherstellen, dass die Szene geladen ist
        BindUIElements(sceneName);
        UpdateUI();
    }

    private void BindUIElements(string sceneName)
    {
        if (sceneName == "GameScene" || sceneName == "TrainingScene")
        {
            characterNameText = FindTextElement("CharacterNameText");
            staminaText = FindTextElement("StaminaText");
            hpText = FindTextElement("HPText");
            levelText = FindTextElement("LevelText");
            rankText = FindTextElement("RankText");
            drachmeText = FindTextElement("DrachmeText");
        }

        if (sceneName == "TrainingScene") // Nur in der Trainingsszene nötig
        {
            strengthText = FindTextElement("StrengthText");
            precisionText = FindTextElement("PrecisionText");
            agilityText = FindTextElement("AgilityText");
            enduranceText = FindTextElement("EnduranceText");
        }
    }

    private TextMeshProUGUI FindTextElement(string name)
    {
        var element = GameObject.Find(name)?.GetComponent<TextMeshProUGUI>();
        if (element == null)
        {
            Debug.LogWarning($"UI-Element '{name}' nicht gefunden.");
        }
        return element;
    }

    public void UpdateUI()
    {
        if (CharacterStats.Instance == null) return;

        var stats = CharacterStats.Instance;

        if (characterNameText != null) characterNameText.text = stats.GetName();
        if (staminaText != null) staminaText.text = $"Ausdauer: {stats.GetStamina()}";
        if (hpText != null) hpText.text = $"Leben: {stats.GetHealth()}";
        if (levelText != null) levelText.text = $"Level: {stats.GetLevel()}";
        if (rankText != null) rankText.text = $"Rang: {stats.GetRank()}";
        if (drachmeText != null) drachmeText.text = $"Drachme: {stats.GetDrachme()}";

        if (strengthText != null) strengthText.text = $"Stärke: {stats.GetStrength()}";
        if (precisionText != null) precisionText.text = $"Präzision: {stats.GetPrecision()}";
        if (agilityText != null) agilityText.text = $"Beweglichkeit: {stats.GetAgility()}";
        if (enduranceText != null) enduranceText.text = $"Ausdauer: {stats.GetEndurance()}";
    }

    public void AdjustStamina(int amount)
    {
        CharacterStats.Instance?.ChangeStamina(amount);
        UpdateUI();
    }

    public void IncreaseStat(string statName, int amount)
    {
        CharacterStats.Instance?.IncreaseStat(statName, amount);
        UpdateUI();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
