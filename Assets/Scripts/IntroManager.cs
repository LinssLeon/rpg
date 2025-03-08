using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_InputField nameInputField;
    public TextMeshProUGUI errorMessage;
    public Button submitButton;

    private void Start()
    {
        nameInputField.Select();
        nameInputField.ActivateInputField();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            OnSubmitName();
        }
    }

    public void OnSubmitName()
    {
        string playerName = nameInputField.text;

        if (!string.IsNullOrEmpty(playerName))
        {
            if (CharacterStats.Instance == null)
            {
                GameObject characterStatsObj = new GameObject("CharacterStats");
                characterStatsObj.AddComponent<CharacterStats>();
            }

            CharacterStats.Instance.UpdateStats(playerName, 100, 100, 1, "Rekrut");
            DontDestroyOnLoad(CharacterStats.Instance.gameObject);

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
}
