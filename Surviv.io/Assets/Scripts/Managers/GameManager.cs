using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] PlayerInventory playerInventory;
    public PlayerInventory PlayerInventory
    {
        get => playerInventory;
        set => playerInventory = value;
    }

    [SerializeField] PrimaryWeaponActivator _primaryWeaponActivator;
    public PrimaryWeaponActivator PrimaryWeaponActivator
    {
        get => _primaryWeaponActivator;
        set => _primaryWeaponActivator = value;
    }

    [SerializeField] SecondaryWeaponActivator _secondaryWeaponActivator;
    public SecondaryWeaponActivator SecondaryWeaponActivator
    {
        get => _secondaryWeaponActivator;
        set => _secondaryWeaponActivator = value;
    }

    [SerializeField] EnemySpawnArea _enemySpawnArea;
    public EnemySpawnArea EnemySpawnArea
    {
        get => _enemySpawnArea;
        set => _enemySpawnArea = value;
    }

    public GameObject Player;

    public void ActivatePlayer()
    {
        Player.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
