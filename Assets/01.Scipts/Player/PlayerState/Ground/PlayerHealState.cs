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
        
        Debug.Log("heal enter");
    }

    public override void Exit()
    {
        base.Exit();
        
        //체력 회복
        _inventory.UsePotion();
        _condition.Heal(); 
        
        StopAnimation(_player.AnimationDB.HealParameterHash);
        Debug.Log("heal exit");
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
    
    public override void PlaySFX1()
    {
        SoundManager.Instance.PlaySFX(_data.healSound);
    }
}
