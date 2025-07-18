using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathState : BossBaseState
{
    private float _time;
    
    public BossDeathState(BossStateMachine bossStateMachine) : base(bossStateMachine) { }

    public override void Enter()
    {        
        //애니메이션 실행
        _boss.Animator.SetTrigger(_boss.AnimationDB.DeathParameterHash);
        //보스 사망 연출
        _boss.BossEvent.OnTriggerBossDeath();
    }

    public override void Exit()
    {
        SoundManager.Instance.PlayBGM(StringNameSpace.SoundAddress.TutorialBGM);
    }

    public override void Update()
    {
        _time += Time.deltaTime;
        if (_time > _data.deathTime)
        {
            ObjectPoolManager.Set(_boss.Data._key, _boss.gameObject, _boss.gameObject);
        }
    }
}
