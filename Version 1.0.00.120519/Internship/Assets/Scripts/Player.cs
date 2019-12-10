/* Ethan Bonavida
 * Player Movement Script
 * test player script. to move around and test enemy AI scripts. 
 * 
 * 
 * 
 * -.DDMMYY
 * 0.0.01 = bug fix,  trying small changes
 * 0.0.1 = major changes, rewriting, changed logic
 * 0.1.-   
 * 1.0.000 = GOLD?
 * Version: 
 * 0.0.00.101619; Created script; made test movement for object to move with key presses. added in-depth comments
 * 1.0.00.120419; GOLD!
 * 
 * */

using UnityEngine;

//*******************************************************************************************************************************************************************************************************************************
/*  HOW TO USE THIS SCRIPT:
 *  This is the player script. This is the script to put on your player object to move around the scene.
 *  Change or Update to however you want the player to move
 *  
 *  
 */

public class Player : MonoBehaviour
{
    // THIS CAMERA CAN BE USED FOR ORTHOGRAPHIC "FAKE 2D" TESTS.
    //public Camera main_camera;
    float speed;
    Vector3 movement;
    Vector3 player_xz;    
    
    // Start is called before the first frame update
    void Start()
    {
        speed = 6.5f;     
    }

    // Update is called once per frame
    void Update()
    {
        //Starting with basic foward and side movements
        movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        transform.Translate(((speed * movement) *Time.deltaTime), Space.Self);
        //main_camera.transform.position = new Vector3(transform.position.x, transform.position.y+4, transform.position.z-4);
    }
}
