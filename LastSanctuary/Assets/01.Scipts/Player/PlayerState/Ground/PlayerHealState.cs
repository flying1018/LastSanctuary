using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealState : PlayerGroundState
{ 
    private float _healTimer;
    
    public PlayerHealState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        StartAnimation(_player.AnimationDB.HealParameterHash);

        _healTimer = _data.healTime;
        _inventory.UsePotion();
        
        //사운드 추가
        SoundClip[0] = _data.healSound;
    }

    public override void Exit()
    {
        base.Exit();
        
        //체력 회복
        _condition.Heal(); 
        StopAnimation(_player.AnimationDB.HealParameterHash);
    }

    public override void HandleInput()
    {
        
    }

    public override void Update()
    {
        base.Update();
        
        _healTimer -= Time.deltaTime;

        if (_healTimer <= 0f) //만약 힐 타임이 0보다 작으면
        {
            CompleteHeal(); //실행
        }
    }

    public override void PhysicsUpdate()
    {

    }
    
    private void CompleteHeal()
    {
        //체력+힐값 적기.
        _stateMachine.ChangeState(_stateMachine.IdleState); //힐이 끝나면 아이들 스테이트로
    }
}
