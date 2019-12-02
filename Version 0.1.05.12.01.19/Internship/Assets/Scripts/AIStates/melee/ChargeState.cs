using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    

    public ChargeState(EnemyAI obj) : base(obj.gameObject)
    {
        _obj = obj;
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

    private IEnumerator Charge()
    {
        Debug.Log("STARTED CORROUTINE!!!");
        start_charge = false;
        //Debug.Log("One second");
        yield return new WaitForSeconds(2f);
        //Debug.Log("Charging...attacking...");
        attack_coords = _obj.transform.position - transform.position;
        // change chargespeed to based of distance from player. 
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

    private IEnumerator WaitCharge()
    {
        slow_speed = 0;
        yield return new WaitForSeconds(0.75f);
        slow_speed = 0.86f;
        start_charge = true;
    }


    //EVERY FRAME
    public override Type Tick()
    {

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


        return null;
    }

    



}
