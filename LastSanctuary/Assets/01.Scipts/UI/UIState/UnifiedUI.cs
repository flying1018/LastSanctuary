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
        _uiManager.PlayerController.enabled = false;
        _uiManager.UnifiedUI.SetActive(true);
    }

    public override void Exit()
    {
        _uiManager.PlayerController.enabled = true;
        _uiManager.UnifiedUI.SetActive(false);
    }
    
    public void OnClickExitButton()
    {
        _uiStateMachine.ChangeState(_uiStateMachine.MainUI);
    }
}
