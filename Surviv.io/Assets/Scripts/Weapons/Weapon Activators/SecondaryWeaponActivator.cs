using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryWeaponActivator : WeaponActivator
{
    private void Awake()
    {
        GameManager.Instance.SecondaryWeaponActivator = this;
    }

    public override void DeactivateActiveWeapon()
    {
        base.DeactivateActiveWeapon();
    }
}
