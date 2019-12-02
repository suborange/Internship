using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : BaseAIState
{
    private EnemyAI _obj;

    Vector3 att_pos;
    float speed;
    float stop_dist;
    float retreat_dist;
    bool runnshoot_att;


    private void Awake()
    {
        runnshoot_att = false;
        speed = 4f;
        stop_dist = 16f;
        retreat_dist = 8f;
    }

    public RunState(EnemyAI obj) : base(obj.gameObject)
    {
        _obj = obj;
    }

    public override Type Tick()
    {

        if (Vector3.Distance(transform.position, _obj.transform.position) > stop_dist)
        {
            transform.position = Vector3.MoveTowards(transform.position, _obj.transform.position, speed * Time.deltaTime);

        }
        //not too far, or not too close from the player, will shoot from here and not move. 
        else if (Vector3.Distance(transform.position, _obj.transform.position) < stop_dist &&
            (Vector3.Distance(transform.position, _obj.transform.position) > retreat_dist))
        {
            transform.position = this.transform.position;

        }
        //too close, runaway from the player
        else if (Vector3.Distance(transform.position, _obj.transform.position) < retreat_dist)
        {
            transform.position = Vector3.MoveTowards(transform.position, _obj.transform.position, -speed * Time.deltaTime);
        }


        //ADD CONDITION HER TO CHANGE THE STATE


        return null;
    }

   

}
