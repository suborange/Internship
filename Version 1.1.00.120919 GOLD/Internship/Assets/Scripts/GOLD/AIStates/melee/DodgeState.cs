﻿/* Ethan Bonavida
 * Enemy Dodge State-AI
 * Enemy dodges around, right to left and left to right. 
 * 
 * 
 * -.DDMMYY
 * 0.0.01 = bug fix, updates, trying small changes
 * 0.1.-  = major changes, rewriting, changed logic  
 * 1.0.00 = GOLD?
 * Version: 
 * 0.0.00.112219; Created script
 * * -.01.113019;  Fleshed out this inherited script with whats needed. Add dodge functionalities 
 * -.120219; added way for both melee and ranged. added in-depth comments 
 * 1.1.00.120319; GOLD?
 * 1.2.00.120419; Finalized touches, everything works in here(I really think) - GOLD!
 * */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*******************************************************************************************************************************************************************************************************************************
/*  HOW TO USE THIS SCRIPT:
 * This is the Dodge State. A state that is a little more fun, it simply dodges right or left away from the players. 
 * Change or Update to however you want the enemy to dodge from the player.
 *  Dodges max 4 times in a row, then changes to chase target
 *  
 */

// DODGE STATE- inherits from BaseAIState for the enemy_object and Tick() function
public class DodgeState : BaseAIState
{
    private EnemyAI _obj;
    private Transform _target;
    private float _speed;
    // bool for 50/50 left or right direction;
    private bool _LRdir;
    // lock/unlock the corroutine
    private bool _dodge;
    private bool _wait;
    private float _range;
    private int count;

    public void Start()
    {
        // find good dash speed
        
        count = 0;
        _LRdir = false;
        _dodge = false;
        _wait = false;
    }
    // Dodge State constructor, is called upon when this object is instantiated. also calls base(baseAIState) constructor
    // so grab the enemy this script is now attatched too.
    public DodgeState(EnemyAI enemy_obj) : base (enemy_obj.gameObject)
    {
        _obj = enemy_obj;        
    }

    // Coroutine decides direction and then dashes in Tick(), until _dodge=false
    private IEnumerator Dodge()
    {
        Debug.Log("DODGE");
        // swap between left and right
        if (_LRdir == false) _LRdir = true;
        else if (_LRdir == true) _LRdir = false;
        this.count++;
        yield return new WaitForSeconds(0.6f); // dodge for half a second
        _dodge = false;

        yield return new WaitForSeconds(2.5f); // shouldnt dodge for x seconds
        _wait = false;
    }

    // Tick() gets called every update frame
    public override Type Tick()
    {
        if (_obj.Target == null)
        {
            _obj.SetTarget(GameManager.player_obj.transform);
        }

        if (_obj._Melee == true)
        {
            _range = GameManager.MeleeAttackRange;
        }
        else if (_obj._Range == true)
        {
            _range = GameManager.RangeAggroRadius;
        }
        // dash 4 times then change to chase state
        if ( this.count >= 4)
        {
            if (_obj.chase == true)
            {
                GameManager.Instance.stopCoroutines();
                return typeof(ChaseState);
            }            
        }
        // if enemy is somehow in range to attack, and not when dodgin, attack the player.  
        if (Vector3.Distance(_obj.transform.position, _obj.Target.transform.position) <= _range && 
            _dodge == false)
        {
            if (_obj.attack == true)
            {
                GameManager.Instance.stopCoroutines();
                return typeof(AttackState);
            }            
        }
        // Dodged until far enough away from the player and  max range, enemy starts to wander around again. 
        else if (Vector3.Distance(_obj.transform.position, _obj.Target.transform.position) > (_range + 6f))
        {
            if (_obj.wander == true) return typeof(WanderState);
        }
        else
        {
            // start dodge- chooses L/R- then dashs            
            if (_dodge == false && _wait == false)
            {
                _dodge = true;
                _wait = true;
                GameManager.Instance.ForCoroutine(Dodge());
            }
            _speed = GameManager.Speed * 5;
            // then dash right
            if (_LRdir == false && _dodge == true) transform.position += _speed * transform.right * Time.deltaTime; //transform.position += speed * Vector3.right * Time.deltaTime;
            //then dash left
            if (_LRdir == true && _dodge == true) transform.position -= _speed *  transform.right * Time.deltaTime; //transform.position += speed * Vector3.left * Time.deltaTime;          
        }     
        return null;
    }  
}
