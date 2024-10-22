using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Collider cld;
    Rigidbody rb;
    public bool canMove;
    float speed;
    float attackRange;
    float maxSpeed;
    
    private void Awake()
    {
        cld = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        speed = GameManager.instance.enemySpeed;
        attackRange = GameManager.instance.enemyAttackRange;
        maxSpeed = GameManager.instance.enemyMaxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        CheckAttackRange();
        MoveTowardPlayer();
    }

    void MoveTowardPlayer()
    {
        if (canMove)
        {
            Vector3 dir = GameManager.instance.player.transform.position - transform.position;
            dir.Normalize();
            dir = new Vector3(dir.x, 0, dir.z);
            rb.AddForce(dir * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
    }

    void CheckAttackRange()
    {
        Vector3 playerHorPos = new Vector3(GameManager.instance.player.transform.position.x, 0, GameManager.instance.player.transform.position.z);
        Vector3 enemyHorPos = new Vector3(transform.position.x, 0, transform.position.z);
        float distance = Vector3.Distance(playerHorPos, enemyHorPos);
        
        if (distance <= attackRange)
        {
            canMove = false;
        }
    }
}
