using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnArea : SpawnArea
{
    protected override void Awake()
    {
        GameManager.Instance.EnemySpawnArea = this;

        InitEnemySpawnArea();

        base.Awake();
    }

    private void InitEnemySpawnArea()
    {
        _spawnCount = 20;
    }
}
