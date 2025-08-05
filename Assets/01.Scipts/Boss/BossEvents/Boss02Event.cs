using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02Event : BossEvent
{
    [SerializeField] private Transform[] leftMirror;
    [SerializeField] private Transform[] rightMirror;
    [SerializeField] private Transform topMirror;
    
    //플레이어 입장 시
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(MapManager.IsBossAlive == false) return;
        if (other.CompareTag(StringNameSpace.Tags.Player))
        {
            _player = other.GetComponent<Player>();
            _boss.gameObject.SetActive(true);
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


}
