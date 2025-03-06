using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    //Beispielwerte zum testen:

    private string characterName { get; set; }
    private int stamina = 100;
    private int health = 100;
    private int level = 1;
    private string rank = "Rekrut";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); //Nur ein Gamemanager soll existieren
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (CharacterStats.Instance == null)
        {
            Debug.Log("CharacterStats wurde nicht gefunden, daher wird es erstellt.");
            GameObject charStatsObj = new GameObject("CharacterStats");
            charStatsObj.AddComponent<CharacterStats>();
        }
    }

    private void Start()
    {
        if (CharacterStats.Instance == null)
        {
            Debug.LogError("CharacterStats.Instance ist null! Stelle sicher, dass CharacterStats in der Szene existiert.");
            return; // verhindert weitere Fehler
        }

        // Werte aus CharacterStats ziehen und ins UI übertragen
        SetCharacterStats(
            CharacterStats.Instance.CharacterName,
            CharacterStats.Instance.Stamina,
            CharacterStats.Instance.Health,
            CharacterStats.Instance.Level,
            CharacterStats.Instance.Rank
        );

        UpdateUI();
        CharacterStats.Instance.OnStatsChanged += UpdateUI;
    }



    //Aktualisiert das UI mit aktuellen Werten
    public void UpdateUI()
    {
        if (characterNameText != null) characterNameText.text = $"{characterName}";
        if (staminaText != null) staminaText.text = $"Ausdauer: {stamina}";
        if (hpText != null) hpText.text = $"Leben: {health}";
        if (levelText != null) levelText.text = $"{level}";
        if (rankText != null) rankText.text = $"{rank}";
    }

    public void SetCharacterStats(string name, int stam, int hp, int lvl, string rnk)
    {
        characterName = name;
        stamina = stam;
        health = hp;
        level = lvl;
        rank = rnk;
        UpdateUI();
    }
}
