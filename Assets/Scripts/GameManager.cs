using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState { MainMenu, Exploration, Training, Battle }
    public GameState currentState { get; private set; }

    [Header("Character UI Elements")]
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI staminaText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI drachmeText;

    [Header("Training Stats UI Elements")]
    public TextMeshProUGUI strengthText;
    public TextMeshProUGUI precisionText;
    public TextMeshProUGUI agilityText;
    public TextMeshProUGUI enduranceText;

    private string characterName;
    private int stamina = 100;
    private int health = 100;
    private int level = 1;
    private string rank = "Rekrut";
    private int drachme = 0;

    // Neue Trainings-Stats
    private int strength = 10;    // Stärke
    private int precision = 10;   // Präzision
    private int agility = 10;     // Beweglichkeit
    private int endurance = 10;   // Ausdauer

    public int GetStrength() => strength;
    public int GetPrecision() => precision;
    public int GetAgility() => agility;
    public int GetEndurance() => endurance;


    private void Awake()
    {
        Debug.Log("GameManager Awake aufgerufen");

        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Ein zweiter GameManager wurde gefunden und wird zerstört!");
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
            Debug.Log($"{scene.name} geladen, UI-Elemente werden gesucht...");

            characterNameText = GameObject.Find("CharacterNameText")?.GetComponent<TextMeshProUGUI>();
            staminaText = GameObject.Find("StaminaText")?.GetComponent<TextMeshProUGUI>();
            hpText = GameObject.Find("HPText")?.GetComponent<TextMeshProUGUI>();
            levelText = GameObject.Find("LevelText")?.GetComponent<TextMeshProUGUI>();
            rankText = GameObject.Find("RankText")?.GetComponent<TextMeshProUGUI>();
            drachmeText = GameObject.Find("DrachmeText")?.GetComponent<TextMeshProUGUI>();

            strengthText = GameObject.Find("StrengthText")?.GetComponent<TextMeshProUGUI>();
            precisionText = GameObject.Find("PrecisionText")?.GetComponent<TextMeshProUGUI>();
            agilityText = GameObject.Find("AgilityText")?.GetComponent<TextMeshProUGUI>();
            enduranceText = GameObject.Find("EnduranceText")?.GetComponent<TextMeshProUGUI>();

            UpdateUI();
        }
    }

    public void SetCharacterStats(string name, int stam, int hp, int lvl, string rnk, int drchme, int str, int prec, int agil, int endur)
    {
        characterName = name;
        stamina = stam;
        health = hp;
        level = lvl;
        rank = rnk;
        drachme = drchme;

        strength = str;
        precision = prec;
        agility = agil;
        endurance = endur;

        Debug.Log($"Character gesetzt: Name={characterName}, Stamina={stamina}, Health={health}, Level={level}, Rank={rank}, Drachme={drachme}, Strength={strength}, Precision={precision}, Agility={agility}, Endurance={endurance}");

        UpdateUI();
    }

    public void UpdateUI()
    {
        if (characterNameText != null) characterNameText.text = $"Name: {characterName}";
        if (staminaText != null) staminaText.text = $"Ausdauer: {stamina}";
        if (hpText != null) hpText.text = $"Leben: {health}";
        if (levelText != null) levelText.text = $"Level: {level}";
        if (rankText != null) rankText.text = $"Rang: {rank}";
        if (drachmeText != null) drachmeText.text = $"Drachme: {drachme}";

        if (strengthText != null) strengthText.text = $"Stärke: {strength}";
        if (precisionText != null) precisionText.text = $"Präzision: {precision}";
        if (agilityText != null) agilityText.text = $"Beweglichkeit: {agility}";
        if (enduranceText != null) enduranceText.text = $"Ausdauer: {endurance}";

        Debug.Log("UpdateUI aufgerufen!");
    }

    public void IncreaseStat(string statName, int amount)
    {
        switch (statName)
        {
            case "Strength":
                strength += amount;
                break;
            case "Precision":
                precision += amount;
                break;
            case "Agility":
                agility += amount;
                break;
            case "Endurance":
                endurance += amount;
                break;
        }

        UpdateUI();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void DecreaseStamina(int amount)
    {
        stamina = Mathf.Max(stamina - amount, 0); // stellt sicher, dass Stamina nie negativ wird
        UpdateUI();
    }

    public int GetStamina()
    {
        return stamina;
    }

}
