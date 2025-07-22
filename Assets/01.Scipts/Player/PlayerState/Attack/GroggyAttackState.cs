using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroggyAttackState : PlayerAttackState
{
    public GroggyAttackState(PlayerStateMachine stateMachine, AttackInfo attackInfo) : base(stateMachine, attackInfo) { }

    public override void PlayEvent1()
    {
        base.PlayEvent1();

        _player.Animator.speed = 0;
        _player.SpriteRenderer.color = new Color(1, 1, 1, 0);
    }
}
