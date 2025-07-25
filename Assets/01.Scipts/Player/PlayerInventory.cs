using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class PlayerInventory : MonoBehaviour
{
    private PlayerCondition _playerCondition;
    private int _gold;

    //일단은 직렬화 나중에 어드레서블로 변환
    public List<CollectObject> relics;
    public List<CollectObjectSO> EquipRelics { get; private set; }
    public int MaxPotionNum { get; private set; }
    public int CurPotionNum { get; private set; }

    public int Gold
    {
        get { return _gold; }
        set { _gold = Mathf.Max(0, value); }
    }


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
                UIManager.Instance.UpdatePotionUI();
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
            switch (data.statType)
            {
                case StatType.None:
                    DebugHelper.LogError("StatType이 None임");
                    break;
                case StatType.Atk:
                    _playerCondition.Attack += data.amount;
                    break;
                case StatType.Def:
                    _playerCondition.Defence += data.amount;
                    break;
                case StatType.Stamina:
                    _playerCondition.MaxStamina += data.amount;
                    break;
                case StatType.Hp:
                    _playerCondition.MaxHp += data.amount;
                    _playerCondition.CurHp += data.amount;
                    break;
                case StatType.Recovery:
                    _playerCondition.HealAmonut += data.amount;
                    break;
                case StatType.Ultimit:
                    _playerCondition.MaxUltimateGauge -= data.amount;
                    if (_playerCondition.CurUltimate >= _playerCondition.MaxUltimateGauge)
                        _playerCondition.CurUltimate = _playerCondition.MaxUltimateGauge;
                    break;
            }

            EquipRelics.Add(data);
        }

    }

    //장착 해제
    public void UnEquipRelic(CollectObjectSO data)
    {
        switch (data.statType)
        {
            case StatType.None:
                DebugHelper.LogError("StatType이 None임");
                break;
            case StatType.Atk:
                _playerCondition.Attack -= data.amount;
                break;
            case StatType.Def:
                _playerCondition.Defence -= data.amount;
                break;
            case StatType.Stamina:
                _playerCondition.MaxStamina -= data.amount;
                break;
            case StatType.Hp:
                _playerCondition.MaxHp -= data.amount;
                _playerCondition.CurHp -= data.amount;
                if (_playerCondition.CurHp <= 0f)
                {
                    _playerCondition.CurHp = 1f;
                }
                break;
            case StatType.Recovery:
                _playerCondition.HealAmonut -= data.amount;
                break;
            case StatType.Ultimit:
                _playerCondition.MaxUltimateGauge += data.amount;
                
                break;
        }

        EquipRelics.Remove(data);
    }

    //성물로 인한 공격력 
    public int EquipRelicAttack()
    {
        int sum = 0;
        foreach (CollectObjectSO data in EquipRelics)
        {
            if (data.statType != StatType.Atk) { continue; }
            sum += data.amount;
        }
        return sum;
    }

    //성물로 인한 방어력
    public int EquipRelicDefense()
    {
        int sum = 0;
        foreach (CollectObjectSO data in EquipRelics)
        {
            if (data.statType != StatType.Def) { continue; }
            sum += data.amount;
        }
        return sum;
    }

    //성물로 인한 체력
    public int EquipRelicHp()
    {
        int sum = 0;
        foreach (CollectObjectSO data in EquipRelics)
        {
            if (data.statType != StatType.Hp) { continue; }
            sum += data.amount;
        }
        return sum;
    }

    //성물로 인한 스태미나
    public int EquipRelicStamina()
    {
        int sum = 0;
        foreach (CollectObjectSO data in EquipRelics)
        {
            if (data.statType != StatType.Stamina) { continue; }
            sum += data.amount;
        }
        return sum;
    }

    //포션 최대치로 회복
    public void SupplyPotion()
    {
        CurPotionNum = MaxPotionNum;
        UIManager.Instance.UpdatePotionUI();
    }
}
