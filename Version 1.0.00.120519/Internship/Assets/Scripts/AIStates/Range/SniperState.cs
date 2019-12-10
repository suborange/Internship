/* Ethan Bonavida
 * Enemy Sniper State-AI
 * Enemy chases the player around. Simple as that
 * 
 * 
 * -.DDMMYY
 * 0.0.01 = bug fix, updates, trying small changes
 * 0.1.-  = major changes, rewriting, changed logic  
 * 1.0.00 = GOLD?
 * Version: 
 * 0.1.00.112219; Created script, copied from old chase script again and updated. 
 * -.112919; add functionality with boss, and add some in-depth  comments. 
 * 
 * 
 * 
 * */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//*******************************************************************************************************************************************************************************************************************************
/*  HOW TO USE THIS SCRIPT:
 * This is the Sniper State. a more advanced state, were the enemy is far away, and has detected the player. will shoot the player from longer ranges
 * Change or Update to however you want the enemy to Snipe the player. 
 * 
 */

// SNIPER STATE- inherits from BaseAIState for the enemy_object and Tick() function
public class SniperState : BaseAIState
{
    private EnemyAI _obj;

    float rotSpeed;
    RaycastHit2D hitInfo;
    RaycastHit2D hitInfo2;
    RaycastHit2D hitInfo3;
    Vector3 rays_;
    float distance;

    private void Awake()
    {
        rotSpeed = 6f;
        distance = 30f;
        Physics2D.queriesStartInColliders = false;
        rays_ = new Vector3(0, 0, 1);
    }

    // Sniper State constructor, is called upon when this object is instantiated. also calls base(baseAIState) constructor
    // so grab the enemy this script is now attatched too.
    public SniperState(EnemyAI obj) : base(obj.gameObject)
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
                // "LOCKS ON" to player location scouting with sight at a certain pace(that can be outrun?)

            }
            else
            {
                Debug.DrawLine(transform.position, transform.position + transform.right * distance, Color.green);
                //Debug.DrawLine((transform.position + rays_), transform.position + transform.right * distance, Color.green);
                //Debug.DrawLine((transform.position + rays_), transform.position + transform.right * distance, Color.green);
            }
        }

        //CONDITION TO CHANGE THE STATES


        return null;
    }

  

}
