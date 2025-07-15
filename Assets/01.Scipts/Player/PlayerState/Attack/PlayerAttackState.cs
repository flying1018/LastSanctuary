using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public AttackInfo attackInfo;
    protected float _time;
    protected float _animationTime;

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
        
        _condition.IsInvincible = attackInfo.isInvincible;
        
        //사운드 추가
        SoundClip[0] = _data.attackSound;
    }

    public override void Exit()
    {
        base.Exit();
        
        StopAnimation(_player.AnimationDB.AttackParameterHash);
        
        _player.Animator.SetInteger(_stateMachine.Player.AnimationDB.ComboParameterHash, 0);
        _condition.IsInvincible = false;
    }

    public override void HandleInput()
    {
        if (_input.IsDash && _condition.UsingStamina(_data.dashCost))
        {
            _stateMachine.ChangeState(_stateMachine.DashState); // 대시 상태로 전환
        }
        if (_player.IsGround() &&_input.IsGuarding && _stateMachine.comboIndex != 2)
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
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }
    

    public override void PhysicsUpdate()
    {

    }
}
