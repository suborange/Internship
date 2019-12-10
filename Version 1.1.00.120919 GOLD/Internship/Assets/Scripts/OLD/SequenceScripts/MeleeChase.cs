/* Ethan Bonavida
 * Enemy Melee Chase AI
 * Enemy just move towards the player, comeplete basic chase behavior
 * 
 * 
 * 
 * -.DDMMYY
 * 0.0.01 = bug fix, updates, trying small changes
 * 0.1.-  = major changes, rewriting, changed logic  
 * 1.0.00 = GOLD?
 * Version: 
 * 0.0.10.101619; Created script, added basic variables and defined in Start(), and in FixedUpdate() this gameobject to follow player position. Basic Chase
 * 0.0.10.103019; Rewrote -.101619 to this new script
 * -.110119; started adding in-range functionality. Only chases within certain range of the enemy. 
 * 
 * 
 * */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeChase : MonoBehaviour
{

    public GameObject player_obj;
    public BoxCollider this_col;
   
    Vector3 movetowards;
    Vector3 col_attsize = new Vector3(22, 1, 22);
    Vector3 col_outofsightsize = new Vector3(16, 1, 16);

    float speed;
    float step;
    bool chase_att;

    // Start is called before the first frame update
    void Start()
    {
            chase_att = false;
            //Enemy move speed
            speed = 3.3f;
            //Get player tramsform for position
         


        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (chase_att == true)
        {
            //Finds player position and targets and chases player
            //Constantly move toward the player, while they are in ranfe of eachother. 
            step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player_obj.transform.position, step);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //when player is in range, enemy targets and attacks player. 
            chase_att = true;
            Debug.Log("CHASE");
            this_col.size = col_attsize;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //when out of enemy range, enemy stops moving. Player got away. 
            chase_att = false;
            Debug.Log("STOP");
            this_col.size = col_outofsightsize;
        }
    }



}
