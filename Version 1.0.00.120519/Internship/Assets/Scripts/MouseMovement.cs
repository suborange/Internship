/* Ethan Bonavida
 * 3rd person camera view and movement
 * 
 * 
 * -.DDMMYY
 * 0.0.01 = bug fix, updates, trying small changes
 * 0.1.-  = major changes, rewriting, changed logic  
 * 1.0.00 = GOLD?
 * Version: 
 * 0.0.00.120419; Created script, found great resource to have a 3rd person like camera movement for the player. GOLD!
 * 
 * */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*******************************************************************************************************************************************************************************************************************************
/* HOW TO USE THIS SCRIPT 
 * Attach this script to the main camera object, that should be a child of the player object. this script goes together with the player script. added in-depth comments
 * 
 * 
 */

public class MouseMovement : MonoBehaviour
{
    public float _RotationSpeed = 1f;
    public Transform Target, Player;

    private float x;
    private float y;

    // Start is called before the first frame update
    void Start()
    {
        // get rid of mouse cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
       _3rdpov();
    }

    void _3rdpov()
    {
        // get x and y values from mouse movement
        x += Input.GetAxis("Mouse X") * _RotationSpeed;
        y -=  Input.GetAxis("Mouse Y") * _RotationSpeed;
        y = Mathf.Clamp(y, -35, 60);

        transform.LookAt(Target);
        Target.rotation = Quaternion.Euler(y, x, 0);
        Player.rotation = Quaternion.Euler(0, x, 0);
    }
}
