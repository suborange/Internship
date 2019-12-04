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
 * -.01.112919; add functionality and made sure it does work with the boss phase
 * -.01.120119; rechecked logic, added in-depth comments. 
 * 1.0.01.120319; GOLD? 
 * 
 * 
 * */


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*******************************************************************************************************************************************************************************************************************************
/*  HOW TO USE THIS SCRIPT:
 * This is the Charge State. a more advanced state where based on conditions, shoud charge and attack the player. it simply charges towards the players location in range 
 * Change or Update to however you want the enemy to Charge the player. 
 * 
 * 
 * 
 */

// CHARGE STATE- inherits from BaseAIState for the enemy_object and Tick() function
public class ChargeState : BaseAIState
{
    protected EnemyAI _obj;

    protected Vector3 attack_coords;
    protected bool start_charge;
    protected bool charge;
    protected bool charge_att;
    protected bool trigger;
    protected float charge_speed;
    protected float slow_speed; //maybe change this number for multiplying with the GameManager Speed


    // Charge State constructor, is called upon when this object is instantiated. also calls base(baseAIState) constructor
    // so grab the enemy this script is now attatched too.
    public ChargeState(EnemyAI enemy_obj) : base(enemy_obj.gameObject)
    {
        _obj = enemy_obj;
    }

    void Awake()
    {
        //start_charge is true when we want enemy to charge. Will have in-range check before.
        start_charge = false;
        charge_att = false;
        trigger = false;

        //charge is true when enemy is moving/charging
        charge = false;
        slow_speed = 0.72f;
    }

    // to charge the enemy, with time yields
    private IEnumerator Charge()
    {
        Debug.Log("STARTED CORROUTINE!!!");
        start_charge = false;
        //Debug.Log("One second");
        yield return new WaitForSeconds(2f);
        //Debug.Log("Charging...attacking...");
        attack_coords = _obj.transform.position - transform.position;

        // change chargespeed to based of distance from player. charge faster closer to player, slower the farther.
        charge_speed = (Vector3.Distance(transform.position, _obj.transform.position) / 5.2f); // get distance then / 5.2
        charge_speed = charge_speed < 1.65f ? 1.65f : charge_speed; // if charge_speed < Val, true: charge_speed = Val, fasle: charge_speed = charge_speed
        charge_speed = charge_speed > 2.11f ? 2.11f : charge_speed; // if charge_speed > Val, true: charge_speed = Val, fasle: charge_speed = charge_speed
                                                                    // Debug.Log(charge_speed);
        charge = true;
        yield return new WaitForSeconds(.45f);
        charge = false;
        trigger = false;
        //Debug.Log("STOP?");
        
    }

    // pause inbetween charging and another action.
    private IEnumerator WaitCharge()
    {
        slow_speed = 0;
        yield return new WaitForSeconds(0.75f);
        slow_speed = 0.86f;
        start_charge = true;
    }


    // Tick() gets called every update frame
    public override Type Tick()
    {
        // need to check if for aggro? should just need target when in aggro state so shouldnt need it. 
        if (_obj.Target == null)
            return typeof(WanderState);
        //if (charge_att == true)
        //{

        if (trigger == false)
        {

            //StartCoroutine(WaitCharge());
            GameManager.Instance.ForCoroutine(WaitCharge());
            trigger = true;
        }
        if (start_charge == true)
        {
            //StartCoroutine(Charge());
            GameManager.Instance.ForCoroutine(Charge());
        }
        // enemy will slowly move towards player, for aggressiveness
        else if (start_charge == false && charge == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _obj.transform.position, slow_speed * Time.deltaTime);
        }
        // now enemy charges right towards the player
        else if (charge == true)
        {
            //transform.position = Vector3.MoveTowards(transform.position, attack_coords, charge_speed * Time.deltaTime);
            // try think of way to go past player a little bit.
            transform.position += attack_coords * charge_speed * Time.deltaTime;
        }
        // }

        //ENTER CHANGE STATE CONDITIONS HERE
        // when charging enemy gets too far from the player, enemy goes back to wander state.
        if (Vector3.Distance(_obj.transform.position, GameManager.player_obj.transform.position) > (GameManager.MeleeAggroRadius + 6f))
        {
            GameManager.Instance.stopCoroutines();
            return typeof(WanderState);
        }

        return null;
    }

    



}
