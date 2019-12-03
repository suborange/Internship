/* Ethan Bonavida
 * Enemy Ranged Run and Shoot AI (flee)
 * Enemy just move towards the player, comeplete basic chase behavior
 * 
 * 
 * 
 * -.DDMMYY
 * 0.0.01 = bug fix, updates, trying small changes
 * 0.1.-  = major changes, rewriting, changed logic  
 * 1.0.00 = GOLD?
 * Version: 
 * 0.0.00.110119; Created script
 * 0.0.00.110419; Started writing variables and basic in-range checks. 
 * 
 * 
 * 
 * */

 //figure out the rotation so the enemy will face the player when in motion/attacking
 //THINK ABOUT ADDING PLACEHOLDER FOR SHOOTING CODE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedRunnshoot : MonoBehaviour
{

    public GameObject player_obj;
    Vector3 att_pos;
    Vector3 col_attsize;
    Vector3 col_outofsightsize;
    float speed;
    float stop_dist;
    float retreat_dist;
    bool runnshoot_att;



    // Start is called before the first frame update
    void Start()
    {
        runnshoot_att = false;
        speed = 4f;
        stop_dist = 16f;
        retreat_dist = 8f;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //att_pos = player_obj.transform.position - transform.position;

       // if (runnshoot_att == true)
        //{
            //too far away from the player
            if (Vector3.Distance(transform.position, player_obj.transform.position) > stop_dist)
            {
                transform.position = Vector3.MoveTowards(transform.position, player_obj.transform.position, speed * Time.deltaTime);

            }
            //not too far, or not too close from the player, will shoot from here and not move. 
            else if (Vector3.Distance(transform.position, player_obj.transform.position) < stop_dist && (Vector3.Distance(transform.position, player_obj.transform.position) > retreat_dist))
            {
                transform.position = this.transform.position;

            }
            //too close, runaway from the player
            else if (Vector3.Distance(transform.position, player_obj.transform.position) < retreat_dist)
            {
                transform.position = Vector3.MoveTowards(transform.position, player_obj.transform.position, -speed * Time.deltaTime);
            }






            //MY ATTEMPT
            //transform.Rotate(att_pos*Time.deltaTime);
            //if (Vector3.Distance(transform.position, player_obj.transform.position) < 10)
            //{
            //    //should riun away from the player, opposite direction
            //    transform.position -= att_pos * Time.deltaTime;

                //}

                //insert shooting code into here. 

       // }

    }

    //  PLAYER ENTERS RADIUS, ATTACK PLAYER
    //private void onTriggerEnter(Collider other)
    //{

    //    if (other.CompareTag("Player"))
    //    {

    //        runnshoot_att = true;
    //        this_col.size = col_attsize;

    //    }


    //}

    ////PLAYER LEAVES BIGGER RADIUS, STOP ATTACKING PLAYER
    //private void onTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        runnshoot_att = false;
    //        this_col.size = col_outofsightsize;

    //    }


    //}
}
