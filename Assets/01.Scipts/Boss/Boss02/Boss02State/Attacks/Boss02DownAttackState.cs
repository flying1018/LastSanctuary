using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02DownAttackState : Boss02AttackState
{
    public Boss02DownAttackState(Boss02StateMachine bossStateMachine, BossAttackInfo attackInfo) : base(bossStateMachine, attackInfo) { }

    public override void Enter()
    {
        base.Enter();

        PlaySFX1();
    }

    public override void Update()
    {
        
        //공격이 끝나면 대기 상태
        _time += Time.deltaTime;
        if (_time > _attackInfo.AnimTime)
        {
            _stateMachine2.ChangeState(_stateMachine2.AreaAttackIdle);
        }
    }

    public override void PhysicsUpdate()
    {
        Move(Vector2.down * _attackInfo.projectilePower);
    }

    public override void PlaySFX1()
    {
        SoundManager.Instance.PlaySFX(_attackInfo.attackSounds[0]);
    }
}
