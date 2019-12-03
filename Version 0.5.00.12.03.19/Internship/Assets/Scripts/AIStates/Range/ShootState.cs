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
 * 1.1.00.120319; GOLD?
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

// WANDER STATE- inherits from BaseAIState for the enemy_object and Tick() function
public class ShootState : BaseAIState
{
    private float _attackReadyTimer = 2f;
    private EnemyAI _obj;
    // Make player flash red from "Hit"
    public ShootState( EnemyAI enemy_obj) : base(enemy_obj.gameObject)
    {
        _obj = enemy_obj;
    }
    // Tick() gets called every update frame
    public override Type Tick()
    {
        if (_obj.Target == null)
            return typeof(WanderState);


        if (Vector3.Distance(_obj.transform.position, GameManager.player_obj.transform.position) <= GameManager.RangeAggroRadius)
        {
            //CHANGE THE ATTACK CODE HERE
            _attackReadyTimer -= Time.deltaTime;
            if (_attackReadyTimer <= 0f)
            {
                Debug.Log("Shoot!");
                _obj.FireTheLaser();
            }
        }
        else if (Vector3.Distance(_obj.transform.position, GameManager.player_obj.transform.position) > GameManager.RangeAggroRadius)
        {
            return typeof(ChaseState);
        }
        



        return null;
    }

   

}
