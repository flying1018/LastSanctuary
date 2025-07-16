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
    public UIBaseState OffUI { get; private set; }
    
    public UIStateMachine(UIManager uiManager)
    {
        UIManager = uiManager;
        MainUI = new MainUI(this);
        RelicUI = new RelicUI(this);
        OffUI = new UIBaseState(this);
        
        ChangeState(MainUI);
    }
}
