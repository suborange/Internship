/* Ethan Bonavida
 * Enemy Run State-AI
 * Enemy does not do much except run away! Escape ya lil bugger! Runs in opposite direction of the player.
 * 
 * 
 * -.DDMMYY
 * 0.0.01 = bug fix, updates, trying small changes
 * 0.1.-  = major changes, rewriting, changed logic  
 * 1.0.00 = GOLD?
 * Version: 
 * 0.0.00.112219; Created script
 * -.01.113019;  Fleshed out this inherited script with whats needed. Add run functionality. 
 * -.120119; doesnt matter about melee or range, should work the same. added comments
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
 * This is the Run State. Another one of the most simple states, it simply runs away from the players location.
 * Change or Update to however you want the enemy to flee from the player. 
 * Reacts to player distance
 */

// RUN STATE- inherits from BaseAIState for the enemy_object and Tick() function
public class RunState : BaseAIState
{
    private EnemyAI _obj;
    private float  _step;
    private float _range;
    // float stop_dist;
    // float retreat_dist;

    public void Start()
    {
     
        // stop_dist = 16f;
        // retreat_dist = 8f;
    }

    // Run State constructor, is called upon when this object is instantiated. also calls base(baseAIState) constructor
    // so grab the enemy this script is now attatched too.
    public RunState(EnemyAI enemy_obj) : base(enemy_obj.gameObject)
    {
        _obj = enemy_obj;
    }

    // Tick() gets called every update frame
    public override Type Tick()
    {
        if (_obj.Target == null)
        {
            _obj.SetTarget(GameManager.player_obj.transform);
        }
        // Literally just move away from the players location, - _step
        // change _step to speed up or down
        _step = GameManager.Speed * 1.25f;
        transform.position = Vector3.MoveTowards(transform.position, _obj.Target.transform.position, -1 * _step * Time.deltaTime);
                 
        if (_obj._Melee == true)
        {
            _range = GameManager.MeleeAttackRange;
        }
        else if (_obj._Range == true)
        {
            _range = GameManager.RangeAggroRadius;
        }

        // conditionals with return with state Type
        // run away until certain distance from player, if player gets close enough into fighting range somehow, then attack, duh
        if ( Vector3.Distance(_obj.transform.position, GameManager.player_position) <= _range && _obj.attack == true)
        {
            return typeof(AttackState);
        }
        // run away until far enough away from the player and its max range, enemy starts to wander around again. 
        if (Vector3.Distance(_obj.transform.position, GameManager.player_position) > (_range + 3f) && _obj.wander == true)
        {
            return typeof(WanderState);
        }

        // SECEONDAY- another way to look at a run enemy. 
        //speed = GameManager.Speed;
        //if (Vector3.Distance(transform.position, GameManager.player_position) > stop_dist)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, GameManager.player_position, speed * Time.deltaTime);
        //}
        ////not too far, or not too close from the player, will shoot from here and not move. 
        //else if (Vector3.Distance(transform.position, GameManager.player_position) < stop_dist &&
        //    (Vector3.Distance(transform.position, GameManager.player_position) > retreat_dist))
        //{
        //    transform.position = this.transform.position;
        //}
        ////too close, runaway from the player
        //else if (Vector3.Distance(transform.position, GameManager.player_position) < retreat_dist)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, GameManager.player_position, -speed * Time.deltaTime);
        //}

        return null;
    }

   

}
