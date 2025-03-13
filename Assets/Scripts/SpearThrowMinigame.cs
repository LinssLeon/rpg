using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpearThrowMinigame : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Slider slider;
    [SerializeField] private RectTransform targetZone;
    [SerializeField] private TextMeshProUGUI resultText;

    [Header("Settings")]
    [SerializeField] private float sliderSpeed = 5f;
    [SerializeField] private int precisionReward = 2;
    [SerializeField] private int staminaCost = 10;

    private bool isMovingRight = true;
    private bool gameActive = true;

    private void Start()
    {
        slider.minValue = 0;
        slider.maxValue = 1;
        slider.value = 0.5f;
        resultText.text = "";
    }

    private void Update()
    {
        if (!gameActive) return;

        MoveSlider();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckResult();
        }
    }

    private void MoveSlider()
    {
        if (isMovingRight)
        {
            slider.value += sliderSpeed * Time.deltaTime;
            if (slider.value >= slider.maxValue)
            {
                isMovingRight = false;
            }
        }
        else
        {
            slider.value -= sliderSpeed * Time.deltaTime;
            if (slider.value <= slider.minValue)
            {
                isMovingRight = true;
            }
        }
    }

    private void CheckResult()
    {
        float sliderPos = slider.value;
        float targetStart = targetZone.anchorMin.x;
        float targetEnd = targetZone.anchorMax.x;

        GameManager.Instance.AdjustStamina(-staminaCost);

        if (sliderPos >= targetStart && sliderPos <= targetEnd)
        {
            GameManager.Instance.IncreaseStat("precision", precisionReward);
            resultText.text = "Treffer! Präzision + " + precisionReward;
        }
        else
        {
            resultText.text = "Verfehlt! Ausdauer - " + staminaCost;
        }

        gameActive = false;
        StartCoroutine(ResetMinigame());
    }

    private IEnumerator ResetMinigame()
    {
        yield return new WaitForSeconds(2f);
        slider.value = 0.5f;
        resultText.text = "";
        gameActive = true;
    }
}
