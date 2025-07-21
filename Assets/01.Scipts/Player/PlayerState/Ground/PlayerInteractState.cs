using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerInteractState : PlayerGroundState
{
    private float _interactTime;
    
    public PlayerInteractState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        
        _input.IsInteract = false;
        _time = 0f;

        if (_player.InteractableTarget is SavePoint)
        {
            StartAnimation(_player.AnimationDB.InteractionParameter);

            _interactTime = _data.InteractionTime;
        }
        else
        {
            _interactTime = 0;
        }
        
        
        _player.InteractableTarget.Interact();
        
    }

    public override void Exit()
    {
        base.Exit();
        
        StopAnimation(_player.AnimationDB.InteractionParameter);
    }

    public override void Update()
    {
        base.Update();
        
        _time += Time.deltaTime;
        if (_interactTime < _time)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }

    public override void HandleInput()
    {
        
    }
}
