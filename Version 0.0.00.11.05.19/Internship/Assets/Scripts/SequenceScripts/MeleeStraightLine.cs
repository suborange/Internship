/* Ethan Bonavida
 * Enemy Melee  AI
 * 
 * 
 * 
 * 
 * -.DDMMYY
 * 0.0.01 = bug fix,  trying small changes
 * 0.0.1 = major changes, rewriting, changed logic
 * 0.1.-   
 * 1.0.000 = GOLD?
 * Version: 
 * 0.0.00.101619; Created script
 * 0.0.10.103119; created varaibles and started adding logic for charging attack. pause followed by fast charge towards player. 
 * -110119; Charge logic was not working correctly, fixed now. Should charge towrds player-still needs adjustments.
 * 
 * 
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeStraightLine : MonoBehaviour
{
    public GameObject player_obj;
    public BoxCollider this_obj;
    Vector3 attack_coords;
    Vector3 col_attsize =  new Vector3(22, 1, 22);
    Vector3 col_outofsightsize = new Vector3(16,1,16);
    
    bool start_charge;
    bool charge;
    bool charge_att;
    bool trigger;
    float charge_speed = 1.8f;
    float step;
    

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
            if(trigger==false)
            {
                StartCoroutine("Wait");
                trigger = true;
            }

            if (start_charge == true)
            {
                StartCoroutine("Charge");
                //Debug.Log("Started Corroutine...");

            }
            else if (charge == true)
            {

                transform.position += attack_coords * charge_speed * Time.deltaTime;
                

            }
            
               
        }
        

        
    }


    private IEnumerator Charge()
    {
        start_charge = false;
        //Debug.Log("One second");
        yield return new WaitForSeconds(2.5f);
        //Debug.Log("Charging...attacking...");
        attack_coords = player_obj.transform.position-transform.position; // - transform.position;
        charge = true;
        yield return new WaitForSeconds(.38f);
        charge = false;
        //Debug.Log("STOP?");
        start_charge = true;
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1f);
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
