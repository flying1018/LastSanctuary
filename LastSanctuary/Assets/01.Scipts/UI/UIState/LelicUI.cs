using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneTemplate;
using UnityEngine;

public class LelicUI : UIBaseState
{
    private List<StatUI> _statUIs;
    private List<EquipUI> _equipUIs;
    private List<SlotUI> _slotUIs;
    private TextMeshProUGUI _relicName;
    private TextMeshProUGUI _relicEffect;
    private TextMeshProUGUI _relicDesc;
    public LelicUI(UIStateMachine uiStateMachine) : base(uiStateMachine)
    {
        _statUIs  = new List<StatUI>();
        _equipUIs = new List<EquipUI>();
        _slotUIs = new List<SlotUI>();
        _relicName = _uiManager.RelicName;
        _relicEffect = _uiManager.RelicEffectText;
        _relicDesc = _uiManager.RelicDecsText;

        for (int i = 0; i < _data.statNames.Length; i++)
        {
            GameObject go = _uiManager.InstantiateUI(_data.statUIPrefab,_uiManager.StatUIPivot);
            
            _statUIs.Add(go.GetComponent<StatUI>());
            _statUIs[i].statName.text = _data.statNames[i];
        }

        for (int i = 0; i < _data.equipNum; i++)
        {
            GameObject go = _uiManager.InstantiateUI(_data.equipUIPrefab,_uiManager.EquipUIPivot);
            
            _equipUIs.Add(go.GetComponent<EquipUI>());
        }

        for (int i = 0; i<_playerInventory.relics.Count; i++)
        {
            GameObject go = _uiManager.InstantiateUI(_data.slotUIPrefab,_uiManager.SlotUIPivot);
            
            _slotUIs.Add(go.GetComponent<SlotUI>());
            _slotUIs[i].data = _playerInventory.relics[i];
            _slotUIs[i].SetActive();
            int index = i;
            _slotUIs[i].OnSelect += () => OnSelect(_slotUIs[index].data.Data);
            _slotUIs[i].OnEquip += () => OnEquip(_slotUIs[index].data.Data);
        }
        
        GetStatus();
    }

    private void GetStatus()
    {
        _statUIs[0].basic.text = _playerCondition.Attack.ToString();
        _statUIs[1].basic.text = _playerCondition.Defence.ToString();
        _statUIs[2].basic.text = _playerCondition.MaxHp.ToString();
        _statUIs[3].basic.text = _playerCondition.MaxStamina.ToString();
    }

    private void OnSelect(CollectObjectSO data)
    {
        _relicName.text = data.relicName;
        _relicEffect.text = data.effectDesc;
        _relicDesc.text = data.relicDesc;
    }

    private void OnEquip(CollectObjectSO data)
    {
        foreach (EquipUI ui in _equipUIs)
        {
            if (ui.data == data)
            {
                ui.data = null;
                ui.SetActive();
                _playerInventory.UnEquipRelic(data);
                break;
            }
            else if (ui.data == null)
            {
                ui.data = data;
                ui.SetActive();
                _playerInventory.EquipRelic(data);
                break;
            }
            else
            {
                Debug.Log("빈 장비 칸이 없습니다.");
                break;
            }
        }
        
    }
}
