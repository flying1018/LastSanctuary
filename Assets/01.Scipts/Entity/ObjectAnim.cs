using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectAnim : MonoBehaviour
{
    [SerializeField] protected Sprite[] animSprites;
    [SerializeField] protected float animInterval;
    
    [SerializeField] protected int index = 0;
    
    protected SpriteRenderer _spriteRenderer;
    protected float _time;
    
    protected void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void Init()
    {
        _time = 0;
        _spriteRenderer.sprite = animSprites[index];
    }
    
    //애니메이션 실행
    protected virtual void FixedUpdate()
    {
        _time += Time.fixedDeltaTime;
        if (_time >= animInterval)
        {
            _time = 0;
            index++;

            if (index >= animSprites.Length)
            {
                index = 0;
                return;
            }
            _spriteRenderer.sprite = animSprites[index];
        }
    }
}
