using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 공중 발판
/// </summary>
public class AerialPlatform : MonoBehaviour
{
    [SerializeField] private float downJumpTime;
    [SerializeField] private LayerMask playerLayer;
    
    private PlatformEffector2D _platformEffector2D;
    
    private void Awake()
    {
        _platformEffector2D = GetComponent<PlatformEffector2D>();
    }

    //블럭 밑으로 내려가기
    public void DownJump()
    {
        StartCoroutine(DownJump_Coroutine());
    }
    
    IEnumerator DownJump_Coroutine()
    {
        _platformEffector2D.colliderMask -= playerLayer;
        yield return new WaitForSeconds(downJumpTime);
        _platformEffector2D.colliderMask += playerLayer;
    }
}
