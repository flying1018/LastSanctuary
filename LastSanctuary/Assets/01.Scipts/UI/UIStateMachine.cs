using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStateMachine : StateMachine
{
    public UIManager UIManager { get; private set; }
    public MainUI MainUI { get; private set; }
    public RelicUI RelicUI { get; private set; }
    
    public UIStateMachine(UIManager uiManager)
    {
        UIManager = uiManager;
        MainUI = new MainUI(this);
        RelicUI = new RelicUI(this);
        
        ChangeState(MainUI);
    }
}
