using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] private List<Sprite> _primaryWeaponSprites = new List<Sprite>();
    [SerializeField] private List<Sprite> _secondaryWeaponSprites = new List<Sprite>();
    [SerializeField] private List<Text> _ammoCountTexts = new List<Text>();

    [SerializeField] private Image _primaryWeaponIcon;
    [SerializeField] private Image _secondaryWeaponIcon;
    [SerializeField] private Image _hpBar;

    [SerializeField] public RawImage primaryWeaponNameBackground;
    [SerializeField] public RawImage secondaryWeaponNameBackground;

    [SerializeField] public Text equippedAmmoText;

    [SerializeField] private Color _equippedWeaponColor;
    [SerializeField] private Color _prevWeaponColor;

    [SerializeField] private GameObject _playerObject;

    private void Awake()
    {
        MenuManager.Instance.InGameUI = this;

        _primaryWeaponIcon.enabled = false;
        _secondaryWeaponIcon.enabled = false;
        primaryWeaponNameBackground.color = _equippedWeaponColor;
        secondaryWeaponNameBackground.color = _prevWeaponColor;

        for (int i = 0; i < 3; i++)
        {
            UpdateTotalAmmoUI(i);
        }
    }

    public void UpdatePrimaryWeaponSlot(int weaponNumber)
    {
        _primaryWeaponIcon.enabled = true;
        _primaryWeaponIcon.sprite = _primaryWeaponSprites[weaponNumber];
    }

    public void UpdateSecondaryWeaponSlot(int weaponNumber)
    {
        _secondaryWeaponIcon.enabled = true;
        _secondaryWeaponIcon.sprite = _secondaryWeaponSprites[weaponNumber];
    }

    public void EquipPrimaryWeapon()
    {
        if (GameManager.Instance.PlayerInventory.primaryWeapon == null)
        {
            return;
        }
        else
        {
            GameManager.Instance.SecondaryWeaponActivator.DeactivateActiveWeapon();
            ChangeEquippedWeapon(ref GameManager.Instance.PlayerInventory.equippedWeapon, ref GameManager.Instance.PlayerInventory.primaryWeapon);
            UpdateEquippedAmmoUI();
            ChangeUIWeaponColor(primaryWeaponNameBackground, secondaryWeaponNameBackground);
            Debug.Log("Equipping primary weapon");
        }
    }

    public void EquipSecondaryWeapon()
    {
        if (GameManager.Instance.PlayerInventory.secondaryWeapon == null)
        {
            return;
        }
        else
        {
            GameManager.Instance.PrimaryWeaponActivator.DeactivateActiveWeapon();
            ChangeEquippedWeapon(ref GameManager.Instance.PlayerInventory.equippedWeapon, ref GameManager.Instance.PlayerInventory.secondaryWeapon);
            UpdateEquippedAmmoUI();
            ChangeUIWeaponColor(secondaryWeaponNameBackground, primaryWeaponNameBackground);
            Debug.Log("Equipping secondary weapon");
        }
    }

    private void ChangeEquippedWeapon(ref GameObject equippedWeapon, ref GameObject weaponToChangeTo)
    {
        equippedWeapon = weaponToChangeTo;
        equippedWeapon.SetActive(true);
    }

    public void ChangeUIWeaponColor(RawImage imageColorToChange, RawImage imageColorToRevert)
    {
        imageColorToChange.color = _equippedWeaponColor;
        imageColorToRevert.color = _prevWeaponColor;
    }

    public void UpdateHealth()
    {
        _hpBar.fillAmount = (_playerObject.GetComponent<Unit>()._currentHP / _playerObject.GetComponent<Unit>()._maxHP);
    }

    public void UpdateEquippedAmmoUI()
    {
        var equippedWeaponScript = GameManager.Instance.PlayerInventory.equippedWeapon.GetComponent<Weapon>();

        equippedAmmoText.text = $"{equippedWeaponScript.currentAmmoMag} / {equippedWeaponScript.maxAmmoMag}";
    }

    public void UpdateTotalAmmoUI(int ammoNumber)
    {
       _ammoCountTexts[ammoNumber].text = $"{GameManager.Instance.PlayerInventory.inventoryData[ammoNumber].currentAmmo}";
    }
}
