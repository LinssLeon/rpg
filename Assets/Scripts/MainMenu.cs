using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public Button optionsButton;
    public Button exitButton;

    private void Awake()
    {
        if (startButton != null) startButton.onClick.AddListener(StartGame);
        if (optionsButton != null) optionsButton.onClick.AddListener(OpenOptions);
        if (exitButton != null) exitButton.onClick.AddListener(ExitGame);

        Debug.Log("StartButton found: " + (startButton != null));
        Debug.Log("OptionsButton found: " + (optionsButton != null));
        Debug.Log("ExitButton found: " + (exitButton != null));
    }

    public void StartGame()
    {
        if (GameManager.Instance != null)
        {
            SceneManager.LoadScene("IntroScene");
        }
        else
        {
            Debug.LogError("GameManager is null!");
        }
    }

    public void OpenOptions()
    {
        Debug.Log("Options opened"); // später könntest du ein OptionsPanel aktivieren
    }

    public void ExitGame()
    {
        Debug.Log("Game exited");
        Application.Quit();
    }
}
