using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryWeaponPickup : WeaponPickup
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            Debug.Log("GameManager.Instance: " + (GameManager.Instance != null));
            Debug.Log("SecondaryWeaponActivator: " + (GameManager.Instance?.PrimaryWeaponActivator != null));
            Debug.Log("PlayerInventory: " + (GameManager.Instance?.PlayerInventory != null));

            EquipWeapon(GameManager.Instance.SecondaryWeaponActivator.weaponsList,
                        _weaponNum,
                        ref GameManager.Instance.PlayerInventory.secondaryWeapon,
                        ref GameManager.Instance.PlayerInventory.primaryWeapon,
                        ref GameManager.Instance.PlayerInventory.equippedWeapon);
        }
    }

    protected override void EquipWeapon(List<GameObject> weaponList, int weaponNumber, ref GameObject assignedWeaponType, ref GameObject otherWeaponType, ref GameObject equippedWeapon)
    {
        GameManager.Instance.SecondaryWeaponActivator.DeactivateActiveWeapon();

        if (GameManager.Instance.PlayerInventory.equippedWeapon == null)
        {
            MenuManager.Instance.InGameUI.ChangeUIWeaponColor(MenuManager.Instance.InGameUI.secondaryWeaponNameBackground, MenuManager.Instance.InGameUI.primaryWeaponNameBackground);
        }

        base.EquipWeapon(weaponList, weaponNumber, ref assignedWeaponType, ref otherWeaponType, ref equippedWeapon);

        MenuManager.Instance.InGameUI.UpdateSecondaryWeaponSlot(weaponNumber);
    }
}
