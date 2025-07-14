using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuardState : PlayerBaseState
{
    private float _guardStart;

    public PlayerGuardState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_player.AnimationDB.GuardParameterHash);
        
        _rigidbody.velocity = Vector2.zero;
        _guardStart = Time.time;
        _condition.IsPerfectGuard = true;
        _condition.IsGuard = true;
        
        SoundClip[0] = _data.guardSound;
        SoundClip[1] = _data.perfectGuardSound;

    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_player.AnimationDB.GuardParameterHash);
        
        _condition.IsGuard = false;
        _condition.IsPerfectGuard = false;
    }

    public override void HandleInput()
    {
        if (!_input.IsGuarding)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
        if (_input.IsDash && _condition.UsingStamina(_data.dashCost))
        {
            _stateMachine.ChangeState(_stateMachine.DashState); // 대시 상태로 전환
        }
    }
    
    public override void PhysicsUpdate()
    {
        PerfectGuard();
    }
    
    public void PerfectGuard()
    {
        if (Time.time - _guardStart >= _data.perfectGuardWindow)
        {
            _condition.IsPerfectGuard = false;
        }
    }
    
    public override void Update()
    {
        
    }
}
