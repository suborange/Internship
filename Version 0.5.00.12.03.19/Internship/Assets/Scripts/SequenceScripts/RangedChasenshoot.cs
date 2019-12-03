/* Ethan Bonavida
 * Enemy Ranged Chase and Shoot AI
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
 * -.01.110419; Started writing variables and basic in-range checks. 
 * -.02.110919; finished adding chase movement and placement holder for shooting actions. 
 * 
 * 
 * */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedChasenshoot : MonoBehaviour
{

    public GameObject player_obj;
    public BoxCollider this_col;
    Vector3 movetowards;
    Vector3 col_attsize;
    Vector3 col_outofsightsize;
    float speed;
    float step;
    bool chasenshoot_att;



    // Start is called before the first frame update
    void Start()
    {
        chasenshoot_att = false;
        speed = 3f;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (chasenshoot_att == true)
        {
            step = speed * Time.deltaTime;
            player_obj.transform.position = Vector3.MoveTowards(transform.position, player_obj.transform.position, step);

            Shoot();

        }

    }

    private void Shoot()
    {
        //insert shooting code into here. 

    }


    //  PLAYER ENTERS RADIUS, ATTACK PLAYER
    private void onTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            chasenshoot_att = true;
           this_col.size = col_attsize;

        }


    }

    //PLAYER LEAVES BIGGER RADIUS, STOP ATTACKING PLAYER
    private void onTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chasenshoot_att = false;
            this_col.size = col_outofsightsize;

        }


    }
}
