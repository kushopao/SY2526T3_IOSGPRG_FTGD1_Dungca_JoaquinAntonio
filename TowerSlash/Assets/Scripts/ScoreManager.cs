using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : Singleton<ScoreManager>
{
    private float _currentScore = 0f;
    [SerializeField] private TextMeshProUGUI _inGameScoreText;
    [SerializeField] private TextMeshProUGUI _gameOverScoreText;

    private void Awake()
    {
        GameManager.Instance.ScoreManager = this;
    }

    public void AddScore(float score)
    {
        _currentScore += score;
        UpdateInGameScoreText();
    }

    private void UpdateInGameScoreText()
    {
        _inGameScoreText.text = "Score: " + _currentScore.ToString();
    }

    public void UpdateGameOverScoreText()
    {
        _gameOverScoreText.text = "Final Score:\n" + _currentScore.ToString();
    }

    public void ResetScore()
    {
        _currentScore = 0f;
        UpdateInGameScoreText();
    }
}
