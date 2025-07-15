using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBaseState : IState
{
    protected UIStateMachine _uiStateMachine;
    protected UIManager _uiManager;
    protected PlayerCondition _playerCondition;
    protected PlayerInventory _playerInventory;
    protected UIManagerSO _data;
    
    public UIBaseState(UIStateMachine uiStateMachine)
    {
        _uiStateMachine = uiStateMachine;
        _uiManager = uiStateMachine.UIManager;
        _playerCondition = _uiManager.PlayerCondition;
        _playerInventory = _uiManager.PlayerInventory;
        _data = _uiManager.Data;
    }
    
    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void HandleInput()
    {

    }

    public  virtual void Update()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }
    
    
}
