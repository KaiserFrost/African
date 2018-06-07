using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAi : MonoBehaviour
{

    // public List<GameObject> arbusto;
    // public float fieldofview = 110f;
    AutoIntensity nighttime;
    public Animator animator;
    private Transform[] points;
    public List<Transform> waypoint;
    private int destPoint = 0;
    private NavMeshAgent agent;
    CharacterController characterController;

    public bool friendly = true;
    public float rotationSpeed = 20.0f;
    public float wanderRadius = 10f;
    public float wanderTimer = 2f;

    private float timer;

    private GameObject player;
    private GameObject bush;
    private GameObject[] tree;
    private GameObject enemy;
    private GameObject target;
    private bool enemyCanAttack = false;
    private bool jaVisteoPlayer = false;
    private bool attack1 = false;
    // Use this for initialization

    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
    }
    void Start()
    {

        characterController = gameObject.GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("enemy");
        bush = GameObject.FindGameObjectWithTag("bush");
        tree = GameObject.FindGameObjectsWithTag("tree");
        points = new Transform[4];
        points[0] = player.transform;
        animator = GetComponent<Animator>();
        waypoint = new List<Transform>();
        
    }


   

    // Update is called once per frame
    void Update()
    {
        

        if (!friendly)
        {
            animator.SetBool("Hit2", attack1);

            
            Vector3 moveToward =  player.transform.position - transform.position;
            Vector3 moveAway = transform.position - player.transform.position;

            //if enemy is close
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance < 30)
            {
                
                    animator.SetBool("isRunning", true);
                  
                    attack1 = false;

                    if (distance < 2)
                    {
                       
                        animator.SetBool("isWalking", true);
                    animator.SetBool("isRunning", false);
                    attack1 = false;

                    }
                if (distance < 1)
                {
                   
                    animator.SetBool("isWalking", false);
                    attack1 = true;
                }


                moveToward.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveToward), rotationSpeed * Time.deltaTime);
                
                transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            }
            else
            {
                Patrol();
               
            }
        }
        else //if friendly
        {
            
            Vector3 moveToward = player.transform.position - transform.position;
            Vector3 moveAway = transform.position - enemy.transform.position;
            float distanceplayer = Vector3.Distance(transform.position, player.transform.position);
            float distanceenemy = Vector3.Distance(transform.position, enemy.transform.position);

           
            float distancebush = Vector3.Distance(transform.position, bush.transform.position);

            if (distanceenemy < 20)
            {
                animator.SetBool("isRunning", true);

                if (jaVisteoPlayer)
                {

                    moveAway.y = 0;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveAway), rotationSpeed * Time.deltaTime);

                    transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                }
                else if (!jaVisteoPlayer)
                {
                    moveToward.y = 0;
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveToward), rotationSpeed * Time.deltaTime);

                    transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
                }
                else if (distanceplayer < 20)
                {
                    jaVisteoPlayer = true;
                }
            }
            else if (distanceenemy > 40)
            {
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", true);

                if (waypoint.Count <2)
                { 
                    Patrol();
                    Debug.Log("chouriço");
                }
                else if(!agent.pathPending && agent.remainingDistance < 1f)
                    GotoNextPoint();

                if (distancebush < 10)
                {
                    Debug.Log("distancia é inferior a 10");
                  // waypoint.Add(bush.transform);
                }

            }




            if (GameObject.Find("sun").GetComponent<AutoIntensity>().nighttime == true)
            {
                agent.SetDestination(player.transform.position);
                if(distanceplayer <2)
                    animator.SetBool("isWalking", false);

            }
            /*
             RaycastHit hit;
             if (Physics.Raycast(transform.position + transform.up, transform.forward, out hit, 50)) 
             {
                 Debug.Log("ray hits " + hit.collider.gameObject.name);
                 Debug.DrawRay(transform.position + transform.up, transform.forward, Color.white, 1f);
                 if (hit.collider.gameObject.tag == "bush")
                 {


                     { 
                              waypoint.Add(hit.collider.gameObject.transform);
                     }

                 }

             }*/







        }

    }
    void GotoNextPoint()
    {
     
       
            agent.destination = waypoint[destPoint].position;
            destPoint = (destPoint + 1);
              if (destPoint == waypoint.Count)
                {
                      destPoint = 0;
                }
            
    }


    void Patrol()
    {
        timer += Time.deltaTime;
        animator.SetBool("isWalking", true);
        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
    }
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}

   