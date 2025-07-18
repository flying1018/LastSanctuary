using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public AttackInfo attackInfo;
    protected float _time;
    protected float _animationTime;

    //생성자
    public PlayerAttackState(PlayerStateMachine stateMachine, AttackInfo attackInfo) : base(stateMachine)
    {
        this.attackInfo = attackInfo;
        _time = 0;
        _animationTime = attackInfo.animTime;
    }

    public override void Enter()
    {
        base.Enter();
        _input.IsAttack = false;
        
        //공격 상태 머신 진입
        StartAnimation(_player.AnimationDB.AttackParameterHash);
        
        //무기에 대미지 전달
        _playerWeapon.Damage = (int)((_condition.Attack + _inventory.EquipRelicAttack() + _condition.BuffAtk) * attackInfo.multiplier);
        _playerWeapon.knockBackForce = attackInfo.knockbackForce;
        _playerWeapon.groggyDamage = attackInfo.groggyDamage;
        _playerWeapon.defpen = _data.Defpen;
        
        //애니메이션 실행
        _player.Animator.SetInteger(_player.AnimationDB.ComboParameterHash, attackInfo.attackIndex);
        _time = 0;
        
        //무적 공격은 무적상태 추가
        _condition.IsInvincible = attackInfo.isInvincible;
    }

    public override void Exit()
    {
        base.Exit();
        
        StopAnimation(_player.AnimationDB.AttackParameterHash);
        
        _player.Animator.SetInteger(_stateMachine.Player.AnimationDB.ComboParameterHash, 0);
        
        //무적 종료
        _condition.IsInvincible = false;
    }

    public override void HandleInput()
    {
        //대쉬 키 입력 시 스태미나가 충분하면
        if (_input.IsDash && _condition.UsingStamina(_data.dashCost))
        {   //대쉬
            _stateMachine.ChangeState(_stateMachine.DashState);
        }
        //공격 중 가드 가능
        if (_move.IsGrounded &&_input.IsGuarding && _stateMachine.comboIndex != 2)
        { 
            _stateMachine.ChangeState(_stateMachine.GuardState);
        }
    }

    //스테미나 회복 막기
    public override void Update()
    {
        //무적 시간은 개선 필요
        _condition.InvincibleStart = Time.time;
        
        //공격 종료
        _time += Time.deltaTime;
        if (_time > (_animationTime + attackInfo.nextComboTime))
        {
            if(_move.IsGrounded)
                _stateMachine.ChangeState(_stateMachine.IdleState);
            else
                _stateMachine.ChangeState(_stateMachine.FallState);
                
            
        }
    }
    

    public override void PhysicsUpdate()
    {
        
    }
    
    //대쉬어택, 3타어택 공용으로 사용
    public override void PlayEvent1()
    {
        Vector2 direction = _spriteRenderer.flipX ? Vector2.left : Vector2.right;
        _move.AddForce(direction * attackInfo.attackForce);
    }

    public override void PlaySFX1()
    {
        SoundManager.Instance.PlaySFX(_data.attackSound,0.2f);
    }
}
