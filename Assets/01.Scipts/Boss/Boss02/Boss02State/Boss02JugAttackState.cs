using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02JugAttackState : BossBaseState
{
    public Boss02JugAttackState(Boss02StateMachine bossStateMachine) : base(bossStateMachine) { }

    public override void Enter()
    {
        base.Enter();

        int num = 0;
        if (_boss2.Phase2)
        {
            num = Random.Range(0, 3);
            switch (num)
            {
                case 0:
                    _stateMachine2.ChangeState(_stateMachine2.DownAttack);
                    break;
                case 1:
                    if(_stateMachine2.ProjectileAttack.CheckCoolTime()) 
                        _stateMachine2.ChangeState(_stateMachine2.ProjectileAttack);
                    else
                        _stateMachine2.ChangeState(_stateMachine2.FakeAttack);
                    break;
                case 2:
                    _stateMachine2.ChangeState(_stateMachine2.FakeAttack);
                    break;
            }


            return;
        }


        num = Random.Range(0, 2);
        if (_stateMachine2.MoveTarget == _boss02Event.TopMirror.position )
        {
            _stateMachine2.ChangeState(_stateMachine2.DownAttack);
        }
        for (int i = 0; i < _boss02Event.LeftMirror.Length; i++)
        {
            if (_stateMachine2.MoveTarget == _boss02Event.LeftMirror[i].position)
            {
                _stateMachine2.MoveTarget = _boss02Event.RightMirror[2-i].position;
                if(num == 0&& _stateMachine2.MoveTarget != _boss02Event.RightMirror[1].position)
                    _stateMachine2.ChangeState(_stateMachine2.BoomerangAttack);
                else
                    _stateMachine2.ChangeState(_stateMachine2.RushAttack);
                return;
            }
        }
        for (int i = 0; i < _boss02Event.RightMirror.Length; i++)
        {
            if (_stateMachine2.MoveTarget == _boss02Event.RightMirror[i].position)
            {
                _stateMachine2.MoveTarget = _boss02Event.LeftMirror[2-i].position;
                if(num == 0&& _stateMachine2.MoveTarget != _boss02Event.LeftMirror[1].position)
                    _stateMachine2.ChangeState(_stateMachine2.BoomerangAttack);
                else
                    _stateMachine2.ChangeState(_stateMachine2.RushAttack);
                return;
            }
        }

    }
}
