using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemCountController : MonoBehaviour
{
    public TMP_Text numberText;
    private float currentScore = 0f;
    private float scoreIncreaseRate;
    public float duration;

    void Start()
    {
        // Calculate the score increase rate based on the target score and desired duration (1 second)
        scoreIncreaseRate = GameManager.Instance.coins / duration;
    }

    void Update()
    {
        // Increase the score over time
        currentScore += scoreIncreaseRate * Time.deltaTime;
        currentScore = Mathf.Min(currentScore, GameManager.Instance.coins);
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        numberText.text = Mathf.RoundToInt(currentScore).ToString();
    }
}
