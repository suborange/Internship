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
 * 0.0.00.112219; Created script after finding a good resource for AI state machine.
 * -.112919; after thouroughly researching, and finally understanding. I got started writing this baseAI, Statemachine, and EnemyAI classes. 
 * 1.0.00.120319; Base state has been complete; GOLD?
 * 
 * 
 * */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*******************************************************************************************************************************************************************************************************************************
/*  HOW TO USE THISE SCRIPT:
 * This is the Base state. All other states do, or can, inherit from this base class. 
 * It contains a Tick() method, and assigns the correct gameobjects and transforms to their respective scripts.  These get inherited from the other States.
 * This class will most likely not change. Changes to this class will affect all the ther state classes. 
 * 
 */

// BASE STATE- allows instantiation of enemy_object and Tick() function
public abstract class BaseAIState 
{
    protected GameObject game_object;
    protected Transform transform;

    //Tick function that every state will need to have, where state functionality goes. 
    public abstract Type Tick();
    
    // Base State constructor, is called upon when a new object is instantiated. 
    // so grabs the enemy the script is attached too and sets it to the base.
    public BaseAIState(GameObject gameobject)
    {
        this.game_object = gameobject;
        this.transform = gameobject.transform;
    }

}
