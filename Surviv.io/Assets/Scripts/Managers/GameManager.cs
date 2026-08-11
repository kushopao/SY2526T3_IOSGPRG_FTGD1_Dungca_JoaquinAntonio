using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] PlayerInventory playerInventory;
    public PlayerInventory PlayerInventory
    {
        get => playerInventory;
        set => playerInventory = value;
    }

}
