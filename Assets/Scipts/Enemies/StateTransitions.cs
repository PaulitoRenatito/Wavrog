using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTransitions
{

    public List<StateMachine.EnemyState> NextStates { get; }

    public StateTransitions(params StateMachine.EnemyState[] nextStates)
    {
        NextStates = new List<StateMachine.EnemyState>(nextStates);
    }
}
