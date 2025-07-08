using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;
using UnityEngine;

public class LelicUI : UIBaseState
{
    private List<StatUI> _statUIs;
    private List<EquipUI> _equipUIs;
    private List<SlotUI> _slotUIs;
    public LelicUI(UIStateMachine uiStateMachine) : base(uiStateMachine)
    {
        _statUIs = new List<StatUI>();
        _equipUIs = new List<EquipUI>();
        _slotUIs = new List<SlotUI>();

        for (int i = 0; i < _data.statNames.Length; i++)
        {
            GameObject go = _uiManager.InstantiateUI(_data.statUIPrefab,_uiManager.StatUIPivot);
            
            _statUIs.Add(go.GetComponent<StatUI>());
            _statUIs[i].statName.text = _data.statNames[i];
            //_statUIs[i].basic.text = _uiManager.PlayerCondition.Attack.ToString();
        }

        for (int i = 0; i < _data.equipNum; i++)
        {
            GameObject go = _uiManager.InstantiateUI(_data.equipUIPrefab,_uiManager.EquipUIPivot);
            
            _equipUIs.Add(go.GetComponent<EquipUI>());
        }

        for (int i = 0; i<_data.slotNum; i++)
        {
            GameObject go = _uiManager.InstantiateUI(_data.slotUIPrefab,_uiManager.SlotUIPivot);
            
            _slotUIs.Add(go.GetComponent<SlotUI>());
        }
    }
}
