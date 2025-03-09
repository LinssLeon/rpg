using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public static CharacterStats Instance { get; private set; }

    [Header("Character Stats")]
    public string CharacterName;
    public int Stamina;
    public int Health;
    public int Level;
    public string Rank;
    public int Drachme;

    public delegate void StatsChanged();
    public event StatsChanged OnStatsChanged;

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

    public void UpdateStats(string characterName, int stamina, int health, int level, string rank, int drachme)
    {
        this.CharacterName = characterName;
        this.Stamina = stamina;
        this.Health = health;
        this.Level = level;
        this.Rank = rank;
        this.Drachme = drachme;

        Debug.Log($"Stats gesetzt: Name={CharacterName}, Stamina={Stamina}, Health={Health}, Level={Level}, Rank={Rank}, Drachme={Drachme}");

        OnStatsChanged?.Invoke();
    }
}
