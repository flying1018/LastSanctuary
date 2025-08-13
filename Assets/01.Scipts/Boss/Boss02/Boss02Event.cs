using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02Event : BossEvent
{
    private Boss02 _boss;
    private SpriteRenderer[] _mirrorSpriteRenderers;

    
    [Header("BossSpawn")]
    [SerializeField] private Sprite originTopMirrorSprite;
    [SerializeField] private Sprite originMirrorSprite;
    [SerializeField] private float blackDuration = 1f;
    [SerializeField] private float cameraZoom = 4f;
    [SerializeField] private Vector3 backGroundSize = new Vector3(3, 3, 0);

    [Header("PhaseShift")] 
    [SerializeField] private Sprite[] brokenMirror;
    [SerializeField] private float brokenTime;
    [SerializeField] private Sprite phaseShiftSprite;
    
    [Header("Boss Death")]
    [SerializeField] private float sloweventDuration = 2f;
    [SerializeField] private Material redSilhouette;
    [SerializeField] private float shakeDuration;

    [Header("Battle")]
    [SerializeField] private Transform leftTopMirror;
    [SerializeField] private Transform leftBottomMirror;
    [SerializeField] private Transform rightTopMirror;
    [SerializeField] private Transform rightBottomMirror;
    [SerializeField] private Transform topMirror;
    [SerializeField] private float teleportRange = 10f;
    [SerializeField] private float projectileYMargin = 2f;

    public Transform LeftTopMirror => leftTopMirror;
    public Transform LeftBottomMirror => leftBottomMirror;
    public Transform RightTopMirror => rightTopMirror;
    public Transform RightBottomMirror => rightBottomMirror;
    public Transform TopMirror => topMirror;
    


    protected override void Start()
    {
        base.Start();
        
        _boss = FindAnyObjectByType<Boss02>();
        _boss.gameObject.SetActive(false);
        
        _mirrorSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    //플레이어 입장 시
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (MapManager.IsBossAlive == false) return;
        if (other.CompareTag(StringNameSpace.Tags.Player))
        {
            _player = other.GetComponent<Player>();
            StartCoroutine(Spawn_Coroutine());
        }
    }

    //플레이어 퇴장 시
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

            SoundManager.Instance.PlayBGM(BGM.FirstSancCenter);
            UIManager.Instance.SetBossUI(false);
            _bossCamera.Priority = 0;
        }
    }
    
    //스폰 이벤트
    IEnumerator Spawn_Coroutine()
    {
        SetMirror(true);
        
        //거울 정상화
        for (int i = 0; i < 5; i++)
        {
            if(i==0)
                _mirrorSpriteRenderers[i].sprite = originTopMirrorSprite;
            else
                _mirrorSpriteRenderers[i].sprite = originMirrorSprite;
        }
        
        //플레이어 이벤트 상태(조작 불가 + 업데이트, 물리 업데이트 막기)
        _player.EventProduction(true);

        _player.StateMachine.ChangeState(_player.StateMachine.FallState);
        //공중이라면 착지 시키기
        Vector2 gravityScale = Vector2.zero;
        gravityScale += _player.Move.Vertical(Vector2.down, _player.Data.gravityPower);
        while (!_player.Move.IsGrounded)
        {
            _player.Move.Move(gravityScale);
            yield return null;
        }
        
        //방향 계산
        Vector2 dir = _player.Move.Horizontal(playerPosition.position - _player.transform.position,_player.Data.moveSpeed);
        //거리 계산
        float distance = Mathf.Abs(_player.transform.position.x - playerPosition.position.x);
        //플레이어 걷기 상태
        _player.StateMachine.ChangeState(_player.StateMachine.MoveState);
        //실질적인 이동 처리
        while (distance > 0.1f)
        {
            distance = Mathf.Abs(_player.transform.position.x - playerPosition.position.x);
            _player.Move.Move(dir);
            yield return  _player.Move.WaitFixedUpdate;
        }
        //대기 상태
        _player.StateMachine.ChangeState(_player.StateMachine.IdleState);

        //벽이 올라와서 막힘
        foreach (MoveObject moveObject in _moveObjects)
        {
            moveObject.MoveObj();
        }
        
        //천천히 암전
        Color originColor = _backGroundSprite.color;
        float elapsed = 0f;
        while (elapsed <= blackDuration)
        {
            elapsed += Time.deltaTime;
            _backGroundSprite.color = Color.Lerp(originColor, Color.black, elapsed / blackDuration);
            yield return null;
        }
        
        
        _boss.Init(this);
        _player.Camera.StartZoomCamera(_boss.transform,cameraZoom);
    }

    public void SetMirror(bool isOn)
    {
        leftTopMirror.gameObject.SetActive(isOn);
        leftBottomMirror.gameObject.SetActive(isOn);
        rightTopMirror.gameObject.SetActive(isOn);
        rightBottomMirror.gameObject.SetActive(isOn);
    }

    public override void StartBattle()
    {
        _player.EventProduction(false);
        StartCoroutine(ReturnBackGround_Coroutine());
    }

    IEnumerator ReturnBackGround_Coroutine()
    {
        _backGroundSprite.gameObject.transform.localScale = backGroundSize;
        
        //색을 돌리기
        float elapsed = 0f;
        while (elapsed <= blackDuration)
        {
            elapsed += Time.deltaTime;
            _backGroundSprite.color = Color.Lerp(Color.black, Color.white, elapsed / blackDuration);
            yield return null;
        }
    }
    public void StartZoomCamera()
    {
        _bossCamera.Priority = 0;
        _player.Camera.StartZoomCamera(_boss.transform,cameraZoom);
    }
    public void EndZoomCamera()
    {
        _player.Camera.EndZoomCamera();
        _bossCamera.Priority = 30;
    }

    public void StartCutScene()
    {
        _player.EventProduction(true);
    }
    
    public void EndCutScene()
    {
        _player.EventProduction(false);
    }
    
    
    

    private Coroutine _phaseShiftCoroutine;
    
    public void BrokenMirror()
    {
        if (_phaseShiftCoroutine != null)
        {
            StopCoroutine(_phaseShiftCoroutine);
            _phaseShiftCoroutine = null;
        }
        _phaseShiftCoroutine = StartCoroutine(BrokenMirror_Coroutine());
    }

    IEnumerator BrokenMirror_Coroutine()
    {
        SoundManager.Instance.PlaySFX(_boss.Data.howlingSound);
        SoundManager.Instance.PlaySFX(_boss.Data.phaseShiftSound);
        
        foreach (Sprite sprite in  brokenMirror)
        {
            yield return new WaitForSeconds(brokenTime);
            for(int i=1;i<5;i++)
            {
                _mirrorSpriteRenderers[i].sprite = sprite;
            }
        }
        
        SetMirror(false);
    }

    public void ChangeMirrorSprite()
    {
        _mirrorSpriteRenderers[0].sprite = phaseShiftSprite;
    }
    
      //보스 사망 연출
    public override void OnTriggerBossDeath()
    {
        StartCoroutine(Death_coroution());
    }
    IEnumerator  Death_coroution()
    {
        StartCutScene();
        
        //보스 포커싱
        StartZoomCamera();
        
        //슬로우 모션
        Time.timeScale = 0.2f;
        float timer = 0f;
        
        //UI 끄기
        _player.EventProduction(true);
        
        //순간 암전
        Color originbackGroundColor = _backGroundSprite.color;
        _backGroundSprite.color = Color.black;
        
        //빨간 실루엣
         SpriteRenderer bossSprite = _boss.SpriteRenderer;
         SpriteRenderer playerSprite = _player.SpriteRenderer;
         
         Material originBossMaterial = bossSprite.material;
         Material originplayerMaterial = playerSprite.material;
         
         bossSprite.material = redSilhouette;
         playerSprite.material = redSilhouette;
        
        //연출 유지
        yield return new WaitForSecondsRealtime(sloweventDuration);

        //색 돌리기
        Time.timeScale = 1f;
        _backGroundSprite.color = originbackGroundColor;
        bossSprite.material = originBossMaterial;
        playerSprite.material = originplayerMaterial;

        //보스 정지
        _boss.Animator.speed = 0f;
        yield return new WaitForSeconds(1f);
        
        //카메라 흔들림
        CameraShake(shakeDuration);
        yield return new WaitForSeconds(shakeDuration);
        
        //죽는 이벤트 시간만큼 대기
        yield return new WaitForSeconds(_boss.Data.deathEventDuration);

        //설정 복구
        _boss.Animator.speed = 1f;
        yield return new WaitForSeconds(2f);
        StartBattle();
        EndZoomCamera();
        _bossCamera.Priority = 0;
        
        
        //문 열기
        foreach (MoveObject moveObject in _moveObjects)
        {
            moveObject.MoveObj();
        }
        
        //브금 복구
        SoundManager.Instance.PlayBGM(BGM.FirstSancCenter);

        EndCutScene();
    }
    

    public Vector2 GetRandomMirror()
    {
        int num = Random.Range(0, 5);
        Vector2 result = Vector2.zero;
        switch (num)
        {
            case 0:
                result = topMirror.position;
                break;
            case 1:
                result = leftTopMirror.position;
                break;
            case 2:
                result = leftBottomMirror.position;
                break;
            case 3:
                result = rightTopMirror.position;
                break;
            case 4:
                result = rightBottomMirror.position;
                break;
        }
        return result;
    }

    public Vector2 GetRandomTopPosition()
    {
        float margin = Random.Range(-teleportRange, teleportRange);
        return new Vector2(topMirror.position.x + margin, topMirror.position.y);
    }
    
    public Vector2 GetRandomProjectilePosition()
    {
        float xMargin = Random.Range(-teleportRange, teleportRange);
        float yMargin = Random.Range(-projectileYMargin, projectileYMargin);
        return new Vector2(topMirror.position.x + xMargin, topMirror.position.y + projectileYMargin + yMargin);
    }
    
}