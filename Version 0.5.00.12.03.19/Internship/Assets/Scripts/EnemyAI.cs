/* Ethan Bonavida
 * Enemy Idle State-AI
 * Enemy does nothing, after a # of seconds it will change to another state. 
 * Default new state set to WanderState for both Melee and Ranged. 
 * Boss state is idle until player gets close enough, then it startschasing the player in either-or Melee-Ranged.
 * 
 * 
 * -.DDMMYY
 * 0.0.01 = bug fix, updates, trying small changes
 * 0.1.-  = major changes, rewriting, changed logic  
 * 1.0.00 = GOLD?
 * Version: 
 * 0.0.00.112219; Created script
 * 
 * 
 * */


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
 * SOME STATES APPLY TO ONLY ONE OR THE EITHER OF MELEE OR RANGED UNITS
 * 
 */

public class EnemyAI : MonoBehaviour
{
    // Select Melee enemy
    [SerializeField] public bool _Melee; 
    // Select Range enemy
    [SerializeField] public bool _Range;
    // if neither are chosen default to melee
    // BOSS enemy
    [SerializeField] public bool _Boss;

    [SerializeField] private bool _idle;
    [SerializeField] private bool _chase;
    [SerializeField] private bool _run;
    [SerializeField] private bool _dodge;
    [SerializeField] private bool _stealth;
    [SerializeField] private bool _charge;
    [SerializeField] private bool _wander;
    [SerializeField] private bool _shoot;
    [SerializeField] private bool _attack;   

    //Transfrom of the player object. 
    public Transform Target { get; private set; }
    public StateMachine StateMachine => GetComponent<StateMachine>();

    private float _matTimer= 1f;
    // called once during its instantiation
    private void Awake()
    {        
        InitializeStateMachine();
    }

    private void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseAIState>();
        //have melee or ranged condition here
        if (_Boss == true )
        {            
            //Switches back and forth between Melee and Range phases.
            states.Add(typeof(IdleState), new IdleState(this)); // default to idle state;

            _idle = true;
            _wander = true;
            _chase = true;
            _run = true;
            _dodge = true;
            _charge = true;
            _stealth = true;
            _attack = true;
            _shoot = true;
           
            // can default to boss enemy with all these states
            // can try to be used for more evolution, learning patterns special to boss.
            //states.Add(typeof(IdleState), new IdleState(this));
            //states.Add(typeof(WanderState), new WanderState(this));
            //states.Add(typeof(ChaseState), new ChaseState(this));
            //states.Add(typeof(RunState), new RunState(this));
            //states.Add(typeof(DodgeState), new DodgeState(this));
            //states.Add(typeof(ChargeState), new ChargeState(this));
            //states.Add(typeof(StealthState), new StealthState(this));
            //states.Add(typeof(AttackState), new AttackState(this));
            //states.Add(typeof(ShootState), new ShootState(this));
            }
        else if (_Melee == false && _Range == false && _Boss == false)
        {
            //defaults to simple enemy
            states.Add(typeof(IdleState), new IdleState(this));
            states.Add(typeof(WanderState), new WanderState(this));
            states.Add(typeof(ChaseState), new ChaseState(this));
            states.Add(typeof(RunState), new RunState(this));
            _idle = true;
            _wander = true;
            _chase = true;
            _run = true;
            _Melee = true;
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
            if (_attack == true) states.Add(typeof(AttackState), new AttackState(this));
        }
        else if( _Range == true)
        {            
            if (_idle == true) states.Add(typeof(IdleState), new IdleState(this));
            if (_wander == true) states.Add(typeof(WanderState), new WanderState(this));
            if (_chase == true) states.Add(typeof(ChaseState), new ChaseState(this));
            if (_run == true) states.Add(typeof(RunState), new RunState(this));
            // ALSO ADD SNIPER/STEALTH ONE?
            if (_shoot == true) states.Add(typeof(ShootState), new ShootState(this));
        }

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

    public IEnumerator BossPhase()
    {
        while (true)
        {
            _Boss = false;
            if (UnityEngine.Random.Range(0, 7) >=4)
            {
                _Melee = true;
                _Range = false;
                InitializeStateMachine();
                Debug.Log("BossPhaseMelee!");
            }
            else
            {
                _Range = true;
                _Melee = false;
                InitializeStateMachine();
                Debug.Log("BossPhaseRange!");
            }
            yield return new WaitForSeconds(14f);
        }
        
         
    }

    public void FireTheLaser()
    {
        Debug.DrawLine(this.transform.position, GameManager.player_obj.transform.position, Color.red, 1.2f);
        var mat = GameManager.player_obj.GetComponent<Material>();
        mat.color = Color.red;

        _matTimer -= Time.deltaTime;
        if (_matTimer <= 0f)
        {
            mat.color = Color.white;
            Debug.Log("turned back to white.. how fast?");
        }
        
    }

}
