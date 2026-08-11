using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAmmoPickups : AmmoPickups
{
    protected override void Start()
    {
        InitShotgunAmmoPickupData();
        base.Start();
    }

    private void InitShotgunAmmoPickupData()
    {
        _maxRandomAmmoAmt = GameManager.Instance.PlayerInventory.inventoryData[2].maxAmmo;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            PlayerPickup(AmmoType.SHOTGUN);
            Debug.Log($"Current Shotgun Ammo: {GameManager.Instance.PlayerInventory.inventoryData[2].currentAmmo}");
        }
    }
}
