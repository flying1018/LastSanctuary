using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAnim : ObjectAnim
{
    [SerializeField] private AudioClip particleSound;
    
    private PoolingIndex _poolingIndex;
    
    public void  Init(PoolingIndex poolingIndex)
    {
        _poolingIndex = poolingIndex;
        index = 0;
        SoundManager.Instance.PlaySFX(particleSound);
    }

    //애니메이션 실행
    protected override void FixedUpdate()
    {
        _time += Time.fixedDeltaTime;
        if (_time >= animInterval)
        {
            _time = 0;
            index++;

            if (index >= animSprites.Length)
            {
                ObjectPoolManager.Set(gameObject, (int)_poolingIndex);
                return;
            }
            _spriteRenderer.sprite = animSprites[index];
        }
    }
}
