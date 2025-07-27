using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : UIBaseState
{
    private Image _potionIcon;
    private TextMeshProUGUI _potionText;
    private TextMeshProUGUI _goldText;
    private List<BuffUI> _buffUIs;
    private ConditionUI _hpConditionUI;
    private ConditionUI _staminaConditionUI;
    private ConditionUI _ultimateConditionUI;
    
    public MainUI(UIStateMachine uiStateMachine) : base(uiStateMachine)
    {
        //변수 셋팅
        _potionIcon = _uiManager.PotionIcon;
        _potionText = _uiManager.PotionText;
        _hpConditionUI = _uiManager.HpUI;
        _staminaConditionUI = _uiManager.StaminaUI;
        _ultimateConditionUI = _uiManager.UltimateConditionUI;
        _goldText = _uiManager.GoldText;

        _buffUIs = new List<BuffUI>();
        for (int i = 0; i < _data.buffUINum; i++)
        {
            GameObject go = _uiManager.InstantiateUI(_data.buffUIPrefab, _uiManager.BuffUIPivot);
            _buffUIs.Add(go.GetComponent<BuffUI>());
        }

        //데이터 셋팅
        _potionIcon.sprite = _data.potionIcon;
    }

    public override void Enter()
    {
        //text 설정
        _potionText.text = _playerInventory.CurPotionNum.ToString();
        _goldText.text = _playerInventory.Gold.ToString();
        
        //hp 설정
        _hpConditionUI.SetMaxValue(_data.conditionSize * _playerCondition.TotalHp);
        _hpConditionUI.SetCurValue(_playerCondition.HpValue());
        _staminaConditionUI.SetMaxValue(_data.conditionSize * _playerCondition.TotalStamina);
        _staminaConditionUI.SetCurValue(_playerCondition.StaminaValue());
        _ultimateConditionUI.SetMaxValue(_data.conditionSize * _playerCondition.MaxUltimate);
        _ultimateConditionUI.SetCurValue(_playerCondition.UltimateValue());
        
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
        _ultimateConditionUI.SetCurValue(_playerCondition.UltimateValue());
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
    
    //골드 수를 갱신
    public void UpdateGoldText()
    {
        _goldText.text = _playerInventory.Gold.ToString();
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
