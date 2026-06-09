using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Player _player;

    [SerializeField] private SwipeDetection _swipeDetection;

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
}
