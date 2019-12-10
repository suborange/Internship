/* Ethan Bonavida
 * Enemy Ranged SniperShoot AI
 * Enemy stays still, or move very quitely and slowly. guards certain areas and should lock onto player , but can be out run with sights on player.
 * 
 * 
 * 
 * -.DDMMYY
 * 0.0.01 = bug fix, updates, trying small changes
 * 0.1.-  = major changes, rewriting, changed logic  
 * 1.0.00 = GOLD?
 * Version: 
 * 0.0.00.110119; Created script
 * -.01.110419; wrote some variables 
 * -.02.112319; finally used the same concept from melee stealth, and will give sniper bigger "sights" and a lock on mechanic. 
 * -.03.112619; got the multi line working out, just a bit buggyt will have to look into again.
 * 
 * */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// sniper like tracking, shooting?, accuracy? moving target till some speed?

public class RagnedSnipershoot : MonoBehaviour
{

    float rotSpeed;
    RaycastHit2D hitInfo;
    RaycastHit2D hitInfo2;
    RaycastHit2D hitInfo3;
    Vector3 rays_;
    float distance;


    // Start is called before the first frame update
    void Start()
    {
        rotSpeed = 6f;
        distance = 30f;
        Physics2D.queriesStartInColliders = false;
        rays_ = new Vector3(0, 0, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
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
}
