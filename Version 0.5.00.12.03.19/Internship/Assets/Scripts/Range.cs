/* Ethan Bonavida
 * Enemy Range AI
 * Enemy just move towards the player, comeplete basic chase behavior
 * 
 * 
 * 
 * -.DDMMYY
 * 0.0.01 = bug fix, updates, trying small changes
 * 0.1.-  = major changes, rewriting, changed logic  
 * 1.0.00 = GOLD?
 * Version: 
 * 0.0.01.101619; Created script
 * -.01.112419; Now that most parts are complete, this is the test script for EnemyAI Ranged controller. Moved each part into own functions. 
 * -.03.112519; updated with sniper script, 
 * */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// serialized bool for each ai type? can maybe turn on or off? 
// also figure out logic and pattern order. like PEMDAS, which action checks for first? (wall/boundry check) etc.

public class Range : MonoBehaviour
{

    Vector3 col_attsize;
    Vector3 col_outofsightsize;
    public GameObject player_obj;
    public BoxCollider this_col;

    bool player_att;

    float speed;
    float stop_dist;
    float retreat_dist;
    bool runnshoot_att;

    //sniper
    float rotSpeed;
    RaycastHit2D hitInfo;
    RaycastHit2D hitInfo2;
    RaycastHit2D hitInfo3;
    Vector3 rays_;
    float distance;


    // Start is called before the first frame update
    void Start()
    {
        player_att = false;
        runnshoot_att = false;
        stop_dist = 16f;
        retreat_dist = 8f;

        //sniper
        rotSpeed = 6f;
        distance = 30f;
        Physics2D.queriesStartInColliders = false;
        rays_ = new Vector3(0, 0, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // MAIN CONTROLLER
        // figure out which case first, then only run that function once-PER FRAME-
        //

    }


    // RANGE CHASE THE PLAYER AND SHOOT THEM
    void rangeChase()
    {
        speed = 3f;
        player_obj.transform.position = Vector3.MoveTowards(transform.position, player_obj.transform.position, speed * Time.deltaTime);

        //insert shooting code into here. 


    }

    //RANGE HIDE AND RUN BEHIND COVER
    void rangeHide()
    {
        speed = 3f;
        transform.position = Vector3.MoveTowards(transform.position, player_obj.transform.position, -1 * speed * Time.deltaTime);

    }

    // RANGE RUN AWAY AND SHOOT FOR CERTAIN DISTANCE FROM THE PLAYER. 
    void rangeRun()
    {
        speed = 4f;
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

    }

    // RANGE SNIPER
    void rangeSniper()
    {
        transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);
        hitInfo = Physics2D.Raycast(transform.position, transform.right, distance);
        hitInfo2 = Physics2D.Raycast(transform.position, transform.right + rays_, distance);
        hitInfo3 = Physics2D.Raycast(transform.position, transform.right + -rays_, distance);


        if (hitInfo.collider != null)
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);

            if (hitInfo.collider.CompareTag("Player"))
            {
                //DETECTION STUFF HERE. chase/attack/whatever the fuck with player
                // "LOCKS ON" to player location scouting with sight at a certain pace(that can be outrun?)

            }

        }
        else if (hitInfo2.collider != null)
        {
            Debug.DrawLine(transform.position, hitInfo2.point, Color.red);

            if (hitInfo2.collider.CompareTag("Player"))
            {
                //DETECTION STUFF HERE. chase/attack/whatever the fuck with player
            }
        }
        else if (hitInfo3.collider != null)
        {
            Debug.DrawLine(transform.position, hitInfo3.point, Color.red);

            if (hitInfo3.collider.CompareTag("Player"))
            {
                //DETECTION STUFF HERE. chase/attack/whatever the fuck with player

            }
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + transform.right * distance, Color.green);
            Debug.DrawLine((transform.position + rays_), transform.position + transform.right * distance, Color.green);
            Debug.DrawLine((transform.position + rays_), transform.position + transform.right * distance, Color.green);
        }


    }


    // ENTER SHOOTING CODE HERE
    void Shoot() { }

    //  PLAYER ENTERS RADIUS, ATTACK PLAYER
    private void onTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            player_att = true;
            this_col.size = col_attsize;
        }
    }
    //PLAYER LEAVES BIGGER RADIUS, STOP ATTACKING PLAYER
    private void onTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player_att = false;
            this_col.size = col_outofsightsize;
        }
    }
}
