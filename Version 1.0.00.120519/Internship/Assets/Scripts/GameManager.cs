/* Ethan Bonavida
 * Enemy Game Manager Script
 * Manages the game and game settings, etc. 
 * Change global game settings and such here
 * 
 * 
 * -.DDMMYY
 * 0.0.01 = bug fix, updates, trying small changes
 * 0.1.-  = major changes, rewriting, changed logic  
 * 1.0.00 = GOLD?
 * Version: 
 * 0.0.00.112219; Created script
 * -.01.112919; after understanding the state machine, fleshed out some GameManeger global settings that can be changed from the inspector view. 
 * -.02.113019; updated with some basic settings like speed and radius
 * -.03.120119; added more settings like player game object, and functions for corroutines. added in-depth comments
 * 1.2.00.120419; Finalized touches, everything works in here(I really think) - GOLD!
 * 
 * */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//*******************************************************************************************************************************************************************************************************************************
/*  HOW TO USE THISE SCRIPT:
 *  Attach this script to an empty object, or GameManager object. There are global settings that you can change safely within the inspector.  
 * Change or Update to whatever game settings, or values you want to test with.
 * 
 */

public class GameManager : MonoBehaviour
{
    [SerializeField] private float speed= 3.5f;
    public static float Speed => Instance.speed;

    [SerializeField] private float rangeaggroRadius = 28f;
    public static float RangeAggroRadius => Instance.rangeaggroRadius;

    [SerializeField] private float meleeaggroRadius =18f;
    public static float MeleeAggroRadius => Instance.meleeaggroRadius;

    [SerializeField] private float meleeattackRange = 1f;
    public static float MeleeAttackRange => Instance.meleeattackRange;

    [SerializeField] private float leaveAttackRange = 4f;
    public static float LeaveAttackRange => Instance.leaveAttackRange;

    [SerializeField] private GameObject playerObject;
    public static GameObject player_obj => Instance.playerObject;

    public static Vector3 player_position => Instance.playerObject.transform.position;
    public static Material player_material => Instance.playerObject.GetComponent<Renderer>().material;    

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
