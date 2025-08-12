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

    public int EquipHp { get; private set; }
    public int EquipStamina { get; private set; }
    public int EquipAtk { get; private set; }
    public int EquipDef { get; private set; }
    

    public void Start()
    {
        _uiManager = UIManager.Instance;
        
        //test Code
        //_gold = 100000;
    }


    public async void Init(Player player)
    {
        //relics = await ResourceLoader.LoadAssetsLabel<CollectObject>(StringNameSpace.Labels.Relic);;
        relics.Sort();
        
        //test Code
        // foreach (var relic in relics)
        // {
        //     relic.IsGet = true;
        // }

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
        _playerCondition.Heal(); 
        CurPotionNum--;
        _uiManager.StateMachine.MainUI.UpdatePotionText();
    }

    //렐릭 장착
    public void EquipRelic(CollectObjectSO data)
    {
        foreach (StatDelta stat in data.statDeltas)
        {
            switch (stat.statType)
            {
                case StatType.None:
                    DebugHelper.LogError("StatType이 None임");
                    break;
                case StatType.Recovery:
                    _playerCondition.HealAmonut += stat.amount;
                    break;
                case StatType.Ultimit:
                    _playerCondition.MaxUltimate -= stat.amount;
                    if (_playerCondition.CurUltimate >= _playerCondition.MaxUltimate)
                        _playerCondition.CurUltimate = _playerCondition.MaxUltimate;
                    break;
            }
        }

        EquipRelics.Add(data);
        EquipRelicStat();
    }

    //장착 해제
    public void UnEquipRelic(CollectObjectSO data)
    {
        foreach (StatDelta stat in data.statDeltas)
        {
            switch (stat.statType)
            {
                case StatType.None:
                    DebugHelper.LogError("StatType이 None임");
                    break;
                case StatType.Recovery:
                    _playerCondition.HealAmonut -= stat.amount;
                    break;
                case StatType.Ultimit:
                    _playerCondition.MaxUltimate += stat.amount;

                    break;
            }
        }

        EquipRelics.Remove(data);
        EquipRelicStat();
    }

    //성물로 인한 공격력 
    public void EquipRelicStat()
    {
        EquipAtk = 0;
        EquipDef = 0;
        EquipHp = 0;
        EquipStamina = 0;
        
        foreach (CollectObjectSO data in EquipRelics)
        {
            foreach (StatDelta stat in data.statDeltas)
            {
                switch (stat.statType)
                {
                    case StatType.None:
                        DebugHelper.LogError("타입이 None");
                        break;
                    case StatType.Atk:
                        EquipAtk += stat.amount;
                        break;
                    case StatType.Def:
                        EquipDef += stat.amount;
                        break;
                    case StatType.Hp:
                        EquipHp += stat.amount;
                        break;
                    case StatType.Stamina:
                        EquipStamina += stat.amount;
                        break;
                }
            }
        }
    }

    //포션 최대치로 회복
    public void SupplyPotion()
    {
        CurPotionNum = MaxPotionNum;
        _uiManager.StateMachine.MainUI.UpdatePotionText();
    }

    public bool UseGold(int gold)
    {
        if(Gold>=gold)
        {
            Gold -= gold;
            return true;
        }
        return false;
    }
}
