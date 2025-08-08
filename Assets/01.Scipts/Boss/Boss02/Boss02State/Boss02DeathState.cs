using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02DeathState : BossBaseState
{
    public Boss02DeathState(Boss02StateMachine bossStateMachine) : base(bossStateMachine) { }

    public override void Enter()
    {        
        //애니메이션 실행
        _boss2.Animator.Rebind();
        _boss2.Animator.SetTrigger(_boss2.AnimationDB.DeathParameterHash);
        
        //보스 사망 연출
        _boss02Event.OnTriggerBossDeath();
        
        //보스 골드 드랍
        ItemManager.Instance.GetGold(_data.dropGold);

        //보스 사망 체크
        MapManager.SetBossDead();
        
        //보스 콜라이더 끄기
        _boxCollider.enabled = false;
        
        //bossUI
        if(_boss2.UIOn)
            UIManager.Instance.SetBossUI(false);
    }

    public override void Update()
    {
        _time += Time.deltaTime;
        if (_time > _data.deathTime)
        {
            _boss2.gameObject.SetActive(false);
        }
    }
}
