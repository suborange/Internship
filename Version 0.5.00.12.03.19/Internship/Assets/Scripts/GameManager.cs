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

public class GameManager : MonoBehaviour
{
    [SerializeField] private float speed= 3.5f;
    public static float Speed => Instance.speed;

    [SerializeField] private float rangeaggroRadius = 25f;
    public static float RangeAggroRadius => Instance.rangeaggroRadius;

    [SerializeField] private float meleeaggroRadius =10f;
    public static float MeleeAggroRadius => Instance.meleeaggroRadius;

    [SerializeField] private float meleeattackRange = 1f;
    public static float MeleeAttackRange => Instance.meleeattackRange;

    [SerializeField] private float leaveAttackRange = 4f;
    public static float LeaveAttackRange => Instance.leaveAttackRange;

    [SerializeField] private GameObject playerObject;
    public static GameObject player_obj => Instance.playerObject;

    

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        // if you forget to set the object to the player, should hopefully find the player object then.
        if(playerObject == null)
        {
            playerObject = GameObject.FindWithTag("Player");
        }
    }

        //think about returning something
    public void ForCoroutine(IEnumerator state)
    {
        StartCoroutine(state);        
    }

    //if coroutines stop, might need to reset some values. 
    public void stopCoroutines()
    {
        StopAllCoroutines();
    }


}
