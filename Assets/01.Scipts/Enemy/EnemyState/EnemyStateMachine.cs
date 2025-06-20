using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public void SetInitialState(IState initialState)
    {
        ChangeState(initialState); 
    }
}
