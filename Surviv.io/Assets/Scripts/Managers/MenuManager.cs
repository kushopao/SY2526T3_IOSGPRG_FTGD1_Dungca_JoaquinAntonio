using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MenuType
{
    MAIN_MENU,
    IN_GAME,
    GAME_OVER,
    CHICKEN_DINNER
}

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField] InGameUI _inGameUI;

    public InGameUI InGameUI 
    {
        get => _inGameUI;
        set => _inGameUI = value;
    }

    [SerializeField] GameObject[] menus;

    private void Start()
    {
        SwitchMenu((int)MenuType.MAIN_MENU);
    }
    public void SwitchMenu(int index)
    {
        foreach (GameObject menuObj in menus)
        {
            menuObj.SetActive(false);
        }
        menus[index].SetActive(true);
    }
}