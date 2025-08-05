using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02 : Boss
{
    public override void Init(BossEvent bossEvent)
    {
        base.Init(bossEvent);

        if (bossEvent is Boss02Event boss02Event)
        {
            BossEvent = boss02Event;
        }
            
        StateMachine = new BossStateMachine(this);
    }
}
