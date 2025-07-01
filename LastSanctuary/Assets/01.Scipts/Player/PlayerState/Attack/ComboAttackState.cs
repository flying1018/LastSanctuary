using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttackState : PlayerAttackState
{
    private AttackInfo _attackInfo;
    private float _animationTime;
    private float _time;
    private Vector2 _dir;
    
    public ComboAttackState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        
        _rigidbody.velocity = Vector2.zero;

        //현재 공격 정보 가져오기
        int comboIndex = _stateMachine.comboIndex;
        _attackInfo = _player.Data.attacks.GetAttackInfo(comboIndex);
        
        //공격 애니메이션 실행
        _player.Animator.SetInteger(_player.AnimationDB.ComboParameterHash, _attackInfo.attackIndex);

        //시간 측정
        _time = 0;
        _animationTime = _player.Animator.GetCurrentAnimatorStateInfo(0).length;
        
        //전진 파워 만큼 앞으로 전진
        _dir = _spriteRenderer.flipX ? Vector2.left : Vector2.right;
        
        //무기에 대미지 전달
        _playerWeapon.Damage = (int)(_condition.Damage * _attackInfo.multiplier);
    }

    public override void Exit()
    {
        base.Exit();
        
        _player.Animator.SetInteger(_stateMachine.Player.AnimationDB.ComboParameterHash, 0);
    }

    public override void HandleInput()
    {
        //다음 공격
        _time += Time.deltaTime;
        
        //입력 시간 내에 공격 입력 시
        if (_time <= (_animationTime + _attackInfo.nextComboTime) && _input.IsAttack)
        {
            //애니메이션 끝나고 공격
            if (_time > _animationTime)
            {
                //다음 공격 번호 가져오기
                _stateMachine.comboIndex =
                    _data.attacks.GetAttackInfoCount() <= _stateMachine.comboIndex + 1
                        ? 0 : _stateMachine.comboIndex + 1;
                
                //다음 공격의 필요 스테미나가 충분하다면 공격
                if (_condition.UsingStamina(_data.attacks.GetAttackInfo(_stateMachine.comboIndex).staminaCost))
                {
                    //다음 공격이 없으면 종료
                    if (_stateMachine.comboIndex == 0)
                    {
                        _input.IsAttack = false;
                        _stateMachine.ChangeState(_stateMachine.IdleState);
                        return;
                    }

                    //다음 공격
                    _stateMachine.ChangeState(_stateMachine.ComboAttack);
                }
            }

        }
        //공격 종료
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
        _rigidbody.velocity = Vector2.Lerp(_rigidbody.velocity, _dir * _attackInfo.attackForce, Time.deltaTime * 10f);
    }
}
