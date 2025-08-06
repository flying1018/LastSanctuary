using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathState : BossBaseState
{
    
    public BossDeathState(BossStateMachine bossStateMachine1) : base(bossStateMachine1) { }

    public override void Enter()
    {        
        //애니메이션 실행
        _boss.Animator.Rebind();
        _boss.Animator.SetTrigger(_boss.AnimationDB.DeathParameterHash);
        
        //보스 골드 드랍
        ItemManager.Instance.GetGold(_data.dropGold);
        
        //보스 사망 연출
        _boss.BossEvent.OnTriggerBossDeath();
        //보스 사망 체크
        MapManager.SetBossDead();
        //보스 콜라이더 끄기
        _boxCollider.enabled = false;
        
        //bossUI
        if(_boss.UIOn)
            UIManager.Instance.SetBossUI(false);
    }

    public override void Update()
    {
        _time += Time.deltaTime;
        if (_time > _data.deathTime)
        {
            ObjectPoolManager.Set(_boss.gameObject, _boss.Data._key);
        }
    }
}
