using System.Collections;
using Cinemachine;
using UnityEngine;


/// <summary>
/// 보스 스폰과 이벤트를 담당하는 스크립트
/// </summary>
public class BossEvent : MonoBehaviour
{
    //필드
    protected MoveObject[] _moveObjects;
    protected CinemachineVirtualCamera _bossCamera;
    protected CinemachineBasicMultiChannelPerlin _perlin;
    protected CinemachineBrain _brain;
    protected Player _player;
    protected SpriteRenderer _backGroundSprite;
    
    
    //초기 설정
    protected virtual void Start()
    {
        _moveObjects = GetComponentsInChildren<MoveObject>();
        
        _backGroundSprite = GameObject.FindGameObjectWithTag(StringNameSpace.Tags.BackGround)
            .GetComponent<SpriteRenderer>();
        
        _bossCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        _brain = Camera.main.GetComponent<CinemachineBrain>();
        _perlin = _bossCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    //카메라 흔들림
    public void CameraShake(float duration = 1f, float amplitude = 10f,  float frequency = 5f)
    {
        StartCoroutine(CameraShake_Coroutine(duration, amplitude, frequency));
    }

    IEnumerator CameraShake_Coroutine(float duration, float amplitude ,  float frequency)
    {
        _perlin.m_AmplitudeGain = amplitude;
        _perlin.m_FrequencyGain = frequency;
        yield return new WaitForSeconds(duration);
        _perlin.m_AmplitudeGain = 0;
        _perlin.m_FrequencyGain = 0;
    }

    //UI 정상화와 플레이어 조작 가능
    public void StartBattle()
    {
        _player.EventProduction(false);
        _bossCamera.Priority = 0;
    }

    public virtual void OnTriggerBossPhaseShift() {}
    
    public virtual void OnTriggerBossDeath() { }
    


}
