using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealState : PlayerGroundState
{ 
    private float _time;
    
    public PlayerHealState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        StartAnimation(_player.AnimationDB.HealParameterHash);

        _time = 0;
        
        //사운드 추가
        SoundClip[0] = _data.healSound;
    }

    public override void Exit()
    {
        base.Exit();
        
        //체력 회복
        _inventory.UsePotion();
        _condition.Heal(); 
        
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
        if (_time <= _data.HealTime) 
        {   //대기
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {

    }
}
