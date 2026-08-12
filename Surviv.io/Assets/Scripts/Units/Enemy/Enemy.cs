using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    // Start is called before the first frame update
    protected override void Start()
    {
        InitEnemyData();
        base.Start();
    }

    private void InitEnemyData()
    {
        _currentHP = 100;
    }
}
