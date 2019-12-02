using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideState : BaseAIState
{
    private EnemyAI _obj;

    public override Type Tick()
    {




        return null;
    }

    public HideState( EnemyAI obj) : base(obj.gameObject)
    {
        _obj = obj;
    }




}
