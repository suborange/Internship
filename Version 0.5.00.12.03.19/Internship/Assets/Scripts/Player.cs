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
 * 0.0.00.101619; Created script
 * 
 * */

using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera main_camera;
    float speed;
    Vector3 movement;
    Vector3 player_xz;    
    
    // Start is called before the first frame update
    void Start()
    {
        speed = 6.5f;     
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Starting with basic foward and side movements
        movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        transform.position += ((speed * movement) *Time.deltaTime);
        main_camera.transform.position = new Vector3(transform.position.x, transform.position.y+4, transform.position.z-4);
    }
}
