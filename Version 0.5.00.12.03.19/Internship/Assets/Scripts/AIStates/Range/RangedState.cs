using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : BaseAIState
{
    private EnemyAI _obj;

    public override Type Tick()
    {






        return null;
    }

    public RangedState(EnemyAI obj) : base(obj.gameObject)
    {
        _obj = obj;        
    }
 
}
