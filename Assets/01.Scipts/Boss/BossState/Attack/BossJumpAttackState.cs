using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJumpAttackState : BossAttackState
{
    public BossJumpAttackState(BossStateMachine bossStateMachine, BossAttackInfo attackInfo) : base(bossStateMachine, attackInfo) { }

    public override void Enter()
    {
        base.Enter();
        
        _BoxCollider.enabled = false;
    }
    
    public override void Exit()
    {
        base.Exit();
        
        _BoxCollider.enabled = true;
    }

    //Attack3에서 사용하는 이벤트
    private Coroutine _chasePlayerCoroutine;

    public override void PlayEvent1()
    {
        ChasePlayer();
    }
    
    public override void PlayEvent2()
    {
        StopChasePlayer();
    }

    //플레이어에게 추적
    public void ChasePlayer()
    {
        if (_chasePlayerCoroutine != null)
        {
            _boss.StopCoroutine(_chasePlayerCoroutine);
            _chasePlayerCoroutine = null;
        }
        _chasePlayerCoroutine = _boss.StartCoroutine(ChasePlayer_Coroutine());
    }

    IEnumerator ChasePlayer_Coroutine()
    {
        while (true)
        {
            Vector2 targetX= _boss.Target.position;
            targetX.y = _boss.transform.position.y;
            _boss.transform.position = targetX;
            yield return null;
        }
    }
    

    //추적 정지
    public void StopChasePlayer()
    {
        if (_chasePlayerCoroutine != null)
        {
            _boss.StopCoroutine(_chasePlayerCoroutine);
            _chasePlayerCoroutine = null;
        }
    }
}
