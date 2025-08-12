using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 상태의 부모
/// </summary>
public interface IState
{
    public void Enter();
    public void Exit();
    public void HandleInput();
    public void Update();
    public void PhysicsUpdate();
}

/// <summary>
/// 상태머신
/// </summary>
public abstract class StateMachine
{
    public IState currentState;

    /// <summary>
    /// 현재 상태를 변경하는 메서드
    /// </summary>
    /// <param name="state"></param>
    public virtual void ChangeState(IState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState?.Enter();
    }

    /// <summary>
    /// 플레이어의 조작을 받는 메서드
    /// </summary>
    public void HandleInput() 
    {
        currentState?.HandleInput();
    }

    /// <summary>
    /// 현재 상태의 외부 자극을 판단하는 메서드
    /// </summary>
    public void Update()
    {
        currentState?.Update();
    }

    /// <summary>
    /// 현재 상태의 물리적 처리를 하는 메서드
    /// </summary>
    public void PhysicsUpdate()
    {
        currentState?.PhysicsUpdate();
    }
}