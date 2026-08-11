using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterType
{
    DEFAULT,
    SPEED,
    TANK
}

public class Player : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemyList = new List<GameObject>();

    public CharacterType characterType;

    // HP Values
    private float _currentHP;
    private float _maxHP = 3f;
    private float _tankMaxHP = 5f;

    // DMG Values
    private float _damage = 1f;
    private float _heal = 1f;

    // Dash Values
    [SerializeField] private float _currentDashGauge = 0f;
    [SerializeField] private float _maxDashGauge = 100f;
    [SerializeField] public bool _isDashing = false;
    [SerializeField] public float _dashGain;
    [SerializeField] private float _defaultDashGain = 5f;
    [SerializeField] private float _speedDashGain = 10f;

    // UI
    [SerializeField] private Image _healthBar;
    [SerializeField] private Image _dashGaugeBar;
    [SerializeField] private Button _dashButton;

    // Scoring
    [SerializeField] private ScoreManager _scoreManager;
    private float _onDashScore = 25f;

    private void Awake()
    {
        GameManager.Instance.Player = this;
        UpdateHealth();
        UpdateDashGauge();
    }

    private void Start()
    {
        if (!_scoreManager)
            _scoreManager = GameManager.Instance.ScoreManager;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        if (enemy != null)
        {
            Spawner.Instance.RemoveEnemyFromList(enemy);
            Destroy(enemy.gameObject);
            TakeDamage();

            //Debug.Log("Bro did not hit it");
        }
    }

    public void TakeDamage()
    {
        _currentHP -= _damage;

        if (_currentHP <= 0)
        {
            //Debug.Log("Player Dead");
            GameManager.Instance.GameOver();
        }
        UpdateHealth();
    }

    public void GivePowerup()
    {
        float randomNum = Random.Range(1, 100);

        if (randomNum <= 3)
        {
            _currentHP = Mathf.Min(_currentHP + _heal, _maxHP);
            Debug.Log("Give Powerup");
        }
        UpdateHealth();
    }
    public void FillDashGauge(float dashPoints)
    {
        _currentDashGauge = Mathf.Min(_currentDashGauge + dashPoints, _maxDashGauge);
        UpdateDashGauge();
        Debug.Log($"Current Gauge: {_currentDashGauge} / {_maxDashGauge}");

        if (_currentDashGauge >= _maxDashGauge)
        {
            _dashButton.gameObject.SetActive(true);
        }
    }

    public void ActivateDash()
    {
        Debug.Log("Dash Activated");

        _currentDashGauge = 0f;
        UpdateDashGauge();

        _scoreManager.AddScore(_onDashScore);

        _isDashing = true;
        _dashButton.gameObject.SetActive(false);
        StartCoroutine(CO_PlayerDash());
    }

    private IEnumerator CO_PlayerDash()
    {
        Vector3 startPos = this.transform.position;
        Vector3 dashPos = new Vector3(startPos.x, startPos.y + 2f);

        // Move to Target Position
        yield return StartCoroutine(CO_MoveOverTime(startPos, dashPos, 1));

        // Wait at Target Position
        yield return new WaitForSeconds(10f);

        // Return to Original Position
        _isDashing = false;
        yield return StartCoroutine(CO_MoveOverTime(dashPos, startPos, 3));

    }

    private IEnumerator CO_MoveOverTime(Vector3 startPos, Vector3 targetPos, float duration)
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    // UI
    private void UpdateHealth()
    {
        _healthBar.fillAmount = (_currentHP / _maxHP);
    }

    private void UpdateDashGauge()
    {
        _dashGaugeBar.fillAmount = (_currentDashGauge / _maxDashGauge);
    }

    public void Reset()
    {
        _maxHP = 3f;
        _currentHP = _maxHP;
        _currentDashGauge = 0f;
        _dashGain = _defaultDashGain;

        switch (characterType)
        {
            case CharacterType.DEFAULT:
                Debug.Log("Default Character Chosen");
                break;

            case CharacterType.SPEED:
                _dashGain = _speedDashGain;
                Debug.Log("Speed Character Chosen");
                break;

            case CharacterType.TANK:
                _maxHP = _tankMaxHP;
                Debug.Log("Tank Character Chosen");
                break;

            default:
                break;
        }

        UpdateHealth();
        UpdateDashGauge();
    }

    public void OnEnemyEnter(GameObject enemy)
    {
        _enemyList.Add(enemy.gameObject);
    }

    public void OnEnemyExit(GameObject enemy)
    {
        if (_enemyList.Contains(enemy))
        { 
            _enemyList.Remove(enemy);
        }
    }
}
