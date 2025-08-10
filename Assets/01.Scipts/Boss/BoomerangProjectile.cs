using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangProjectile : ProjectileWeapon
{
    [SerializeField] private float height;
    [SerializeField] private float width;
    [SerializeField] private AudioClip sound;

    private Vector2 _dir;
    private float _arrowPower;
    private float _time;
    private float _width;
    private float _soundTime;

    public override void Shot(Vector2 dir, float arrowPower)
    {
        _dir = new Vector2(dir.x*width, dir.y*height);
        _width = _dir.x;
        _arrowPower = arrowPower;
        _time = 0;
        _soundTime = sound.length;
    }

    private void FixedUpdate()
    {
        _time += Time.fixedDeltaTime;

        _dir.x = _width - _width * _time;
        
        _rigidbody2D.MovePosition(_rigidbody2D.position + _arrowPower*Time.fixedDeltaTime*_dir);

        _soundTime += Time.deltaTime;
        if (_soundTime >= sound.length)
        {
            _soundTime = 0;
            SoundManager.Instance.PlaySFX(sound);
        }
    }
}
