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
    
    public void UpgradeStat(StatObjectSO data)
    {
        //버프 적용
        if (data.duration > 0)
        {
            UIManager.Instance.UpdateBuffUI(data);
            int StatObjectSO;
            playerCondition.ApplyTempBuff(data);
        }
        //지속시간 0이면(보다 작으면)영구 적용 또는 즉시 적용 (궁극기 회복등)
        
    }

    public void GetCollectItem(CollectObjectSO collectObjectSO)
    {
        playerInventory.AddItem(collectObjectSO);
    }



}
