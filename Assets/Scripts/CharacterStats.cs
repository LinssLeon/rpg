using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public static CharacterStats Instance { get; private set; }

    private string characterName = "Unbekannt";
    private int stamina = 100;
    private int health = 100;
    private int level = 1;
    private string rank = "Rekrut";
    private int drachme = 0;

    private int strength = 10;
    private int precision = 10;
    private int agility = 10;
    private int endurance = 10;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public string GetName() => characterName;
    public int GetStamina() => stamina;
    public int GetHealth() => health;
    public int GetLevel() => level;
    public string GetRank() => rank;
    public int GetDrachme() => drachme;

    public int GetStrength() => strength;
    public int GetPrecision() => precision;
    public int GetAgility() => agility;
    public int GetEndurance() => endurance;

    public void ChangeStamina(int amount)
    {
        stamina = Mathf.Clamp(stamina + amount, 0, 100);
    }

    public void IncreaseStat(string statName, int amount)
    {
        switch (statName)
        {
            case "Strength": strength += amount; break;
            case "Precision": precision += amount; break;
            case "Agility": agility += amount; break;
            case "Endurance": endurance += amount; break;
        }
    }

    public void UpdateStats(string name, int hp, int stam, int lvl, string playerRank, int drchme)
    {
        characterName = name;
        health = hp;
        stamina = stam;
        level = lvl;
        rank = playerRank;
        drachme = drchme;
    }
}
