using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02Event : BossEvent
{
    private Boss02 _boss;
    
    [Header("BossSpawn")]
    [SerializeField] private float blackDuration = 1f;
    [SerializeField] private float cameraZoom = 4f;
    [SerializeField] private Vector3 backGroundSize = new Vector3(3, 3, 0);

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

            SoundManager.Instance.PlayBGM(StringNameSpace.SoundAddress.TutorialBGM);
            UIManager.Instance.SetBossUI(false);
        }
    }
    
         //스폰 이벤트
    IEnumerator Spawn_Coroutine()
    {
        // _bossCamera.Priority = 20;
        // _backGroundSprite.gameObject.transform.localScale = new Vector3(3, 3, 0);
        
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
            yield return null;
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
    public void EndZoomCamera()
    {
        _player.Camera.EndZoomCamera();
        _bossCamera.Priority = 30;
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