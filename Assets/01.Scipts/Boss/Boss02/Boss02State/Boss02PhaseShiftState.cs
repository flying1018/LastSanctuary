using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02PhaseShiftState : BossBaseState
{
    private float _twinklingTime = 0.2f;
    private int _count;
    private bool _check;
    private float _shakeTime = 3f;
    
    public Boss02PhaseShiftState(Boss02StateMachine bossStateMachine) : base(bossStateMachine) { }
    
    public override void Enter()
    {
        //애니메이션 시작
        _boss2.Animator.SetTrigger(_boss2.AnimationDB.GroggyEnterParameter);
        
        _boss02Event.StartZoomCamera();
        
        //데이터 초기화 
        _time = 0;
        _count = 0;
        _check = false;
        
        //페이즈 변경
        _boss2.Phase2 = true;
    }

    public override void Exit()
    {
        //애니메이션 종료
        _boss2.Animator.SetTrigger(_boss2.AnimationDB.GroggyExitParameter);
    }

    public override void Update()
    {
        //시간이 끝나면
        _time += Time.deltaTime;
        if (_time % _twinklingTime < Time.deltaTime && !_check)
        {
            if (_count % 2 == 0)
            {
                _spriteRenderer.material = _data.materials[0];
            }
            else
            {
                _spriteRenderer.material = _data.materials[1];
            }
            _count++;
        }
        
        if(_time > _data.phaseShiftDuration && !_check)
        {
            _check = true;
            
            //외곽선 변경
            _spriteRenderer.material = _data.materials[1];
            
            PlaySFX1();
            //줌 종료
            _boss02Event.EndZoomCamera();
            _boss02Event.CameraShake(_data.howlingSound.length);
            _boss02Event.BrokenMirror();
        }

        if (_time > _data.phaseShiftDuration + _data.howlingSound.length)
        {
            SetMovePosition();
            _boss2.StartCoroutine(ChangeMirrorSprite_Coroutine());
            _stateMachine2.ChangeState(_stateMachine2.TeleportState);
        }
    }

    public override void PlaySFX1()
    {
        SoundManager.Instance.PlaySFX(_data.howlingSound);
    }

    IEnumerator ChangeMirrorSprite_Coroutine()
    {
        yield return new WaitForSeconds(_data.TeleportTime);
        _boss02Event.ChangeMirrorSprite();
    }
}
