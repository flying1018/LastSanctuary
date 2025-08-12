using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealState : PlayerGroundState
{ 
    public PlayerHealState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        StartAnimation(_player.AnimationDB.HealParameterHash);

        _time = 0;
    }

    public override void Exit()
    {
        base.Exit();
        
        StopAnimation(_player.AnimationDB.HealParameterHash);
    }

    public override void HandleInput()
    {
        
    }

    public override void Update()
    {
        base.Update();
        
        //힐 시간이 끝나면
        _time += Time.deltaTime;
        if (_time > _data.HealTime) 
        {   //대기
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {

    }

    public override void PlayEvent1()
    {
        //체력 회복
        _inventory.UsePotion();
    }
    
    public override void PlaySFX1()
    {
        SoundManager.Instance.PlaySFX(_data.healSound);
    }
}
