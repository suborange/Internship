using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public GameObject enemy_object;
    public bool chase_att = false;
    public bool charge_att = false;
    public bool LOS_att = false;
    public bool Hardcoded_att = false;
    public bool explore_att = false;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (enemy_object.name=="meleeChase")
            {
                Debug.Log("Chase");
                chase_att = true;             

            }
            else if (enemy_object.name == "meleeCharge")
            {
                Debug.Log("Charged");
                charge_att = true;

            }
            else if (enemy_object.name == "meleeLOS")
            {

                Debug.Log("LOS-AI");
                LOS_att = true;
            }
            else if (enemy_object.name == "meleeHardcoded")
            {
                Debug.Log("Hardcoded");
                Hardcoded_att = true;
            }
            else if (enemy_object.name == "meleeExplore")
            {
                Debug.Log("Explore");
                explore_att = true;
            }


        }
    }
}
