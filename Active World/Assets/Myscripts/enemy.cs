using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    public enum enemystate
    {
        PATROL,
        ATTACK,
        HEAL,
        DEAD,
    }
    public Material reddamage;
    public Material normal;
    public GameObject[] waypoints;
    public GameObject player;
    private NavMeshAgent myAgent;
    private int currentWaypoint;
    public int health;
    private bool insight;
    private bool gothit;
    int damagedealt = 0;
    private float colortimer = 0.0f;
    private float healtimer = 0.0f;
    private float healwaittime = 3f;
    public float EnemyDistanceRun = 4.0f;
    private float ColorwaitTime = 1.5f;
    public enemystate Enemystate = enemystate.PATROL;
    public int playerhealth;
    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        myAgent.destination = waypoints[currentWaypoint].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Enemy Health:" + health);
        switch (Enemystate)
        {
            case enemystate.PATROL:
                Patrol();

                if (insight || gothit == true)
                {
                    Enemystate=enemystate.ATTACK;
                        }
                if (health <= 0)
                {
                    Enemystate = enemystate.DEAD;
                }
               
                    break;
            case enemystate.ATTACK:
              
                    Attack();
                if (insight == false)
                    {
               
                    Enemystate = enemystate.PATROL;
              
                    }
                if(health < 20)
                {
                    Enemystate = enemystate.HEAL;
                }
                if (health <= 0)
                {
                    Enemystate = enemystate.DEAD;
                }


                break;
            case enemystate.HEAL:
                Heal();
              
                if (health <= 0)
                {
                    Enemystate = enemystate.DEAD;
                }
                if(health == 80)
                {
                    Enemystate = enemystate.PATROL;
                }
                break;

            case enemystate.DEAD:
                Dead();
                break;


        }
       
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "shot")
        {
            health = health - 10;
            gothit = true; 

        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            insight = true;

        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.tag == "Player")
            insight = false;
    }


    private void Patrol()
    {
    
        Debug.Log("Patrolling");//enemy patrolls around certain waypoints
        if (Vector3.Distance(myAgent.destination, transform.position) <= 1)
        {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }
            myAgent.destination = waypoints[currentWaypoint].transform.position;
        }
    }

    private void Attack()//enemy chases player if it is hit or  they fall into line of sight

    {
        if (Vector3.Distance(transform.position, player.transform.position) <= 1)
        {
            Debug.Log("chasing");
            colortimer += Time.deltaTime;
            if (colortimer > ColorwaitTime)
            {
                myAgent.destination = this.transform.position;

                colortimer = colortimer - ColorwaitTime;
                damagedealt = damagedealt +1;
                playerhealth = playerhealth - 10;
                Debug.Log("playerhealth" + playerhealth);
                player.GetComponent<MeshRenderer>().material = reddamage;
                gothit = false;
            }
            else
            {
                player.GetComponent<MeshRenderer>().material = normal;

            }

        }
        myAgent.destination = player.transform.position;
    }

    private void Heal()//enemy runs away from player and heals if below certain health
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
   
        //enemy runs away
        if (distance < EnemyDistanceRun) //code is written by following Jayanam tutorial on fleeing
        {
            Vector3 dirToPlayer = transform.position - player.transform.position;
            Vector3 newPos = transform.position + dirToPlayer;
            myAgent.SetDestination(newPos);
        }

        healtimer += Time.deltaTime;//slowly healing while running away from player 
        if (healtimer > healwaittime)
        {
            healtimer = healtimer - healwaittime;
            if (health < 80)
            {
                health = health + 10;
                Debug.Log("Enemy Health:" + health);
            }

        }
    }

    private void Dead()
    {
        
            Destroy(this.gameObject);
        
    }
   

}
