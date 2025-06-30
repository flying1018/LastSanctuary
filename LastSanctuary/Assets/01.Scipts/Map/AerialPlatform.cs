using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AerialPlatform : MonoBehaviour
{
    [SerializeField] private float recoveryTime;
    
    private PlatformEffector2D _platformEffector2D;
    
    private void Awake()
    {
        _platformEffector2D = GetComponent<PlatformEffector2D>();
    }

    public void DownJump()
    {
        StartCoroutine(DownJump_Coroutine());
    }
    
    IEnumerator DownJump_Coroutine()
    {
        _platformEffector2D.rotationalOffset = 180f;
        yield return new WaitForSeconds(recoveryTime);
        _platformEffector2D.rotationalOffset = 0f;
    }


}
