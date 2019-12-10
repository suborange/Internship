/* Ethan Bonavida
 * Enemy Attack State-AI
 * eney close range attacks. Melee range. Also hit them with a laser for somereason. 
 * 
 * 
 * -.DDMMYY
 * 0.0.01 = bug fix, updates, trying small changes
 * 0.1.-  = major changes, rewriting, changed logic  
 * 1.0.00 = GOLD?
 * Version: 
 * 0.0.00.112219; Created script
 * -.01.112919; add functionality and made sure it works the differnt states
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
 * This is the Attack State. a simple state where based on conditions should do attack. it simply attacks the players in range 
 * Change or Update to however you want the enemy to attack the player. 
 * Default set to attack 3 times, then to change states. If there is no target in range, then wanders. otherwise will do nothing
 * 
 */

// Melee ATTACK STATE- inherits from BaseAIState for the enemy_object and Tick() function
public class AttackState : BaseAIState
{
    private float _attackReadyTimer = 3f;
    private EnemyAI _obj;

    // COUNT VARIABLE FOR POTENTIAL STATE CHANGE- 3 MELEE ATTACKS IN A ROW, ENEMY GOES OFF TO WANDER FOR A BIT
    private byte count = 0;

    // Make player flash red from "Hit"

    // Attack State constructor, is called upon when this object is instantiated. also calls base(baseAIState) constructor
    // so grab the enemy this script is now attatched too.
    public AttackState(EnemyAI enemy_obj) : base(enemy_obj.gameObject)
    {
        _obj = enemy_obj;
    }

    // Tick() gets called every update frame
    public override Type Tick()
    {
        // need to check if for aggro? should just attack when in aggro state so shouldnt need it. 
        if (_obj.Target == null)
            return typeof(WanderState);


        // after 3 continuious melee hits, change states 
        if (count >= 3)
        {
            if (_obj.run == true) return typeof(RunState); //Debug.Log("3 MELEE ATTACKS CHANGE");    
        }
        // when enemy becomes too far from the player position, go into wanderstate
        else if (Vector3.Distance(_obj.transform.position, GameManager.player_position) > GameManager.MeleeAggroRadius)
        {
            if ( _obj.wander == true) return typeof(WanderState);
        }      
        //if enemy is out attack rangem then chase the player
        //else if (Vector3.Distance(_obj.transform.position, GameManager.player_position) > GameManager.MeleeAttackRange + 0.4f)
        //{
        //    return typeof(ChaseState);
        //}

        //CHANGE THE MELEE ATTACK CODE HERE
        //wait to attack every several frames
        _attackReadyTimer -= Time.deltaTime;
        if (_attackReadyTimer <= 0f)
        {
            Debug.Log("Attack!");
            _obj.FireTheLaser();
            count++;
        }
        // should change color back from getting attacked
        else if (_attackReadyTimer <= 2.5f)
        {
            var mat = GameManager.player_material;
            mat.color = Color.yellow;
        }
        return null;
    }
   


}
