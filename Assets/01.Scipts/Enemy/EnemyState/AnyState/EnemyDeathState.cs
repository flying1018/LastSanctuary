using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : EnemyBaseState
{
    public EnemyDeathState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }

    public override void Enter()
    {
        //사망 애니메이션
        _enemy.Animator.SetTrigger(_enemy.AnimationDB.DeathParameterHash);
        
        //시간 체크
        _time = 0;
    }

    public override void Exit()
    {
        //스폰 포인트에 사망 처리
        var spawnPoint = _spawnPoint.GetComponent<EnemySpawnPoint>();
        
        //엘리트 구분
        if (spawnPoint.Enemytype == EnemyType.Elite)
        {
            MapManager.Instance.SetEliteDead(spawnPoint);
        }
        spawnPoint.isSpawn = false;
        
        //애니메이터 초기화
        _enemy.Animator.Rebind();
        
        //골드 드랍
        ItemManager.Instance.GetGold(_data.dropGold);
        
        //오브젝트 회수
        ObjectPoolManager.Set(_enemy.gameObject, _enemy.Data._key);
    }

    public override void HandleInput() { }

    public override void Update()
    {
        _time += Time.deltaTime;
        if (_time > _data.DeathTime)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }
    
    public override void PhysicsUpdate() { }
}
