using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FireSpiritStateMachine
{
    public FireSpiritState currentState { get; private set; } // you can see the value but can't change it

    public void Initialize(FireSpiritState _startState)
    {
        currentState = _startState;
        currentState.Enter();
    }

    public void ChangeState(FireSpiritState _newState)
    {
        currentState.Exit();
        currentState = _newState;
        currentState.Enter();
    }
}
