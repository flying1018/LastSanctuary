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
        
        StartAnimation(_stateMachine.Player.AnimationDB.AttackParameterHash);

        int comboIndex = _stateMachine.comboIndex;
        _attackInfo = _stateMachine.Player.Data.attacks.GetAttackInfo(comboIndex);
        _stateMachine.Player.Animator.SetInteger(_stateMachine.Player.AnimationDB.ComboParameterHash, _attackInfo.attackIndex);

        //시간 측정
        _time = 0;
        _animationTime = _stateMachine.Player.Animator.GetCurrentAnimatorStateInfo(0).length;

    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Player.AnimationDB.AttackParameterHash);
        
        _stateMachine.Player.Animator.SetInteger(_stateMachine.Player.AnimationDB.ComboParameterHash, 0);
    }

    public override void HandleInput()
    {
        _time += Time.deltaTime;
        if (_time <= (_animationTime + _attackInfo.nextComboTime) && _input.IsAttack)
        {
            _stateMachine.comboIndex = _stateMachine.Player.Data.attacks.GetAttackInfoCount() == _stateMachine.comboIndex ? 0 : _stateMachine.comboIndex+1;
            _stateMachine.ChangeState(_stateMachine.AttackState);
        }
        else if (_time > (_animationTime + _attackInfo.nextComboTime))
        {
            _stateMachine.comboIndex = 0;
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
        
    }

    public override void Update()
    {
    }

    public override void PhysicsUpdate()
    {

    }
}
