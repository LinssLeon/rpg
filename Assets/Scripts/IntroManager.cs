using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class IntroManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_InputField nameInputField;
    public TextMeshProUGUI errorMessage;

    public void OnSubmitName()
    {
        string playerName = nameInputField.text;

        if (!string.IsNullOrEmpty(playerName))
        {
            CharacterStats.Instance.UpdateStats(playerName, 100, 100, 1, "Recruit");
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            StartCoroutine(ShowErrorMessage("Bitte gebe einen Namen ein.."));
        }
    }

    private IEnumerator ShowErrorMessage(string message)
    {
        errorMessage.text = message;
        errorMessage.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        errorMessage.gameObject.SetActive(false);
    }

    private void Awake()
    {
        if (errorMessage == null)
        {
            Debug.LogError("ErrorMessage is not assigned in the Inspector!");
        }
        if (CharacterStats.Instance == null)
        {
            Debug.LogError("CharacterStats.Instance ist null! Der GameManager fehlt in der Szene.");
        }

    }



}
