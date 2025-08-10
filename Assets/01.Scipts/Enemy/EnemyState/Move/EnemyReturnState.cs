using UnityEngine;

public class EReturnState : EnemyBaseState
{
    public EReturnState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }
    
    public override void Enter()
    {
        StartAnimation(_enemy.AnimationDB.WalkParameterHash);
        
        //복귀시 무적
        _condition.IsInvincible = true;
        _enemy.Target = null;  
        
        //회복
        _condition.Recovery();
    }

    public override void Exit()
    {
        StopAnimation(_enemy.AnimationDB.WalkParameterHash);
        
        //무적 해제
        _condition.IsInvincible = false;
    }
    
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        //복귀
        Return();
    }

    //복귀 가상 메서드
    protected virtual void Return()
    {
        
    }
    
    public override void PlaySFX1()
    {
        SoundManager.Instance.PlaySFX(_data.walkSound);
    }
}


public class EnemyReturnState :EReturnState
{
    public EnemyReturnState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Update()
    {
        //공격 쿨타임 체크
        base.Update();
        
        //스폰포인트와 오차가 0.1f이라면
        if (Mathf.Abs(_enemy.transform.position.x - _spawnPoint.position.x) < 0.1f)
        {   
            if(_enemy.FindTarget())
                _stateMachine.ChangeState(_stateMachine.ChaseState);//추적
            else
                _stateMachine.ChangeState(_stateMachine.IdleState);//대기       
        }
    }

    //스폰 포인트를 행햐서 복귀
    protected override void Return()
    {
        Vector2 direction = DirectionToSpawnPoint();
        Move(direction);
        Rotate(direction);
    }
}
