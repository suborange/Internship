using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeState : BaseAIState
{
    private EnemyAI _obj;

    public override Type Tick()
    {



        return null;
    }

    public DodgeState(EnemyAI obj) : base (obj.gameObject)
    {
        _obj = obj;
    }

}
