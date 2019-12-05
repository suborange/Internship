/* Ethan Bonavida
 * Enemy Ranged Hide and Shoot AI
 * Enemy just move towards the player, comeplete basic chase behavior
 * 
 * 
 * 
 * -.DDMMYY
 * 0.0.01 = bug fix, updates, trying small changes
 * 0.1.-  = major changes, rewriting, changed logic  
 * 1.0.00 = GOLD?
 * Version: 
 * 0.0.00.110119; Created script
 * 0.0.00.110419; Started writing variables and basic in-range checks. 
 * 
 * 
 * 
 * */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedHidenshoot : MonoBehaviour
{
    NavMeshAgent nav;
    public int frameInterval = 10;
    public int facePlayerFactor = 50;

    Vector3 randomPosition;
    Vector3 coverPoint;
    public float rangeRandPoint = 30f;
    public bool isHiding = false;


    // Go to cover
    public LayerMask coverLayer;
    Vector3 coverObj;
    public LayerMask visibleLayer;

    private float _maxCovDist = 30;
    public bool coverIsClose;
    public bool coverNotReached = true;

    public float distToCoverPos = 1f;
    public float distToCoverObj = 20f;

    public float rangeDist = 15f;
    private bool playerInRange = false;

    private int testCoverPos = 10;



    public GameObject player_obj;
    public BoxCollider this_col;
    Vector3 movetowards;
    Vector3 col_attsize;
    Vector3 col_outofsightsize;
    float speed;
    float step;
    bool hidenshoot_att;




    bool RandomPoint(Vector3 center, float rangeRandPoint, out Vector3 resultCover)
    {
        for (byte i=0; i<testCoverPos; i++)
        {
            randomPosition = center + Random.insideUnitSphere * rangeRandPoint;
            Vector3 direction = player_obj.transform.position - randomPosition;
            RaycastHit hitTestCov;
            if (Physics.Raycast(randomPosition, direction.normalized, out hitTestCov, rangeRandPoint, visibleLayer))
            {
                if ( hitTestCov.collider.gameObject.layer == 18)
                {
                    resultCover = randomPosition;
                    return true;
                }
            }
        }
        resultCover = Vector3.zero;
        return false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        hidenshoot_att = false;
        speed = 3f;

        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
            if (nav.isActiveAndEnabled)
            {
                if (Time.frameCount % frameInterval == 0)
                {
                    float distance = ((player_obj.transform.position - transform.position).sqrMagnitude);

                    if (distance < rangeDist * rangeDist)
                    {
                        playerInRange = true;
                    }
                    else playerInRange = false;
                }

                if (playerInRange == true)
                {
                    CheckCoverDist();

                    if (coverIsClose == true)
                    {
                        if (coverNotReached == true)
                        {
                            nav.SetDestination(coverObj);
                        }
                        if( coverNotReached == false)
                        {
                            TakeCover();
                            FacePlayer();
                        }
                    }
                    if (coverIsClose == false)
                    {
                        //do something else. change state here?
                    }
                }
            }
            Shoot();        
    }

    void FacePlayer()
    {
        Vector3 direction = (player_obj.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.z, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * facePlayerFactor);
    }

    void CheckCoverDist()
    {
        //check if cover is indexer vicinity
        Collider[] colliders = Physics.OverlapSphere(transform.position, _maxCovDist, coverLayer);
        Collider nearestCollider = null;
        float minSqrDistance = Mathf.Infinity;

        Vector3 AI_position = transform.position;

        for(byte i=0; i <colliders.Length; i++)
        {
            float sqrDistanceToCenter = (AI_position - colliders[i].transform.position).sqrMagnitude;
            if (sqrDistanceToCenter < minSqrDistance)
            {
                minSqrDistance = sqrDistanceToCenter;
                nearestCollider = colliders[i];

                //check AI is already close enough to take cover
                float coverDistance = (nearestCollider.transform.position - AI_position).sqrMagnitude;
                if ( coverDistance <= _maxCovDist*_maxCovDist)
                 {
                    coverIsClose = true;
                    coverObj = nearestCollider.transform.position;
                    if (coverDistance <= distToCoverObj * distToCoverObj)
                    {
                        coverNotReached = false;
                    }
                    else if (coverDistance > distToCoverObj*distToCoverObj)
                    {
                        coverNotReached = true;
                    }                    
                }
                if (coverDistance >= _maxCovDist*_maxCovDist)
                {
                    coverIsClose = false;
                }
            }
        }
        if (colliders.Length < 1)
        {
            coverIsClose = false;
        }
    }

    void TakeCover()
    {
        if (RandomPoint(transform.position, rangeRandPoint, out coverPoint))
        {
            if (nav.isActiveAndEnabled)
            {
                nav.SetDestination(coverPoint);
                if ((coverPoint-transform.position).sqrMagnitude <= distToCoverPos*distToCoverPos)
                {
                    isHiding = true;
                }
            }
        }
    }


    public void Shoot()
    {
        //insert shoot code here
    }
    //  PLAYER ENTERS RADIUS, ATTACK PLAYER
   
}
