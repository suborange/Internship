/* Ethan Bonavida
 * Enemy Melee Hardcoded attack patterns AI
 * enemy will a hardcode, repeatable pattern when in large range of player; attacking, blocking, moving towards or dodge 
 * 
 * 
 * -.DDMMYY
 * 0.0.01 = bug fix, updates,  trying small changes
 * 0.1.-  = major changes, rewriting, changed logic
 * 1.0.00 = GOLD?
 * Version: 
 * 0.0.00.101619; Created script
 * -.110119; Added simple variables, started to include in-range functionality for enemy. 
 * -.01.112319; fleshed out a simple 3 pattern rotation to change between a "phase" of attack,
 * -.02.112419; tried to work out a rotation system by starting with the one coroutine that word sort the attack and then run the functions needed everyframe and swtiching to others when needed to. I got too deep and i think i started confusing myself
 * -.1.02.112519; started over with a new idea for now; the idea was to have coroutines for each attack pattern alone, as i started working i realized it started to go back to my other idea i started, and got stuck again. thinking about re- orking a thrid time. jeez. 
 * -.1.03.112619; got a simple pattern to work, now i can easily work off this and tackle the bigger challenge if needed. 
 * -.04.112919; fixed up the dash, got it working with rotation based on player. 
 * 
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*PATTERN IDEAS:
 *  basic- move towards, hit, run away, repeat. 
 * basic2- move towards, hit, move/dodge, hit again, repeat
 * defense- slowly moves around player, dodge if too close, then runs away certain distance right after dodge, repeat
 * attack- move towards, hit, hit, dodge, hit, repeat
 *
 * 
 * TEST:
 * test dodge,
 * think about the Hit() function. test for it to "work"
 *
 * */

public class MeleeHardcodedPattern : MonoBehaviour
{
    public GameObject player_obj;

    float speed;
    bool hardcoded_att;
    float pattern;
    bool pattern_timer;
    bool pattern_run;
    bool dodge_;
    int p_;
    bool chase_;
    bool run_;
    bool hit_;
    bool LRDir;
    bool coroutine_reset;

