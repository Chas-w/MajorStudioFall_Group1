using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor.AI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Collider cld;
    Rigidbody rb;
    NavMeshAgent navA;
    public bool canMove;
    public MusicBoxManager mbManager; 
    float speed;
    float attackRange;
    float maxSpeed;
    
    private void Awake()
    {
        cld = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        navA = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        speed = GameManager.instance.enemySpeed;
        attackRange = GameManager.instance.enemyAttackRange;
        maxSpeed = GameManager.instance.enemyMaxSpeed;
        navA.speed = speed;
        navA.stoppingDistance = attackRange;
    }

    // Update is called once per frame
    void Update()
    {
        //CheckAttackRange();
        MoveTowardPlayer();
    }

    void MoveTowardPlayer()
    {
        if (canMove && !mbManager.boxHealed) //if the music box is also not healed yet (the player hasn't finished the puzzle)
        {
            navA.isStopped = false;
            Vector3 destination = GameManager.instance.player.transform.position;
            navA.destination = destination;
            /*
            Vector3 dir = GameManager.instance.player.transform.position - transform.position;
            dir.Normalize();
            dir = new Vector3(dir.x, 0, dir.z);
            rb.AddForce(dir * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
            */
        } else
        {
            navA.isStopped = true;
        }
    }

    void CheckAttackRange()
    {
        /*
        Vector3 playerHorPos = new Vector3(GameManager.instance.player.transform.position.x, 0, GameManager.instance.player.transform.position.z);
        Vector3 enemyHorPos = new Vector3(transform.position.x, 0, transform.position.z);
        float distance = Vector3.Distance(playerHorPos, enemyHorPos);
        
        if (distance <= attackRange)
        {
            canMove = false;
        }
        */
    }
}
