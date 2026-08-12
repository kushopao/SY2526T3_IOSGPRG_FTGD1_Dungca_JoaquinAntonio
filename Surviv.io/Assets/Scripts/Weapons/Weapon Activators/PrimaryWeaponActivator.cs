using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryWeaponActivator : WeaponActivator
{
    private void Awake()
    {
        GameManager.Instance.PrimaryWeaponActivator = this;
    }

    public override void DeactivateActiveWeapon()
    {
        base.DeactivateActiveWeapon();
    }
}
