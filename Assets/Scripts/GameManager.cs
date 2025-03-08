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

    private string characterName;
    private int stamina = 100;
    private int health = 100;
    private int level = 1;
    private string rank = "Rekrut";

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
        if (scene.name == "GameScene")
        {
            Debug.Log("GameScene geladen, UI-Elemente werden gesucht...");

            characterNameText = GameObject.Find("CharacterNameText")?.GetComponent<TextMeshProUGUI>();
            staminaText = GameObject.Find("StaminaText")?.GetComponent<TextMeshProUGUI>();
            hpText = GameObject.Find("HPText")?.GetComponent<TextMeshProUGUI>();
            levelText = GameObject.Find("LevelText")?.GetComponent<TextMeshProUGUI>();
            rankText = GameObject.Find("RankText")?.GetComponent<TextMeshProUGUI>();

            if (CharacterStats.Instance != null)
            {
                SetCharacterStats(
                    CharacterStats.Instance.CharacterName,
                    CharacterStats.Instance.Stamina,
                    CharacterStats.Instance.Health,
                    CharacterStats.Instance.Level,
                    CharacterStats.Instance.Rank
                );
            }
            else
            {
                Debug.LogError("CharacterStats Instance ist null!");
            }

            UpdateUI();
        }
    }


    public void SetCharacterStats(string name, int stam, int hp, int lvl, string rnk)
    {
        characterName = name;
        stamina = stam;
        health = hp;
        level = lvl;
        rank = rnk;

        Debug.Log($"Character gesetzt: Name={characterName}, Stamina={stamina}, Health={health}, Level={level}, Rank={rank}");

        UpdateUI();
    }

    public void UpdateUI()
    {
        if (characterNameText != null) characterNameText.text = $"Name: {characterName}";
        if (staminaText != null) staminaText.text = $"Ausdauer: {stamina}";
        if (hpText != null) hpText.text = $"Leben: {health}";
        if (levelText != null) levelText.text = $"Level: {level}";
        if (rankText != null) rankText.text = $"Rang: {rank}";

        Debug.Log("UpdateUI aufgerufen!");
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
