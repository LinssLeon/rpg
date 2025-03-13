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
        yield return new WaitForEndOfFrame(); // Warten, bis die Szene vollständig geladen ist
        BindUIElements(sceneName);
        UpdateUI();
    }

    private void BindUIElements(string sceneName)
    {
        // GameScene UI
        if (sceneName == "GameScene")
        {
            characterNameText = TryFindTextElement("CharacterNameText");
            staminaText = TryFindTextElement("StaminaText");
            hpText = TryFindTextElement("HPText");
            levelText = TryFindTextElement("LevelText");
            rankText = TryFindTextElement("RankText");
            drachmeText = TryFindTextElement("DrachmeText");
        }

        // TrainingScene UI
        if (sceneName == "TrainingScene")
        {
            characterNameText = TryFindTextElement("CharacterNameText");
            staminaText = TryFindTextElement("StaminaText");
            strengthText = TryFindTextElement("StrengthText");
            precisionText = TryFindTextElement("PrecisionText");
            agilityText = TryFindTextElement("AgilityText");
            enduranceText = TryFindTextElement("EnduranceText");
        }
    }

    private TextMeshProUGUI TryFindTextElement(string name)
    {
        var element = GameObject.Find(name)?.GetComponent<TextMeshProUGUI>();
        return element; // Kein Log, wenn null
    }

    public void UpdateUI()
    {
        if (CharacterStats.Instance == null) return;

        var stats = CharacterStats.Instance;

        // GameScene-UI
        if (characterNameText) characterNameText.text = stats.GetName();
        if (staminaText) staminaText.text = $"Ausdauer: {stats.GetStamina()}";
        if (hpText) hpText.text = $"Leben: {stats.GetHealth()}";
        if (levelText) levelText.text = $"Level: {stats.GetLevel()}";
        if (rankText) rankText.text = $"Rang: {stats.GetRank()}";
        if (drachmeText) drachmeText.text = $"Drachme: {stats.GetDrachme()}";

        // TrainingScene-UI
        if (strengthText) strengthText.text = $"Stärke: {stats.GetStrength()}";
        if (precisionText) precisionText.text = $"Präzision: {stats.GetPrecision()}";
        if (agilityText) agilityText.text = $"Beweglichkeit: {stats.GetAgility()}";
        if (enduranceText) enduranceText.text = $"Ausdauer: {stats.GetEndurance()}";
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
