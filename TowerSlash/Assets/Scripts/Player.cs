using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemyList = new List<GameObject>();

    // HP Values
    private float _currentHP = 100f;
    private float _maxHP = 100f;

    // DMG Values
    [SerializeField] private float _damage = 50f;
    [SerializeField] private float _heal = 50f;

    // Dash Values
    [SerializeField] private float _currentDashGauge = 0f;
    [SerializeField] private float _maxDashGauge = 100f;
    [SerializeField] public bool _isDashing = false;
    [SerializeField] public float _dashGain = 5f; // change later
    //[SerializeField] private float _defaultDashGain = 5f;
    //[SerializeField] private float _speedDashGain = 10f;

    // UI
    [SerializeField] private Image _healthBar;
    [SerializeField] private Image _dashGaugeBar;
    [SerializeField] private Button _dashButton;

    private void Awake()
    {
        GameManager.Instance.Player = this;
        UpdateHealth();
        UpdateDashGauge();
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
