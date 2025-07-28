using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnifiedUI : UIBaseState
{
    protected GameObject _mouseLeft;
    protected TextMeshProUGUI _mouseLeftDesc;
    protected GameObject _mouseRight;
    protected TextMeshProUGUI _mouseRightDesc;
    
    public UnifiedUI(UIStateMachine uiStateMachine) : base(uiStateMachine)
    {
        _uiManager.ExitButton.onClick.AddListener(OnClickExitButton);
        _uiManager.RelicUIButton.onClick.AddListener(OnClickRelicUIButton);
        _uiManager.SkillUIButton.onClick.AddListener(OnClickSkillUIButton);
        _uiManager.SettingUIButton.onClick.AddListener(OnClickSettingUIButton);
        
        _mouseLeft = _uiManager.MouseLeft;
        _mouseRight= _uiManager.MouseRight;
        _mouseLeftDesc = _mouseLeft.GetComponentInChildren<TextMeshProUGUI>();
        _mouseRightDesc = _mouseRight.GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void Enter()
    {
        _uiManager.PlayerInput.enabled = false;
        _uiManager.UnifiedUI.SetActive(true);
    }

    public override void Exit()
    {
        _uiManager.PlayerInput.enabled = true;
        _uiManager.UnifiedUI.SetActive(false);
    }
    //성물 버튼
    public void OnClickRelicUIButton()
    {
        _uiStateMachine.ChangeState(_uiStateMachine.RelicUI);
    }
    //스킬 버튼
    public void OnClickSkillUIButton()
    {
       // _uiStateMachine.ChangeState(_uiStateMachine.SkillUI);
       //아직 없음
    }
    //설정 버튼
    public void OnClickSettingUIButton()
    {
        _uiStateMachine.ChangeState(_uiStateMachine.SettingUI);
    }

    //나가기 버튼
    public void OnClickExitButton()
    {
        _uiStateMachine.ChangeState(_uiStateMachine.MainUI);
    }
    
    public override void HandleInput()
    {
        //Esc 입력 시
        if (Input.GetKeyDown(KeyCode.Escape))
        {   //메인 UI로 이동
            _uiStateMachine.ChangeState(_uiStateMachine.MainUI);
        }
    }

}
