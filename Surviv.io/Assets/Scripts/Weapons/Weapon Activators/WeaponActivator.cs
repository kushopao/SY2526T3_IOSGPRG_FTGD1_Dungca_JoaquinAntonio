using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponActivator : MonoBehaviour
{
    public List<GameObject> weaponsList = new List<GameObject>();
    private Unit _unit;

    private void Start()
    {
        //FillWeaponList();
        _unit = GetComponentInParent<Unit>();

        if (_unit.unitType == UnitType.Enemy)
        {
            int randomWeaponNum = Random.Range(0, weaponsList.Count);
            weaponsList[randomWeaponNum].SetActive(true);
        }
    }

    public virtual void DeactivateActiveWeapon()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.activeSelf)
                child.gameObject.SetActive(false);
        }
    }

    private void FillWeaponsList()
    {
        foreach (Transform child in transform)
            weaponsList.Add(child.gameObject);
    }

    private void OnDisable()
    {
        foreach (Weapon weapon in GetComponentsInChildren<Weapon>())
            weapon.currentAmmoMag = 0;
    }
}
