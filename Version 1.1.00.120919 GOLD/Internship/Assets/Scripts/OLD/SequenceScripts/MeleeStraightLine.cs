/* Ethan Bonavida
 * Enemy Melee Charged attack AI
 * Enemy is till or slow moving as they charge for an attack in a straight line.Also now moves faster and slower to randomize the speeds.
 * 
 * 
 * 
 * -.DDMMYY
 * 0.0.01 = bug fix,  trying small changes
 * 0.1.-  = major changes, rewriting, changed logic  
 * 1.0.000 = GOLD?
 * Version: 
 * 0.0.00.101619; Created script
 * 0.0.01.103119; created varaibles and started adding logic for charging attack. pause followed by fast charge towards player. 
 * -.02.110119; Charge logic was not working correctly, fixed now. Should charge towrds player-still needs adjustments.
 * -.03.111719; added slow movement total, before charge.
 * -.04.112319; now charges at various speeds, based off player distance from the enemy. Also added a pause after the charge(recahrge(heh)). 
 * 
 * */



 // FIGURE OUT HOW TO CHARGE PAST PLAYER

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeStraightLine : MonoBehaviour
{
    public GameObject player_obj;
    public BoxCollider this_obj;
    Vector3 attack_coords;
    Vector3 col_attsize =  new Vector3(24, 1, 24);
    Vector3 col_outofsightsize = new Vector3(16,1,16);
    
    bool start_charge;
    bool charge;
    bool charge_att;
    bool trigger;
    float charge_speed;
    float slow_speed = .72f;
    

    // Start is called before the first frame update
    void Start()
    {
        //start_charge is true when we want enemy to charge. Will have in-range check before.
        start_charge = false;
        charge_att = false;
        trigger = false;

        //charge is true when enemy is moving/charging
        charge = false;
                
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(charge_att==true)
        {

            if (trigger == false)
            {

                StartCoroutine("WaitCharge");
                trigger = true;
            }
            if (start_charge == true)
            {
                StartCoroutine("Charge");
            }
            // enemy will slowly move towards player, for aggressiveness
            else if( start_charge == false && charge ==false )
            {
                transform.position = Vector3.MoveTowards(transform.position, player_obj.transform.position, slow_speed * Time.deltaTime);
            }
            // now enemy charges right towards the player
            else if (charge == true)
            {
                //transform.position = Vector3.MoveTowards(transform.position, attack_coords, charge_speed * Time.deltaTime);
               // try think of way to go past player a little bit.
                transform.position += attack_coords * charge_speed * Time.deltaTime;                
            }                           
        }                
    }


    private IEnumerator Charge()
    {
        start_charge = false;
        //Debug.Log("One second");
        yield return new WaitForSeconds(2f);
        //Debug.Log("Charging...attacking...");
        attack_coords = player_obj.transform.position - transform.position;
        // change chargespeed to based of distance from player. 
        charge_speed = (Vector3.Distance(transform.position, player_obj.transform.position) / 5.2f); // get distance then / 5.2
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //when player is in range, enemy targets and charge attack player.
            charge_att = true;
            //Debug.Log("CHARGE");
            this_obj.size = col_attsize;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //when out of enemy range, enemy stops moving. Player got away. 
            charge_att = false;
            //Debug.Log("STOP");
            this_obj.size = col_outofsightsize;
            trigger = false;
        }
    }

}
