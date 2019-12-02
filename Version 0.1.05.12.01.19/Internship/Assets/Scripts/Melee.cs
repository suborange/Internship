/* Ethan Bonavida
 * Enemy Melee  AI
 * Enemy has several choices and will go between the different choices smoothly and effectively.
 * 
 * 
 * 
 * -.DDMMYY
 * 0.0.01 = bug fix, updates, trying small changes
 * 0.1.-  = major changes, rewriting, changed logic  
 * 1.0.00 = GOLD?
 * Version: 
 * 0.0.10.101619; Created script
 * -.01.112419; Now that most parts are complete, this is the test script for EnemyAI Melee controller. Moved each part into own functions. 
 * -.02.112519;  updated any functions that i changed from test scripts. 
 * 
 * */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// serialized bool for each ai type? can maybe turn on or off? 
// also figure out logic and pattern order. like PEMDAS, which action checks for first? (wall/boundry check) etc.
//rotation based on player = +transfrom.right, - transfrom.right(left)
//add mouse controls for player to showcase the enemy in 3D. 

public class Melee : MonoBehaviour
{
    [SerializeField] bool Chase_;
    [SerializeField] bool Charge_;
    [SerializeField] bool Explore_;
    [SerializeField] bool Stealth_;
    [SerializeField] bool LOS_;
    public GameObject player_obj;
    public BoxCollider this_col;

    Vector3 movetowards;
    Vector3 attack_coords;
    Vector3 col_attsize = new Vector3(25, 1, 25);
    Vector3 col_outofsightsize = new Vector3(16, 1, 16);

    float speed;
    float step;
    bool player_att;
    bool start_charge;
    bool charge;
    bool trigger;
    float charge_speed;
    float slow_speed = .42f;

    float rotSpeed;
    RaycastHit2D hitInfo;
    float distance;

    public Transform[] move_points;

    int randomspot;
    float waitTime;
    float startWaitTime;
    bool wait;


    // Start is called before the first frame update
    void Start()
    {
        // player is not in range, do no attack yet. For some actions
        player_att = false;

        // start_charge is true when we want enemy to charge. Will have in-range check before.
        start_charge = false;
        trigger = false;


        // charge is true when enemy is moving/charging
        charge = false;

        rotSpeed = 12f;
        distance = 30f;
        Physics2D.queriesStartInColliders = false;

        randomspot = Random.Range(0, move_points.Length);
        wait = false;
        startWaitTime = 2f;
        waitTime = startWaitTime;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // MAIN CONTROLLER
        // figure out which case first, then only run that function once-PER FRAME-
        //bool for each choice, choose which optoins on or off. Then it will go through conditions to choose what to do next. 
        //





    }
    //MELEE RUNAWAY?

    // MELEE CHASE
    void meleeChase()
    {
        if (player_att == true)
        {
            //Enemy move speed
            speed = 3.3f;
            //Finds player position and targets and chases player
            //Constantly move toward the player, while they are in ranfe of eachother. 
            step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player_obj.transform.position, step);
        }


    }
    
    // MELEE STRAIGHT LINE/CHARGE
    void meleeStraighLine()
    {
        if (player_att == true)
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
            else if (start_charge == false && charge == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, player_obj.transform.position, slow_speed * Time.deltaTime);
            }
            // now enemy charges right towards the player
            else if (charge == true)
            {                
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
        charge_speed = charge_speed < 1.6f ? 1.6f : charge_speed; // if charge_speed < Val, true: charge_speed = Val, fasle: charge_speed = charge_speed
        charge_speed = charge_speed > 2.22f ? 2.22f : charge_speed; // if charge_speed > Val, true: charge_speed = Val, fasle: charge_speed = charge_speed
        // Debug.Log(charge_speed);
        charge = true;
        yield return new WaitForSeconds(.45f);
        charge = false;
        trigger = false;
    }

    private IEnumerator WaitCharge()
    {
        slow_speed = 0;
        yield return new WaitForSeconds(0.75f);
        slow_speed = .72f;
        start_charge = true;


    }

    // MELEE STEALTH
    void meleeStealth()
    {
        transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);
        hitInfo = Physics2D.Raycast(transform.position, transform.right, distance);

        if (hitInfo.collider != null)
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);

            if (hitInfo.collider.CompareTag("Player"))
            {
                //DETECTION STUFF HERE. chase/attack/whatever the fuck with player

            }

        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + transform.right * distance, Color.green);
        }

    }

    // MELEE HARDCODE
    void meleeHardcode()
    {
        

    }





    // MELEE EXPLORE/WANDER
    void meleeExplore()
    {
        speed = 10f;
        transform.position = Vector3.MoveTowards(transform.position, move_points[randomspot].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, move_points[randomspot].position) < 0.2f)
        {

            if (waitTime <= 0)
            {
                //  TO PATROL AROUND THE WHOLE ROOM
                //move_point.position = new Vector3(random.Range(minX, maxX), Random.Range(minY, maxY));
                randomspot = Random.Range(0, move_points.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }

        }


    }
    
    //MELEE LOS, MOVES AROUND ENVIRONMENT, PATHING
    void meleeLOS() { }



    // ON PLAYER TRIGGER ENTER AND EXIT
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //when player is in range, enemy targets and attacks player. 
            player_att = true;
            this_col.size = col_attsize;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //when out of enemy range, enemy stops moving. Player got away. 
            player_att = false;
            //think about making this huge, or maybe based off which state it is in.or just keep simple with bigger range
            this_col.size = col_outofsightsize;
        }
    }

    
}
