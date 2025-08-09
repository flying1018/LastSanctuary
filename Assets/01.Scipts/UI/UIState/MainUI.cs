using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : UIBaseState
{
    
    [Header("MainUI")]
    [SerializeField] private RectTransform buffUIPivot;
    [SerializeField] private ConditionUI hpConditionUI;
    [SerializeField] private ConditionUI staminaConditionUI;
    [SerializeField] private ConditionUI ultimateConditionUI;
    [SerializeField] private Image potionIcon;
    [SerializeField] private TextMeshProUGUI potionText;
    [SerializeField] private TextMeshProUGUI goldText;
    
    //필드
    private List<BuffUI> _buffUIs;
    
    public MainUI(UIStateMachine uiStateMachine) : base(uiStateMachine) { }

    public override void Init(UIStateMachine uiStateMachine)
    {
        base.Init(uiStateMachine);

        _buffUIs = new List<BuffUI>();
        for (int i = 0; i < _data.buffUINum; i++)
        {
            GameObject go = _uiManager.InstantiateUI(_data.buffUIPrefab, buffUIPivot);
            _buffUIs.Add(go.GetComponent<BuffUI>());
        }

        //데이터 셋팅
        potionIcon.sprite = _data.potionIcon;
    }

    public override void Enter()
    {
        //text 설정
        potionText.text = _playerInventory.CurPotionNum.ToString();
        goldText.text = _playerInventory.Gold.ToString();
        
        //컨디션 설정
        UpdateCondition();
        
        gameObject.SetActive(true);
    }
    public override void Exit()
    {
        gameObject.SetActive(false);
    }

    public override void HandleInput()
    {
        //esc 이력 시
        if (Input.GetKeyDown(KeyCode.Escape))
        {   //성물 UI
            _uiStateMachine.ChangeState(_uiStateMachine.SkillUI);
        }
    }

    public override void Update()
    {
        base.Update();

        //UI 갱신
        hpConditionUI.SetCurValue(_playerCondition.HpValue());
        staminaConditionUI.SetCurValue(_playerCondition.StaminaValue());
        ultimateConditionUI.SetCurValue(_playerCondition.UltimateValue());
    }
    
    //포션의 개수를 갱신
    public void UpdatePotionText()
    {
        potionText.text = _playerInventory.CurPotionNum.ToString();
        
        if(_playerInventory.CurPotionNum > 0)
            potionIcon.sprite = _data.potionIcon;
        else
            potionIcon.sprite = _data.emptyPotionIcon;
    }
    
    //골드 수를 갱신
    public void UpdateGoldText()
    {
        goldText.text = _playerInventory.Gold.ToString();
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

    public void UpdateCondition()
    {
        hpConditionUI.SetMaxValue(_data.conditionSize * _playerCondition.TotalHp);
        staminaConditionUI.SetMaxValue(_data.conditionSize * _playerCondition.TotalStamina);
        ultimateConditionUI.SetMaxValue(_data.conditionSize * _playerCondition.MaxUltimate);
    }
    
    
}
