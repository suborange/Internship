/* Ethan Bonavida
 * Enemy Chase State-AI
 * Enemy chases the player around. Simple as that
 * 
 * 
 * -.DDMMYY
 * 0.0.01 = bug fix, updates, trying small changes
 * 0.1.-  = major changes, rewriting, changed logic  
 * 1.0.00 = GOLD?
 * Version: 
 * 0.1.00.112219; Created script, copied from old chase script again and updated. 
 * -.112919; add functionality with boss, and add comments. 
 * 1.1.00.120319; GOLD?
 * 
 * 
 * */


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*******************************************************************************************************************************************************************************************************************************
/*  HOW TO USE THIS SCRIPT:
 * This is the Chase State. One of the most simple states, it simply chases the players location. Change or Update to however you want the enemy to chase the player. 
 * 
 */

// CHASE STATE- inherits from BaseAIState for the enemy_object and Tick() function
public class ChaseState : BaseAIState
{
    // need enemy object
    private EnemyAI _obj;
    private float _step;
    private float _range;

    // Chase State constructor, is called upon when this object is instantiated. also calls base(baseAIState) constructor
    // so grab the enemy this script is now attatched too. 
    public ChaseState (EnemyAI enemy_obj) : base(enemy_obj.gameObject)
    {
        _obj = enemy_obj; // Enemy Object
    }
    
    // Tick() gets called every update frame
    public override Type Tick()
    {
        // Literally just move towards the players location
        // change _step to speed up or down
        _step = GameManager.Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, GameManager.player_obj.transform.position, _step);

        if (_obj._Melee==true)
        {
            _range = GameManager.MeleeAttackRange;
        }
        else if (_obj._Range == true)
        {
            _range = GameManager.RangeAggroRadius;
        }
        
        //conditionals with return with state Type
        var distance = Vector3.Distance(transform.position, _obj.Target.transform.position);       
        // chase the player when close to eachother, then attack when in range
        if ( distance <= _range)
        {
            if (_obj._Melee == true) return typeof(AttackState);
            if (_obj._Range == true) return typeof(ShootState);
        }
        // chase the player until player is too far away, then start to wander around.
        else if (distance > _range +5f)
        {
            return typeof(WanderState);
        }      
        return null;
    }
}