    // Start is called before the first frame update
    void Start()
    {        
        hardcoded_att = false;
        //to debug a specific pattern if neccessary
        pattern = 3;

        speed = 2.5f;
        pattern_timer = false;
        chase_ = false;
        coroutine_reset = true;
        LRDir = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pattern_timer == false)
        {
            pattern_timer = true;
            //StartCoroutine("ChangePattern");
        }        
        if (hardcoded_att == true)
        {
            //if (player_obj.GetComponent<Material>().color == Color.red) { player_obj.GetComponent<Material>().color = Color.white; }
            transform.LookAt(2 * transform.position - player_obj.transform.position);
            

            //pattern 1 is BASIC run towards, then run away 
            if (pattern == 1)
            {
                if (chase_ == true)
                {
                    Chase();
                }
                else if (run_ == true)
                {
                    Run();
                }
                if (coroutine_reset == true)
                {
                    coroutine_reset = false;
                    StartCoroutine(Pattern1());
                }
            }

            //pattern 2 focuses more on an attack patters, consistant attacks. 
            else if (pattern == 2)
            {



            }

            //pattern 3 is focused on surviving, consistent dodging. 
            else if (pattern == 3)
            {

                if (run_ == true)
                {
                    Run();
                }
                else if (dodge_ == true)
                {
                    Dodge();
                }
                if (coroutine_reset == true)
                {
                    coroutine_reset = false;
                    StartCoroutine(Pattern3());
                }
                    
                    
            }
                
        }
          
 
    }
    void RESET()
    {
        hardcoded_att = false;
        pattern = 0;
        speed = 2.5f;
        pattern_timer = false;
        LRDir = false;

        StopAllCoroutines();

    }

    private IEnumerator ChangePattern()
    {
        RESET();
        if (pattern >=3)
        {
            pattern = 0;
        }
        pattern++;
        yield return new WaitForSeconds(30f);
        pattern_timer = false;
    }

    // chases towards the player, then runs away from the player. 
    private IEnumerator Pattern1()
    {
        chase_ = true;
        yield return new WaitForSeconds(6f);
        chase_ = false;
        run_ = true;
        yield return new WaitForSeconds(6f);
        run_ = false;
        coroutine_reset = true;
    }


    private IEnumerator Pattern2()
    {
        chase_ = true;
        yield return new WaitForSeconds(7f);
        chase_ = false;
        run_ = true;
        yield return new WaitForSeconds(7f);
        run_ = false;
        coroutine_reset = true;
    }


    private IEnumerator Pattern3()
    {
        if (LRDir == false) LRDir = true;
        else if (LRDir == true) LRDir = false;
        run_ = true;
        yield return new WaitForSeconds(2f);
        run_ = false;
        dodge_ = true;
        yield return new WaitForSeconds(.5f);
        dodge_ = false;
        coroutine_reset = true;
    }

    void Chase()
    {
        speed = 2.5f;
        transform.position = Vector3.MoveTowards(transform.position, player_obj.transform.position, speed * Time.deltaTime);

    }

    void Run()
    {
        speed = 2.5f;
        transform.position = Vector3.MoveTowards(transform.position, player_obj.transform.position, -1 * speed * Time.deltaTime);

    }

    void Hit()
    {

        //if figure out good flow, maybe just have this be only hit condition
        // Continue to chase, until close enough to hit?
        //testing wtih faster running speed, maybe a sligh dash?
        if (Vector3.Distance(transform.position, player_obj.transform.position) > 1)
        {
            if (speed < 7)
            {
                speed = 7;
            }
            transform.position = Vector3.MoveTowards(transform.position, player_obj.transform.position, speed * Time.deltaTime);
        }
        if (Vector3.Distance(transform.position, player_obj.transform.position) < 1.2f)
        {
            // hit player here 
            Debug.Log("HIT PLAYER");
             if (player_obj.GetComponent<Material>().color != Color.red) { player_obj.GetComponent<Material>().color = Color.red; }
            
        }

    }

    void Dodge()
    {
        //if (dodge_ == false )
        //{
        //    if (Vector3.Distance(transform.position, player_obj.transform.position) < 1.2f)
        //    {
        //        dodge_ = true;
        //    }
        //    if (Vector3.Distance(transform.position, player_obj.transform.position) > 0.8f)
        //    {
        //        transform.position = Vector3.MoveTowards(transform.position, player_obj.transform.position, speed * Time.deltaTime);
        //    }
        //}        
        //if (dodge_ == true)
        //{
        // DODGE here
        //StartCoroutine("D");
        speed = 15f;
        // dash right
        if (LRDir == false) transform.position += speed * transform.right * Time.deltaTime; //transform.position += speed * Vector3.right * Time.deltaTime;
        //then dash left
        if (LRDir == true) transform.position -= speed * transform.right * Time.deltaTime;//transform.position += speed * Vector3.left * Time.deltaTime;
        Debug.Log("DODGE");
        //}
    }
    // DODGE
    //private IEnumerator D()
    //{
    //    // dash enemy away from player
    //    yield return new WaitForSeconds(1);
    //    dodge_ = false;
        
    //}

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
            //hardcoded_att = false;

        }
    }


}


/*
 * if (pattern == 1)
            {
                p_ = 1;
                StartCoroutine(ChoosePattern(p_));
                if (chase_ == true)
                {
                    Chase();
                
                }
                else if (run_ == true)
                {
                        Run();
                }
                    

            }
 * 
 * 
 * 
 * 
 * private IEnumerator ChoosePattern(int p)
    {
        switch(p)
        {
            //Chase
            case 1:
                chase_ = true;
                break;

            //Run
            case 2:
                run_ = true;
                break;

            // Hit
            case 3:
                hit_ = true;
                break;

            // Dodge
            case 4:
                dodge2 = true;
                break;
            
            default:
                Debug.Log("Choosng a pattern went wrong");
                break;
        }
        yield return new WaitForSeconds(20f);        
        chase_ = false;
        run_ = false;
        hit_ = false;
        dodge2 = false;


    }
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 *
 * 
 *
 * */
