using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    private AttackInfo _attackInfo;
    private float _animationTime;
    private float _time;
    
    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        _input.IsAttack = false;
        
        //공격 상태 진입
        StartAnimation(_player.AnimationDB.AttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        
        StopAnimation(_player.AnimationDB.AttackParameterHash);
    }

    public override void HandleInput()
    {
        if (_input.IsDash && _condition.UsingStamina(_data.dashCost))
        {
            _stateMachine.ChangeState(_stateMachine.DashState); // 대시 상태로 전환
        }
        if (_input.IsGuarding && _stateMachine.comboIndex != 2)
        {
            _stateMachine.ChangeState(_stateMachine.GuardState);
        }
    }

    public override void Update()
    {
    }

    public override void PhysicsUpdate()
    {

    }
}
