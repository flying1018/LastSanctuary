using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AerialPlatform : MonoBehaviour
{
    private PlatformEffector2D _platformEffector2D;
    private void Awake()
    {
        _platformEffector2D = GetComponent<PlatformEffector2D>();
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        UpJump();
    }

    public void DownJump()
    {
        _platformEffector2D.rotationalOffset = 180f;
    }

    public void UpJump()
    {
        _platformEffector2D.rotationalOffset = 0f;
    }
}
