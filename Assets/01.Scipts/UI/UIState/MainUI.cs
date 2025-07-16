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

    public override void Enter()
    {
        _uiManager.MainUI.SetActive(true);
    }
    public override void Exit()
    {
        _uiManager.MainUI.SetActive(false);
    }

    public override void HandleInput()
    {
        //esc 이력 시
        if (Input.GetKeyDown(KeyCode.Escape))
        {   //성물 UI
            _uiStateMachine.ChangeState(_uiStateMachine.RelicUI);
        }
    }

    public override void Update()
    {
        base.Update();
        
        //UI 갱신
        _hpConditionUI.SetCurValue(_playerCondition.HpValue());
        _staminaConditionUI.SetCurValue(_playerCondition.StaminaValue());
    }
    
    //포션의 개수를 갱신
    public void UpdatePotionText()
    {
        _potionText.text = _playerInventory.CurPotionNum.ToString();
        if(_playerInventory.CurPotionNum > 0)
            _potionIcon.sprite = _data.potionIcon;
        else
            _potionIcon.sprite = _data.emptyPotionIcon;
    }

    //버프 갱신
    public void UpdateBuffUI(StatObjectSO data)
    {
        foreach (BuffUI buffUI in _buffUIs)
        {
            if (buffUI.data == null || buffUI.data == data)
            {
                buffUI.SetBuff(data);
                break;
            }
        }
    }
    
    
}
