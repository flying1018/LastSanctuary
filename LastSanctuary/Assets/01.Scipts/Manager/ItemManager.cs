using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public PlayerCondition playerCondition;
    public PlayerInventory playerInventory;

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    public void Init()
    {
        playerCondition = FindAnyObjectByType<PlayerCondition>();
        playerInventory = FindAnyObjectByType<PlayerInventory>();
    }
    public void UpgradeStat(StatObjectSO statObjectSO)
    {
        
    }

    public void GetCollectItem(CollectObjectSO collectObjectSO)
    {
        playerInventory.AddItem(collectObjectSO);
    }
}
