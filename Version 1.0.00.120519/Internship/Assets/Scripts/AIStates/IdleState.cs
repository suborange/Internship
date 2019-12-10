/* Ethan Bonavida
 * Enemy Idle State-AI
 * Enemy does nothing, after X amount of seconds it will change to another state. 
 * Default new state set to WanderState for both Melee and Ranged. 
 * Boss state is idle until player gets close enough, then it starts chasing the player in either-or Melee-Ranged.
 * 
 * 
 * -.DDMMYY
 * 0.0.01 = bug fix, updates, trying small changes
 * 0.1.-  = major changes, rewriting, changed logic  
 * 1.0.00 = GOLD?
 * Version: 
 * 0.0.00.112219; Created script
 * -.01.113019; Fleshed out this inherited script with whats needed.
 * -.02.120119; added basic swtich statement with default, can choose which state to start in. 
 * -.03.120219; add functionality with boss and added comments for script  
 * 1.1.00.120319; GOLD?
 * 1.2.00.120419; Finalized touches, everything works in here(I really think) - GOLD!
 * 
 * */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*******************************************************************************************************************************************************************************************************************************
/*  HOW TO USE THIS SCRIPT:
 * This script is the default starting Tick() for all states. This is the place to mess around and change how you want the AI to start, or default to. 
 * Used a switch statement to help randomize or organize enemy AI actions. Returns the next state to change to. 
 * Default state is set to WanderState.  _state = 2;
 * Boss _state = 1; set active if Boss is clicked.
 */

// IDLE STATE- inherits from BaseAIState for the enemy_object and Tick() function
public class IdleState : BaseAIState
{
    // Need an enemy object to use
    private EnemyAI _obj;
    private int _state = 0;
    private bool _default = true;
    private float _idleTimer = 2f; // change for different time

    // Idle State constructor, is called upon when this object is instantiated. also calls base(baseAIState) constructor
    // so grab the enemy this script is now attatched too. 
    public IdleState(EnemyAI enemy_obj) : base(enemy_obj.gameObject)
    {
        _obj = enemy_obj;
    }

    // TICK() GETS CALLED EVERY UPDATE FRAME.
    public override Type Tick()
    {
        // ALL CONDITIONS HERE TO CHANGE FROM IDLE STATE TO ANOTHER ACTION STATE.
        // special case for boss functionality.
        if (_obj._Boss == true)
        {
            _default = false;
            // if player is in "15f" range of the boss, bossphase should start.
            if(Vector3.Distance(_obj.transform.position, GameManager.player_position) < 9f)
            {
                GameManager.Instance.ForCoroutine(_obj.BossPhase()); // Call coroutine from manager, passing this _obj own boss phase.
                //Debug.Log("WHEN DID THIS HAPPEN?!?!");
                _state = 1; // set _state to 1, to activate first boss State
            }
        }
        else if (_default == true) GameManager.Instance.ForCoroutine(IdleWait()); // if not a boss, continue normally with simple idle timer
        // idle state, does nothing
        // _state = 2, waits for x seconds then starts to wander. Can choose and add cases here to change. Also need to change _state as well in IdleWait()
        switch (_state)
        {
            // _state==0 for null state
            case 0: return null; // continue in this state.
            // _state==1 for boss state
            case 1:
                Debug.Log("Should be boss now!");
                GameManager.Instance.stopCoroutines(); // incase any running coroutine fucks anything up 
                return typeof(ChaseState); // force the chase state for boss phase. 
            case 2:
                Debug.Log("Should stay idle till now! NOW WANDER!");
                GameManager.Instance.stopCoroutines();
                if (_obj.wander == true) return typeof(WanderState); 
                else return null;             
                           
            case 3:
                Debug.Log("Should stay idle till now! NOW STEALTH!");
                GameManager.Instance.stopCoroutines();
                return typeof (StealthState);

            //add more cases for different _states below here
            // case 4:
            //    return 

            default: return null; 
        }            
    }

    // wait x seconds of doing nothing, then change _state to state2: WanderState
    private IEnumerator IdleWait()
    {
        yield return new WaitForSeconds(_idleTimer);
        // can change manually(hardcoded), or can use Random.Range(); or defaults to wander
        //var rand = 2;
        //var rand = 3; 
        var rand = UnityEngine.Random.Range(2,3);

        if (_obj.wander == true && rand == 2) _state = 2; // hopefully random number chooses 2
        else if (_obj._Melee == true && _obj.stealth == true && rand == 3) _state = 3; // hopefully random number chooses 3
        // add more default choices here

        else { _state = 2; _idleTimer = 0; } // DEFAULTS TO 2, time set to 0 so it only waits once.
    } 
}
