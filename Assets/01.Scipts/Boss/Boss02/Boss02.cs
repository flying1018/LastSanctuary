using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02 : Boss
{
    public Boss02StateMachine StateMachine2 { get; private set; }
    public Boss02Event Boss02Event { get; private set; }
    public Boss02Condition Condition2 { get; private set; }
    
    
    public void Init(Boss02Event bossEvent)
    {
        Init();
        
        Boss02Event = bossEvent;
        if (Condition is Boss02Condition condition)
        { 
            Condition2 = condition;
            Condition2.Init(this);
        }
            
        StateMachine2 = new Boss02StateMachine(this);
    }
    
    protected override void Update()
    {
        StateMachine2.HandleInput();
        StateMachine2.Update();
    }

    protected override void FixedUpdate()
    {
        StateMachine2.PhysicsUpdate();
        //Debug.Log(StateMachine2.currentState);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (Condition2.DontCollision) return;
        
        base.OnTriggerEnter2D(other);
    }
    
    
    #region AnimationEvent Method
    
    //애니메이션 이벤트
    public override void AnimationEvent1()
    {
        if (StateMachine2.currentState is BossBaseState bossBaseState)
        {
            bossBaseState.PlayEvent1();
        }
    }
    
    //애니메이션 이벤트
    public override void AnimationEvent2()
    {
        if (StateMachine2.currentState is BossBaseState bossBaseState)
        {
            bossBaseState.PlayEvent2();
        }
    }
    
    
    //효과음 실행르 위한 메서드
    //사운드 실행 애니메이션 이벤트
    public override void EventSFX1()
    {
        if (StateMachine2.currentState is BossBaseState bossBaseState)
        {
            bossBaseState.PlaySFX1();
        }
    }

    public override void EventSFX2()
    {
        if (StateMachine2.currentState is BossBaseState bossBaseState)
        {
            bossBaseState.PlaySFX2();
        }
    }
    
    public override void EventSFX3()
    {
        if (StateMachine2.currentState is BossBaseState bossBaseState)
        {
            bossBaseState.PlaySFX3();
        }
    }

    #endregion
}
