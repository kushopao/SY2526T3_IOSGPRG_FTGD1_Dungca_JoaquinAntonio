using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public enum ArrowType
{
    UP
    , RIGHT
    , DOWN
    , LEFT
}

public enum EnemyType
{
    GREEN = 0
    , RED = 1
}

public class Enemy : MonoBehaviour
{
    //[SerializeField] private int _health;
    //[SerializeField] private int _speed;

    [SerializeField] private ArrowType _arrowType;
    [SerializeField] private EnemyType _enemyType;

    [SerializeField] SpriteRenderer arrowObject;
    [SerializeField] private List<Sprite> _arrowSprites = new List<Sprite>();
    [SerializeField] private GameObject _blackBox;

    [SerializeField] private Player player;

    [SerializeField] public SwipeDetection swipeDetection;
    [SerializeField] private bool _isSwipable = false;

    public void Initialize()
    {
        //_health = Random.Range(0, 100);
        //_speed = Random.Range(1, 10);

        _enemyType = (EnemyType)Random.Range(0, System.Enum.GetValues(typeof(EnemyType)).Length);
        Debug.Log($"Enemy Type Initialized: {_enemyType}");

        ApplyArrowType(_enemyType);
    }

    private void Update()
    {
        if (_isSwipable)
        {
            CheckPlayerSwipe();
        }
    }

    private void ApplyArrowType(EnemyType enemyTypeNumber)
    {
        switch (enemyTypeNumber)
        {
            case EnemyType.GREEN:
                arrowObject.color = Color.green;
                break;
            case EnemyType.RED:
                arrowObject.color = Color.red;
                break;
        }
    }

    private void SetRandomArrowDirection()
    {
        _arrowType = (ArrowType)Random.Range(0, System.Enum.GetValues(typeof(ArrowType)).Length);
        arrowObject.sprite = _arrowSprites[(int)_arrowType];  
    }

    private void CheckPlayerSwipe()
    {
        switch (_enemyType)
        {
            case EnemyType.GREEN:
                if ((int)_arrowType == (int)swipeDetection.swipeType)
                {
                    KillEnemy();
                }
                break;

            case EnemyType.RED:
                switch (_arrowType)
                {
                    case ArrowType.UP:
                        if (swipeDetection.swipeType == SwipeType.DOWN)
                        {
                            KillEnemy();
                        }
                        break;

                    case ArrowType.RIGHT:
                        if (swipeDetection.swipeType == SwipeType.LEFT)
                        {
                            KillEnemy();
                        }
                        break;

                    case ArrowType.DOWN:
                        if (swipeDetection.swipeType == SwipeType.UP)
                        {
                            KillEnemy();
                        }
                        break;

                    case ArrowType.LEFT:
                        if (swipeDetection.swipeType == SwipeType.RIGHT)
                        {
                            KillEnemy();
                        }
                        break;
                }
                break;
        }
    }

    private void KillEnemy()
    {
        player.OnEnemyExit(this.gameObject);
        Destroy(gameObject);

        _isSwipable = false;
    }

    void OnPlayerEnter(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            swipeDetection.swipeType = SwipeType.NONE;
            _isSwipable = true;
            _blackBox.SetActive(true);

            player.OnEnemyEnter(this.gameObject);
        }
    }

    void onPlayerExit(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Player"))
        {
            _isSwipable = false;

            player.OnEnemyExit(this.gameObject);
        }
    }
}