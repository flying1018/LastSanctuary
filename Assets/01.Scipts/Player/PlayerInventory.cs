using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class PlayerInventory : MonoBehaviour
{
    private PlayerCondition _playerCondition;

    //일단은 직렬화 나중에 어드레서블로 변환
    public List<CollectObject> relics;
    public List<CollectObjectSO> EquipRelics { get; private set; }
    public int MaxPotionNum { get; private set; }
    public int CurPotionNum { get; private set; }


    public async void Init(Player player)
    {
        //relics = await ResourceLoader.LoadAssetsLabel<CollectObject>(StringNameSpace.Labels.Relic);;
        relics.Sort();

        MaxPotionNum = player.Data.potionNum;
        CurPotionNum = MaxPotionNum;
        _playerCondition = player.Condition;
        EquipRelics = new List<CollectObjectSO>();
    }

    //아이템 습득
    public void AddItem(CollectObjectSO data)
    {
        switch (data.collectType)
        {
            case CollectType.Relic:
                foreach (var relic in relics)
                {
                    if (data.index == relic.Data.index)
                    {
                        relic.IsGet = true;
                        break;
                    }
                }
                break;
            case CollectType.Potion:
                if (data.isMaxIncrease)
                {
                    MaxPotionNum++;
                    CurPotionNum++;
                    CurPotionNum = Mathf.Clamp(CurPotionNum, 0, MaxPotionNum);
                }
                else
                {
                    CurPotionNum++;
                    CurPotionNum = Mathf.Clamp(CurPotionNum, 0, MaxPotionNum);
                }
                break;
        }
    }

    //포션 사용
    public void UsePotion()
    {
        CurPotionNum--;
        CurPotionNum = Mathf.Clamp(CurPotionNum, 0, MaxPotionNum);
        UIManager.Instance.UpdatePotionUI();
    }

    //렐릭 장착
    public void EquipRelic(CollectObjectSO data)
    {
        if (EquipRelics.Contains(data))
        {
            UnEquipRelic(data);
        }
        else
        {
            _playerCondition.Attack += data.attack;
            _playerCondition.Defence += data.defense;
            _playerCondition.MaxHp += data.hp;
            _playerCondition.CurHp += data.hp;
            _playerCondition.MaxStamina += data.stamina;
            EquipRelics.Add(data);
        }

    }

    //장착 해제
    public void UnEquipRelic(CollectObjectSO data)
    {
        _playerCondition.Attack -= data.attack;
        _playerCondition.Defence -= data.defense;
        _playerCondition.MaxHp -= data.hp;
        if (_playerCondition.CurHp < data.hp)
        {
            _playerCondition.CurHp = 1f;
        }
        _playerCondition.MaxStamina -= data.stamina;
        EquipRelics.Remove(data);
    }

    //성물로 인한 공격력 
    public int EquipRelicAttack()
    {
        int sum = 0;
        foreach (CollectObjectSO data in EquipRelics)
        {
            sum += data.attack;
        }
        return sum;
    }

    //성물로 인한 방어력
    public int EquipRelicDefense()
    {
        int sum = 0;
        foreach (CollectObjectSO data in EquipRelics)
        {
            sum += data.defense;
        }
        return sum;
    }

    //성물로 인한 체력
    public int EquipRelicHp()
    {
        int sum = 0;
        foreach (CollectObjectSO data in EquipRelics)
        {
            sum += data.hp;
        }
        return sum;
    }

    //성물로 인한 스태미나
    public int EquipRelicStamina()
    {
        int sum = 0;
        foreach (CollectObjectSO data in EquipRelics)
        {
            sum += data.stamina;
        }
        return sum;
    }

    public void SupplyPotion()
    {
        CurPotionNum = MaxPotionNum;
        UIManager.Instance.UpdatePotionUI();
    }
}
