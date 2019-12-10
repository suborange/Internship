using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : MonoBehaviour
{
    //Transfrom of the player object. 
    public Transform Target { get; private set; }
    public StateMachine StateMachine => GetComponent<StateMachine>();

    // called once during its instantiation
    private void Awake()
    {
        InitializeStateMachine();
    }

    private void InitializeStateMachine()
    {
        //have melee or ranged condition here

        //THINK ABOUT ADDING RANDOM OR SEPCIAL CONDITION OPTIONS
        // _available_states.Add(typeof(ChaseState) , new ChaseState(GameObject));

        // Create a dictionary with each of the differnt EnemyAI state Types and States
        var states = new Dictionary<Type, BaseAIState>()
        {
            //{ typeof (IdleState), new IdleState(this)},            //State wher enemy stands idle, waiting for time or special condition
            //{ typeof (WanderState), new WanderState(this)},       // State where enemy wanders around 
            //{ typeof (ChaseState), new ChaseState(this) },         //State where enemy chases towards the player
            //{ typeof (RunState), new RunState(this) },             //State where enemy runs away from the player
            //{ typeof (DodgeState), new DodgeState(this) },         //State where the enemy will dodge away from the player
            //{ typeof (StealthState), new StealthState(this)},     //State where the enemy will in stealth, alert position looking for the player. 
            //{ typeof (RangedState), new RangedState(this)}        //State where the enemy will 
        };
        //StateMachine.SetStates(states); does this work too? 
        GetComponent<StateMachine>().SetStates(states);
    }

    public void SetTarget(Transform target)
    {
        //sets the Target from other states.
        Target = target;
    }
}
