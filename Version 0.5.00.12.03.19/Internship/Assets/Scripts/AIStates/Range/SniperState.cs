using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperState : BaseAIState
{
    private EnemyAI _obj;

    float rotSpeed;
    RaycastHit2D hitInfo;
    RaycastHit2D hitInfo2;
    RaycastHit2D hitInfo3;
    Vector3 rays_;
    float distance;

    private void Awake()
    {
        rotSpeed = 6f;
        distance = 30f;
        Physics2D.queriesStartInColliders = false;
        rays_ = new Vector3(0, 0, 1);
    }

    public SniperState(EnemyAI obj) : base(obj.gameObject)
    {
        _obj = obj;
    }


    public override Type Tick()
    {
        transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);
        hitInfo = Physics2D.Raycast(transform.position, transform.right, distance);

        if (hitInfo.collider != null)
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.red);

            if (hitInfo.collider.CompareTag("Player"))
            {
                //DETECTION STUFF HERE. chase/attack/whatever the fuck with player
                // "LOCKS ON" to player location scouting with sight at a certain pace(that can be outrun?)

            }
            else
            {
                Debug.DrawLine(transform.position, transform.position + transform.right * distance, Color.green);
                //Debug.DrawLine((transform.position + rays_), transform.position + transform.right * distance, Color.green);
                //Debug.DrawLine((transform.position + rays_), transform.position + transform.right * distance, Color.green);
            }
        }

        //CONDITION TO CHANGE THE STATES


        return null;
    }

  

}
