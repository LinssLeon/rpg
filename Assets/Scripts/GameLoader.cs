using UnityEngine;

public class GameLoader : MonoBehaviour
{
    public GameObject gameManagerPrefab;

    private void Awake()
    {
        if (GameManager.Instance == null)
        {
            GameObject gm = Instantiate(gameManagerPrefab);
            DontDestroyOnLoad(gm);
        }
    }
}
