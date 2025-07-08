using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public UIStateMachine UiStateMachine { get; set; }
    public PlayerCondition PlayerCondition { get; set; }
    public PlayerInventory PlayerInventory { get; set; }


    private void Start()
    {
        //testCode
        Init();
    }

    public void Init()
    {
        PlayerCondition = FindAnyObjectByType<PlayerCondition>();
        PlayerInventory = FindAnyObjectByType<PlayerInventory>();
        
        UiStateMachine = new UIStateMachine(this);
    }

    public int ShowWarpUI(WarpObject slectWarpObj)
    {
        return 1;
    }

    private void Update()
    {
        UiStateMachine.HandleInput();
        UiStateMachine.Update();
    }

    private void FixedUpdate()
    {
        UiStateMachine.PhysicsUpdate();
    }
}
