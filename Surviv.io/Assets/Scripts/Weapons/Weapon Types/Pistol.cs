using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    protected override void SubtractTotalAmmoWithCurrentClipSize()
    {
        GameManager.Instance.PlayerInventory.WeaponReload(ref GameManager.Instance.PlayerInventory.inventoryData[0].currentAmmo, ref currentAmmoMag, maxAmmoMag);
    }

    public override void Fire()
    {
        if (GetComponentInParent<Unit>().unitType == UnitType.Enemy)
            base.Fire();
        else
            if (GameManager.Instance.PlayerInventory.inventoryData[0].currentAmmo > 0 || currentAmmoMag > 0)
                base.Fire();
    }
}
