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
        if (scene.name == "GameScene" || scene.name == "TrainingScene")
        {
            StartCoroutine(InitializeUI());
        }
    }

    private IEnumerator InitializeUI()
    {
        yield return new WaitForEndOfFrame(); // Warten, bis die Szene komplett gerendert ist
        BindUIElements();
        UpdateUI();
    }



    private void BindUIElements()
    {
        characterNameText = FindTextElement("CharacterNameText");
        staminaText = FindTextElement("StaminaText");
        hpText = FindTextElement("HPText");
        levelText = FindTextElement("LevelText");
        rankText = FindTextElement("RankText");
        drachmeText = FindTextElement("DrachmeText");

        strengthText = FindTextElement("StrengthText");
        precisionText = FindTextElement("PrecisionText");
        agilityText = FindTextElement("AgilityText");
        enduranceText = FindTextElement("EnduranceText");
    }

    private TextMeshProUGUI FindTextElement(string name)
    {
        var element = GameObject.Find(name)?.GetComponent<TextMeshProUGUI>();
        if (element == null)
        {
            Debug.LogError($"UI-Element '{name}' nicht gefunden.");
        }
        return element;
    }

    public void UpdateUI()
    {
        if (CharacterStats.Instance == null) return;

        var stats = CharacterStats.Instance;

        characterNameText.text = stats.GetName();
        staminaText.text = $"Ausdauer: {stats.GetStamina()}";
        hpText.text = $"Leben: {stats.GetHealth()}";
        levelText.text = $"Level: {stats.GetLevel()}";
        rankText.text = $"Rang: {stats.GetRank()}";
        drachmeText.text = $"Drachme: {stats.GetDrachme()}";

        strengthText.text = $"Stärke: {stats.GetStrength()}";
        precisionText.text = $"Präzision: {stats.GetPrecision()}";
        agilityText.text = $"Beweglichkeit: {stats.GetAgility()}";
        enduranceText.text = $"Ausdauer: {stats.GetEndurance()}";
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
