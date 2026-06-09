using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    , YELLOW = 2
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

    [SerializeField] private Player _player;

    [SerializeField] public SwipeDetection swipeDetection;
    [SerializeField] private bool _isSwipable = false;

    private float _speed = 2.5f;
    private Vector2 _direction = Vector2.down;

    public void Initialize()
    {
        //_health = Random.Range(0, 100);
        //_speed = Random.Range(1, 10);

        if (!_player)
        {
            _player = GameManager.Instance.Player;
        }

        if (!swipeDetection)
        {
            swipeDetection = GameManager.Instance.SwipeDetection;
        }

        _enemyType = (EnemyType)Random.Range(0, System.Enum.GetValues(typeof(EnemyType)).Length);
        //Debug.Log($"Enemy Type Initialized: {_enemyType}");

        ApplyArrowType(_enemyType);
    }

    private void Update()
    {
        transform.Translate((Vector3)_direction * _speed * Time.deltaTime);

        if (_isSwipable)
        {
            CheckPlayerSwipe();
        }
    }

    private IEnumerator CO_RotateArrow()
    {
        int index = 0;
        int arrowCount = _arrowSprites.Count;

        while (!_isSwipable)
        {
            arrowObject.sprite = _arrowSprites[index % arrowCount];
            index++;
            yield return new WaitForSeconds(0.25f);

        }

        _arrowType = (ArrowType)((index - 1 + arrowCount) % arrowCount);
    }

    private void ApplyArrowType(EnemyType enemyTypeNumber)
    {
        switch (enemyTypeNumber)
        {
            case EnemyType.GREEN:
                arrowObject.color = Color.green;
                SetRandomArrowDirection();
                break;
            case EnemyType.RED:
                arrowObject.color = Color.red;
                SetRandomArrowDirection();
                break;
            case EnemyType.YELLOW:
                arrowObject.color = Color.yellow;
                StartCoroutine(CO_RotateArrow());
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
            case EnemyType.YELLOW:
                {
                    if ((int)_arrowType == (int)swipeDetection.swipeType)
                    {
                        KillEnemy();
                    }
                }
                break;
        }
    }

    private void KillEnemy()
    {
        _player.OnEnemyExit(this.gameObject);
        Destroy(gameObject);

        _isSwipable = false;

        Debug.Log("Enemy Killed");
    }

    public void OnPlayerEnter(Collider2D collision)
    {
        if (collision != null)
        {
            swipeDetection.swipeType = SwipeType.NONE;
            _isSwipable = true;
            _blackBox.SetActive(true);

            _player.OnEnemyEnter(this.gameObject);
        }
    }

    public void OnPlayerExit(Collider2D collision)
    {
        if (collision != null)
        {
            _isSwipable = false;

            _player.OnEnemyExit(this.gameObject);
        }
    }
}