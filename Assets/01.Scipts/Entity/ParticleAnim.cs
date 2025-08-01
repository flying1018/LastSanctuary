using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAnim : MonoBehaviour
{
    [SerializeField] private Sprite[] animSprites;
    [SerializeField] private float animInterval;
    
    private SpriteRenderer _spriteRenderer;
    private int _index;
    private float _time;
    private PoolingIndex _poolingIndex;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(PoolingIndex poolingIndex)
    {
        _poolingIndex = poolingIndex;
        _index = 0;
    }

    //애니메이션 실행
    private void FixedUpdate()
    {
        _time += Time.fixedDeltaTime;
        if (_time >= animInterval)
        {
            _time = 0;
            _index++;

            if (_index >= animSprites.Length)
            {
                ObjectPoolManager.Set(gameObject, (int)_poolingIndex);
                return;
            }
            _spriteRenderer.sprite = animSprites[_index];
        }
    }
}
