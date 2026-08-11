using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Player _player;

    [SerializeField] private SwipeDetection _swipeDetection;

    [SerializeField] private ScoreManager _scoreManager;

    public Player Player
    {
        get => _player;
        set => _player = value;
    }

    public SwipeDetection SwipeDetection
    {
        get => _swipeDetection;
        set => _swipeDetection = value;
    }

    public ScoreManager ScoreManager
    {
        get => _scoreManager;
        set => _scoreManager = value;
    }

    public void DefaultCharacterPick()
    {
        Player.characterType = CharacterType.DEFAULT;

        Reset();
    }

    public void SpeedCharacterPick()
    {
        Player.characterType = CharacterType.SPEED;

        Reset();
    }

    public void TankCharacterPick()
    {
        Player.characterType = CharacterType.TANK;

        Reset();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        SceneManager.Instance.SwitchScene((int)SceneType.GAME_OVER);
        ScoreManager.Instance.UpdateGameOverScoreText();
    }

    public void Reset()
    {
        Player.Reset();
        ScoreManager.Instance.ResetScore();
        Time.timeScale = 1;
    }
}
