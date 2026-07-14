using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemyList = new List<GameObject>();

    // HP Values
    [SerializeField] private float _currentHP;
    [SerializeField] private float _maxHP = 100f;

    // DMG Values
    [SerializeField] private float _damage = 50f;
    [SerializeField] private float _heal = 50f;

    // Dash
    public bool isDashing = false;

    private void Awake()
    {
        GameManager.Instance.Player = this;

        _currentHP = _maxHP;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        if (enemy != null)
        {
            Spawner.Instance.RemoveEnemyFromList(enemy);
            Destroy(enemy.gameObject);
            TakeDamage();

            Debug.Log("Bro did not hit it");
        }
    }

    public void TakeDamage()
    {
        _currentHP -= _damage;

        if (_currentHP <= 0)
        {
            Debug.Log("Player Dead");
        }
    }

    public void GivePowerup()
    {
        float randomNum = Random.Range(1, 100);

        if (randomNum <= 3)
        {
            Debug.Log("Give Powerup");
            _currentHP += _heal;
        }
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
