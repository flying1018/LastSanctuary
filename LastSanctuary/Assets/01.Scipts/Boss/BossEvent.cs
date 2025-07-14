using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class BossEvent : MonoBehaviour
{
    private MoveObject[] _moveObjects;
    private Boss _boss;
    private Player _player;
    private CinemachineVirtualCamera _bossCamera;
    private SpriteRenderer _backGroundSprite;
    private CinemachineBasicMultiChannelPerlin _perlin;
    private CinemachineBrain _brain;
    private CinemachineBlendDefinition _originBlend;

    [Header("Boss Spawn")] 
    [SerializeField] private float enterTime = 1f;
    [SerializeField] private float blackDuration = 1f;
    [SerializeField] private AudioClip howlingSound;
    [SerializeField] private float blinkInterval;
    [SerializeField] private GameObject[] parts;
    [SerializeField] private float cameraShakeTime;
    [SerializeField] private float targetLesSise = 7f;
    
    [Header("Boss Death")]
    [SerializeField] private float sloweventDuration = 2f;
    [SerializeField] private float cameraZoom = 5f;

    private void Start()
    {
        _moveObjects = GetComponentsInChildren<MoveObject>();
        _boss = FindAnyObjectByType<Boss>();
        _boss.gameObject.SetActive(false);
        _bossCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        _perlin = _bossCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _backGroundSprite = GameObject.FindGameObjectWithTag(StringNameSpace.Tags.BackGround)
            .GetComponent<SpriteRenderer>();
        _brain = Camera.main.GetComponent<CinemachineBrain>();
        _originBlend = _brain.m_DefaultBlend; //카메라 기본 설정

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(StringNameSpace.Tags.Player))
        {
            _player = other.GetComponent<Player>();
            StartCoroutine(Spawn_Coroutine());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(StringNameSpace.Tags.Player))
        {
            _player = null;
            _boss.gameObject.SetActive(false);
            //벽 치우기
            foreach (MoveObject moveObject in _moveObjects)
            {
                moveObject.MoveObj();
            }
        }
    }

    IEnumerator Spawn_Coroutine()
    {
        //플레이어가 입장 시 잠시 이동
        yield return new WaitForSeconds(enterTime);

        //벽이 올라와서 막힘
        foreach (MoveObject moveObject in _moveObjects)
        {
            moveObject.MoveObj();
        }

        //UI 끄기
        UIManager.Instance.OnOffUI(false);
        //조작 불가
        _player.PlayerInput.enabled = false;

        //천천히 암전
        Color originColor = _backGroundSprite.color;
        float elapsed = 0f;
        while (elapsed <= blackDuration)
        {
            elapsed += Time.deltaTime;
            _backGroundSprite.color = Color.Lerp(originColor, Color.black, elapsed / blackDuration);
            yield return null;
        }

        //카메라 변경
        _bossCamera.Priority = 20;
        yield return new WaitForSeconds(blackDuration);

        //빨간색 순차적으로 점멸
        foreach (GameObject part in parts)
        {
            part.SetActive(true);
            yield return new WaitForSeconds(blinkInterval);
        }

        foreach (GameObject part in parts)
        {
            part.SetActive(false);
        }
        
        //카메라 줌 아웃
        while (_bossCamera.m_Lens.OrthographicSize < targetLesSise)
        {
            _bossCamera.m_Lens.OrthographicSize += Time.deltaTime * 10;
            yield return null;
        }

        //색을 돌리기
        elapsed = 0f;
        while (elapsed <= blackDuration)
        {
            elapsed += Time.deltaTime;
            _backGroundSprite.color = Color.Lerp(Color.black, originColor, elapsed / blackDuration);
            yield return null;
        }

        //보스 스폰 상태
        _boss.gameObject.SetActive(true);
        _boss.Init(this);
    }

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

    public void StartBattle()
    {
        UIManager.Instance.OnOffUI(true);
        _player.PlayerInput.enabled = true;
        _bossCamera.Priority = 0;
    }

    public void OnTriggerBossDeath()
    {
        StartCoroutine(Death_coroution());
    }

    IEnumerator  Death_coroution()
    {
        //보스 포커싱
        _brain.m_DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.Cut, 0f);
        _bossCamera.transform.position = new Vector3(_boss.transform.position.x, _boss.transform.position.y, _bossCamera.transform.position.z);
        _bossCamera.m_Lens.OrthographicSize = cameraZoom;
        _bossCamera.Priority = 20;
        
        //슬로우 모션
        Time.timeScale = 0.2f;
        float timer = 0f;
        
        //UI 끄기
        UIManager.Instance.OnOffUI(false);
        //조작 불가
        _player.PlayerInput.enabled = false;
        
        //순간 암전
        Color originbackGroundColor = _backGroundSprite.color;
        _backGroundSprite.color = Color.black;
        
        //빨간 실루엣
        SpriteRenderer bossSprite = _boss.SpriteRenderer;
        SpriteRenderer playerSprite = _player.SpriteRenderer;
        Color originBossColor = bossSprite.color;
        Color originPlayerColor = playerSprite.color;
        bossSprite.color = Color.red;
        playerSprite.color = Color.red;
        
        //연출 유지
        while (timer < sloweventDuration)
        {
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        //색 돌리기
        Time.timeScale = 1f;
        _backGroundSprite.color = originbackGroundColor;
        bossSprite.color = originBossColor;
        playerSprite.color = originPlayerColor;

        _boss.Animator.speed = 0f;
        yield return new WaitForSeconds(1f);
        CameraShake();
        while (timer < _boss.Data.deathEventDuration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        _brain.m_DefaultBlend = _originBlend;
        _boss.Animator.speed = 1f;
        yield return new WaitForSeconds(2f);
        StartBattle();
        
    }
}
