using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterStats : MonoBehaviour
{
    public static CharacterStats Instance { get; private set; }

    [Header("CharacterInfo")]
    public string characterName = "Janis";
    public int stamina = 100;
    public int hp = 100;
    public int level = 1;
    public string rank = "Rekrut";

    public event Action OnStatsChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateStats(string name, int newStamina, int newHP, int newLevel, string newRank)
    {
        characterName = name;
        this.stamina = newStamina;
        this.hp = newHP;
        this.level = newLevel;
        this.rank = newRank;

        OnStatsChanged?.Invoke(); // Event auslösen, um das UI zu aktualisieren
    }
}
