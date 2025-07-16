using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{
    public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_player.AnimationDB.MoveParameterHash);
        
        //효과음 설정
        SoundClip[0] = _data.moveSound;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_player.AnimationDB.MoveParameterHash);
        
        //나갈 때 이동 정지
        Move(Vector2.zero);
    }

    public override void HandleInput()
    {
        base.HandleInput();
        
        //좌우 키 입력 해제 시
        if (_input.MoveInput.x == 0f)
        {   //대기
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }
}
