using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState { MainMenu, Exploration, Training, Battle}
    public GameState currentState { get; private set; }

    [Header("Character UI Elements")]
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI staminaText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI rankText;

    //Beispielwerte zum testen:

    private string characterName = "Janis";
    private int stamina = 100;
    private int health = 10;
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
    }

    private void Start()
    {
        UpdateUI();
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
