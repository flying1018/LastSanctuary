using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 공중에서 떨어지고 있는 상태
/// </summary>
public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_player.AnimationDB.FallParameterHash);;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_player.AnimationDB.FallParameterHash);;
    }

    

}
