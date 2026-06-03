using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemyList = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        if (enemy != null)
        {
            Spawner.Instance.RemoveEnemyFromList(enemy);
            Destroy(enemy.gameObject);
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
