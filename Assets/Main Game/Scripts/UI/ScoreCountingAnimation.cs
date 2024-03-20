using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCountingAnimation : MonoBehaviour
{
    public TMP_Text numberText;
    private float currentScore = 0f;
    private float scoreIncreaseRate;
    public float duration;

    void Start()
    {
        // Calculate the score increase rate based on the target score and desired duration (1 second)
        scoreIncreaseRate = GameManager.Instance.score / duration;
    }

    void Update()
    {
        // Increase the score over time
        currentScore += scoreIncreaseRate * Time.deltaTime;
        currentScore = Mathf.Min(currentScore, GameManager.Instance.score);
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        numberText.text = Mathf.RoundToInt(currentScore).ToString();
    }
}
