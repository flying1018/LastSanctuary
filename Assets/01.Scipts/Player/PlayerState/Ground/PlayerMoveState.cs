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
        
        SoundClip[0] = _data.moveSound;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_player.AnimationDB.MoveParameterHash);
        
        Move(Vector2.zero);
    }


    public override void Update()
    {
        base.Update();

        if (_input.MoveInput.x == 0f)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }
}
