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
public class AttackState : BaseAIState
{
    private float _attackReadyTimer = 2f;
    private EnemyAI _obj;

    // COUNT VARIABLE FOR POTENTIAL STATE CHANGE- 3 MELEE ATTACKS IN A ROW, ENEMY GOES OFF TO WANDER FOR A BIT
    private byte count = 0;

    // Make player flash red from "Hit"
    public AttackState(EnemyAI enemy_obj) : base(enemy_obj.gameObject)
    {
        _obj = enemy_obj;
    }

    // Tick() gets called every update frame
    public override Type Tick()
    {
        if (_obj.Target == null)
            return typeof(WanderState);

        if (count >= 3) return typeof(RunState);
        if (Vector3.Distance(_obj.transform.position, GameManager.player_obj.transform.position) <= GameManager.MeleeAttackRange)
        {
            //CHANGE THE MELEE ATTACK CODE HERE
            _attackReadyTimer -= Time.deltaTime;
            if (_attackReadyTimer <= 0f)
            {
                Debug.Log("Attack!");
                _obj.FireTheLaser();
                count++;
            }
        }
        else if (Vector3.Distance(_obj.transform.position, GameManager.player_obj.transform.position) > GameManager.MeleeAggroRadius)
        {
            return typeof(WanderState);
        }      
        else if (Vector3.Distance(_obj.transform.position, GameManager.player_obj.transform.position) > GameManager.MeleeAttackRange + 0.4f)
        {
            return typeof(ChaseState);
        }
            return null;
    }
   


}
