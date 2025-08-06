using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02Event : BossEvent
{
    private Boss02 _boss;

    [SerializeField] private Transform[] leftMirror;
    [SerializeField] private Transform[] rightMirror;
    [SerializeField] private Transform topMirror;

    public Transform[] LeftMirror => leftMirror;
    public Transform[] RightMirror => rightMirror;
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
        int num = Random.Range(0, 7);
        Vector2 result = Vector2.zero;
        switch (num)
        {
            case 0:
                result = topMirror.position;
                break;
            case 1:
                result = leftMirror[0].position;
                break;
            case 2:
                result = leftMirror[1].position;
                break;
            case 3:
                result = leftMirror[2].position;
                break;
            case 4:
                result = rightMirror[0].position;
                break;
            case 5:
                result = rightMirror[1].position;
                break;
            case 6:
                result = rightMirror[2].position;
                break;
        }

        return result;
    }
}