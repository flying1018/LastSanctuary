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

        if (_player.InteractableTarget is SavePoint savePoint)
        {
            InteractAnim = _player.StartCoroutine(Interact_Coroutine(savePoint));
        }
        else
        {
            _interactTime = 0;
            _player.InteractableTarget.Interact();
        }
    }

    public override void Exit()
    {
        base.Exit();
        
        StopAnimation(_player.AnimationDB.InteractionParameter);
    }

    public override void Update()
    {
        base.Update();
        
        if(InteractAnim != null) return;
        
        //애니메이션이 완전히 종료 후
        _time += Time.deltaTime;
        if (_interactTime < _time)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }

    public override void HandleInput()
    {
        
    }
    
    public override void PhysicsUpdate()
    {
        
    }
    
    private Coroutine InteractAnim;
    IEnumerator Interact_Coroutine(SavePoint savePoint)
    {
        Vector2 direction = (savePoint.NearPosition().position - _player.transform.position).normalized;
        direction.y = 0;

        StartAnimation(_player.AnimationDB.MoveParameterHash);
        while (Mathf.Abs(savePoint.NearPosition().position.x - _player.transform.position.x) > 0.1f)
        {
            _move.Move(direction * _data.moveSpeed);
            Rotate(direction);
            yield return null;
        }
        StopAnimation(_player.AnimationDB.MoveParameterHash);

        direction = (savePoint.transform.position - _player.transform.position).normalized;
        Rotate(direction);
        
        StartAnimation(_player.AnimationDB.InteractionParameter);

        _interactTime = _data.InteractionTime;
        
        savePoint.Interact();

        InteractAnim = null;
    }
}
