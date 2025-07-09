using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : UIBaseState
{
    private Image _potionIcon;
    private TextMeshProUGUI _potionText;
    private List<BuffUI> _buffUIs;
    private ConditionUI _hpConditionUI;
    private ConditionUI _staminaConditionUI;
    
    public MainUI(UIStateMachine uiStateMachine) : base(uiStateMachine)
    {
        //변수 셋팅
        _potionIcon = _uiManager.PotionIcon;
        _potionText = _uiManager.PotionText;
        _hpConditionUI = _uiManager.HpUI;
        _staminaConditionUI = _uiManager.StaminaUI;
        
        _buffUIs = new List<BuffUI>();
        for (int i = 0; i < _data.buffUINum; i++)
        {
            GameObject go = _uiManager.InstantiateUI(_data.buffUIPrefab, _uiManager.BuffUIPivot);
            _buffUIs.Add(go.GetComponent<BuffUI>());
        }

        //데이터 셋팅
        _potionIcon.sprite = _data.potionIcon;
        _potionText.text = _playerInventory.CurPotionNum.ToString();
        _hpConditionUI.SetMaxValue(_data.conditionSize * _playerCondition.MaxHp);
        _hpConditionUI.SetCurValue(_playerCondition.HpValue());
        _staminaConditionUI.SetMaxValue(_data.conditionSize * _playerCondition.MaxStamina);
        _staminaConditionUI.SetCurValue(_playerCondition.StaminaValue());
    }

    public override void Update()
    {
        base.Update();
        
        //UI 갱신
        _hpConditionUI.SetCurValue(_playerCondition.HpValue());
        _staminaConditionUI.SetCurValue(_playerCondition.StaminaValue());
    }
    
    public void UpdatePotionText()
    {
        _potionText.text = _playerInventory.CurPotionNum.ToString();
        if(_playerInventory.CurPotionNum > 0)
            _potionIcon.sprite = _data.potionIcon;
        else
            _potionIcon.sprite = _data.emptyPotionIcon;
    }

    public void UpdateBuffUI(StatObjectSO data)
    {
        foreach (BuffUI buffUI in _buffUIs)
        {
            if (buffUI.data == null)
            {
                buffUI.SetBuff(data);
            }
        }
    }
    
    
}
