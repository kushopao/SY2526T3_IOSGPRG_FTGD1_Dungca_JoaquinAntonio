using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    private Enemy _parentEnemy;

    private void Awake()
    {
        _parentEnemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (_parentEnemy != null)
        {
            _parentEnemy.OnPlayerEnter(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       if (_parentEnemy != null)
        {
            _parentEnemy.OnPlayerExit(collision);
        }
    }
}
