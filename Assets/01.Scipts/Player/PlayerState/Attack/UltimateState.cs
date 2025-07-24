using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateState : PlayerBaseState
{
    private float _animationTime;
    public UltimateState(PlayerStateMachine stateMachine, AttackInfo attackInfo) : base(stateMachine)
    {
        _animationTime = attackInfo.animTime * 5f;
    }

    public override void Enter()
    {
        base.Enter();
        _condition.CurUltimate = 0f;
        _condition.InvincibleFunc(_animationTime);


    }

    public override void Exit()
    {

    }


    public override void HandleInput()
    {

    }

    public override void Update()
    {

    }

    public override void PhysicsUpdate()
    {

    }

    IEnumerator UltimateSkill_Coroutine()
    {
        yield return null;
    }
}
