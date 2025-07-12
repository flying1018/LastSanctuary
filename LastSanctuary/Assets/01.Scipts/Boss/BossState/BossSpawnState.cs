using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnState : BossBaseState
{
    private float _elapsed;

    public BossSpawnState(BossStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        UpdateCollider();
        _rigidbody.gravityScale = 15f;
        _boss.Animator.SetTrigger(_boss.AnimationDB.SpawnParameterHash);
    }

    public override void Exit()
    {
        _rigidbody.gravityScale = 3f;
        _boss.BossEvent.StartBattle();
    }

    public override void Update()
    {
        _elapsed += Time.deltaTime; 
        if (_elapsed >= _data.SpawnAnimeTime) //스폰 애니메이션 지속시간이 끝나면
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        
    }
}