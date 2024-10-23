using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Data")]
    public float mSpeed;
    public bool haltMovement;
    [SerializeField] Transform orientation;
    [SerializeField] CharacterController controller;

    float mX = 0;
    float mZ = 0;

    [Header("Physics")]
    public float gravity = -9.81f;
    [SerializeField] float groundDistance = .4f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;

    [Header("Audio Data")]
    [SerializeField] AudioSource stepSource;
    [SerializeField] AudioClip[] footsteps;
    [SerializeField] float stepTimerMax; 

    Vector3 velocity;

    bool isGrounded;
    float stepTimer;
    int stepChoice; 
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (!haltMovement)
        {
            mX = Input.GetAxis("Horizontal");
            mZ = Input.GetAxis("Vertical");

        }
        else
        {
            mX = 0;
            mZ = 0;
        }



        velocity.y += gravity * Time.deltaTime;
        Vector3 move = orientation.right * mX + orientation.forward * mZ; //direction we want to move
        controller.Move(move * mSpeed * Time.deltaTime);


        controller.Move(velocity * Time.deltaTime);
        playFootsteps();
        
    }

    private void playFootsteps()
    {
        if (isGrounded && (mX != 0 || mZ != 0))
        {
            stepTimer -= Time.deltaTime; 
            if(stepTimer <= 0)
            {
                stepSource.PlayOneShot(footsteps[(Random.Range(0, footsteps.Length))]);
                stepTimer = stepTimerMax; 
            }
        }
    }

}
