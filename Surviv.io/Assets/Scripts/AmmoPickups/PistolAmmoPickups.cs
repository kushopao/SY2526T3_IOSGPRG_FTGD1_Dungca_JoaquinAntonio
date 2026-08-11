using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolAmmoPickups : AmmoPickups
{
    protected override void Start()
    {
        InitPistolAmmoPickupData();
        base.Start();
    }

    private void InitPistolAmmoPickupData()
    {
        _maxRandomAmmoAmt = GameManager.Instance.PlayerInventory.inventoryData[0].maxAmmo;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            PlayerPickup(AmmoType.PISTOL);
            Debug.Log($"Current Pistol Ammo: {GameManager.Instance.PlayerInventory.inventoryData[0].currentAmmo}");
        }
    }
}
