using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class PlayerInventory : MonoBehaviour
{
    private PlayerCondition _playerCondition;
    private UIManager _uiManager;
    private int _gold;
    private int _curPotionNum;

    //일단은 직렬화 나중에 어드레서블로 변환
    public List<CollectObject> relics;
    public List<CollectObjectSO> EquipRelics { get; private set; }
    public int MaxPotionNum { get; private set; }
    public int CurPotionNum
    {
        get =>  _curPotionNum;
        set => _curPotionNum = Mathf.Clamp(value, 0, MaxPotionNum);
    }

    public int Gold
    {
        get { return _gold; }
        set { _gold = Mathf.Max(0, value); }
    }

    public void Start()
    {
        _uiManager = UIManager.Instance;
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
                }
                else
                {
                    CurPotionNum++;
                }
                _uiManager.StateMachine.MainUI.UpdatePotionText();
                break;
        }
    }

    //포션 사용
    public void UsePotion()
    {
        CurPotionNum--;
        _uiManager.StateMachine.MainUI.UpdatePotionText();
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
                case StatType.Recovery:
                    _playerCondition.HealAmonut += data.amount;
                    break;
                case StatType.Ultimit:
                    _playerCondition.MaxUltimate -= data.amount;
                    if (_playerCondition.CurUltimate >= _playerCondition.MaxUltimate)
                        _playerCondition.CurUltimate = _playerCondition.MaxUltimate;
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
            case StatType.Recovery:
                _playerCondition.HealAmonut -= data.amount;
                break;
            case StatType.Ultimit:
                _playerCondition.MaxUltimate += data.amount;
                
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
        _uiManager.StateMachine.MainUI.UpdatePotionText();
    }
}
