using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02Event : BossEvent
{
    private Boss02 _boss;

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
            _boss.gameObject.SetActive(true);
            _boss.Init(this);
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