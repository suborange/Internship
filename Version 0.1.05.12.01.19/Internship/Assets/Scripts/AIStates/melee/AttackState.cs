﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseAIState
{

    private EnemyAI _obj;

    // Make player flash red from "Hit"

    public override Type Tick()
    {



        return null;
    }
    public AttackState(EnemyAI obj): base(obj.gameObject)
    {
        _obj = obj;
    }


}
