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
        StartAnimation(_stateMachine.Player.AnimationDB.AttackParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        
        StopAnimation(_stateMachine.Player.AnimationDB.AttackParameterHash);
    }

    public override void HandleInput()
    {
        
    }

    public override void Update()
    {
    }

    public override void PhysicsUpdate()
    {

    }
}
