using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    List<Enemy> _enemies;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        //_enemies.Add(enemy);


        if (enemy != null)
        {
            Spawner.Instance.RemoveEnemyFromList(enemy);
            Destroy(enemy.gameObject);
        }
    }
}
