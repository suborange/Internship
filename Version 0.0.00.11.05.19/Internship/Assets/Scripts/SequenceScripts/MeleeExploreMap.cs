/* Ethan Bonavida
 * Enemy Melee Explores around the map AI
 * enemy will possibly patrol around randomly unitl a certain smaller range of player
 * or will follow a pattern until a certain smaller range of the player
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
 * 
 * 
 * 
 * */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeExploreMap : MonoBehaviour
{
    public GameObject player_obj;
    List<Transform> explore_points = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
