using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public GameObject player;


    float playerX;
    float playerZ;
    Transform playerXYZ;

    float speed;
    float step;



    Vector3 movetowards; 


    // Start is called before the first frame update
    void Start()
    {
        //Enemy move speed
        speed = 3.1f;
        //Get player transform for position
        playerX = player.GetComponent<Transform>().transform.position.x;
        playerZ = player.GetComponent<Transform>().transform.position.z;

        playerXYZ = player.GetComponent<Transform>();
        //playerVector = new Vector3(playerXYZ.position.x, playerXYZ.position.y, playerXYZ.position.z);

       
    }

    // Update is called once per frame
    void Update()
    {
        //Find players position, and targets and chases the player. 
        //movetowards = new vector3(playerx, 0f, playerz);
        //transform.position += ((speed * movetowards) * time.deltatime);

        step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, playerXYZ.position, step);



    }
}
