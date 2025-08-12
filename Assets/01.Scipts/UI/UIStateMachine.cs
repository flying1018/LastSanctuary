using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI의 상태머신
/// </summary>
public class UIStateMachine : StateMachine
{
    public UIManager UIManager { get; private set; }
    public MainUI MainUI { get; private set; }
    public RelicUI RelicUI { get; private set; }
    public SettingUI SettingUI { get; private set; }
    public SkillUI SkillUI { get; private set; }
    public UIBaseState OffUI { get; private set; }
    
    public UIStateMachine(UIManager uiManager)
    {
        UIManager = uiManager;
        MainUI = UIManager.MainUI;
        RelicUI = UIManager.RelicUI;
        OffUI = UIManager.OffUI;
        SettingUI = UIManager.SettingUI;
        SkillUI = UIManager.SkillUI;

        MainUI.Init(this);
        RelicUI.Init(this);
        SettingUI.Init(this);
        SkillUI.Init(this);
        OffUI.Init(this);
        
        ChangeState(MainUI);
    }

    public override void ChangeState(IState state)
    {
        MainUI.Exit();
        RelicUI.Exit();
        SettingUI.Exit();
        SkillUI.Exit();
        OffUI.Exit();
        currentState = state;
        currentState.Enter();
    }
}
