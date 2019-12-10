/* Ethan Bonavida
 * Enemy State Machine-AI
 * This is basically the brain and controller of everything else. Each state gets called from here and each change state changes from here
 * 
 * 
 * -.DDMMYY
 * 0.0.01 = bug fix, updates, trying small changes
 * 0.1.-  = major changes, rewriting, changed logic  
 * 1.0.00 = GOLD?
 * Version: 
 * 0.0.00.112219; Created script
 * -1.01.112919; after thouroughly researching, and finally understanding. I got started writing this baseAI, Statemachine, and EnemyAI classes. 
 * 1.1.01.120419; State Machine is running smoothly. added in-depth comments GOLD!
 * 
 * */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//*******************************************************************************************************************************************************************************************************************************
/* HOW TO USE THIS SCRIPT ALONG WITH EnemyAI SCRIPTS
 * THIS SCRIPT WILL BE PUT ON THE ENEMY OBJECT IN CONJUNCTION WITH EnemyAI SCRIPT. THEY GO HAND IN HAND
 *  This is the who control operation for each state, and each change of state. Will call each state tick each frame. added in-depth comments
 * 
 */

public class StateMachine : MonoBehaviour
{
    // create dictionary to hold all our state types
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
        if (Current_state == null && _available_states != null)
        {
            // grab first value of dictionary of states by default
            Current_state = _available_states.Values.First();
            //Debug.Log(_available_states.Values.First());
        }

        // Calls the next Tick() of the state it is in. 
        // then returns with the next State 
        var nextState = Current_state?.Tick();

        // changes next state if not null, needs return type
        if (nextState != null &&
            nextState != Current_state?.GetType())
        {
            SwitchToNewState(nextState);
        }
        // Debug.Log("NULL NEXT same STATE, are we safe?");
    }


    private void SwitchToNewState(Type nextState)
    {
        // switch to the new state type
        Current_state = _available_states[nextState];
        OnStateChanged?.Invoke(Current_state);
    }
}
