using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/* HOW TO USE THIS SCRIPT ALONG WITH EnemyAI SCRIPTS
 * THIS SCRIPT WILL BE PUT ON THE ENEMY OBJECT
 * 
 * 
 * 
 * 
 */

public class StateMachine : MonoBehaviour
{
    private Dictionary<Type, BaseAIState> _available_states;

    public BaseAIState Current_state { get; private set; }
    public event Action<BaseAIState> OnStateChanged;


    public void SetStates(Dictionary<Type, BaseAIState> states)
    {
        _available_states = states;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Current_state == null)
        {
            Current_state = _available_states.Values.First();
        }

        // Calls the next Tick() of the state it is in. 
        // then returns with the next State
        var nextState = Current_state?.Tick();

        if (nextState != null &&
            nextState != Current_state?.GetType())
        {
            SwitchToNewState(nextState);
        }
        else if (nextState == null) Debug.Log("NULL NEXT STATE, are we safe?");
    }


    private void SwitchToNewState(Type nextState)
    {
        Current_state = _available_states[nextState];
        OnStateChanged?.Invoke(Current_state);
    }
}
