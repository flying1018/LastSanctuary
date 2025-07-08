using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class PlayerInventory : MonoBehaviour
{
    //일단은 직렬화 나중에 어드레서블로 변환
    public List<CollectObject> relics;
    public int MaxPotionNum { get; private set; }
    public int CurPotionNum { get; private set; }
    
    public async void Init(Player player)
    {
        //relics = await ResourceLoader.LoadAssetsLabel<CollectObject>(StringNameSpace.Labels.Relic);;
        relics.Sort();
        
        MaxPotionNum = player.Data.potionNum;
        CurPotionNum = MaxPotionNum;
    }

    public void AddItem(CollectObjectSO collectObjectSO)
    {
        switch (collectObjectSO.collectType)
        {
            case CollectType.Relic:
                foreach (var relic in relics)
                {
                    if (collectObjectSO.index == relic.Index)
                    {
                        relic.IsGet = true;
                        break;
                    }
                }
                break;
            case CollectType.Potion:
                if (collectObjectSO.isMaxIncrease)
                    MaxPotionNum++;
                else
                {
                    CurPotionNum++;
                    CurPotionNum = Mathf.Clamp(CurPotionNum, 0, MaxPotionNum);
                }
                break;
        }
    }
}
