using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AmmoPickups : MonoBehaviour
{
    [SerializeField] private int _ammoAmt;
    [SerializeField] private int _ammoNum;
    [SerializeField] private int _minRandomAmmoAmt = 1; 
    protected int _maxRandomAmmoAmt = 1;

    protected virtual void Start()
    {
        _ammoAmt = Random.Range(_minRandomAmmoAmt, _maxRandomAmmoAmt);
    }

    protected virtual void PlayerPickup(AmmoType ammoType)
    {
        switch (ammoType)
        {
            case AmmoType.PISTOL:
                AddAmmo(0);
                break;

            case AmmoType.RIFLE:
                AddAmmo(1);
                break;

            case AmmoType.SHOTGUN:
                AddAmmo(2);
                break;

            default:
                break;
        }
    }

    private void AddAmmo(int number)
    {
        GameManager.Instance.PlayerInventory.inventoryData[number].currentAmmo = Mathf.Min(GameManager.Instance.PlayerInventory.inventoryData[number].currentAmmo + _ammoAmt,
                                                                                           GameManager.Instance.PlayerInventory.inventoryData[number].maxAmmo);

        // code for updating ammo UI

        Destroy(this.gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            Debug.Log("Player picked up ammo");
        }
    }
}
