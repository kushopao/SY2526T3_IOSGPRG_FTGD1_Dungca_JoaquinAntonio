using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    [SerializeField] protected List<GameObject> _gameObjectPrefab = new List<GameObject>();
    [SerializeField] public List<GameObject> spawnedGameObjects = new List<GameObject>();
    [SerializeField] protected int _spawnCount = 5;
    protected BoxCollider2D _spawnArea;

    protected virtual void Awake()
    {
        _spawnArea = GetComponent<BoxCollider2D>();
    }

    protected virtual void SpawnPrefabs(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SpawnGameObject(_gameObjectPrefab);
        }
    }

    protected virtual Vector2 RandomizePositionInCollider()
    {
        UnityEngine.Bounds bounds = _spawnArea.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector2(randomX, randomY);
    }

    protected virtual void SpawnGameObject(List<GameObject> gameObject)
    {
        Vector2 randomPosition = RandomizePositionInCollider();

        int randomIndex = Random.Range(0, gameObject.Count);
        GameObject spawnedObject = Instantiate(gameObject[randomIndex], randomPosition, Quaternion.identity);
        spawnedGameObjects.Add(spawnedObject);
    }

    public void Reset()
    {
        foreach (var gameObject in spawnedGameObjects)
        {
            Destroy(gameObject);
        }

        spawnedGameObjects.Clear();
    }

    private void OnEnable()
    {
        SpawnPrefabs(_spawnCount);
    }

    private void OnDisable()
    {
        Reset();
    }
}
