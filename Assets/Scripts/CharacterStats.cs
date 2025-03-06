using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public static CharacterStats Instance { get; private set; }

    public string CharacterName { get; private set; }
    public int Stamina { get; private set; }
    public int Health { get; private set; }
    public int Level { get; private set; }
    public string Rank { get; private set; }

    public delegate void StatsChanged();
    public event StatsChanged OnStatsChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Stellt sicher, dass nur eine Instanz existiert
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateStats(string characterName, int stamina, int health, int level, string rank)
    {
        CharacterName = characterName;
        Stamina = stamina;
        Health = health;
        Level = level;
        Rank = rank;

        OnStatsChanged?.Invoke();
    }
}
