using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private Dictionary<Type, BaseState> _avilableStates;

    public BaseState CurrentState { get; private set; }
    public event Action<BaseState> OnStateChanged;


    public string currentType;

    public void SetState(Dictionary<Type, BaseState> states)
    {
        _avilableStates = states;
    }


    public void StateTick()
    {
        if (CurrentState == null)
            CurrentState = _avilableStates.Values.First();

        var nextState = CurrentState?.Tick();

        if (nextState != null && nextState != CurrentState?.GetType())
            SwitchToNewState(nextState);

    }

    private void SwitchToNewState(Type nextState)
    {
        CurrentState = _avilableStates[nextState];
        OnStateChanged?.Invoke(CurrentState);
        currentType = CurrentState.ToString();
    }
}
