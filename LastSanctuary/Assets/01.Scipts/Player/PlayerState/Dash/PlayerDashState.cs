using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerBaseState
{
    private Vector2 dir;
    private float _time;
    public PlayerDashState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        
        _player.Animator.SetTrigger(_player.AnimationDB.DashParameterHash);
        
        _input.IsDash = false;
        _time = 0;
        if (_input.MoveInput.x == 0)
        {
            dir = _spriteRenderer.flipX ? Vector2.left : Vector2.right;
        }
        else
        { 
            dir = _input.MoveInput.x < 0 ? Vector2.left : Vector2.right;
        }
        Rotate(dir);
        
        SoundClip[0] = _data.dashSound;
        _player.PlaySFX1();
    }

    public override void Exit()
    {
        base.Exit();
        
        Move(Vector2.zero);
    }

    public override void HandleInput()
    {
        if (_input.IsAttack)
        {
            int cost = _stateMachine.DashAttack.attackInfo.staminaCost;
            //스테미나가 충분하다면 3타 시작
            if (_condition.UsingStamina(cost))
            {
                _stateMachine.ChangeState(_stateMachine.DashAttack);
            }
            
        }
    }

    public override void Update()
    {
        _time += Time.deltaTime;
        if (_time < _data.DashTime) return;

        if (!_player.IsGround())
        {
            _stateMachine.ChangeState(_stateMachine.FallState);
        }
        else if (_input.MoveInput.x != 0)
        {
            _stateMachine.ChangeState(_stateMachine.MoveState);
        }
        else if (_input.MoveInput.x == 0)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        _rigidbody.velocity = dir * _data.dashPower;
    }
    


}
