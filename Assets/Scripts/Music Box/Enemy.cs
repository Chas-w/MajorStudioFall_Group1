using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor.AI;
using UnityEngine.AI;
using System;
using System.Reflection;

public class Enemy : MonoBehaviour
{
    Collider cld;
    //public GameObject spawnArea;
    [HideInInspector]public Collider spawnCld;
    [HideInInspector]public GameObject toy;
    [HideInInspector] public int pitch;
    Rigidbody rb;
    NavMeshAgent navA;
    public bool canMove;
    public MusicBoxManager mbManager; 
    float speed;
    float attackRange;
    AudioSource audioSource;
    InstrumentPlayer instrument;

    float timer;
    
    private void Awake()
    {
        cld = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        navA = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        //spawnCld = spawnArea.GetComponent<Collider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        speed = GameManager.instance.enemySpeed;
        attackRange = GameManager.instance.enemyAttackRange;
        navA.speed = speed;
        navA.stoppingDistance = attackRange;
        instrument = GameObject.Find("Instrument").GetComponent<InstrumentPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        //CheckAttackRange();
        //MoveTowardPlayer();
        MoveTowardToy();
        PlayNode();
        CheckPlayerNode();
    }

    void CheckPlayerNode()
    {
        Vector3 playerHorPos = new Vector3(GameManager.instance.player.transform.position.x, 0, GameManager.instance.player.transform.position.z);
        Vector3 enemyHorPos = new Vector3(transform.position.x, 0, transform.position.z);
        float distance = Vector3.Distance(playerHorPos, enemyHorPos);

        if (distance <= GameManager.instance.killableRange)
        {
            if (instrument.pitch > 0)
            {
                if (instrument.pitch == pitch)
                {
                    Spawn();
                }
            }
        }
    }

    void PlayNode()
    {
        if (timer <= 0)
        {
            Debug.Log("111");
            audioSource.Play();
            timer = GameManager.instance.playbackInterval;
        } else
        {
            timer -= Time.deltaTime;
        }
    }

    public void Spawn()
    {
        Bounds bounds = spawnCld.bounds;
        Vector3 pos = GetRandomTopEdgePoint(bounds);
        pos += new Vector3(0, transform.localScale.y / 2, 0);
        transform.position = pos;
        audioSource.clip = instrument.instrumentNotes[pitch - 1];
        timer = GameManager.instance.playbackInterval;
    }


    Vector3 GetRandomTopEdgePoint(Bounds bounds)
    {
        // Get the max Y to define the top face
        float y = bounds.max.y;
        Vector3 min = bounds.min;
        Vector3 max = bounds.max;

        // Randomly select one of the 4 edges on the top face
        int edge = UnityEngine.Random.Range(0, 4);

        switch (edge)
        {
            case 0: // Top edge along X, at min Z
                return new Vector3(UnityEngine.Random.Range(min.x, max.x), y, min.z);
            case 1: // Top edge along X, at max Z
                return new Vector3(UnityEngine.Random.Range(min.x, max.x), y, max.z);
            case 2: // Left edge along Z, at min X
                return new Vector3(min.x, y, UnityEngine.Random.Range(min.z, max.z));
            case 3: // Right edge along Z, at max X
                return new Vector3(max.x, y, UnityEngine.Random.Range(min.z, max.z));
            default:
                return bounds.center; // Fallback to center (shouldn't happen)
        }
    }

    void MoveTowardPlayer()
    {
        if (canMove && !mbManager.boxHealed && !GameManager.instance.freeze) //if the music box is also not healed yet (the player hasn't finished the puzzle)
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

    void MoveTowardToy()
    {
        if (!mbManager.boxHealed && !GameManager.instance.freeze) //if the music box is also not healed yet (the player hasn't finished the puzzle)
        {
            navA.isStopped = false;
            Vector3 destination = toy.transform.position;
            navA.destination = destination;
        }
        else
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

[Serializable]
public class EnemyInfo
{
    public Collider spawnCld;
    public GameObject toy;
    public int pitch;
}
