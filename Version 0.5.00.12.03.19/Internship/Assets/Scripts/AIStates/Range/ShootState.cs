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
 * -.01.112919; add functionality and made sure it works with the boss logic 
 * -.01.120119; rechecked and changed some logic, added in-depth comments. 
 * 1.0.01.120319; GOLD? 
 * 
 * 
 * */


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*******************************************************************************************************************************************************************************************************************************
/*  HOW TO USE THIS SCRIPT:
 * 
 * 
 * 
 * 
 */

// Range SHOOT STATE- inherits from BaseAIState for the enemy_object and Tick() function
public class ShootState : BaseAIState
{
    private float _attackReadyTimer = 2f;
    private EnemyAI _obj;

    // Make player flash red from "Hit"
    // Shoot State constructor, is called upon when this object is instantiated. also calls base(baseAIState) constructor
    // so grab the enemy this script is now attatched too.
    public ShootState( EnemyAI enemy_obj) : base(enemy_obj.gameObject)
    {
        _obj = enemy_obj;
    }
    // Tick() gets called every update frame
    public override Type Tick()
    {
        // need to check if for aggro? should just attack when in aggro state so shouldnt need it.
        if (_obj.Target == null)
            return typeof(WanderState);
        
        // when enemy is father away than player, go back to wandering around. Then will decide from there if the player is in any range or not.
        else if (Vector3.Distance(_obj.transform.position, GameManager.player_obj.transform.position) > GameManager.RangeAggroRadius)
        {
            return typeof(WanderState);
        }

        // timer for shooting every few frames.
        _attackReadyTimer -= Time.deltaTime;
        if (_attackReadyTimer <= 0f)
        {
            //CHANGE THE SHOOT CODE HERE
            Debug.Log("Shoot!");
            _obj.FireTheLaser();
        }

        return null;
    }

   

}
