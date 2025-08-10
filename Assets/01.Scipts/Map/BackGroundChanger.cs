using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class BackGroundChanger : MonoBehaviour
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private Vector2 size;
    [SerializeField] private BGM bgm;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.OriginBackGround = sprite;
            player.BackGround.sprite = sprite;
            player.BackGround.transform.localScale = size;
            SoundManager.Instance.PlayBGM(bgm);
        }
    }
}
