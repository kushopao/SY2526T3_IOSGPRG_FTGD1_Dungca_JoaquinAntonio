using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AmmoType
{
    PISTOL,
    RIFLE,
    SHOTGUN
}

[System.Serializable] public class InventoryData
{
    public AmmoType type;
    public int currentAmmo;
    public int maxAmmo;
}

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] public List<InventoryData> inventoryData = new List<InventoryData>();
    [SerializeField] public GameObject equippedWeapon;
    [SerializeField] public GameObject primaryWeapon;
    [SerializeField] public GameObject secondaryWeapon;

    private void Awake()
    {
        GameManager.Instance.PlayerInventory = this;
        equippedWeapon = null;
    }

    public void WeaponReload(ref int currentTotalAmmo, ref int currentAmmoMag, int maxAmmoMag)
    {
        int ammoToLoad = Mathf.Min(currentTotalAmmo, maxAmmoMag);
        currentAmmoMag = ammoToLoad;
        currentTotalAmmo = Mathf.Max(currentTotalAmmo - ammoToLoad, 0);
    }

    private void OnDisable()
    {
        equippedWeapon = null;
        primaryWeapon = null;
        secondaryWeapon = null;

        for (int i = 0; i < inventoryData.Count; i++)
        {
            inventoryData[i].currentAmmo = 0;
            // UI code for updating ammo count 
        }
    }
}
