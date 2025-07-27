using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템의 기능을 담당해주는 아이템 매니저
/// </summary>
public class ItemManager : Singleton<ItemManager>
{
    public PlayerCondition playerCondition;
    public PlayerInventory playerInventory;
    public UIManager uiManager;

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    public void Init()
    {
        playerCondition = FindAnyObjectByType<PlayerCondition>();
        playerInventory = FindAnyObjectByType<PlayerInventory>();
        uiManager = UIManager.Instance;
    }

    //스탯 오브젝트 기능
    public void UpgradeStat(StatObjectSO data)
    {
        //버프 적용
        if (data.duration > 0)
        {
            uiManager.StateMachine.MainUI.UpdateBuffUI(data);
            playerCondition.ApplyTempBuff(data);
        }
        //지속시간 0이면(보다 작으면)영구 적용 또는 즉시 적용 (궁극기 회복등)

        if (data.isConsumable == false)
        {
            playerCondition.ApplyPermanent(data);
        }
        
    }

    //수집 오브젝트 인벤토리에 추가
    public void GetCollectItem(CollectObjectSO collectObjectSO)
    {
        playerInventory.AddItem(collectObjectSO);
    }

    public void GetGold(int amount)
    {
        playerInventory.Gold += amount;
        uiManager.StateMachine.MainUI.UpdateGoldText();
    }



}
