using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseAIState
{
    private EnemyAI _obj;
    private float step;

    public ChaseState (EnemyAI obj) : base(obj.gameObject)
    {
        _obj = obj;
    }
    
    public override Type Tick()
    {
        step = 3.3f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _obj.transform.position, step);


        //add conditions to change state
        //return with state Type

        return null;
    }
}
