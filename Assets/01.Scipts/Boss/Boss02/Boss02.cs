using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02 : Boss
{
    public new Boss02StateMachine StateMachine { get; private set; }
    public new Boss02Event BossEvent { get; private set; }
    
    public override void Init(BossEvent bossEvent)
    {
        if (bossEvent is Boss02Event boss02Event)
        { 
            BossEvent = boss02Event;
        }
        //무기 데이터 설정
        WeaponInfo = new WeaponInfo();
        WeaponInfo.Condition = Condition;
        WeaponInfo.Attack = Data.attack;
        WeaponInfo.Defpen = Data.defpen;
        WeaponInfo.KnockBackForce = Data.knockbackForce;
        WeaponInfo.DamageType = DamageType.Attack;
        
        Move.Init(BoxCollider.bounds.size.x, BoxCollider.bounds.size.y, Rigidbody);
        AnimationDB.Initailize(); 
        Condition.Init(this);
        Phase2 = false;
            
        StateMachine = new Boss02StateMachine(this);
    }
    
    protected override void Update()
    {
        StateMachine.HandleInput();
        StateMachine.Update();
    }

    protected override void FixedUpdate()
    {
        StateMachine.PhysicsUpdate();
    }
    
    #region AnimationEvent Method
    
    //애니메이션 이벤트
    public override void AnimationEvent1()
    {
        if (StateMachine.currentState is BossBaseState bossBaseState)
        {
            bossBaseState.PlayEvent1();
        }
    }
    
    //애니메이션 이벤트
    public override void AnimationEvent2()
    {
        if (StateMachine.currentState is BossBaseState bossBaseState)
        {
            bossBaseState.PlayEvent2();
        }
    }
    
    
    //효과음 실행르 위한 메서드
    //사운드 실행 애니메이션 이벤트
    public override void EventSFX1()
    {
        if (StateMachine.currentState is BossBaseState bossBaseState)
        {
            bossBaseState.PlaySFX1();
        }
    }

    public override void EventSFX2()
    {
        if (StateMachine.currentState is BossBaseState bossBaseState)
        {
            bossBaseState.PlaySFX2();
        }
    }
    
    public override void EventSFX3()
    {
        if (StateMachine.currentState is BossBaseState bossBaseState)
        {
            bossBaseState.PlaySFX3();
        }
    }

    #endregion
}
