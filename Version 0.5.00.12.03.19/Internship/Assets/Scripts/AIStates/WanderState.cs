﻿/* Ethan Bonavida
 * Enemy Wander State-AI
 * Enemy wanders and looks around until it detcts the player object. One of the most important and interconnected states. 
 * 
 * -.DDMMYY
 * 0.0.01 = bug fix, updates, trying small changes
 * 0.1.-  = major changes, rewriting, changed logic  
 * 1.0.00 = GOLD?
 * Version: 
 * 0.0.00.112219; Created script 
 * -.01.112919;Fleshed out this inherited script with whats needed. added in-depth comments
 * 
 * 1.1.00.120319; GOLD?
 * */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*******************************************************************************************************************************************************************************************************************************
/*  HOW TO USE THIS SCRIPT:
* This is the Wander State. The most advanced state, were the enemy will wander and look around, and can detect the player. 
* Change or Update to however you want the enemy to wander around and detect the player. 
* 
*/

// WANDER STATE- inherits from BaseAIState for the enemy_object and Tick() function
public class WanderState : BaseAIState
{
    private Vector3? _destination;
    private float stopDistance = 1f;
    private float turnSpeed = 1f;
    private readonly LayerMask _layerMask = LayerMask.NameToLayer("Wall");
    private float _rayDistance = 3.5f;
    private Quaternion _desiredRotation;
    private Vector3 _direction;
    private EnemyAI _obj;
    private float _range;

    public WanderState(EnemyAI enemy_obj) : base(enemy_obj.gameObject)
    {
        _obj = enemy_obj;
    }

    // Tick() gets called every update frame
    public override Type Tick()
    {
        var chaseTarget = CheckForAggro();
        if (chaseTarget != null)
        {
            _obj.SetTarget(chaseTarget);
            return typeof(ChaseState);
            // else if (_obj._Range == true) return typeof(ShootState);
        }

        if (_destination.HasValue == false ||
            Vector3.Distance(transform.position, _destination.Value) <= stopDistance)
        {
            FindRandomDestination();
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, _desiredRotation, Time.deltaTime * turnSpeed);

        if (IsForwardBlocked())
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _desiredRotation, 0.2f);
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * GameManager.Speed);
        }

        Debug.DrawRay(transform.position, _direction * _rayDistance, Color.red);

        while (IsPathBlocked())
        {
            FindRandomDestination();
            Debug.Log("Wall");
        }

        return null;
    }
    private bool IsForwardBlocked()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        return Physics.SphereCast(ray, 0.5f, _rayDistance, _layerMask);

    }
    private bool IsPathBlocked()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        return Physics.SphereCast(ray, 0.5f, _rayDistance, _layerMask);
    }

    private void FindRandomDestination()
    {
        Vector3 testPosition = (transform.position + (transform.forward * 4f))
            + new Vector3(UnityEngine.Random.Range(-4.5f, 4.5f), 0f, UnityEngine.Random.Range(-4.5f, 4.5f));

        _destination = new Vector3(testPosition.x, 1f, testPosition.z);

        _direction = Vector3.Normalize(_destination.Value - transform.position);
        _direction = new Vector3(_direction.x, 0f, _direction.z);
        _desiredRotation = Quaternion.LookRotation(_direction);
        Debug.Log("Got Direction");
    }


    Quaternion startAngle = Quaternion.AngleAxis(-60, Vector3.up);
    Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);


    private Transform CheckForAggro()
    {
        if (_obj._Melee == true)
        {
            _range = GameManager.MeleeAggroRadius;
        }
        else if (_obj._Range == true)
        {
            _range = GameManager.RangeAggroRadius;
        }

        RaycastHit hit;
        var angle = transform.rotation * startAngle;
        var direction = angle * Vector3.forward;
        var pos = transform.position;

        for (byte i = 0; i < 24; i++)
        {
            if (Physics.Raycast(pos, direction, out hit, _range))
            {

                //NEED TO LOOK FURTHER INTO THIS
                var gobj = hit.collider.GetComponent<GameObject>();
                if (gobj.gameObject == GameManager.player_obj.gameObject) 
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.red);
                    return gobj.transform;
                }
                else { Debug.DrawRay(pos, direction * hit.distance, Color.yellow); }                
            }
        }
        return null;
    }
}
