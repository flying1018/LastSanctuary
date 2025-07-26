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
            _interactAnim = _player.StartCoroutine(Interact_Coroutine(savePoint));
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
        
        if(_interactAnim != null) return;
        
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
    
    private Coroutine _interactAnim;
    IEnumerator Interact_Coroutine(SavePoint savePoint)
    {
        //카메라 정지
        _camera.StopCamera = true;
        
        //가장 가까운 세이브 위치 방향 구하기
        Vector2 direction = (savePoint.NearPosition().position - _player.transform.position).normalized;
        direction.y = 0;

        //세이브 포인트 위치로 이동
        StartAnimation(_player.AnimationDB.MoveParameterHash);
        while (Mathf.Abs(savePoint.NearPosition().position.x - _player.transform.position.x) > 0.1f)
        {
            _move.Move(direction * _data.moveSpeed);
            Rotate(direction);
            yield return _move.WaitFixedUpdate;
        }
        StopAnimation(_player.AnimationDB.MoveParameterHash);

        //밧줄 위치를 쳐다보기
        direction = (savePoint.transform.position - _player.transform.position).normalized;
        Rotate(direction);
        
        //상호작용 애니메이션
        StartAnimation(_player.AnimationDB.InteractionParameter);

        //상호작용 상태 탈출 시간 설정
        _interactTime = _data.InteractionTime;
        
        //상호작용 실행
        savePoint.Interact();

        //초기화
        _interactAnim = null;
        _camera.StopCamera = false;
    }
}
