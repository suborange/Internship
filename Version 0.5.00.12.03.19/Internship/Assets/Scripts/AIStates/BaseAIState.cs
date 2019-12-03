/* Ethan Bonavida
 * Enemy Idle State-AI
 * Enemy does nothing, after a # of seconds it will change to another state. 
 * Default new state set to WanderState for both Melee and Ranged. 
 * Boss state is idle until player gets close enough, then it startschasing the player in either-or Melee-Ranged.
 * 
 * 
 * -.DDMMYY
 * 0.0.01 = bug fix, updates, trying small changes
 * 0.1.-  = major changes, rewriting, changed logic  
 * 1.0.00 = GOLD?
 * Version: 
 * 0.0.00.112219; Created script
 * 
 * 
 * */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*******************************************************************************************************************************************************************************************************************************
/*  HOW TO USE THISE SCRIPT:
 * 
 * 
 * 
 * 
 */

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
