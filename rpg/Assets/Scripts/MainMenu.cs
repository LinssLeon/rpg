using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public void OpenOptions()
    {
        // Muss später noch implementiert werden
        Debug.Log("Optionen geöffnet");
    }

    public void ExitGame()
    {
        Debug.Log("Spiel beendet..");
        Application.Quit();
    }
}
