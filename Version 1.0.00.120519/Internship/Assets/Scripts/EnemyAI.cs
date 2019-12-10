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
 * -1.01.112919; after thouroughly researching, and finally understanding. I got started writing this baseAI, Statemachine, and EnemyAI classes. 
 * -.02.113019; exploring and adding more functionality. testing how to combine with my idea goal. 
 * -.02.120119; more additions and fixing functionality, and adding and testing boss phase. Boss should switch back and forth between Melee and Ranged.
 * 1.1.02.120419; EnemyAI seems to be running smoothly and everything seems to work. added in-depth comments. GOLD!
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
 * ONLY BEFORE RUNTIME!! IT WILL NOT WORK AFTER HITTING START. 
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

    // SHARED STATES 
    [SerializeField] private bool _idle;    
    [SerializeField] private bool _wander;
    [SerializeField] private bool _chase;
    [SerializeField] private bool _run;
    // MELEE
    [SerializeField] private bool _dodge;
    [SerializeField] private bool _stealth;
    [SerializeField] private bool _charge;   
    [SerializeField] private bool _attack;
    // RANGE    
    [SerializeField] private bool _shoot;
    
    [HideInInspector]
    public bool idle;
    [HideInInspector]
    public bool chase;
    [HideInInspector]
    public bool run;
    [HideInInspector]
    public bool dodge;
    [HideInInspector]
    public bool stealth;
    [HideInInspector]
    public bool charge;
    [HideInInspector]
    public bool wander;
    [HideInInspector]
    public bool shoot;
    [HideInInspector]
    public bool attack;

    //Transfrom of the player object. 
    public Transform Target { get; private set; }
    public StateMachine StateMachine => GetComponent<StateMachine>();

    [HideInInspector]
    public bool _inRange;   

    // called once during its instantiation
    private void Awake()
    {        
        // initialize the machine known as enemyAI 3000 blaster gaster
        InitializeStateMachine();
        _inRange = false;
    }

    private void InitializeStateMachine()
    {
        // create dictionary to store the active states in.
        var states = new Dictionary<Type, BaseAIState>();
        //have melee or ranged condition here
        // IF BOSS IS ACTIVE, DEFAULTS TO THIS STATE
        if (_Boss == true)
        {
            //Switches back and forth between Melee and Range phases.
            states.Add(typeof(IdleState), new IdleState(this)); // default to idle state;

            // set the right values
            _idle = true;   idle = _idle;
            _wander = true; wander = _wander;
            _chase = true;  chase = _chase;
            _run = true;    run = _run;
            _dodge = true;  dodge = _dodge;
            _charge = true; charge = _charge;
            _stealth = true;stealth = _stealth;
            _attack = true; attack = _attack;
            _shoot = true;  shoot = _shoot;

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
            //defaults to a simple melee enemy
            states.Add(typeof(IdleState), new IdleState(this));
            states.Add(typeof(WanderState), new WanderState(this));
            states.Add(typeof(ChaseState), new ChaseState(this));
            states.Add(typeof(RunState), new RunState(this));
            _idle = true; idle = _idle;
            _wander = true; wander = _wander;
            _chase = true; chase = _chase;
            _run = true;  run = _run;
            _Melee = true;
        }
        else if (_Melee == true)
        {
            // add melee states based off what is active
            if (_idle == true) states.Add(typeof(IdleState), new IdleState(this)); idle = _idle;
            if (_wander == true) states.Add(typeof(WanderState), new WanderState(this)); wander = _wander;
            if (_chase == true) states.Add(typeof(ChaseState), new ChaseState(this)); chase = _chase;
            if (_run == true) states.Add(typeof(RunState), new RunState(this)); run = _run;
            if (_dodge == true) states.Add(typeof(DodgeState), new DodgeState(this));  dodge = _dodge;
            if (_charge == true) states.Add(typeof(ChargeState), new ChargeState(this)); charge = _charge;
            if (_stealth == true) states.Add(typeof(StealthState), new StealthState(this)); stealth = _stealth;
            if (_attack == true) states.Add(typeof(AttackState), new AttackState(this)); attack = _attack;
        }
        else if (_Range == true)
        {
            if (_idle == true) states.Add(typeof(IdleState), new IdleState(this)); idle = _idle;
            if (_wander == true) states.Add(typeof(WanderState), new WanderState(this)); wander = _wander;
            if (_chase == true) states.Add(typeof(ChaseState), new ChaseState(this)); chase = _chase;
            if (_run == true) states.Add(typeof(RunState), new RunState(this)); run = _run;
            // ALSO ADD SNIPER/STEALTH ONE?
            if (_shoot == true) states.Add(typeof(ShootState), new ShootState(this)); shoot = _shoot;
        }

        // pass our dictionary of states to add to the statemachine queue
        StateMachine.SetStates(states); 
        //GetComponent<StateMachine>().SetStates(states);                

        // differnt way to initialize the dictionary
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

    // boss phase pattern. Should go from melee to range and vice versa every x seconds
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
            yield return new WaitForSeconds(16f);
        }      
    }

    // FIREEE THA LAYZAAR AT THE PLAYAAR
    public void FireTheLaser()
    {
        // change player color, to visual a hit from an attack or shot
        Debug.DrawLine(this.transform.position, GameManager.player_position, Color.red, 1.2f);
        var mat = GameManager.player_material;
        mat.color = Color.red;
        Debug.Log("PLAYER GOT HIT!");        
    }

    // on trigger enter switch
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _inRange = true;
        }
    }

    // try to draw wire frame to see whats happening??
    // draws a wiresphere where wall detection should happen.
    private void OnDrawGizmosSelected()
    {      
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position + this.transform.forward * 3f, 0.5f);
    }
}
