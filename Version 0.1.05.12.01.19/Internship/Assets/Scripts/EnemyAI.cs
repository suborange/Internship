using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*******************************************************************************************************************************************************************************************************************************
 /*HOW TO USE THIS SCRIPT ALONG WITH StateMachine SCRIPTS
 * 
 * 
 * THIS SCRIPT WILL BE PUT ON THE ENEMY OBJECT
 * IN THE INSPECTOR, YOU WILL BE ABLE TO CHOOSE WHICH STATES YOU WANT TO HAVE ACTIVE,
 * BEFORE RUNTIME!! IT WILL NOT WORK AFTER HITTING START. 
 * 
 * IN THE UNITY INSPECTOR, CLICK TO CHOOSE WHICH STATES YOU WANT ON OR OFF(TRUE OR FALSE)
 * SOME STATES APPLY TO BOTH MELEE AND RANGED UNITS
 * SOMT STATES APPLY TO ONLY ONE OR THE EITHER OF MELEE OR RANGED UNITS
 * 
 */

public class EnemyAI : MonoBehaviour
{
    // Select Melee enemy
    [SerializeField] private bool _Melee;
    // Select Range enemy
    [SerializeField] private bool _Range;
    // if neither are chosen default to melee
    // BOSS enemy
    //[SerializedField] private bool _Boss;

    [SerializeField] private bool _idle;
    [SerializeField] private bool _chase;
    [SerializeField] private bool _run;
    [SerializeField] private bool _dodge;
    [SerializeField] private bool _stealth;
    [SerializeField] private bool _charge;
    [SerializeField] private bool _wander;
    [SerializeField] private bool _shoot;
    [SerializeField] private bool _attack;
    //[SerializedField] private bool _hide;    

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
        var states = new Dictionary<Type, BaseAIState>();
        //have melee or ranged condition here
        if (_Melee == false && _Range == false)
        {
            //defaults to simple enemy
            states.Add(typeof(IdleState), new IdleState(this));
            states.Add(typeof(WanderState), new WanderState(this));
            states.Add(typeof(ChaseState), new ChaseState(this));
            states.Add(typeof(RunState), new RunState(this));
        }
        else if (_Melee == true)
        {            
            if (_idle == true) states.Add(typeof(IdleState), new IdleState(this));
            if (_wander == true) states.Add(typeof(WanderState), new WanderState(this));
            if (_chase == true) states.Add(typeof (ChaseState), new ChaseState(this));
            if (_run == true) states.Add(typeof (RunState), new RunState(this));
            if (_dodge == true) states.Add(typeof (DodgeState), new DodgeState(this));
            if (_charge == true) states.Add(typeof(ChargeState), new ChargeState(this));
            if (_stealth == true) states.Add(typeof(StealthState), new StealthState(this));
            //NEEDS ATTACK STATE
            //if (_attack == true) states.Add(typeof(AttackState), new AttackState(this));
        }
        else if( _Range == true)
        {            
            if (_idle == true) states.Add(typeof(IdleState), new IdleState(this));
            if (_wander == true) states.Add(typeof(WanderState), new WanderState(this));
            if (_chase == true) states.Add(typeof(ChaseState), new ChaseState(this));
            if (_run == true) states.Add(typeof(RunState), new RunState(this));
            //NEEDS SHOOTING FORM RANGED STATE, ALSO ADD SNIPER/STEALTH ONE?
            //if (_shoot == true) states.Add(typeof(ShootState), new ShootState(this));
        }
        //ADD BOSS VARIANT THAT SWITCHES BETWEEN MELEE FORM AND RANGE? OR SWITCHES BETWEEN ALL OF THEM?

        //StateMachine.SetStates(states); does this work too? 
        GetComponent<StateMachine>().SetStates(states);
                

        // Create a dictionary with each of the differnt EnemyAI state Types and States
        //var states = new Dictionary<Type, BaseAIState>()
        //{
        //{ typeof (IdleState), new IdleState(this)},            //State wher enemy stands idle, waiting for time or special condition
        //{ typeof (WanderState), new WanderState(this)},       // State where enemy wanders around 
        //{ typeof (ChaseState), new ChaseState(this) },         //State where enemy chases towards the player
        //{ typeof (RunState), new RunState(this) },             //State where enemy runs away from the player
        //{ typeof (DodgeState), new DodgeState(this) },         //State where the enemy will dodge away from the player
        //{ typeof (StealthState), new StealthState(this)},     //State where the enemy will in stealth, alert position looking for the player. 
        //{ typeof (RangedState), new RangedState(this)}        //State where the enemy will 
        //};

    }

    public void SetTarget(Transform target)
    {
        //sets the Target from other states.
        Target = target;
    }


}
