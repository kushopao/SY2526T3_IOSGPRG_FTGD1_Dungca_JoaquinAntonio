using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleAmmoPickups : AmmoPickups
{
    protected override void Start()
    {
        InitRifleAmmoPickupData();
        base.Start();
    }

    private void InitRifleAmmoPickupData()
    {
        _maxRandomAmmoAmt = GameManager.Instance.PlayerInventory.inventoryData[1].maxAmmo;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            PlayerPickup(AmmoType.RIFLE);
            Debug.Log($"Current Rifle Ammo: {GameManager.Instance.PlayerInventory.inventoryData[1].currentAmmo}");
        }
    }
}
