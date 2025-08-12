using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// UI의 기초 상태
/// </summary>
public class UIBaseState : MonoBehaviour, IState
{
    protected UIStateMachine _uiStateMachine;
    protected UIManager _uiManager;
    protected PlayerCondition _playerCondition;
    protected PlayerInventory _playerInventory;
    protected PlayerSkill _playerSkill;
    protected UIManagerSO _data;
    
    public UIBaseState(UIStateMachine uiStateMachine) { }

    public virtual void Init(UIStateMachine uiStateMachine)
    {
        _uiStateMachine = uiStateMachine;
        _uiManager = uiStateMachine.UIManager; 
        _playerCondition = _uiManager.PlayerCondition;
        _playerInventory = _uiManager.PlayerInventory;
        _playerSkill = _uiManager.PlayerSkill;
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
