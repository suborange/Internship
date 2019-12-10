/* Ethan Bonavida
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
 * 1.1.00.120319; hours and hours of testing and debugging this script. so close to figuring it all out.
 * 1.2.00.120419; Figured it all out. Finalized touches, everything works in here(I really think) - GOLD!
 * 1.2.01.120919; gold was broke ofcourse, fixed most of the issue and should run much better now.
 * */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*******************************************************************************************************************************************************************************************************************************
/*  HOW TO USE THIS SCRIPT:
* This is the Wander State. An advanced state, were the enemy will wander and look around, and can detect the player. 
* Change or Update to however you want the enemy to wander around and detect the player. 
* 
*/

// WANDER STATE- inherits from BaseAIState for the enemy_object and Tick() function
public class WanderState : BaseAIState
{
    private Vector3? _destination;
    private float stopDistance = 1.5f;
    private float turnSpeed = 1f;
    private readonly LayerMask _layerMask = LayerMask.NameToLayer("Player"); // should find player mask
    private float _raySearchRange = 3f; 
    private float _rayDistance = 4f;
    private const float _rayRadius = 0.5f;
    private Quaternion _desiredRotation;
    private Vector3 _direction;
    private EnemyAI _obj;
    private bool _debug = false;
    private Collider _trigger;

    // angles for raycasting
    Quaternion startAngle = Quaternion.AngleAxis(-60, Vector3.up);
    Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);

    // Wander State constructor, is called upon when this object is instantiated. also calls base(baseAIState) constructor
    // so grab the enemy this script is now attatched too.
    public WanderState(EnemyAI enemy_obj) : base(enemy_obj.gameObject)
    {
        _obj = enemy_obj;
    }

    public void Start()
    {
        if (_obj._Melee == true)
        {
            _rayDistance = GameManager.MeleeAggroRadius;
        }
        else if (_obj._Range == true)
        {
            _rayDistance = GameManager.RangeAggroRadius;
        }
    }

    // Tick() gets called every update frame
    public override Type Tick()
    {
        // check if player is aggro range
        var chaseTarget = CheckForAggro();
        if (_debug == true)
        {
            _obj.SetTarget(GameManager.player_obj.transform);
        }
        if (chaseTarget != null )
        {
            // choose which state you want to change into.
            _obj.SetTarget(chaseTarget);
            if (_obj.chase == true) return typeof(ChaseState);
            if (_obj.dodge == true) return typeof(DodgeState);
            if (_obj.charge == true) return typeof(ChargeState);
            // else if (_obj._Range == true) return typeof(ShootState);
        }
        // if distance between enemy and player is too far, go into stealth mode
        //if (Vector3.Distance(transform.position, GameManager.player_obj.transform.position) >= (_rayDistance * 2.2f))
        //{
        //    if (_obj.stealth == true) return typeof(StealthState);
        //}
        // find new destination if there is no current destination
        if (_destination.HasValue == false )
        {
            FindRandomDestination();
        }
        // find new direction when approching previous distance target
        else if ( Vector3.Distance(transform.position, _destination.Value) < stopDistance)
        {
            FindRandomDestination();            
        }

        // rotate towards the desired location
        transform.rotation = Quaternion.Slerp(transform.rotation, _desiredRotation, Time.deltaTime * turnSpeed);
      
        // if foward is blocked return true to inside, false then continue on
        if (IsForwardBlocked())
        {
            Debug.Log("forward blocked");
            transform.rotation = Quaternion.Lerp(transform.rotation, _desiredRotation, 0.2f);
        }
        else
        {
            transform.Translate(Vector3.forward * Time.deltaTime * GameManager.Speed);
        }

        Debug.DrawRay(transform.position, _direction * _rayDistance, Color.red);

        // if path is blocked return true, and find new open destination
        if (IsPathBlocked())
        {
            Debug.Log("Wall");
            FindRandomDestination();

        }

        return null;
    }
    private bool IsForwardBlocked()
    {
        // cast a ray hit and determine if it hits the player or walls
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, _direction * _raySearchRange, Color.green);
        //var S = Physics.SphereCast(ray, _rayRadius, _raySearchRange, _layerMask.value);
        var R = Physics.Raycast(transform.position, transform.forward, out hit, _raySearchRange, _layerMask);
        //Debug.Log(R);
        return R;       

    }
    private bool IsPathBlocked()
    {
        // cast a ray hit and determine if it hits the player or walls
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, _direction * _raySearchRange, Color.green);
        //var S = Physics.SphereCast(ray, _rayRadius, _raySearchRange, _layerMask.value);
        var R = Physics.Raycast(transform.position, transform.forward, out hit, _raySearchRange);
        //Debug.Log(R);
        return R;
    }

    // FOR BETTER AREA CHECKING, CAN ADD QUICK CHECK OF RIGHT, LEFT, AND BACKWARDS?
    private void FindRandomDestination()
    {
        // get random close-by coordinates to go to next. 
        var _random = (UnityEngine.Random.Range( -3, 3));
        var _random1 = (UnityEngine.Random.Range(-3, 3));
        Vector3 testPosition = ((transform.position + transform.forward)+ new Vector3(_random, 0f, _random1));
        // new destination
        _destination = new Vector3(testPosition.x, 1f, testPosition.z);

        // new direction
        _direction = Vector3.Normalize(_destination.Value - transform.position);
        _direction = new Vector3(_direction.x, 0f, _direction.z);
        // new rotation
        _desiredRotation = Quaternion.LookRotation(_direction);
        Debug.Log("New Direction");
    }

    private Transform CheckForAggro()
    {
        // cast a rayhit outwards for detection
        RaycastHit hit;
        var angle = transform.rotation * startAngle;
        var direction = angle * Vector3.forward;
        var pos = transform.position;

        for (byte i = 0; i < 15; i++)
        {
            // direction = ((angle * stepAngle) * Vector3.forward);
            // shoot out a raycast in each direction
            if (Physics.Raycast(pos, direction, out hit, _rayDistance))
            {
                //NEED TO LOOK FURTHER INTO THIS
                var gobj = hit.collider; //grab the object, then if its player, return that position now and target the player
                if (gobj.gameObject == GameManager.player_obj.gameObject)
                {
                    Debug.Log("PLAYER FOUND");
                    Debug.DrawRay(pos, direction * hit.distance, Color.red);
                    return gobj.transform;
                } // other wise debug a ray
                else { Debug.DrawRay(pos, direction * hit.distance, Color.yellow); }
            }
        }
        return null;
    }    
}
