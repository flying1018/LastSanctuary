using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangProjectile : ProjectileWeapon
{
    [SerializeField] private float height;
    [SerializeField] private float width;

    private Vector2 _dir;
    private float _arrowPower;
    private float _time;

    private void Awake()
    {
        Init(40, 5, PoolingIndex.Boss02Projectile1);
        Shot(new Vector2(1,1), 5);
    }

    public override void Shot(Vector2 dir, float arrowPower)
    {
        _dir = new Vector2(dir.x*width, dir.y*height);
        _arrowPower = arrowPower;
        _time = 0;
    }

    private void FixedUpdate()
    {
        _time += Time.fixedDeltaTime;

        _dir.x = width - width * _time;
        
        _rigidbody2D.MovePosition(_rigidbody2D.position + _arrowPower*Time.fixedDeltaTime*_dir);
    }
}
