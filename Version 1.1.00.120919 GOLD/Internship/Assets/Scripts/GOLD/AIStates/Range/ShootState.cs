/* Ethan Bonavida
 * Enemy Shoot State- AI
 *  Shoot the player. shoot the player with the laser. LAYZAR
 * 
 * 
 * -.DDMMYY
 * 0.0.01 = bug fix, updates, trying small changes
 * 0.1.-  = major changes, rewriting, changed logic  
 * 1.0.00 = GOLD?
 * Version: 
 * 0.0.00.112219; Created script
 * -.01.112919; add functionality and made sure it works with all  the differnt states and ranges 
 * -.01.120119; rechecked and changed some logic, added in-depth comments. 
 * 1.0.01.120319; GOLD? 
 * 1.2.00.120419; Finalized touches, everything works in here(I really think) - GOLD!
 * 
 * */


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*******************************************************************************************************************************************************************************************************************************
/*  HOW TO USE THIS SCRIPT:
 * This is the Shoot State. a simple state where based on conditions should do shoot ranged attack. it simply shoots the players in long range 
 * Change or Update to however you want the enemy to shoot the player. 
 * Shoots a laser beam pretty much. Hits player and shows visual injury. 
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
        if (_obj.Target == null)
        {
            _obj.SetTarget(GameManager.player_obj.transform);
        }               
        // when enemy is farther away than player, go back to wandering around. Then will decide from there if the player is in any range or not.
        else if (Vector3.Distance(_obj.transform.position, _obj.Target.transform.position) > GameManager.RangeAggroRadius)
        {
            if (_obj.wander == true) return typeof(WanderState);
        }
        // timer for shooting every few frames.
        _attackReadyTimer -= Time.deltaTime;
        if (_attackReadyTimer <= 0f)
        {            
            //CHANGE THE SHOOT CODE HERE
            Debug.Log("Shoot!");
            _obj.FireTheLaser();
            _attackReadyTimer = 2f;
        }
        // should change color back from getting shot
        else if (_attackReadyTimer <1.6)
        {
            var mat = GameManager.player_material;
            mat.color = Color.yellow;
        }
        return null;
    } 
}
