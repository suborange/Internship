using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAIState 
{
    protected GameObject game_object;
    protected Transform transform;

    //Tick function that every state will need to have, where state functionality goes. 
    public abstract Type Tick();

    public BaseAIState(GameObject gameobject)
    {
        this.game_object = gameobject;
        this.transform = gameobject.transform;
    }



}
