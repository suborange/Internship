/* Ethan Bonavida
 * Enemy Stealth State-AI
 * Enemy chases the player around. Simple as that
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
 * 
 * 
 * */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//*******************************************************************************************************************************************************************************************************************************
/*  HOW TO USE THIS SCRIPT:
 * This is the Stealth State. a more advanced state, where enemy rotates around and then detects the player in LOS
 * Change or Update to however you want the enemy to be stealthy. 
 * 
 */

// STEALTH STATE- inherits from BaseAIState for the enemy_object and Tick() function
public class StealthState : BaseAIState
{
    private EnemyAI _obj;
    float rotSpeed;
    RaycastHit2D hitInfo;
    float distance;

    void Awake()
    {
        rotSpeed = 12f;
        distance = 30f;
        Physics2D.queriesStartInColliders = false;
    }

    // Stealth State constructor, is called upon when this object is instantiated. also calls base(baseAIState) constructor
    // so grab the enemy this script is now attatched too.
    public StealthState(EnemyAI obj) : base(obj.gameObject)
    {
        _obj = obj;
    }

    public override Type Tick()
    {
        transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);
        hitInfo = Physics2D.Raycast(transform.position, transform.right, distance);

        if (hitInfo.collider != null)
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);

            if (hitInfo.collider.CompareTag("Player"))
            {
                //DETECTION STUFF HERE. chase/attack/whatever the fuck with player
                return typeof(ChaseState);
                // also think of other for ranged shooting. maybe sniper?
            }
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + transform.right * distance, Color.green);
        }


        //condition to return with the changed state type




        return null;
    }

    
}
