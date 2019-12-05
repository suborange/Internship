/* Ethan Bonavida
 * Enemy Stealth State-AI
 * Enemy stands still. Looks around spying for the player. When player walks in sights it attacks. 
 * 
 * 
 * -.DDMMYY
 * 0.0.01 = bug fix, updates, trying small changes
 * 0.1.-  = major changes, rewriting, changed logic  
 * 1.0.00 = GOLD?
 * Version: 
 * 0.1.00.112219; Created script, copied from old chase script again and updated. 
 * -.112919; add functionality with boss, and add in-depth  comments. 
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
 * This is the Stealth State. a more advanced state, where enemy stays in place and rotates around and then detects the player in LOS
 * Change or Update to however you want the enemy to be stealthy. 
 * 
 */

// STEALTH STATE- inherits from BaseAIState for the enemy_object and Tick() function
public class StealthState : BaseAIState
{
    private EnemyAI _obj;
    private float rotSpeed = 12f;
    
    private float distance = 30f;

    public void Start()
    {
        Physics2D.queriesStartInColliders = false;
    }

    // Stealth State constructor, is called upon when this object is instantiated. also calls base(baseAIState) constructor
    // so grab the enemy this script is now attatched too.
    public StealthState(EnemyAI obj) : base(obj.gameObject)
    {
        _obj = obj;
    }
     
    // Tick() gets called every frame
    public override Type Tick()
    {
        if (_obj.Target == null)
        {
            _obj.SetTarget(GameManager.player_obj.transform);
        }

        // cast a ray foward, then will rotate
        RaycastHit hitInfo;
        // rotates on y-axis? so spins correctly
        transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
        // if ray hits something check if it hit player
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, distance))
        {
            Debug.DrawRay(transform.position, transform.forward, Color.red, distance);

            if (hitInfo.collider.CompareTag("Player"))
            {
                //DETECTION STUFF HERE. chase/attack/whatever the fuck with player
                if (_obj.chase == true) return typeof(ChaseState);
            }
        }          
        else
        {
            Debug.DrawLine(transform.position, transform.position + transform.forward * distance, Color.green);
        }
        return null;
    }
}
