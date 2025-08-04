using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUIAnim : MonoBehaviour
{
    [SerializeField] private Sprite[] animSprites;
    [SerializeField] private float animInterval;
    [SerializeField] private int index;
    
    private SpriteRenderer _spriteRenderer;
    private float _time;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init()
    {
        if (_spriteRenderer == null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        _time = 0;
        _spriteRenderer.sprite = animSprites[index];
        
    }

    //애니메이션 실행
    private void FixedUpdate()
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
