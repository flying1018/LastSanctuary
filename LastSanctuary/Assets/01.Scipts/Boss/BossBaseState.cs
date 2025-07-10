using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossBaseState : IState
{
    protected BossStateMachine _stateMachine;
    protected BossSO _data;
    protected Rigidbody2D _rigidbody;
    protected SpriteRenderer _spriteRenderer;
    protected PolygonCollider2D _polygonCollider;
    protected BossCondition _condition;
    protected Boss _boss;

    public BossBaseState(BossStateMachine bossStateMachine)
    {
        this._stateMachine = bossStateMachine;
        _boss = _stateMachine.Boss;
        _data = _boss.Data;
        _rigidbody = _boss.Rigidbody;
        _spriteRenderer =_boss.SpriteRenderer;
        _polygonCollider = _boss.PolygonCollider;
        _condition = _boss.Condition;
    }
    
    public virtual void Enter()
    {
        
    }

    public virtual void Exit()
    {

    }

    public virtual void HandleInput()
    {

    }

    public virtual void Update()
    {
        if(_stateMachine.Attack1.CheckCoolTime())
            _stateMachine.Attacks.Enqueue(_stateMachine.Attack1);
        if(_stateMachine.Attack2.CheckCoolTime())
            _stateMachine.Attacks.Enqueue(_stateMachine.Attack2);
    }

    public virtual void PhysicsUpdate()
    {
        UpdateCollider();
    }
    
    protected void StartAnimation(int animatorHash)
    {
        _boss.Animator.SetBool(animatorHash, true);
    }

    protected void StopAnimation(int animatorHash)
    {
        _boss.Animator.SetBool(animatorHash, false);
    }

    public void UpdateCollider()
    {
        Sprite sprite = _spriteRenderer.sprite;
        _polygonCollider.pathCount = sprite.GetPhysicsShapeCount();

        for (int i = 0; i < _polygonCollider.pathCount; i++)
        {
            List<Vector2> path = new List<Vector2>();
            sprite.GetPhysicsShape(i, path);
            if (_spriteRenderer.flipX)
            {
                for (int j = 0; j < path.Count; j++)
                {
                    Vector2 point = path[j];
                    point.x *= -1;
                    path[j] = point;
                }
            }
            _polygonCollider.SetPath(0, path.ToArray());
        }
    }
}
