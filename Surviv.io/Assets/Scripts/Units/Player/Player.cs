using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    // Start is called before the first frame update
    protected override void Start()
    {
        InitPlayerData();
        base.Start();
    }

    private void InitPlayerData()
    {
        _currentHP = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
