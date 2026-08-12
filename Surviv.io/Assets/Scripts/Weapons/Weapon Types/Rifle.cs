using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Rifle : Weapon, IUpdateSelectedHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private bool isPressed;

    protected override void SubtractTotalAmmoWithCurrentClipSize()
    {
        GameManager.Instance.PlayerInventory.WeaponReload(ref GameManager.Instance.PlayerInventory.inventoryData[1].currentAmmo, ref currentAmmoMag, maxAmmoMag);
    }

    public override void Fire()
    {
        if (GetComponentInParent<Unit>().unitType == UnitType.Enemy)
            base.Fire();
        else
            if (GameManager.Instance.PlayerInventory.inventoryData[1].currentAmmo > 0 || currentAmmoMag > 0)
                base.Fire();
    }

    public void OnUpdateSelected(BaseEventData data)
    {
        if (isPressed)
            Fire(); 
    }

    public void OnPointerDown(PointerEventData data)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData data)
    {
        isPressed = false;
    }

    private void ClickDown()
    {
        OnPointerDown(null);
    }

    private void ClickUp()
    {
        OnPointerUp(null);
    }
}
