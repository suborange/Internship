using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthState : BaseAIState
{
    private EnemyAI _obj;

    float rotSpeed;
    RaycastHit2D hitInfo;
    float distance;


    void Awake()
    {
        rotSpeed = 12f;
        distance = 30f;
        Physics2D.queriesStartInColliders = false;
    }

    public StealthState(EnemyAI obj) : base(obj.gameObject)
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

            }

        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + transform.right * distance, Color.green);
        }


        //condition to return with the changed state type




        return null;
    }

    
}
