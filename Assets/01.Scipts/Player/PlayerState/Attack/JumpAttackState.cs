using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttackState : PlayerAttackState
{
    public JumpAttackState(PlayerStateMachine stateMachine, AttackInfo attackInfo) : base(stateMachine, attackInfo)
    {
    }
    
    

    public override void PhysicsUpdate()
    {
        //점프 유지
        if(_move.addForceCoroutine != null) return;
        
        //끝나면 떨어지기 시작
        _move.gravityScale += _move.Vertical(Vector2.down, _data.gravityPower);
        _move.Move( _move.gravityScale);
    }
}
