using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawnArea : SpawnArea
{
    [SerializeField] private List<GameObject> _ammoLootPrefab = new List<GameObject>();
    [SerializeField] private List<GameObject> _weaponLootPrefab = new List<GameObject>();
    [SerializeField] private int _weaponSpawnChance = 30; //30% chance to spawn weapon

    protected override void SpawnPrefabs(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int lootSpawnChance;

            lootSpawnChance = Random.Range(0, 101);

            if (lootSpawnChance <= _weaponSpawnChance)
            {
                SpawnGameObject(_weaponLootPrefab);
            }
            else
            {
                SpawnGameObject(_ammoLootPrefab);
            }
        }
    }

}
