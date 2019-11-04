/* Ethan Bonavida
 * Enemy Melee Hardcoded attack patterns AI
 * enemy will a hardcode, repeatable pattern when in large range of player; attacking, blocking, moving towards or dodge
 * 
 * 
 * 
 * 
 * -.DDMMYY
 * 0.0.01 = bug fix, updates,  trying small changes
 * 0.0.1 = major changes, rewriting, changed logic
 * 0.1.-   
 * 1.0.00 = GOLD?
 * Version: 
 * 0.0.00.101619; Created script
 * -.110119; Added simple variables, started to include in-range functionality for enemy.  s
 * 
 * 
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHardcodedPattern : MonoBehaviour
{
    public GameObject player_obj;
    List<Transform> endpoints = new List<Transform>();
    Vector3 endpoint1;
    Vector3 endpoint2;
    Vector3 endpoint3;

    bool hardcoded_att;


    // Start is called before the first frame update
    void Start()
    {
        //endpoints.Add();
        hardcoded_att = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hardcoded_att = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hardcoded_att = false;

        }
    }


}

}
