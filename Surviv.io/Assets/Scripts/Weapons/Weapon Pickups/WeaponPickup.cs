using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] protected int _weaponNum;
    public Action onPlayerPickup;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Player"))
            Debug.Log("Player got a weapon");
    }

    protected virtual void EquipWeapon(List<GameObject> weaponList, int weaponNumber, ref GameObject assignedWeaponType, ref GameObject otherWeaponType, ref GameObject equippedWeapon)
    {
        assignedWeaponType = weaponList[weaponNumber];

        if (equippedWeapon == otherWeaponType && otherWeaponType != null)
        {
            Debug.Log($"Equipped weapon is the secondary weapon: {equippedWeapon}, so no changes, only changed {assignedWeaponType} slot");
        }
        else
        {
            equippedWeapon = assignedWeaponType;
            equippedWeapon.SetActive(true);
            MenuManager.Instance.InGameUI.UpdateEquippedAmmoUI();
            Debug.Log($"Equipped weapon is now: {equippedWeapon}");
        }

        Destroy(this.gameObject);
    }
}
