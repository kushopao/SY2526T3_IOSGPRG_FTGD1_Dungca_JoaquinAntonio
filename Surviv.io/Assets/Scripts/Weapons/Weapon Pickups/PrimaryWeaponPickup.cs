using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryWeaponPickup : WeaponPickup
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            Debug.Log("GameManager.Instance: " + (GameManager.Instance != null));
            Debug.Log("PrimaryWeaponActivator: " + (GameManager.Instance.PrimaryWeaponActivator != null));
            Debug.Log("PlayerInventory: " + (GameManager.Instance.PlayerInventory != null));

            EquipWeapon(GameManager.Instance.PrimaryWeaponActivator.weaponsList,
                        _weaponNum,
                        ref GameManager.Instance.PlayerInventory.primaryWeapon,
                        ref GameManager.Instance.PlayerInventory.secondaryWeapon,
                        ref GameManager.Instance.PlayerInventory.equippedWeapon);

            Destroy(this.gameObject);
        }
    }

    protected override void EquipWeapon(List<GameObject> weaponList, int weaponNumber, ref GameObject assignedWeaponType, ref GameObject otherWeaponType, ref GameObject equippedWeapon)
    {
        GameManager.Instance.PrimaryWeaponActivator.DeactivateActiveWeapon();

        if (GameManager.Instance.PlayerInventory.equippedWeapon == null)
        {
            MenuManager.Instance.InGameUI.ChangeUIWeaponColor(MenuManager.Instance.InGameUI.primaryWeaponNameBackground, MenuManager.Instance.InGameUI.secondaryWeaponNameBackground);
        }

        base.EquipWeapon(weaponList, weaponNumber, ref assignedWeaponType, ref otherWeaponType, ref equippedWeapon);

        MenuManager.Instance.InGameUI.UpdatePrimaryWeaponSlot(weaponNumber);
    }
}
