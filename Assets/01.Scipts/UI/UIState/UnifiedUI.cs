using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnifiedUI : UIBaseState
{
    public UnifiedUI(UIStateMachine uiStateMachine) : base(uiStateMachine)
    {
        _uiManager.ExitButton.onClick.AddListener(OnClickExitButton);
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
