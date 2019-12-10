/* Ethan Bonavida
 * Enemy Melee Explores around the map AI
 * enemy will possibly patrol around randomly unitl a certain smaller range of player
 * or will follow a pattern until a certain smaller range of the player
 * OR will strictly patrol around certain points, or random points. 
 * 
 * 0.0.00.111619; goes from random point to random point, with small pause inbetween. does not interact with player. 
 * 
 * 
 * -.DDMMYY
 * 0.0.01 = bug fix, updates,  trying small changes
 * 0.1.-  = major changes, rewriting, changed logic  
 * 1.0.00 = GOLD?
 * Version: 
 * 0.0.00.101619; Created script, added basic variables
 * -.111619; Found good resource on a patroling script from point to point. Can add or subtract any points, even option to patrol within a room/square area randomly. 
 * 
 * 
 * */


//ALSO FINISH THE OTHER SCRIPTS, AND GET CRACKIN ON RANGED ONES BEFORE TOMORROW!!!


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeExploreMap : MonoBehaviour
{
    
    public Transform[] move_points;

    int randomspot;
    float speed;
    float waitTime;
    float startWaitTime;
    bool wait;

    //  TO PATROL AROUND WHOLE ROOM
    /* float minX;
     * float minY;
     * float maxX;
     * float maxY;
     *  
     * 
     * 
     * */


    // Start is called before the first frame update
    void Start()
    {
        randomspot = Random.Range(0, move_points.Length);
        speed = 10f;
        wait = false;
        startWaitTime = 2f;
        waitTime = startWaitTime;

        //TO PATROL RANDOMELY AROUND WHOLE ROOM
        //move_point.position = new Vector3(random.Range(minX, maxX), Random.Range(minY, maxY));
       


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, move_points[randomspot].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, move_points[randomspot].position) < 0.2f)
        {

            if (waitTime <=0)
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

}  
