using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RelicUI : UnifiedUI
{
    private List<StatUI> _statUIs;
    private List<EquipUI> _equipUIs;
    private List<SlotUI> _slotUIs;
    private TextMeshProUGUI _relicName;
    private TextMeshProUGUI _relicEffect;
    private TextMeshProUGUI _relicDesc;

    public RelicUI(UIStateMachine uiStateMachine) : base(uiStateMachine)
    {
        _statUIs = new List<StatUI>();
        _equipUIs = new List<EquipUI>();
        _slotUIs = new List<SlotUI>();
        _relicName = _uiManager.RelicName;
        _relicEffect = _uiManager.RelicEffectText;
        _relicDesc = _uiManager.RelicDecsText;

        //스탯 슬롯 생성
        for (int i = 0; i < _data.statNames.Length; i++)
        {
            GameObject go = _uiManager.InstantiateUI(_data.statUIPrefab, _uiManager.StatUIPivot);

            _statUIs.Add(go.GetComponent<StatUI>());
            _statUIs[i].statName.text = _data.statNames[i];
        }

        //장비 슬롯 생성
        for (int i = 0; i < _data.equipNum; i++)
        {
            GameObject go = _uiManager.InstantiateUI(_data.equipUIPrefab, _uiManager.EquipUIPivot);

            _equipUIs.Add(go.GetComponent<EquipUI>());

            int index = i;
            _equipUIs[i].OnSelect += () => OnSelect(_equipUIs[index].data);
            _equipUIs[i].OnEquip += () => OnEquip(_equipUIs[index].data);
        }

        //성물 슬롯 생성
        for (int i = 0; i < _playerInventory.relics.Count; i++)
        {
            GameObject go = _uiManager.InstantiateUI(_data.slotUIPrefab, _uiManager.SlotUIPivot);

            _slotUIs.Add(go.GetComponent<SlotUI>());
        }


    }

    public override void Enter()
    {
        base.Enter();
        _uiManager.RelicUI.SetActive(true);
        UpdateStatus();
        UpdateSlot();
    }

    public override void Exit()
    {
        base.Exit();
        _uiManager.RelicUI.SetActive(false);
    }

    public override void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _uiStateMachine.ChangeState(_uiStateMachine.MainUI);
        }
    }



    //스테이터스 갱신
    private void UpdateStatus()
    {
        _statUIs[0].basic.text = _playerCondition.Attack.ToString();
        _statUIs[1].basic.text = _playerCondition.Defence.ToString();
        _statUIs[2].basic.text = _playerCondition.MaxHp.ToString();
        _statUIs[3].basic.text = _playerCondition.MaxStamina.ToString();

        _statUIs[0].relic.text = "+" + _playerInventory.EquipRelicAttack();
        _statUIs[1].relic.text = "+" + _playerInventory.EquipRelicDefense();
        _statUIs[2].relic.text = "+" + _playerInventory.EquipRelicHp();
        _statUIs[3].relic.text = "+" + _playerInventory.EquipRelicStamina();

        //버프 효과 표시 필요
    }

    //좌클릭 시 실행
    private void OnSelect(CollectObjectSO data)
    {
        _relicName.text = data.relicName;
        _relicEffect.text = data.effectDesc;
        _relicDesc.text = data.relicDesc;
    }


    //우클릭 시 실행
    private void OnEquip(CollectObjectSO data)
    {
        _playerInventory.EquipRelic(data);
        foreach (EquipUI equipUI in _equipUIs)
        {
            equipUI.data = null;
            equipUI.SetActive();
        }

        for (int i = 0; i < _playerInventory.EquipRelics.Count; i++)
        {
            _equipUIs[i].data = _playerInventory.EquipRelics[i];
            _equipUIs[i].SetActive();
        }

        UpdateStatus();
    }

    //성물 슬롯 갱신
    private void UpdateSlot()
    {
        for (int i = 0; i < _playerInventory.relics.Count; i++)
        {
            if (_playerInventory.relics[i].IsGet)
            {
                _slotUIs[i].data = _playerInventory.relics[i];
                _slotUIs[i].SetActive();
                int index = i;
                _slotUIs[i].OnSelect += () => OnSelect(_slotUIs[index].data.Data);
                _slotUIs[i].OnEquip += () => OnEquip(_slotUIs[index].data.Data);
            }
        }
    }
}
