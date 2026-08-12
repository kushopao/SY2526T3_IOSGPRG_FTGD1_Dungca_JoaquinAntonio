using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Unit>())
        {
            Destroy(other.gameObject);

            GameManager.Instance.EnemySpawnArea.spawnedGameObjects.Remove(other.gameObject);

            // if (GameManager.Instance.EnemySpawnArea.spawnedGameObjects.Count <= 0)
                // Menu Manager
        }
    }
}
