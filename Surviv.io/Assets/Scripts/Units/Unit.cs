using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UnitType
{
    Player,
    Enemy
}

public class Unit : MonoBehaviour
{
    [SerializeField] public UnitType unitType;

    [SerializeField] public float _currentHP;
    [SerializeField] public float _maxHP;

    [SerializeField] public float movementSpeed;

    [SerializeField] private bool _isAlive = true;

    [SerializeField] private Vector3 _startPos;

    private void Awake()
    {
        _currentHP = _maxHP;

        if (unitType == UnitType.Player)
        {
            _startPos = this.transform.position;
        }
    }

    protected virtual void Start()
    {
        
    }

    private void Death()
    {
        if (_isAlive)
        {
            switch (unitType)
            {
                case UnitType.Enemy:
                    Destroy(gameObject);
                    break;
                case UnitType.Player:
                    this.gameObject.SetActive(false);
                    break;
            }
        }
    }

    public void TakeDamage(float damageTaken)
    {
        _currentHP  -= damageTaken;

        if (_currentHP <= 0)
        {
            _isAlive = false;
            Death();
        }
    }

    private void OnEnable()
    {
        if (unitType == UnitType.Player)
        {
            this.transform.position = _startPos;
            _currentHP = _maxHP;
        }
    }
}
