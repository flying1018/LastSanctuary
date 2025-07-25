using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnifiedUI : UIBaseState
{
    public UnifiedUI(UIStateMachine uiStateMachine) : base(uiStateMachine)
    {
        _uiManager.ExitButton.onClick.AddListener(OnClickExitButton);
        _uiManager.RelicUIButton.onClick.AddListener(OnClickRelicUIButton);
        _uiManager.SkillUIButton.onClick.AddListener(OnClickSkillUIButton);
        _uiManager.SettingUIButton.onClick.AddListener(OnClickSettingUIButton);
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
