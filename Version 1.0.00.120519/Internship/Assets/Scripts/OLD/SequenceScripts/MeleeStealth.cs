/* Ethan Bonavida
 * Enemy Melee stealth AI
 * stays still and looks around waiting to detect los of player. this alerts the enmy which can then attack, etc
 * 
 * 
 * -.DDMMYY
 * 0.0.01 = bug fix, updates,  trying small changes
 * 0.1.-  = major changes, rewriting, changed logic
 * 1.0.00 = GOLD?
 * Version: 
 * 0.0.00.111619; Created script, added varaibles and functionality for stealth enemy. 
 * -.112319; Found a great resource to demonstrate an alert enemy for stealth players. Allows to draw debug line and works well and is modifyable.
 * 
 * 
 * */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeStealth : MonoBehaviour
{


    float rotSpeed;
    RaycastHit2D hitInfo;
    float distance;
    

    // Start is called before the first frame update
    void Start()
    {
        rotSpeed = 12f;
        distance = 30f;
        Physics2D.queriesStartInColliders = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * rotSpeed *Time.deltaTime);
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
}
