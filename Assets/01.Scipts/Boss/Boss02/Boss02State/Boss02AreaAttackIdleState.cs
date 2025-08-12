using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02AreaAttackIdleState : Boss02IdleState
{

    public Boss02AreaAttackIdleState(Boss02StateMachine bossStateMachine) : base(bossStateMachine) { }

    public override void Exit()
    {
        base.Exit();

        _condition2.DontCollision = false;
    }

    public override void Update()
    {
        _stateMachine2.AreaAttack.CheckCoolTime();
        _stateMachine2.ProjectileAttack.CheckCoolTime();
        
        _time += Time.deltaTime;
        if (_time > _data.attackIdleTime)
        {
            //시간 초기화
            _time = 0;

            if (_stateMachine2.AreaAttack.CheckCoolTime())
            {
                _stateMachine2.ChangeState(_stateMachine2.AreaAttack);
            }
            else
            {
                SetMovePosition();
                _stateMachine2.ChangeState(_stateMachine2.TeleportState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        _move.Move(Vector2.down);
    }
}
