using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02AreaAttackState : Boss02AttackState
{
    public Boss02AreaAttackState(Boss02StateMachine bossStateMachine, BossAttackInfo attackInfo) : base(bossStateMachine, attackInfo)
    {
        //초기에는 바로 사용 가능
        _coolTime = _attackCoolTime;
    }

    public override void Enter()
    {
        base.Enter();
        
        PlaySFX1();
    }

    public override void PlaySFX1()
    {
        SoundManager.Instance.PlaySFX(_attackInfo.attackSounds[0]);
    }
}
