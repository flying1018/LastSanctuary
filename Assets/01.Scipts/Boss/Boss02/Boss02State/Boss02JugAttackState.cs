using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02JugAttackState : BossBaseState
{
    public Boss02JugAttackState(Boss02StateMachine bossStateMachine) : base(bossStateMachine) { }

    public override void Enter()
    {
        base.Enter();

        int jugment = 0;
        if (_boss2.Phase2)
        {
            jugment = Random.Range(0, 3);
            switch (jugment)
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
        

        if (_stateMachine2.MoveTarget == _boss02Event.TopMirror.position)
        {
            _stateMachine2.ChangeState(_stateMachine2.DownAttack);
            return;
        }
        if (_stateMachine2.MoveTarget == _boss02Event.LeftTopMirror.position)
        {
            _stateMachine2.MoveTarget = _boss02Event.RightBottomMirror.position;
        }
        else if (_stateMachine2.MoveTarget == _boss02Event.LeftBottomMirror.position)
        {
            _stateMachine2.MoveTarget = _boss02Event.RightTopMirror.position;
        }
        else if (_stateMachine2.MoveTarget == _boss02Event.RightTopMirror.position)
        {
            _stateMachine2.MoveTarget = _boss02Event.LeftBottomMirror.position;
        }
        else if (_stateMachine2.MoveTarget == _boss02Event.RightBottomMirror.position)
        {
            _stateMachine2.MoveTarget = _boss02Event.LeftTopMirror.position;
        }

        
        jugment = Random.Range(0, 2);
        switch (jugment)
        {
            case 0:
                _stateMachine2.ChangeState(_stateMachine2.RushAttack);
                break;
            case 1:
                _stateMachine2.ChangeState(_stateMachine2.BoomerangAttack);
                break;
        }

    }
}
